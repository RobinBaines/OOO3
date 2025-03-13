//
// @Copyright 2025 Robin Baines
// Licensed under the MIT license. See MITLicense.txt file in the project root for details.
//

using System.Globalization;

namespace OOO3
{
    internal class SensualQuality : BaseClass
    {
        public string Value ="";
        public bool IsSO;
        public SensualObject? SOParent;  //is the owner of this SQ.
        public SensualObject? SOEvent; 
        public virtual void PrintQuality(string prefix, string _SOName)
        {
            if (SOParent != null && SOEvent != null)
            {
                string strEvent;
                if (SOParent.Name != SOEvent.Name)
                {
                    //SO MeetNida has qualities.
                    //2025 - 02 - 02 14:00:00.005 => Nida Tail = Short
                    if (SOEvent.Name == _SOName)
                        strEvent = " => " + SOParent.Name + " ";  //SOParent.Name + " ";  //+ " defined by " + SOEvent.Name + " ";

                    //SO Nida has qualities.
                    //2025 - 02 - 02 14:00:00.005 MeetNida => Tail = Short
                    else strEvent = " " + SOEvent.Name + " => ";  //SOParent.Name + " ";  //+ " defined by " + SOEvent.Name + " ";
                }
                else
                {
                    strEvent = " Quality ";
                }

                Console.WriteLine("\t" + prefix + this.created.ToString("yyyy-MM-dd HH:mm:ss.fff",
                        CultureInfo.InvariantCulture) + strEvent + this.Name + " = " + this.Value);

                if (references.Count > 0)
                {
                    //Console.WriteLine("\t" + prefix + "SO Reference of a quality: ");
                    foreach (SensualObject SO in references)
                    {
                        SO.PrintSO("\t" + "\t" + prefix, " SO Reference ");
                    }
                }
            }
        }
        private void SensualQualityInit(SensualObject _SOEvent, SensualObject _SOparent, bool _IsSO, string _name, string _value, string _description, DateTime _dt)
        {
            Name = _name;
            IsSO = _IsSO;
            Value = _value;
            Description = _description;
            this.created = _dt;
            SOParent = _SOparent;
            SOEvent = _SOEvent;
        }

        public SensualQuality(SensualObject _SOEvent, SensualObject _SOparent, string _name, string _value, string _description, SensualObject _SOOfValue, DateTime _dt)
        {
            SensualQualityInit(_SOEvent, _SOparent, false, _name, _value, _description, _dt);
            if (_SOOfValue != null)
            {
                references.Add(_SOOfValue);
            }
        }

        public SensualQuality(SensualObject _SOEvent, SensualObject _SOparent, bool _IsSO, string _name, string _value, string _description, DateTime _dt)
        {
            SensualQualityInit(_SOEvent, _SOparent, _IsSO, _name, _value,  _description, _dt);
        }

        public SensualQuality(SensualQuality SQ)
        {
            if (SQ.SOEvent != null && SQ.SOParent != null)
                SensualQualityInit(SQ.SOEvent, SQ.SOParent, SQ.IsSO, SQ.Name, SQ.Value, SQ.Description, SQ.created);
        }

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Sensual Object
    /// qualitiesofchildren stores all the qualitites of preceding SOs so that overridden SQs are not printed.
    /// </summary>
    class SensualObject : BaseClass
    {
        public SensualObject? ptrDerivedFrom = null;
       
        internal List<SensualQuality> qualities = new List<SensualQuality>();
        public virtual void PrintSO(string prefix, string extraText)
        {
            string str = prefix + this.created.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + extraText + this.Name;
             if (this.ptrDerivedFrom != null)
                str = str + " : " + this.ptrDerivedFrom.Name;
             Console.WriteLine(str);
        }

        /// <summary>
        /// Display the qualities of an SO. 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="qualitiesofchildren"></param>
        public virtual void PrintQualities(string prefix)
        {
            if (qualities.Count > 0)
                {
                    //print the  qualities if not in this.qualities.
                    Console.WriteLine(prefix + "SO " + this.Name + " has qualities.");
                    foreach (SensualQuality SQ in qualities)
                    {
                        SQ.PrintQuality(prefix, this.Name);
                    }
                }
        }

