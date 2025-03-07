using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static OOO3.Program;

namespace OOO3
{
    internal class SensualQuality : BaseClass
    {
        public string Value ="";
        public SensualObject? SOFrom; //= new SensualObject(""); //as in AnEvent { Me.Feel = Cold }
        public SensualObject? SOTo; // = new SensualObject("");
        public SensualObject? SOParent; // = new SensualObject(""); //is the owner of this SQ.
        public SensualObject? SOEvent; // = new SensualObject("");
        public virtual void PrintQuality(string prefix, string _SOName)
        {
            if (SOParent != null && SOEvent != null)
            {

                string from = "";
                if (this.SOFrom != null)
                {
                    if (SOFrom.Name != this.Name)
                        from = this.SOFrom.Name + ".";
                }

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


                if (this.SOFrom != null && this.SOTo == null)
                {
                    if (this.SOFrom.Name != "")
                        Console.WriteLine("\t" + prefix + this.created.ToString("yyyy-MM-dd HH:mm:ss.fff",
                    CultureInfo.InvariantCulture) + strEvent + from + this.Name + " = " + this.Value);
                }
                else
                {
                    if (this.SOTo != null)
                    {
                        if (this.SOTo.Name != "")
                            Console.WriteLine("\t" + prefix + this.created.ToString("yyyy-MM-dd HH:mm:ss.fff",
                        CultureInfo.InvariantCulture) + strEvent + from + this.Name + " = " + this.Value + " SO To " + SOTo.Name);
                    }
                    else
                    {
                        Console.WriteLine("\t" + prefix + this.created.ToString("yyyy-MM-dd HH:mm:ss.fff",
                        CultureInfo.InvariantCulture) + strEvent + this.Name + " = " + this.Value);
                    }
                }

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
        private void SensualQualityInit(SensualObject _SOEvent, SensualObject _SOparent, SensualObject? _SOFrom, string _name, string _value, SensualObject? _SOTo, string _description, DateTime _dt)
        {
            if(_SOFrom != null)
                SOFrom = _SOFrom;
            Name = _name;
            Value = _value;
            Description = _description;
            if (_SOTo != null)
                SOTo = _SOTo;
            this.created = _dt;
            SOParent = _SOparent;
            SOEvent = _SOEvent;
        }

        public SensualQuality(SensualObject _SOEvent, SensualObject _SOparent, string _name, string _value, string _description, SensualObject _SO, DateTime _dt)
        {
            SensualQualityInit(_SOEvent, _SOparent, null, _name, _value, null, _description, _dt);
            if (_SO != null)
            {
                references.Add(_SO);
            }
        }
        public SensualQuality(SensualObject _SOEvent, SensualObject _SOparent, string _name, string _value, string _description, DateTime _dt)
        {
            SensualQualityInit(_SOEvent, _SOparent, null, _name, _value, null, _description, _dt);
        }

        public SensualQuality(SensualObject _SOEvent, SensualObject _SOparent, SensualObject _SOFrom, string _name, string _value, string _description, DateTime _dt)
        {
            SensualQualityInit(_SOEvent, _SOparent, _SOFrom, _name, _value, null, _description, _dt);
        }

        public SensualQuality(SensualObject _SOEvent, SensualObject _SOparent, SensualObject _SOFrom, string _name, string _value, SensualObject _SOTo, string _description, DateTime _dt)
        {
            SensualQualityInit(_SOEvent, _SOparent, _SOFrom, _name, _value, _SOTo, _description, _dt);
        }

        public SensualQuality(SensualQuality SQ)
        {
            if (SQ.SOEvent != null && SQ.SOParent != null)
                SensualQualityInit(SQ.SOEvent, SQ.SOParent, SQ.SOFrom, SQ.Name, SQ.Value, SQ.SOTo, SQ.Description, SQ.created);
        }
        protected virtual void MovePropertyToReference()
        {
            Console.WriteLine("Move a property to a new SensualObject and add reference.");
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
            string str = prefix + this.created.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                                                       CultureInfo.InvariantCulture) + extraText + this.Name;
             if (this.ptrDerivedFrom != null)
                str = str + " : " + this.ptrDerivedFrom.Name;
            Console.WriteLine(str);
        }
        public virtual void PrintQualities(string prefix, List<SensualQuality>? qualitiesofchildren)
        {
            SOs.count += 1;

            //avoid endless loop if PrintQualities is used recursively.
            if (SOs.count > 100)
                return;

            {
                if (qualities.Count > 0)
                {
                    //print the  qualities if not in this.qualities.
                    Console.WriteLine(prefix + "SO " + this.Name + " has qualities.");
                    foreach (SensualQuality SQ in qualities)
                    {
                        int index = -1;
                        if (qualitiesofchildren != null)
                        {
                            index = qualitiesofchildren.IndexOf(SQ);
                        }
                        if (index < 0)
                        {
                            SQ.PrintQuality(prefix, this.Name);
                        }
                    }
                }
            }

            if (references.Count > 0)
            {
                Console.WriteLine("");
                Console.WriteLine(prefix + "SO " + this.Name + " has references.");
                foreach (SensualObject SO in references)
                {
                    SO.PrintSO("\t" + prefix, " SO Reference ");
                }
            }
            Console.WriteLine();
        }
        protected virtual void Store()
        {
            Console.WriteLine("Remove reference from short term/concious memory.");
        }
        protected virtual void Recall()
        {
            Console.WriteLine("Recall by adding a reference to short term/concious memory.");
        }
        protected virtual void MovePropertyToReference()
        {
            Console.WriteLine("Move a property to a new SensualObject and add reference.");
        }
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

        void AddQuality(SensualQuality SQ, bool recurse)
        {
            if (SQ.SOEvent != null)
            { 
                SensualObject? SOFrom = BuildSOs.GetSO(SQ.Name);
                if (SOFrom != null)
                {
                    //SQ.SOFrom = SOFrom;
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
            SensualQuality? SQ = new(_SOEvent, this, _name, _value, _description, _dt);
            AddQuality(SQ, recurse);
        }
        public virtual void AddQuality(SensualObject _SOEvent, string _name, string _value, string _description, DateTime _dt)
        {
            SensualQuality? SQ = new(_SOEvent, this, _name, _value, _description, _dt);
            AddQuality(SQ, true);
        }

        //if the Value is an SO.
        public virtual void AddQuality(SensualObject _SOEvent, string _name, string _value, string _description, SensualObject _SO, DateTime _dt)
        {
            SensualQuality? SQ = new(_SOEvent, this, _name, _value, _description, _SO, _dt);
            AddQuality(SQ, true);
        }

        public virtual void AddQuality(SensualObject _SOEvent, SensualObject _SOFrom, string _name, string _value, string _description, DateTime _dt)
        {
            AddReference(_SOFrom);
            SensualQuality? SQ = new(_SOEvent,this, _SOFrom, _name, _value, _description, _dt);
            AddQuality(SQ, true);
        }

        public virtual void AddQuality(SensualObject _SOEvent, SensualObject _SOFrom, string _name, string _value, SensualObject _SOTo, string _description, DateTime _dt)
        {
            AddReference(_SOFrom);
            AddReference(_SOTo);
            SensualQuality? SQ = new(_SOEvent, this, _SOFrom, _name, _value, _SOTo, _description, _dt);
            AddQuality(SQ, true);
        }

        public virtual void MoveQualities(SensualObject Dest, DateTime _dt)
        {
        foreach (SensualQuality SQ in qualities)
            {
                int index = Dest.qualities.IndexOf(SQ);
                if (index < 0 && SQ.SOEvent != null)
                {
                    Dest.AddQuality(SQ.SOEvent, SQ.Name, "True", SQ.Description, _dt, false);
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
