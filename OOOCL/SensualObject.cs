//
// @Copyright 2025 Robin Baines
// Licensed under the MIT license. See MITLicense.txt file in the project root for details.
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OOOCL
{

    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Sensual Object
    /// qualitiesofchildren stores all the qualitites of preceding SOs so that overridden SQs are not printed.
    /// </summary>
   public class SensualObject : BaseClass, INotifyPropertyChanged, INotifyPropertyChanging

    {
       static List<SensualObject> TheNewSOs = new List<SensualObject>();
       static int iDepth = 0;

        public SensualObject? ptrDerivedFrom = null;
        public SensualObject? _EndedBySO = null;
        
        public SensualObject? EndedBySO
        {
            get { return _EndedBySO; }
            set { _EndedBySO = value; Ended = true; }
        }

        public List<SensualObject> IncludesReference = new List<SensualObject>();
        public List<SensualObject> ReferencedBy = new List<SensualObject>();
        public List<SensualObject> IsPartOfReference = new List<SensualObject>();
        public List<SensualQuality> qualities = new List<SensualQuality>();

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        //[CallerMemberName]attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        //But also changes e.ListChangedType to ListChangedType.ItemChanged in HandleSOChanged.
         public new SensualObject SOParent
        {
            get { return _SOParent; }
            set
            {
                if (_SOParent != null)
                    _SOParent.PreviousSOParents.Add(this);
                _SOParent = value;
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private int _someValue;
        public int SomeValue
        {
            get { return _someValue; }
            set { _someValue = value; NotifyPropertyChanged("SomeValue"); }
        }

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
                    if (SOReference.Name == "Jasper")
                    {
                        Console.WriteLine("");
                    }

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

            //20250628
            if (ReferencedBy.Count > 0)
            {
                ReferencedBy.Sort((x, y) => DateTime.Compare(x.created, y.created));

                Console.WriteLine(prefix + "SO " + this.Name + " is referenced by.");
                foreach (SensualObject SOReference in ReferencedBy)
                {
                  
                    string str = " Referenced by          ";
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
                        SO.AddReferencedBy(this);

                    }
                }
            }
        }

        /// <summary>
        /// Add a reference to an SO if the reference is a new one and if it is not a self reference.
        /// </summary>
        /// <param name="SO"></param>
        public virtual void AddReferencedBy(SensualObject SO)
        {
            if (SO != null)
            {
                //Check it is not a self reference.
                if (this != SO)
                {
                    if (!ReferencedBy.Exists(_SO => Equals(_SO, SO)))
                    {
                        ReferencedBy.Add(SO);
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
        /// Add an IsPartOf reference to an SO if the reference is a new one and if it is not a self reference.
        /// </summary>
        /// <param name="SO"></param>
        public virtual void SetEndedBy(SensualObject Context)
        {
            if (Context != null)
            {
                //Check it is not a self reference.
                if (this != Context)
                {
                    EndedBySO = Context;
                }

                //propagate to children.
                foreach (SensualObject SO in BuildSOs.TheSOs)
                {
                   if(SO.SOParent == this)
                    {
                        SO.EndedBySO = Context;
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

                //Remove the following as there is too much noise on the screen when updating after
                //every change of a quality.
                //if (qualities.Count%3 == 0)
                {
                  //  NotifyPropertyChanged();
                  //  Thread.Sleep(BuildSOs.SleepTime);
                }

                //make a copy of the SQ in the Event if this is not the Event.
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
        public virtual void AddQuality(SensualObject _SOEvent, string _name, string _value, string _description, DateTime _dt, bool recurse, string Preposition)
        {
            SensualQuality? SQ = new(_SOEvent, this, false, _name, _value, _description, _dt, Preposition);
            AddQuality(SQ, recurse);
        }
        public virtual void AddQuality(SensualObject _SOEvent, string _name, string _value, string _description, DateTime _dt, string Preposition)
        {
            SensualQuality? SQ = new(_SOEvent, this, false, _name, _value, _description, _dt, Preposition);
            AddQuality(SQ, true);
        }

        //if the Value is an SO.
        public virtual void AddQuality(SensualObject _SOEvent, string _name, string _value, string _description, SensualObject _SOOfValue, DateTime _dt, string Preposition)
        {
            SensualQuality? SQ = new(_SOEvent, this, _name, _value, _description, _SOOfValue, _dt, Preposition);
            AddQuality(SQ, true);
        }

        /// <summary>
        /// Called if SOFrom is defined meaning that the SQ.Name refers to an object.
        /// </summary>
        /// <param name="_SOEvent"></param>
        /// <param name="_SOFrom"></param>
        /// <param name="_name"></param>
        /// <param name="_value"></param>
        /// <param name="_description"></param>
        /// <param name="_dt"></param>
        public virtual void AddQuality(SensualObject _SOEvent, SensualObject _SOFrom, string _name, string _value, string _description, DateTime _dt, string Preposition)
        {
            AddIncludesReference(_SOFrom);
            SensualQuality? SQ = new(_SOEvent, this, true, _name, _value, _description, _dt, Preposition);
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
                    DerivedFrom.AddQuality(SQ.SOEvent, SQ.Name, "True", SQ.Description, _dt, false, "");
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

        /// <summary>
        /// Get the name of the top level parent.
        /// </summary>
        /// <param name="SO"></param>
        /// <returns></returns>
        public string NewName(SensualObject SO)
        {
            string strNewName = "";
            if (SO.SOParent != null)
                strNewName = NewName(SO.SOParent);
            else
                strNewName= "(" + SO.Name + ")";
            return strNewName;
        }

        /// <summary>
        /// Copy an SO without including the references.
        /// </summary>
        /// <param name="Source"></param>
        public SensualObject(SensualObject Source)
        {
            if (iDepth == 0)
                TheNewSOs.Clear();
            Name = Source.Name;
            ptrDerivedFrom = Source.ptrDerivedFrom;
            if (Source.EndedBySO != null)
            {
                EndedBySO = Source.EndedBySO;
            }

           string test;
            if (Source.Name.Length >= 3 && Source.Name.Substring(0,3) == "dog")
            {
                test = Source.Name;
            }

            //do not copy the references.
            //foreach (SensualObject SO in Source.IncludesReference)
            //{
            //    IncludesReference.Add(SO);
            //}
            //foreach (SensualObject SO in Source.ReferencedBy)
            //{
            //    ReferencedBy.Add(SO);
            //}
            //foreach (SensualObject SO in Source.IsPartOfReference)
            //{
            //    IsPartOfReference.Add(SO);
            //}

            foreach (SensualQuality SQ in Source.qualities)
            {
                qualities.Add(SQ);
            }

            string strNewName = Source.Name + NewName(Source);
            SensualObject _SO = new(strNewName, new DateTime(2025, 01, 01, 12, 0, 0, 0), null);
            while (BuildSOs.TheSOs.IndexOf(_SO) >= 0)
            {
                strNewName = strNewName + "_";
                _SO.Name = strNewName;
            }
                

            Source.Name = strNewName;

 
            if (Source.Name == "ball(SPLIT)")
            {
                test = Source.Name;
            }

            Source.Ended = true;

            //identify children of the Source and replicate in the copy.

            //  foreach (SensualObject SO in BuildSOs.TheSOs)
            //Add the new SOs in reverse order so that adding them below in the reverse order retains the same order 
            //at the same depth.
            // A [B [C, D] results in TheNewSOs D, C, B, A and BuildSOs.TheSOs.Add adds them to the array as A, B, C, D.
            for (int i = BuildSOs.TheSOs.Count - 1; i >= 0; i--)
                {
                if (BuildSOs.TheSOs[i].SOParent == Source) // && SO.Name != Source.Name)
                {
                    iDepth++;
                    SensualObject newSO = new SensualObject(BuildSOs.TheSOs[i]);
                    newSO.SOParent = this;
                    iDepth--;

                    //do not add to TheSOs here because the collection will change creating an exeception.
                    TheNewSOs.Add(newSO);
                }
            }

            if (iDepth == 0)
            {
                //add in reverse order as this matters for the presentation in the form.
                for (int i = TheNewSOs.Count - 1; i >=0; i--)
                {
                    BuildSOs.TheSOs.Add(TheNewSOs[i]);
                }
            }
        }
    }

}