        /// <summary>
        /// Display the references of an SO. 
        /// </summary>
        /// <param name="prefix"></param>
        public virtual void PrintReferences(string prefix)
        {
            if (references.Count > 0)
            {
                Console.WriteLine(prefix + "SO " + this.Name + " has references.");
                foreach (SensualObject SO in references)
                {
                    SO.PrintSO("\t" + prefix, " SO Reference ");
                }
            }
         }

        /// <summary>
        /// Add a reference to an SO if the reference is a new one.
        /// </summary>
        /// <param name="SO"></param>
        public virtual void AddReference(SensualObject SO)
        {
            if(SO != null)
            {
                if (!references.Exists(_SO => Equals(_SO, SO)))
                {
                    references.Add(SO);
                }
            }
        }

        /// <summary>
        /// Add a quality SQ to this SO and if recurse = true add a copy of the SQ to the Event.
        /// </summary>
        /// <param name="SQ"></param>
        /// <param name="recurse"></param>
        void AddQuality(SensualQuality SQ, bool recurse)
        {
            if (SQ.SOEvent != null)
            { 
                SensualObject? SOFrom = BuildSOs.GetSO(SQ.Name);
                if (SOFrom != null)
                {
                    AddReference(SOFrom);
                }

                //Add a quality to this SO.
                qualities.Add(SQ);

                //make a copy of the SQ in the Event if this not the Event.
                //SO Nida has qualities.
                //2025 - 02 - 02 14:00:00.005 => MeetNida Tail = Short
                // but also
                //SO MeetNida has qualities.
                //2025 - 02 - 02 14:00:00.005 => Nida Tail = Short
                if (this != SQ.SOEvent && recurse == true)
                {
                    SensualQuality? SQNew = new(SQ);
                    SQ.SOEvent.AddQuality(SQNew, false);
                }
            }
        }
        public virtual void AddQuality(SensualObject _SOEvent, string _name, string _value, string _description, DateTime _dt, bool recurse)
        {
            SensualQuality? SQ = new(_SOEvent, this, false, _name, _value, _description, _dt);
            AddQuality(SQ, recurse);
        }
        public virtual void AddQuality(SensualObject _SOEvent, string _name, string _value, string _description, DateTime _dt)
        {
            SensualQuality? SQ = new(_SOEvent, this, false, _name, _value, _description, _dt);
            AddQuality(SQ, true);
        }

        //if the Value is an SO.
        public virtual void AddQuality(SensualObject _SOEvent, string _name, string _value, string _description, SensualObject _SOOfValue, DateTime _dt)
        {
            SensualQuality? SQ = new(_SOEvent, this, _name, _value, _description, _SOOfValue, _dt);
            AddQuality(SQ, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_SOEvent"></param>
        /// <param name="_SOFrom"></param>
        /// <param name="_name"></param>
        /// <param name="_value"></param>
        /// <param name="_description"></param>
        /// <param name="_dt"></param>
        public virtual void AddQuality(SensualObject _SOEvent, SensualObject _SOFrom, string _name, string _value, string _description, DateTime _dt)
        {
            AddReference(_SOFrom);
            SensualQuality? SQ = new(_SOEvent,this, true, _name, _value, _description, _dt);
            AddQuality(SQ, true);
        }

        /// <summary>
        /// Move default properties from the Child SO to the Parent.
        /// </summary>
        /// <param name="DerivedFrom"></param>
        /// <param name="_dt"></param>
        public virtual void MoveQualities(SensualObject DerivedFrom, DateTime _dt)
        {
        foreach (SensualQuality SQ in qualities)
            {
                int index = DerivedFrom.qualities.IndexOf(SQ);
                if (index < 0 && SQ.SOEvent != null)
                {
                    DerivedFrom.AddQuality(SQ.SOEvent, SQ.Name, "True", SQ.Description, _dt, false);
                }
            }
            qualities.RemoveAll(SQ => SQ.Value == "True");
        }

        public SensualObject(string _name, DateTime _dt)
        {
            Name = _name;
            this.created = _dt;
        }
    }

}
