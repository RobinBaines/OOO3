﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static OOO3.Program;
using static System.Net.WebRequestMethods;

namespace OOO3
{
    internal class SOs
    {
        private static bool ReturnTrue(SensualObject SO)
        {
            return true;
        }

        public static int count = 0;
        public static void PrintSOs(int Verbose)
        {
            Console.WriteLine("SOs");
            foreach (SensualObject SO in BuildSOs.TheSOs)
            {
                Console.WriteLine("//////////////////////////////////////" + SO.Name + "//////////////////////////////////////");
                Console.WriteLine(SO.created.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                            CultureInfo.InvariantCulture) + " " + SO.Name);
                count = 0;
                SO.PrintQualities("", null);
            }
        }

        public static void PrintEvents(string Parent)
        {
            Console.WriteLine("Event SOs. Selection from " + BuildSOs.TheSOs.Count.ToString() + " SOs.");
            int index = 1;
            foreach (SensualObject SO in BuildSOs.TheSOs)
            {
                bool blnParent = false;
                if (SO.ptrDerivedFrom != null)
                {
                    if (SO.ptrDerivedFrom.Name == Parent)
                    {
                        blnParent = true;
                    }
                }

                if (SO.Name == Parent || Parent == "*" || blnParent)
                {
                    Console.WriteLine("//////////////////////////////////////" + SO.Name + " " + index.ToString() + " //////////////////////////////////////");
                    SO.PrintSO("", " ");
                    count = 0;
                    //SOsPrinted.RemoveAll(ReturnTrue);
                    SO.PrintQualities("", null);
                }
                index += 1;
            }
        
        }
        private static void QueryReferences(SensualObject? _SOParent, string _Child)
        {
            if (_SOParent != null)
            {
                foreach (SensualObject SO in _SOParent.references)
                {
                    if (SO.Name == _Child)
                    {
                        Console.Write(" Parent: " + _SOParent.created.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                            CultureInfo.InvariantCulture) + " " + _SOParent.Name);
                        Console.WriteLine(" Child: " + SO.created.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                            CultureInfo.InvariantCulture) + " " + SO.Name);
                    }
                    else
                    {
                        if (CheckParents(SO, _Child) != null)
                        { 
                        Console.Write(" Parent: " + _SOParent.created.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                            CultureInfo.InvariantCulture) + " " + _SOParent.Name);
                        Console.WriteLine(" Child: " + SO.created.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                            CultureInfo.InvariantCulture) + " " + SO.Name);
                        }
                    }
                }
            }
        }
        private static SensualObject? CheckParents(SensualObject SOParent, string _Name)
        {
            if (SOParent.ptrDerivedFrom == null)
            {
                return null;
            }
            if (SOParent.ptrDerivedFrom.Name == _Name)
            {
                return SOParent.ptrDerivedFrom;
            }
            return CheckParents(SOParent.ptrDerivedFrom, _Name);
        }

        /// <summary>
        /// Query SOs for example SOs.QuerySOs("Event", "Dog");
        /// It looks for SOs with a Parent = "Event" and referenced SOs with a Parent = "Dog".
        /// </summary>
        /// <param name="_parent"></param>
        /// <param name="_child"></param>
        public static void QuerySOs(string _parent, string _child)
        {
            Console.WriteLine("////////////////////////////// QuerySOs of " + _parent + "//////////////////////////////////////");

            Console.WriteLine("Query Parent: \'" + _parent + "\' for Child: \'" + _child + "\'");
            SensualObject? SOParent;
            
            foreach (SensualObject SO in BuildSOs.TheSOs)
            {
                if(SO.Name ==  _parent)
                {
                    SOParent = SO;
                    QueryReferences(SO, _child);
                }
                else
                {
                    //check whether the SO has a Parent SO with name _parent.
                    //Is a recursive call so it will return Nida if _parent = Animal.
                    SOParent = CheckParents(SO, _parent);
                    if (SOParent != null)
                    {
                        QueryReferences(SO, _child);
                    }
                }
            }
        }

        /// <summary>
        ///Look for SOFrom pointers in _parent, for example Events, and create a script to add the quality to the subject.
        ///MeetBakkeveen : Event {
        ///Nida = playing Siena
        ///will create 
        ///Nida{
        /// playing = Siena
        ///}
        /// </summary>
        /// <param name="_parent"></param>
        public static void QuerySOSQ(string _parent)
        {
            Console.WriteLine("Query Parent: \'" + _parent + ".");
           
            List<SensualQuality> qualities = new List<SensualQuality>();
           
            foreach (SensualObject SO in BuildSOs.TheSOs)
            {
                if (SO.ptrDerivedFrom != null)
                {
                    if (SO.ptrDerivedFrom.Name == _parent)
                    {
                       // lSOs.Add(SO);
                        foreach (SensualQuality SQ in SO.qualities)
                        {
                            if (SQ.SOFrom != null)
                            {
                                //if (SQ.SOFrom.Name == _quality) // && SQ.Value == _value)
                                {
                                    if (!qualities.Exists(_SQ => Equals(_SQ, SQ) && Equals(_SQ.Value, SQ.Value)) )
                                        {
                                        qualities.Add(SQ);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (qualities.Count > 0)
            {
                List<string> verbs = new List<string>();
                string theFile = ".\\OOO3\\GeneratedScripts\\Generated.scr";
                using (StreamWriter sw = new StreamWriter(theFile))
                {
                    sw.WriteLine("Time " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                            CultureInfo.InvariantCulture));
                    sw.WriteLine("Generated : Event {");
                    foreach (SensualQuality SQ in qualities)
                    {
                        int index = SQ.Value.IndexOf(" ");
                        string value = SQ.Value;
                        string value2 = "True";
                        if (index > -1)
                        {
                            value = SQ.Value.Substring(0, index);
                            value2= SQ.Value.Substring(index + 1);
                        }
                        sw.WriteLine(SQ.SOFrom.Name + " {");
                        sw.WriteLine(value + " = " + value2);
                        sw.WriteLine("}");
                        sw.WriteLine("");
                        if (!verbs.Contains(value) && BuildSOs.GetSO(value) == null)
                        {
                            verbs.Add(value);
                            sw.WriteLine(value + " : Verb {");
                            value = value.Replace("ing", "");
                            sw.WriteLine(" Root = " + value);
                            sw.WriteLine("}");
                        }
                    }
                    sw.WriteLine("}");
                }
                BuildSOs.ProcessFile(theFile);
            }
        }
        public static void PrintInput()
        {
           
            Console.WriteLine("SOs PrintInput");
            //TheSOs.Sort(delegate (SensualObject x, SensualObject y)
            //{
            //    return x.created.CompareTo(y.created);
            //});

            //make a list of qualitites.
            List<SensualQuality> qualities = new List<SensualQuality>();
            foreach (SensualObject SO in BuildSOs.TheSOs)
            {
                foreach(SensualQuality SQ in SO.qualities)
                {
                    qualities.Add(SQ);
                }
            }
            //choose an SQ.
            int index = RandomInt(qualities.Count);

            //sort the list.
            qualities.Sort(delegate (SensualQuality x, SensualQuality y)
            {
                return x.created.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                          CultureInfo.InvariantCulture)
                .CompareTo(y.created.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                          CultureInfo.InvariantCulture));
            });

            foreach (SensualQuality SQ in qualities)
            {
                SQ.PrintQuality("", SQ.SOParent.Name);
            }
        }
        public static int RandomInt(int maxbase)
        {
            long elapsedTicks = DateTime.Now.Ticks;
            return (int)(elapsedTicks % maxbase);
        }

        public static bool CheckSQ(SensualQuality ASQ, string SQName)
        {
            if (ASQ.SOParent.Name != SQName)
            {
                if (ASQ.SOParent.ptrDerivedFrom != null)
                {
                    if (ASQ.SOParent.ptrDerivedFrom.Name != SQName)
                        return false;
                }
                else
                    return false;
            }
            return true;
        }
        public static bool CheckSQContains(SensualQuality ASQ, string SQName)
        {
            if (ASQ.SOParent.Name.Contains(SQName))
            {
                return true;
            }
           if (ASQ.SOParent.ptrDerivedFrom != null)
                {
                    if (ASQ.SOParent.ptrDerivedFrom.Name.Contains(SQName))
                        return true;
                }
            return false;
        }


        /// <summary>
        /// SOs RandomSQs based on 272 qualitites and starting with index = 204
	//2025-02-04 17:40:00.000 T2 : Event
	//2025-02-04 17:40:00.011 Quality Place = Home
	//	2025-01-01 17:00:00.000 SO Reference Home : Place
	//2025-01-01 13:00:00.000 Event
	//2025-01-01 13:00:00.011 Init => Place = True
	//2025-02-02 14:00:00.000 MeetNida : Event
	//2025-02-02 14:00:00.011 Quality Place = Bakkeveen
	//	2025-02-02 14:00:00.000 SO Reference Bakkeveen : Place
	//2025-02-03 14:00:00.000 MeetSiena : Event
	//2025-02-03 14:00:00.011 Quality Place = Dino Bos
	//	2025-01-01 15:00:00.000 SO Reference Dino Bos : Place
	//2025-02-04 14:00:00.000 DogsAreDogs : Event
	//2025-02-04 14:00:00.011 Quality Place = Home
	//	2025-01-01 17:00:00.000 SO Reference Home : Place
	//2025-02-04 15:00:00.000 MeetBakkeveen : Event
	//2025-02-04 15:00:00.011 Quality Place = Bakkeveen
	//	2025-02-02 14:00:00.000 SO Reference Bakkeveen : Place

 //   Switch from MeetBakkeveen.Place to BlackDog.Size
	//2025-02-04 16:10:00.000 BlackDog : Dog
	//2025-02-04 16:10:00.011 MeetBakkeveen => Size = Small
	//2025-02-02 14:00:00.000 Nida : Dog
	//2025-02-04 14:00:00.011 DogsAreDogs => Size = Medium
	//2025-02-03 14:00:00.000 Siena : Dog
	//2025-02-04 14:00:00.011 DogsAreDogs => Size = Large
	//2025-02-04 14:00:00.000 Dog : Animal
	//2025-02-04 14:00:00.011 DogsAreDogs => Size = True


 //   Switch from Dog.Size to Colour.Secondary
	//2025-01-01 14:00:00.000 Colour
	//2025-01-01 14:00:00.011 Init => Secondary = True
	//2025-01-01 14:00:00.000 White : Colour
	//2025-01-01 14:00:00.011 Init => Secondary = False
	//2025-01-01 14:00:00.000 Black : Colour
	//2025-01-01 14:00:00.011 Init => Secondary = False


        /// </summary>
        public static void RandomSQs()
        {
            //make a list of qualitites.
            List<SensualQuality> qualities = new List<SensualQuality>();
            List<SensualQuality> printedqualities = new List<SensualQuality>();
            foreach (SensualObject ASO in BuildSOs.TheSOs)
            {
                foreach (SensualQuality ASQ in ASO.qualities)
                {
                  //if (CheckSQContains(ASQ, BuildSOs.INHERITSO) == false && CheckSQ(ASQ, "Verb") == false && CheckSQ(ASQ, "Colour") == false)
                    {
                        qualities.Add(ASQ);
                    }
                }
            }

            if (qualities.Count == 0) return;
            int count = 40;
            //choose an SQ.
            int index = RandomInt(qualities.Count);
            Console.WriteLine("");
            Console.WriteLine("SOs RandomSQs based on " + qualities.Count.ToString() + " qualitites and starting with index = " + index.ToString());

            SensualQuality SQ = qualities[index];
            SensualObject? SO;
            SensualQuality SQ2= null;
            while (count > 0)
            {
                count--;
                SQ.SOParent.PrintSO("\t", " ");
                SQ.PrintQuality("", SQ.SOParent.Name);
                printedqualities.Add(SQ);
                SQ2 = null;

                //find a SQ with the same Name but other SO which has not been printed yet.
                foreach (SensualQuality ASQ in qualities)
                {
                    if (ASQ.Name == SQ.Name && ASQ.SOParent.Name != SQ.SOParent.Name)
                    {
                        SQ2 = ASQ;
                        foreach (SensualQuality pSQ in printedqualities)
                        {
                            if (pSQ.Name == ASQ.Name && pSQ.SOParent.Name == ASQ.SOParent.Name)
                            {
                                SQ2 = null;
                                break;
                            }
                        }

                        //stay in loop to select SQ where the SO has max references?
                        if (SQ2 != null)                      
                            break;
                    }
                }

                //switch to another SO if there are no matching SQs.
                if (SQ2 == null)
                {
                    SO = SQ.SOParent;
                    if (SO.references.Count == 0)
                        break;
                    index = RandomInt(SO.references.Count);
                    SO = SO.references[index];
                    if (SO.qualities.Count == 0)
                        break;

                    index = RandomInt(SO.qualities.Count);
                    SQ2 = SO.qualities[index];
                    Console.WriteLine("");
                    Console.WriteLine("\t" + "Switch from " + SQ.SOParent.Name + "." + SQ.Name + " to " + SQ2.SOParent.Name + "." + SQ2.Name);
                }
                
                    SQ = SQ2;
            }

            //while (count > 0)
            //{
            //    count--;
            //    SQ.SOParent.PrintSO("", " ");
            //    SQ.PrintQuality("", SQ.SOParent.Name);
            //    SO = SQ.SOParent;
            //    if (SO.references.Count == 0)
            //        break;

            //    index = RandomInt(SO.references.Count);
            //    SO = SO.references[index];
            //    if (SO.qualities.Count == 0)
            //        break;
            //    index = RandomInt(SO.qualities.Count);
            //    SQ = SO.qualities[index];
            //}

            ////sort the list.
            //qualities.Sort(delegate (SensualQuality x, SensualQuality y)
            //{
            //    return x.created.ToString("yyyy-MM-dd HH:mm:ss.fff",
            //                              CultureInfo.InvariantCulture)
            //    .CompareTo(y.created.ToString("yyyy-MM-dd HH:mm:ss.fff",
            //                              CultureInfo.InvariantCulture));
            //});

            //foreach (SensualQuality SQ in qualities)
            //{
            //    SQ.PrintQuality("", SQ.SOParent.Name);
            //}
        }
    }
}
