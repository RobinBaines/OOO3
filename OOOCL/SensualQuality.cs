//
// @Copyright 2025 Robin Baines
// Licensed under the MIT license. See MITLicense.txt file in the project root for details.
//
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOCL
{
    public class SensualQuality : BaseClass
    {
        public string Value = "";
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

                string str = "\t" + prefix;
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
            SensualQualityInit(_SOEvent, _SOparent, _IsSO, _name, _value, _description, _dt);
        }

        public SensualQuality(SensualQuality SQ)
        {
            if (SQ.SOEvent != null && SQ.SOParent != null)
                SensualQualityInit(SQ.SOEvent, SQ.SOParent, SQ.IsSO, SQ.Name, SQ.Value, SQ.Description, SQ.created);
        }

    }

}
