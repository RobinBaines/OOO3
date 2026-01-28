//
// @Copyright 2025 Robin Baines
// Licensed under the MIT license. See MITLicense.txt file in the project root for details.
//

using System;
using System.ComponentModel;

using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.Design;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace OOOCL
{
    public class BuildSOs
    {
        public static BindingList<SensualObject> TheSOs = new BindingList<SensualObject>();
        public static List<string> GenerateScript = new List<string>();
        public static List<string> RandomWalk = new List<string>();
        public static List<string> DisplaySOs = new List<string>();
        public static List<string> QuerySOs = new List<string>();
        public static bool blnNaturalText = false;
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
        internal static SensualObject? Context = null;
        internal static SensualObject? SOEvent = null;

        internal const string INHERITSO = "INHERIT";
        internal const string GENERATESCRIPT = "GenerateScript";
        internal const string CONTEXT = "context";
        internal const string ENDOBJECT = "endobject";
        

        internal const string RANDOMWALK = "RandomWalk";
        internal const string DISPLAYSOS = "DisplaySOs";
        internal const string QUERYSOS = "QuerySOs";
        internal const string NATURALTEXT = "NaturalText";
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
            
            return result;
        }

        /// <summary>
        /// Check whether a line of script is a command.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        internal static bool CheckCommands(string line)
        {
            int pos = line.IndexOf(GENERATESCRIPT);
            if (pos != -1)
            {
                line = line.Substring(pos + GENERATESCRIPT.Length).Trim();
                GenerateScript.Add(line);
                return true;
            }

            pos = line.IndexOf(RANDOMWALK);
            if (pos != -1)
            {
                line = line.Substring(pos + RANDOMWALK.Length).Trim();
                RandomWalk.Add(line);
                return true;
            }


            pos = line.IndexOf(DISPLAYSOS);
            if (pos != -1)
            {
                line = line.Substring(pos + DISPLAYSOS.Length).Trim();
                DisplaySOs.Add(line);
                return true;
            }

            pos = line.IndexOf(QUERYSOS);
            if (pos != -1)
            {
                line = line.Substring(pos + QUERYSOS.Length).Trim();
                  QuerySOs.Add(line);
                return true;
            }

            pos = line.IndexOf(NATURALTEXT);
            if (pos != -1)
            {
                line = line.Substring(pos + NATURALTEXT.Length).Trim();
                blnNaturalText = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Parse a line of script.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="SOName"></param>
        /// <param name="Inherits"></param>
        /// <param name="SQName"></param>
        /// <param name="Value"></param>
        /// <param name="blnDateTime"></param>
        /// <param name="SOIncludedInSOName"></param>
        /// <returns></returns>
        internal static string GetNameNaturalText(string result, ref string SOName, ref string Inherits, ref string SQName, ref string Value, ref bool blnDateTime, ref string SOIncludedInSOName)
        {
            
            result = result.Replace("an ", " ", StringComparison.OrdinalIgnoreCase);
            result = result.Replace(" a ", " ", StringComparison.OrdinalIgnoreCase);
            result = result.Replace(" at ", " ");
            result = result.Replace(" the ", " ", StringComparison.OrdinalIgnoreCase);
            if(result.Substring(0, 4).ToLower() == "the ")
                result = result.Substring(4);

            result = result.Replace(" of ", " ");
            result = result.Replace(" and ", " ");
            result = result.Replace(" is ", " ");
            result = result.Replace(" it ", " ");
            result = result.Replace(" has ", " ");
            result = result.Replace("=", " ");
            result = Regex.Replace(result, @"\s+", " ");
            //result = result.Replace("   ", " ");
            //result = result.Replace("  ", " ");
            int indexof = result.LastIndexOf('.');
            if (indexof == (result.Length - 1)) result = result.Remove(indexof, 1);

            char[] charSeparators = new char[] { ' ', '\t' };
            string[] words = result.Trim().Split(charSeparators);
            int index = 0;
            foreach (string word in words)
            {
                string wordlc = word.ToLower();
                switch (wordlc)
                {
                    case CONTEXT:
                        if (words.Count() > index + 1)
                        {
                            index++;
                            SOName = words[index];
                        }
                        break;
                    case ENDOBJECT:
                        if (words.Count() > index + 1)
                        {
                            index++;
                            SOName = words[index];
                        }
                        break;
                    case "includes":
                        if (words.Count() > index + 1)
                        {
                            index++;
                            SOIncludedInSOName = words[index];
                        }
                        break;
                    case "property":
                        if (words.Count() > index + 1)
                        {
                            index++;
                            SQName = words[index];
                            Value = "true";
                            while (words.Count() > index + 1)
                            {
                                if(words[index] !="")
                                {
                                    index++;
                                    Value = words[index];
                                    break;
                                }
                                index++;
                            }
                        }
                        break;
                    case "type":
                        if (words.Count() > index + 1)
                        {
                            index++;
                            //EndOfObject = true;
                            Inherits = words[index];
                        }
                        break;

                    case "":
                        break;

                    default:
                        if (index == 0)
                        {
                            SOName = words[index];
                        }
                        // code block
                        break;
                }
                index++;
                if (index == words.Count()) break;
            }
            return result;
        }

        /// <summary>
        /// Set the IsSO bit of an SQ if the SQ is an SO.
        /// </summary>
        /// <param name="NewSO"></param>
        public static void QualityIsSO(SensualObject NewSO)
        {
            //check whether the new SO has already been defined as a property of another SO.
            foreach (SensualObject SO in TheSOs)
            {
                foreach (SensualQuality SQ in SO.qualities)
                {
                    if (SQ.Name == NewSO.Name)
                        SQ.IsSO = true;
                }
            }
        }

     /// <summary>
     /// The Context is the SO from which a property is set in another SO.
     /// It is set explicitely using Contect "SOName" of is set if the Includes command is used.
     /// </summary>
     /// <param name="ParentSO"></param>
     /// <param name="SOIncludedInSOName"></param>
     /// <param name="Inherits"></param>
     /// <param name="line"></param>
        public static void SetContext(SensualObject ParentSO, string SOIncludedInSOName, string Inherits, string line)
        {
            if(line.Substring(0, CONTEXT.Length).ToLower() == CONTEXT || SOIncludedInSOName.Length > 0)
                Context = ParentSO;
        }

        public static bool IsNotAParent(SensualObject ParentSO)
        {
            //foreach (SensualObject SO in TheSOs)
            //{
            //    if (SO.SOParent == ParentSO)
            //        return false;
            //}
            return true;
        }

        public static void ProcessNaturalSO(int msecs, string line, string SOName, string Inherits, string SQName, string SQValue,  string SOIncludedInSOName)
        {



            string test2;
            if (SQName == "ends" && SQValue == "true")
            {
                test2 = "test";
            }

            //An SO is created or referenced MeetSiena : Event {
            if (SOName.Length > 0) // && StartOfObject == true)
            {
                msecs = 0;

                //create the SO if it is a new one.
                //The SOParent = null here because it is only set if Includes command is used to set explicitely, see below.
                SOEvent = new(SOName, dateTime, null);
               
                int index = TheSOs.IndexOf(SOEvent);
                if (index < 0)
                {
                    //SetLastSO(SOEvent, SOEvent, _tabs);
                    TheSOs.Add(SOEvent);
                }
                
                index = TheSOs.IndexOf(SOEvent);
                if (index >= 0)
                {
                    SOEvent = TheSOs[index];
                    SetContext(SOEvent, SOIncludedInSOName, Inherits, line);
                   
                    if (SOIncludedInSOName.Length > 0 && SOIncludedInSOName != SOName)
                    {
                        SensualObject SOInSO = new(SOIncludedInSOName, dateTime, SOEvent);
                        int index2 = TheSOs.IndexOf(SOInSO);
                        if (index2 >= 0)
                        {
                            string test;
                            if (TheSOs[index2].Name == "club_ball")
                            {
                                test = TheSOs[index2].Name;
                            }
                            if (IsNotAParent(TheSOs[index2]))
                            {
                                SOInSO = new(TheSOs[index2]);
                                SOInSO.SOParent = SOEvent;
                                //TheSOs[index2].Ended = true;
                                TheSOs.Add(SOInSO);

                                //add the reference from the parent to the child and vice versa.
                                SOEvent.AddIncludesReference(SOInSO);
                                SOInSO.AddIsPartOfReference(SOEvent);
                            }
                            else
                            {
                                TheSOs[index2].SOParent = SOEvent;
                                SOEvent.AddIncludesReference(TheSOs[index2]);
                                TheSOs[index2].AddIsPartOfReference(SOEvent);
                            }

                            TheSOs.ResetBindings(); //HandleSOChanged with  ListChangedType.Reset
                        }
                        else
                        {
                            SOEvent.AddIncludesReference(SOInSO);
                            SOInSO.AddIsPartOfReference(SOEvent);
                            TheSOs.Add(SOInSO);
                            QualityIsSO(SOInSO);
                        }
                        Thread.Sleep(SleepTime);
                    }
                }

                //add the reference from the parent to the child and vice versa.
                if (SOEvent != null && Context != null && SOEvent != Context)
                {
                    SOEvent.AddIsPartOfReference(Context);
                    Context.AddIncludesReference(SOEvent);
                }

                //If the new SO inherits, MeetSiena: Event {
                if (Inherits.Length > 0)
                {
                    if (SOEvent != null)
                    {
                        SensualObject SOInherits = new(Inherits, dateTime, SOEvent);
                        int ind = TheSOs.IndexOf(SOInherits);
                        if (ind >= 0)
                        {
                            TheSOs[ind].AddIncludesReference(SOEvent);
                        }
                        InheritSensualObject(SOEvent, SOEvent, Inherits, msecs);
                    }
                }
            }

            //A quality Nida = sleeping
            if (SQName.Length > 0 && SOEvent != null && SOName.Length > 0) // && LastSO != null)
            {
                string test;
                if (SOEvent.Name == "Nida" && SQName == "Running" && SQValue == "false")
                {
                    test = "";
                }
                if (SOEvent.Name == "I_SEE_NIDA_BARKING" && SQName == "Running" && SQValue == "false")
                {
                    test = "";
                }

                if (Context != null)
                {
                    WriteQuality( SOEvent, Context, SQName, SQValue, msecs);
                }
                else WriteQualityNatural(SOEvent, SQName, SQValue, msecs);
            }

            if (SOEvent != null)
            {
                if (line.Substring(0, ENDOBJECT.Length).ToLower() == ENDOBJECT)
                {
                    SOEvent.Ended = true;
                    //add the reference from the parent to the child and vice versa.
                    if (SOEvent != null && Context != null && SOEvent != Context)
                    {
                        SOEvent.SetEndedBy(Context);
                    }
                    return;
                }
            }
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
        internal static string GetName(string result, ref string SOName, ref string Inherits, ref string SQName, ref string Value, ref bool StartOfObject, ref bool EndOfObject, ref bool blnDateTime)
        {
            int pos = 0;
                
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
                string extRef = "";
                if (SO.Name != SOEvent.Name)
                    extRef = "> " + SO.Name + " ";
                SO.AddQuality(SOEvent, extRef + INHERITSO + " " + Inherits, "True", "", dateTime.AddMilliseconds(msecs));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SOEvent"></param>
        /// <param name="SQName"></param>
        /// <param name="SQValue"></param>
        internal static void WriteQualityNatural(SensualObject TheSO, string SQName, string SQValue, int msecs)
        {
            SensualObject? SOOfValue;
            SOOfValue = GetSO(SQValue);
            if (SOOfValue == null)
            {
                SensualObject SOEvent = TheSO.SOParent;
                if (SOEvent == null)
                    SOEvent = TheSO;
                //Check if the Quality is an SO.
                SensualObject? SOFrom = GetSO(SQName);
                if (SOFrom != null)
                    if (SOFrom == LastSO)   //do not add a self reference.
                        SOFrom = null;

                if (SOFrom == null)
                    TheSO?.AddQuality(SOEvent, SQName, SQValue, "", dateTime.AddMilliseconds(msecs));
                else
                    TheSO?.AddQuality(SOEvent, SOFrom, SQName, SQValue, "", dateTime.AddMilliseconds(msecs));
            }
            else
            {
                TheSO?.AddQuality(SOEvent, SQName, SQValue, "", SOOfValue, dateTime.AddMilliseconds(msecs));
                TheSO?.AddIncludesReference(SOOfValue);
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
                                TheSOs[ind].AddIncludesReference(SOEvent);
                            }
                            InheritSensualObject(SOEvent, SOEvent, Inherits, msecs);
                        }
                    }
                    else
                    {
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
                LastSO = null;
                //tabs = 0;
                SOEvent = null;
                using (StreamReader sr = new StreamReader(filepath))
                {
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        //uint tabs = CountLeadingTabs(line);
                        line = RemoveComments(line).Trim();
                        if (line.Length > 0)
                        {
                            string Command = "";
                            string SOName = "";
                            string SOIncludedInSOName = "";
                            string Inherits = "";
                            string SQName = "";
                            string SQValue = "";
                            bool StartOfObject = false;
                            bool EndOfObject = false;
                            bool blnDateTime = false;
                            if (CheckCommands(line) == false)
                            {
                                if (blnNaturalText)
                                {
                                    line = GetNameNaturalText(line, ref SOName, ref Inherits, ref SQName, ref SQValue,  ref blnDateTime, ref SOIncludedInSOName);
                                    if (blnDateTime == false)
                                    {
                                        ProcessNaturalSO(msecs, line, SOName, Inherits, SQName, SQValue,  SOIncludedInSOName);
                                    }
                                }
                                else
                                {
                                    line = GetName(line, ref SOName, ref Inherits, ref SQName, ref SQValue, ref StartOfObject, ref EndOfObject, ref blnDateTime);
                                    if (blnDateTime == false)
                                    {
                                        ProcessSO(msecs, Command, SOName, Inherits, SQName, SQValue, StartOfObject, EndOfObject);
                                    }
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
            //20251019 Clear these lists everytime a script is run.
            RandomWalk.Clear();
            GenerateScript.Clear();
            DisplaySOs.Clear();
            QuerySOs.Clear();
            blnNaturalText = false;
            TheSOs.Clear();
            SOEvent = null;
            LastSO = null;
            StackSOEvent.Clear();
            StackLastSO.Clear();

            BuildSOs.ProcessFile(filepath);

            blnNaturalText = false;
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
