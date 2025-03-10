//
// @ Copyright 2025 Robin Baines
// Licensed under the MIT license. See LICENSE file in the project root for details.
//

namespace OOO3
{
    internal class BuildSOs
    {
        public enum State
        {
            START,
            EVENT,
            OBJECT
        }

        public static List<SensualObject> TheSOs = new List<SensualObject>();
        public static SensualObject? LastSO = null;
        public static SensualObject? SOEvent = null;
        public const string INHERITSO = "INHERIT_SENSUALOBJECT";

        /// <summary>
        /// Remove comments from a line of input.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string RemoveComments(string result)
        {
            int pos = result.IndexOf("//");
            if (pos != -1)
            {
                result = result.Substring(0, pos);
            }
            result = result.Trim();
            return result;
        }

        public static DateTime dateTime = new DateTime(2025, 01, 01, 12, 0, 0, 0);

        /// <summary>
        /// Parse a line of input.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="SOName"></param>
        /// <param name="Inherits"></param>
        /// <param name="SQName"></param>
        /// <param name="Value"></param>
        /// <param name="StartOfObject"></param>
        /// <param name="EndOfObject"></param>
        /// <returns></returns>
        public static string GetName(string result, ref string SOName, ref string Inherits, ref string SQName, ref string Value, ref bool StartOfObject, ref bool EndOfObject, ref bool blnDateTime)
        {
            int pos = result.IndexOf("Time");
            if (pos != -1)
            {
                result = result.Substring(pos + 4).Trim();
                pos = result.IndexOf("=");
                if (pos != -1)
                {
                    result = result.Substring(pos + 1).Trim();
                }
                dateTime = DateTime.Parse(result);
                result = "";
                blnDateTime = true;
                return result;
            }

            pos = result.IndexOf("}");
            if (pos != -1)
            {
                EndOfObject = true;
                result = "";
            }

            pos = result.IndexOf("{");
            if (pos != -1)
            {
                SOName = result.Substring(0, pos).Trim();
                StartOfObject = true;
                result = result.Substring(0, pos).Trim();
            }

            pos = result.IndexOf(":");
            if (pos != -1)
            {
                SOName = result.Substring(0, pos).Trim();
                Inherits = result.Substring(pos + 1).Trim();
                result = "";
            }

            pos = result.IndexOf("=");
            if (pos != -1)
            {

                Value = result.Substring(pos + 1).Trim();
                result = result.Substring(0, pos).Trim();

                //decided not to include Name.Name = Name in the input syntax.
                //Me.Feel = Cold
                //pos = result.IndexOf(".");
                //if (pos != -1)
                //{
                //    SOName = result.Substring(0, pos).Trim();
                //    SQName = result.Substring(pos + 1).Trim();
                //}
                //else

                SQName = result;
                result = "";
            }
            else
            {
                if (result.Length > 0 && SOName.Length == 0)
                {
                    SQName = result;
                    Value = "True";
                }
            }
            return result;
        }

        /// <summary>
        /// Find and return an SO from the List of SOs.
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public static SensualObject? GetSO(string _name)
        {
            foreach (SensualObject SO in TheSOs)
            {
                if (SO.Name == _name)
                {
                    return SO;
                }
            }
            return null;
        }

