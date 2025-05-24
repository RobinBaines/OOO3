//
// @Copyright 2025 Robin Baines
// Licensed under the MIT license. See MITLicense.txt file in the project root for details.
//

using System;
using System.ComponentModel;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OOOCL
{
    public class BuildSOs
    {
        public static BindingList<SensualObject> TheSOs = new BindingList<SensualObject>();
        public static List<string> GenerateScript = new List<string>();
        public static List<string> RandomWalk = new List<string>();
        public static List<string> DisplaySOs = new List<string>();
        public static List<string> QuerySOs = new List<string>();

        static int _sleeptime;
        static public int SleepTime { 
            get
            {
                return _sleeptime;
            }
            set {
                _sleeptime = value;
            }
        }

        internal static SensualObject? LastSO = null;
        internal static SensualObject? SOEvent = null;
        public const string INHERITSO = "INHERIT";
        internal const string GENERATESCRIPT = "GenerateScript";
        
        internal const string RANDOMWALK = "RandomWalk";
        internal const string DISPLAYSOS = "DisplaySOs";
        internal const string QUERYSOS = "QuerySOs";
        internal static DateTime dateTime = new DateTime(2025, 01, 01, 12, 0, 0, 0);
        /// <summary>
        /// Remove comments from a line of input.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        internal static string RemoveComments(string result)
        {
            int pos = result.IndexOf("//");
            if (pos != -1)
            {
                result = result.Substring(0, pos);
            }
            result = result.Trim();
            return result;
        }

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
        internal static string GetName(string result, ref string Command, ref string SOName, ref string Inherits, ref string SQName, ref string Value, ref bool StartOfObject, ref bool EndOfObject, ref bool blnDateTime)
        {
            int pos = result.IndexOf(GENERATESCRIPT);
            if (pos != -1)
            {
                result = result.Substring(pos + GENERATESCRIPT.Length).Trim();
                Command = GENERATESCRIPT;
                return result;
            }

             pos = result.IndexOf(RANDOMWALK);
            if (pos != -1)
            {
                result = result.Substring(pos + RANDOMWALK.Length).Trim();
                Command = RANDOMWALK;
                return result;
            }
          

            pos = result.IndexOf(DISPLAYSOS);
            if (pos != -1)
            {
            result = result.Substring(pos + DISPLAYSOS.Length).Trim();
                Command = DISPLAYSOS;
                return result;
            }

            pos = result.IndexOf(QUERYSOS);
            if (pos != -1)
            {
                result = result.Substring(pos + QUERYSOS.Length).Trim();
                Command = QUERYSOS;
                return result;
            }

            pos = result.IndexOf("Time");
            if (pos != -1)
            {
                result = result.Substring(pos + 4).Trim();

                //allow Time = 2025-01-01 12:00:00.000 or
                // Time 2025-01-01 12:00:00.000 
                pos = result.IndexOf("=");
                result = result.Substring(pos + 1).Trim();

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
        internal static SensualObject? GetSO(string _name)
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
        internal static void InheritSensualObject(SensualObject SOEvent, string SOName, string Inherits, int msecs)
        {
            SensualObject? SO;
            if(SOEvent.Name == SOName)
                SO = SOEvent;
            else SO = GetSO(SOName);
            if (SO != null)
            {
                SO.ptrDerivedFrom = GetSO(Inherits);
                if (SO.ptrDerivedFrom != null)
                {
                    //Move default qualities from the Child (LastSO) to the Parent (ptrDerivedFrom). 
                    SO.MoveQualities(SO.ptrDerivedFrom, dateTime.AddMilliseconds(msecs));
                }
                SO.AddQuality(SOEvent, INHERITSO + " " + Inherits, "True", "", dateTime.AddMilliseconds(msecs));
            }
        }
        internal static void InheritSensualObject(SensualObject SOEvent, SensualObject SO, string Inherits, int msecs)
        {
            if (SO != null)
            {
                SO.ptrDerivedFrom = GetSO(Inherits);
                if (SO.ptrDerivedFrom != null)
                {
                    //Move default qualities from the Child (LastSO) to the Parent (ptrDerivedFrom). 
                    SO.MoveQualities(SO.ptrDerivedFrom, dateTime.AddMilliseconds(msecs));
                }
                SO.AddQuality(SOEvent, INHERITSO + " " + Inherits, "True", "", dateTime.AddMilliseconds(msecs));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SOEvent"></param>
        /// <param name="SQName"></param>
        /// <param name="SQValue"></param>
        internal static void WriteQuality(SensualObject LastSO, SensualObject SOEvent, string SQName, string SQValue, int msecs)
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
                    LastSO?.AddQuality(SOEvent, SQName, SQValue, "", dateTime.AddMilliseconds(msecs));
                else
                    LastSO?.AddQuality(SOEvent, SOFrom, SQName, SQValue, "", dateTime.AddMilliseconds(msecs));
            }
            else
            {
                LastSO?.AddQuality(SOEvent, SQName, SQValue, "", SOOfValue, dateTime.AddMilliseconds(msecs));
                LastSO?.AddIncludesReference(SOOfValue);
            }
        }

     internal static  Stack<SensualObject> StackLastSO = new Stack<SensualObject>();
        internal static Stack<SensualObject> StackSOEvent = new Stack<SensualObject>();
        public static void ProcessSO(int msecs, string Command, string SOName, string Inherits, string SQName, string SQValue, bool StartOfObject, bool EndOfObject)
        {
            if (LastSO == null)
            {
                LastSO = SOEvent;
            }

            //A quality Nida = sleeping
            if (SQName.Length > 0 && SOEvent != null && LastSO != null)
            {
                WriteQuality(SOEvent, LastSO,  SQName, SQValue, msecs);
            }

            //An SO is created or referenced MeetSiena : Event {
            if (SOName.Length > 0 && StartOfObject == true)
            {
                msecs = 0;

                //If switching from a parent SO to a nested child SO save the context of the parent.
                if (LastSO != null)
                {
                    StackLastSO.Push(LastSO);
                }
                if (SOEvent != null)
                {
                    StackSOEvent.Push(SOEvent);
                }
                LastSO = SOEvent;

                //create the SO if it is a new one.
                SOEvent = new(SOName, dateTime, LastSO);
                int index = TheSOs.IndexOf(SOEvent);
                if (index >= 0)
                    {
                    SOEvent = TheSOs[index];
                    }

                //if(SOEvent.Name == "TreasureIsland")
                //    Console.WriteLine("");

                //add the reference from the parent to the child and vice versa.
                if (SOEvent != null && LastSO != null)
                {
                    SOEvent.AddIsPartOfReference(LastSO);
                    LastSO.AddIncludesReference(SOEvent);
                }

                //If the new SO inherits, MeetSiena: Event {
                if (Inherits.Length > 0 )
                {
                    //If this is a root SO (has no parent) then create the inheritance and references.
                    if (LastSO == null || LastSO.Name == Inherits)
                    {
                        if (SOEvent != null)
                        {
                            SensualObject SOInherits = new(Inherits, dateTime, SOEvent);
                            int ind = TheSOs.IndexOf(SOInherits);
                            if (ind >= 0)
                            {
                                //SOEvent.AddIsPartOf(TheSOs[ind]);
                                TheSOs[ind].AddIncludesReference(SOEvent);
                            }
                           //InheritSensualObject(SOEvent, SOName, Inherits, msecs);
                            InheritSensualObject(SOEvent, SOEvent, Inherits, msecs);
                        }
                    }
                    else
                    {
                        //InheritSensualObject(LastSO, SOName, Inherits, msecs);
                        InheritSensualObject(LastSO, SOEvent, Inherits, msecs);
                    }
                }

                //Add the SO after updating Inherits.
                if (index < 0)
                {
                    if(SleepTime > 0)
                        Thread.Sleep(SleepTime);
                    TheSOs.Add(SOEvent);
                }
            }

            //support Init : Event			//INHERIT_SENSUALOBJECT
            if (SOName.Length > 0 && StartOfObject == false && Inherits.Length > 0 && LastSO!=null)
            {
                InheritSensualObject(LastSO, SOName, Inherits, msecs);
            }

            if (EndOfObject == true)
            {
                if (StackLastSO.Count > 0)
                {
                    LastSO = StackLastSO.Pop();
                    SOEvent = StackSOEvent.Pop();
                }
                else
                {
                    LastSO = null;
                    SOEvent = null;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        internal static void ProcessFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                string? line;
                int msecs = 0;

                using (StreamReader sr = new StreamReader(filepath))
                {
                    line = sr.ReadLine();
                    while (line != null)
                    {

                        line = RemoveComments(line).Trim();
                        if (line.Length > 0)
                        {
                            string Command = "";
                            string SOName = "";
                            string Inherits = "";
                            string SQName = "";
                            string SQValue = "";
                            bool StartOfObject = false;
                            bool EndOfObject = false;
                            bool blnDateTime = false;
                            line = GetName(line, ref Command, ref SOName, ref Inherits, ref SQName, ref SQValue, ref StartOfObject, ref EndOfObject, ref blnDateTime);
                            if (blnDateTime == false)
                            {
                                if (Command != "")
                                {
                                    if (Command == RANDOMWALK)
                                        RandomWalk.Add(line);
                                    if (Command == GENERATESCRIPT)
                                        GenerateScript.Add(line);
                                    if (Command == DISPLAYSOS)
                                        DisplaySOs.Add(line);
                                    if (Command == QUERYSOS)
                                        QuerySOs.Add(line);
                                }
                                else
                                {
                                    ProcessSO(msecs, Command, SOName, Inherits, SQName, SQValue, StartOfObject, EndOfObject);
                                }
                            }
                        }
                        //Read the next line
                        line = sr.ReadLine();
                    }
                }
            }
        }
        

        public static void CreateOutput(string filepath)
        {
            BuildSOs.ProcessFile(filepath);

            //Look for SOFrom pointers in Events and create a script to add the quality to the subject.
            //SOs.QuerySOSQ("Event");
            foreach (string s in GenerateScript)
            {
                SOs.GenerateAScript(s);
            }


            //BuildSOs.DisplaySOs is a list of output commands read from the script
            //string s is the DisplaySOs parameter for example
            //* or Dog.
            foreach (string s in DisplaySOs)
            {
                SOs.DisplaySOs(s);
            }

            //BuildSOs.QuerySOs is a list of output commands read from the script.
            //string s is the QuerySOs parameter for example
            //Event, Dog
            foreach (string s in QuerySOs)
            {
                string[] tokens = s.Split(',');
                if (tokens.Length > 1)
                {
                    SOs.QuerySOs(tokens[0].Trim(), tokens[1].Trim());
                }
            }

            foreach (string s in RandomWalk)
            {
                int nr = 0;
                if (Int32.TryParse(s, out nr))
                {
                    for (int i = 0; i < nr; i++)
                    {
                        Console.WriteLine("///////////////// Random started connected walk through SQs /////////////////");
                        SOs.RandomSQs();
                        Console.WriteLine("");
                    }
                }
            }
        }

    }
}
