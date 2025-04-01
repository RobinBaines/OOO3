//
// @Copyright 2025 Robin Baines
// Licensed under the MIT license. See MITLicense.txt file in the project root for details.
//

using System.Globalization;
using System.Security.Cryptography;

namespace OOO3
{
    internal class SensualQuality : BaseClass
    {
        public string Value ="";
        public bool IsSO;
        public SensualObject? SOEvent;

        public List<SensualObject> SOReferences = new List<SensualObject>();

        //unicity of an SQ is SOParent and Name.
        public override int GetHashCode()
        {
            string s = "";
            if (SOParent != null)
                s = SOParent.Name + ";";
            s += Name;
            return s.GetHashCode();
        }
        public new bool Equals(BaseClass? other)
        {
            if (other == null) return false;
            string s = "";
            if (SOParent != null)
                s = SOParent.Name + ";";
            s += Name;

            string sother = "";
            if (other.SOParent != null)
                sother = other.SOParent.Name + ";";
            sother += other.Name;
            return (s.Equals(sother));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="_SOName"></param>
        /// <param name="blnDisplayCreated"></param>
        public virtual void PrintQuality(string prefix, string _SOName, bool blnDisplayCreated)
        {
            if (SOParent != null && SOEvent != null)
            {
                string strEvent;
                if (SOParent.Name != SOEvent.Name)
                {
                    //SO MeetNida has qualities.
                    //2025 - 02 - 02 14:00:00.005 => Nida Tail = Short
                    if (SOEvent.Name == _SOName)
                        strEvent = " > " + SOParent.Name + " ";  //SOParent.Name + " ";  //+ " defined by " + SOEvent.Name + " ";

                    //SO Nida has qualities.
                    //2025 - 02 - 02 14:00:00.005 MeetNida => Tail = Short
                    else strEvent = " " + SOEvent.Name + " => ";  //SOParent.Name + " ";  //+ " defined by " + SOEvent.Name + " ";
                }
                else
                {
                    strEvent = " Quality ";
                }

                string str= "\t" + prefix;
                if (blnDisplayCreated)
                {
                    str += this.created.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                }
                else
                    str += "                       ";

                str += strEvent + this.Name + " = " + this.Value;
                Console.WriteLine(str);

                if (SOReferences.Count > 0)
                {
                    SOReferences.Sort((x, y) => DateTime.Compare(x.created, y.created));
                    foreach (SensualObject SO in SOReferences)
                    {
                        SO.PrintSO("\t" + "\t" + prefix, " SO Reference ", true);
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
                SOReferences.Add(_SOOfValue);
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
        public List<SensualObject> IncludesReference = new List<SensualObject>();
        public List<SensualObject> IsPartOfReference = new List<SensualObject>();
        internal List<SensualQuality> qualities = new List<SensualQuality>();


        public virtual void PrintSO(string prefix, string extraText, bool blnDisplayCreated)
        {
            string str = prefix;
            if (blnDisplayCreated)
            {
                str += this.created.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
            }
            else
                str += "                       ";
            str += extraText + this.Name;
            if (this.ptrDerivedFrom != null)
                str = str + " : " + this.ptrDerivedFrom.Name;
             Console.WriteLine(str);
        }

        public virtual void PrintParent(string prefix, string extraText)
        {
            if (SOParent != null)
            {
                this.SOParent.PrintSO("\t" + prefix, "Parent SO = ", false);
            //Console.WriteLine();
            }
        }


        /// <summary>
        /// Display the qualities of an SO. 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="qualitiesofchildren"></param>
        public virtual void PrintQualities(string prefix)
        {
            DateTime created = DateTime.MinValue;
            if (qualities.Count > 0)
            {
                qualities.Sort((x, y) => DateTime.Compare(x.created, y.created));

                //print the  qualities if not in this.qualities.
                Console.WriteLine(prefix + "SO " + this.Name + " has qualities.");
                foreach (SensualQuality SQ in qualities)
                {
                    if (SQ.created != created)
                    {
                        created = SQ.created;
                        SQ.PrintQuality(prefix, this.Name, true);
                    }
                    else
                    {
                        SQ.PrintQuality(prefix, this.Name, false);
                    }
                }
            }
        }

        /// <summary>
        /// Display the references of an SO. 
        /// </summary>
        /// <param name="prefix"></param>
        public virtual void PrintReferences(string prefix)
        {
            DateTime created = DateTime.MinValue;
            if (IncludesReference.Count > 0)
            {
                IncludesReference.Sort((x, y) => DateTime.Compare(x.created, y.created));

                Console.WriteLine(prefix + "SO " + this.Name + " has references.");
                foreach (SensualObject SOReference in IncludesReference)
                {
                    string str = " Includes          ";
                    if (SOReference.created != created)
                    {
                        created = SOReference.created;
                        SOReference.PrintSO("\t" + prefix, str, true);
                    }
                    else
                    {
                        SOReference.PrintSO("\t" + prefix, str, false);
                    }
                }
            }
         }
        public virtual void PrintIsPartOf(string prefix)
        {
            DateTime created = DateTime.MinValue;
            if (IsPartOfReference.Count > 0)
            {
                IsPartOfReference.Sort((x, y) => DateTime.Compare(x.created, y.created));
                Console.WriteLine(prefix + "SO " + this.Name + " is part of other objects.");
                foreach (SensualObject SOIsPartOf in IsPartOfReference)
                {
                    string str = " Is part of          ";
                    if (SOIsPartOf == SOParent)
                    {
                        str =    " Is part of Parent = ";
                    }
                    
                    if (SOIsPartOf.created != created)
                    {
                        created = SOIsPartOf.created;
                        SOIsPartOf.PrintSO("\t" + prefix, str, true);
                    }
                    else
                    {
                        SOIsPartOf.PrintSO("\t" + prefix, str, false);
                    }
                }
            }
        }

        /// <summary>
        /// Add a reference to an SO if the reference is a new one and if it is not a self reference.
        /// </summary>
        /// <param name="SO"></param>
        public virtual void AddIncludesReference(SensualObject SO)
        {
            if(SO != null)
            {
                //Check it is not a self reference.
                if (this != SO)
                {
                    if (!IncludesReference.Exists(_SO => Equals(_SO, SO)))
                    {
                        IncludesReference.Add(SO);
                    }
                }
            }
        }

        /// <summary>
        /// Add an IsPartOf reference to an SO if the reference is a new one and if it is not a self reference.
        /// </summary>
        /// <param name="SO"></param>
        public virtual void AddIsPartOfReference(SensualObject SO)
        {
            if (SO != null)
            {
                //Check it is not a self reference.
                if (this != SO)
                {
                    if (!IsPartOfReference.Exists(_SO => Equals(_SO, SO)))
                    {
                        IsPartOfReference.Add(SO);
                    }
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
                    AddIncludesReference(SOFrom);
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
            AddIncludesReference(_SOFrom);
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
        List<SensualQuality> MovedQualities = new List<SensualQuality>();
        foreach (SensualQuality SQ in qualities)
            {
                int index = DerivedFrom.qualities.IndexOf(SQ);

                //add the quality to DerivedFrom if it is not already in DerivedFrom.
                if (index < 0 && SQ.SOEvent != null && SQ.SOParent == this && SQ.IsSO == false)
                {
                    DerivedFrom.AddQuality(SQ.SOEvent, SQ.Name, "True", SQ.Description, _dt, false);
                    if(SQ.Value == "True")
                        MovedQualities.Add(SQ);
                }
            }
        foreach (SensualQuality SQ in MovedQualities)
            {
                qualities.Remove(SQ);
            }
            //qualities.RemoveAll(SQ => SQ.Value == "True");
        }

        public SensualObject(string _name, DateTime _dt, SensualObject Parent)
        {
            Name = _name;
            this.created = _dt;
            this.SOParent = Parent;
        }
    }

}