        /// <summary>
        /// Set the inherited pointer of an SO if Inherits is an SO. Move default qualities from the Child (LastSO) to the Parent (ptrDerivedFrom). 
        /// </summary>
        /// <param name="SOEvent"></param>
        /// <param name="SOName"></param>
        /// <param name="Inherits"></param>
        public static void InheritSensualObject(SensualObject SOEvent, string SOName, string Inherits)
        {
            LastSO = GetSO(SOName);
            if (LastSO != null)
            {
                LastSO.ptrDerivedFrom = GetSO(Inherits);
                if (LastSO.ptrDerivedFrom != null)
                {
                    //Move default qualities from the Child (LastSO) to the Parent (ptrDerivedFrom). 
                    LastSO.MoveQualities(LastSO.ptrDerivedFrom, dateTime.AddMilliseconds(14));
                }
                LastSO.AddQuality(SOEvent, INHERITSO + " " + Inherits, "True", "", dateTime.AddMilliseconds(14));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SOEvent"></param>
        /// <param name="SQName"></param>
        /// <param name="SQValue"></param>
        public static void WriteSensualQuality(SensualObject SOEvent, string SQName, string SQValue)
        {
            SensualObject? SOOfValue;
            SOOfValue = GetSO(SQValue);
            if (SOOfValue == null)
            {
                //Check if the Quality is an SO.
                SensualObject? SOFrom = GetSO(SQName);
                if (SOFrom != null)
                    if (SOFrom == LastSO)   //do not add a self reference.
                        SOFrom = null;

                if (SOFrom == null)
                    LastSO?.AddQuality(SOEvent, SQName, SQValue, "", dateTime.AddMilliseconds(11));
                else
                    LastSO?.AddQuality(SOEvent, SOFrom, SQName, SQValue, "", dateTime.AddMilliseconds(11));
            }
            else
            {
                LastSO?.AddQuality(SOEvent, SQName, SQValue, "", SOOfValue, dateTime.AddMilliseconds(11));
                LastSO?.AddReference(SOOfValue);
            }
            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        public static void ProcessFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                String? line;
                //InterfaceEvent? IE = null;

                using (StreamReader sr = new StreamReader(filepath))
                {
                    State State = State.START;
                    line = sr.ReadLine();
                    while (line != null)
                    {

                        line = RemoveComments(line).Trim();
                        if (line.Length > 0)
                        {
                            if (line == "Colour = Black")
                            //+                                                "" && SQName == "Colour")
                            {
                             //   Console.WriteLine(line);
                            }
                            string SOName = "";
                            string Inherits = "";
                            string SQName = "";
                            string SQValue = "";
                            bool StartOfObject = false;
                            bool EndOfObject = false;
                            bool blnDateTime = false;
                            line = GetName(line, ref SOName, ref Inherits, ref SQName, ref SQValue, ref StartOfObject, ref EndOfObject, ref blnDateTime);
                            //Console.WriteLine(Name + " " + Inherits + " " + SQName + " " + Value + " " + EndOfObject.ToString() + ".");
                            if (blnDateTime == false)
                            {
                                if (LastSO == null)
                                {
                                    LastSO = SOEvent;
                                }
                                switch (State)
                                {
                                    case State.OBJECT:
                                        {
                                            if (SQName.Length > 0 && SOEvent != null)
                                            {
                                                WriteSensualQuality(SOEvent, SQName, SQValue);
                                            }

                                            if (SOName.Length > 0 && Inherits.Length > 0 && SOEvent != null)
                                            {
                                                //Record the inheritance as a quality.
                                                //2025 - 02 - 04 16:10:00.014 MeetBakkeveen => INHERIT_SENSUALOBJECT Dog = True
                                                SQValue = Inherits;
                                                SQName = SOName;
                                                WriteSensualQuality(SOEvent, SQName, SQValue);
                                            }

                                            if (EndOfObject == true)
                                            {
                                                // if (IE != null)
                                                {
                                                    LastSO = null;
                                                    State = State.EVENT;
                                                }
                                            }
                                        }
                                        break;

                                    case State.EVENT:
                                        {
                                            if (SOName.Length > 0 && SQName.Length > 0 && SQValue.Length > 0 && SOEvent != null)
                                            {
                                                SensualObject? SOFrom = GetSO(SOName);
                                                if (SOFrom != null)
                                                {
                                                    SOEvent.AddQuality(SOEvent, SOFrom, SQName, SQValue,  "", dateTime.AddMilliseconds(12));
                                                }
                                            }
                                            else
                                            {
                                                if (SQName.Length > 0 && SOEvent != null)
                                                {
                                                    WriteSensualQuality(SOEvent, SQName, SQValue);
                                                }

                                                if (SOName.Length > 0)
                                                {
                                                    SensualObject SONew = new(SOName, dateTime);
                                                    int index = TheSOs.IndexOf(SONew);
                                                    if (index >= 0)
                                                    {
                                                        SONew = TheSOs[index];
                                                    }

                                                    //add the new SO to the Event.
                                                    //but first check that a self reference does not occur.
                                                    if (SOEvent != null)
                                                    {
                                                        SOEvent.AddReference(SONew);
                                                        LastSO = SONew;
                                                        if (index < 0)
                                                            TheSOs.Add(LastSO);
                                                    }


                                                    if (Inherits.Length > 0 && SOEvent != null)
                                                    {
                                                        InheritSensualObject(SOEvent, SOName, Inherits);
                                                    }
                                                    if (StartOfObject == true)
                                                    {
                                                        State = State.OBJECT;
                                                    }

                                                }
                                                if (EndOfObject == true)
                                                {
                                                    State = State.START;
                                                }
                                            }
                                        }
                                        break;

                                    case State.START:
                                        {
                                            if (SOName.Length > 0)
                                            {
                                                SOEvent = new(SOName, dateTime);
                                                int index = TheSOs.IndexOf(SOEvent);
                                                if (index < 0)
                                                {
                                                    TheSOs.Add(SOEvent);
                                                }
                                                else
                                                {
                                                    SOEvent = TheSOs[index];
                                                }
                                                if (Inherits.Length > 0)
                                                {
                                                    InheritSensualObject(SOEvent, SOName, Inherits);
                                                }
                                                State = State.EVENT;
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                        //Read the next line
                        line = sr.ReadLine();
                    }
                }
            }
        }

    }
}
