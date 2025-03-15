//
// @Copyright 2025 Robin Baines
// Licensed under the MIT license. See MITLicense.txt file in the project root for details.
//
using System.Buffers.Text;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Threading;
namespace OOO3
{
    internal class SOs
    {
        /// <summary>
        /// Display the SOs with SQs filtering on Parent.
        /// </summary>
        /// <param name="Parent"></param>
        public static void DisplaySOs(string Parent)
        {
            Console.WriteLine("Print SOs. Selection from " + BuildSOs.TheSOs.Count.ToString() + " SOs " + "filtered on " + Parent);
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
                    SO.PrintQualities("");
                    Console.WriteLine();
                    SO.PrintReferences("");
                    Console.WriteLine();
                }
                index += 1;
            }
        
        }

        /// <summary>
        /// Look for a referenced SO called Child.
        /// </summary>
        /// <param name="SOParent"></param>
        /// <param name="Name"></param>
        private static void QueryReferences(SensualObject? SOParent, string Name)
        {
            if (SOParent != null)
            {
                foreach (SensualObject SO in SOParent.references)
                {
                    if (SO.Name == Name)
                    {
                        Console.Write(" Parent: " + SOParent.created.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                            CultureInfo.InvariantCulture) + " " + SOParent.Name);
                        Console.WriteLine(" Child: " + SO.created.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                            CultureInfo.InvariantCulture) + " " + SO.Name);
                    }
                    else
                    {
                        if (CheckParents(SO, Name) != null)
                        { 
                        Console.Write(" Parent: " + SOParent.created.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                            CultureInfo.InvariantCulture) + " " + SOParent.Name);
                        Console.WriteLine(" Child: " + SO.created.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                            CultureInfo.InvariantCulture) + " " + SO.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// recursive call looking for inherited SO called Name.
        /// </summary>
        /// <param name="SOParent"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        private static SensualObject? CheckParents(SensualObject SOParent, string Name)
        {
            if (SOParent.ptrDerivedFrom == null)
            {
                return null;
            }
            if (SOParent.ptrDerivedFrom.Name == Name)
            {
                return SOParent.ptrDerivedFrom;
            }
            return CheckParents(SOParent.ptrDerivedFrom, Name);
        }

        /// <summary>
        /// Query SOs for example SOs.QuerySOs("Event", "Dog");
        /// It looks for SOs with a Parent = "Event" and referenced SOs with a Parent = "Dog".
        /// </summary>
        /// <param name="Parent"></param>
        /// <param name="Child"></param>
        public static void QuerySOs(string Parent, string Child)
        {
            Console.WriteLine("////////////////////////////// QuerySOs of " + Parent + "//////////////////////////////////////");

            Console.WriteLine("Query \'" + Parent + "\' for \'" + Child + "\'");
            SensualObject? SOParent;
            
            foreach (SensualObject SO in BuildSOs.TheSOs)
            {
                if(SO.Name ==  Parent)
                {
                    SOParent = SO;
                    QueryReferences(SO, Child);
                }
                else
                {
                    //check whether the SO has a Parent SO with name _parent.
                    //Is a recursive call so it will return Nida if _parent = Animal.
                    SOParent = CheckParents(SO, Parent);
                    if (SOParent != null)
                    {
                        QueryReferences(SO, Child);
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
            Console.WriteLine("Query \'" + _parent + ".");

            //Create a List of qualities where the Name of the Quality is an SO in SOFrom.           
            List<SensualQuality> qualities = new List<SensualQuality>();
            foreach (SensualObject SO in BuildSOs.TheSOs)
            {
                if (SO.ptrDerivedFrom != null)
                {
                    if (SO.ptrDerivedFrom.Name == _parent)
                    {
                        foreach (SensualQuality SQ in SO.qualities)
                        {
                            //	2025-02-03 14:00:00.011 Quality Nida = playing Siena
                            if(SQ.IsSO)
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

            //Input 	2025-02-03 14:00:00.011 Quality Nida = playing Siena
            //Results in output
            //      Nida {
            //           playing = Siena
            //           }
            //      playing: Verb {
            //           Root = play
            //           }
            if (qualities.Count > 0)
            {
                List<string> verbs = new List<string>();
                string theFile = "Generated.txt";
                using (StreamWriter sw = new StreamWriter(theFile))
                {
                    sw.WriteLine("Time " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                            CultureInfo.InvariantCulture));
                    sw.WriteLine("Generated : Event {");
                    foreach (SensualQuality SQ in qualities)
                    {
                        if (SQ.IsSO)
                        {
                            //	2025-02-03 14:00:00.011 Quality Nida = playing Siena
                            int index = SQ.Value.IndexOf(" ");
                            //Split playing Siena
                            string value = SQ.Value;
                            string value2 = "True";
                            if (index > -1)
                            {
                                value = SQ.Value.Substring(0, index);
                                value2 = SQ.Value.Substring(index + 1);
                            }
                            //                            Nida {
                            //                                playing = Siena
                            //}

                            //                        playing: Verb {
                            //                                Root = play
                            //                        }
                            //sw.WriteLine(SQ.SOFrom.Name + " {");
                            sw.WriteLine(SQ.Name + " {");
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
                    }
                    sw.WriteLine("}");
                }
                BuildSOs.ProcessFile(theFile);
            }
        }


        /// <summary>
        /// return a random integer with maxbase as maximum of the range.
        /// </summary>
        /// <param name="maxbase"></param>
        /// <returns></returns>
        public static int RandomInt(int maxbase)
        {
            long elapsedTicks = DateTime.Now.Ticks;
            return (int)(elapsedTicks % maxbase);
        }

       
        /// <summary>
        /// Using the SQ which has just been printed find another SQ from another SO, which hasn't been printed yet.
        /// </summary>
        /// <param name="SQ"></param>
        /// <param name="qualities"></param>
        /// <param name="printedqualities"></param>
        /// <returns></returns>
        private static SensualQuality? GetSQ(SensualQuality SQ, List<SensualQuality> qualities, List<SensualQuality> printedqualities)
        {
            SensualQuality? SQ2 = null;
            foreach (SensualQuality ASQ in qualities)
            {
                if (ASQ.SOParent != null)
                {
                    //find an SQ with the same Name but another SO.
                    if (ASQ.Name == SQ.Name && ASQ.SOParent.Name != SQ.SOParent.Name)
                    {
                        SQ2 = ASQ;
                        foreach (SensualQuality pSQ in printedqualities)
                        {
                            if (pSQ.SOParent != null)
                            {
                                if (pSQ.Name == ASQ.Name && pSQ.SOParent.Name == ASQ.SOParent.Name)
                                {
                                    //already printed.
                                    SQ2 = null;
                                    break;
                                }
                            }
                        }

                        //SQ2 is the next SQ.
                        if (SQ2 != null)
                            break;
                    }
                }
            }
            return SQ2;
        }

        /// <summary>
        ///  Print a list of SQs by starting with a random SQ then moving to an SQ with the same Name but another SO 
        ///  which has not been printed yet.
        ///  When there are no more SQs meeting the above criteria switch to another SO which is referenced by the last SO and
        ///  find an SQ which hasn't been printed yet.
        /// Typical output:
/*SOs RandomSQs 
	Using 235 qualities and starting with index = 198
		SO Feel with SQ Root = Feel
		SO Verb with SQ Root = True
		SO playing with SQ Root = play
		SO sleeping with SQ Root = sleep
		SO running with SQ Root = runn
		SO call with SQ Root = call
		SO come with SQ Root = come
		SO coming with SQ Root = com
		SO sit with SQ Root = sit
		SO sitting with SQ Root = sitt
		SO eating with SQ Root = eat
		SO swimming with SQ Root = swimm
	
	SWITCH from swimming.Root using reference Generated to Me.Feel
		SO Me with SQ Feel = Cold
	
	SWITCH from Me.Feel using reference InitEvent to Me.Gender
		SO Me with SQ Gender = Male
		SO Animal with SQ Gender = True
		SO M with SQ Gender = Female
	
	SWITCH from M.Gender using reference MeetNida to MeetNida.Weather
		SO MeetNida with SQ Weather = Cold
		SO Event with SQ Weather = True
		SO MeetSiena with SQ Weather = Cold
		SO MeetBakkeveen with SQ Weather = Cold
	
	SWITCH from MeetBakkeveen.Weather using reference Black Dog to Black Dog.Size
		SO Black Dog with SQ Size = Small
		SO Nida with SQ Size = Medium
		SO Siena with SQ Size = Large
		SO Dog with SQ Size = True
	
	SWITCH from Dog.Size using reference DogsAreDogs to DogsAreDogs.Place
		SO DogsAreDogs with SQ Place = Home
		SO Event with SQ Place = True
		SO MeetNida with SQ Place = Bakkeveen
		SO MeetSiena with SQ Place = Dino Bos
		SO MeetBakkeveen with SQ Place = Bakkeveen
		SO T2 with SQ Place = Home
	
	SWITCH from T2.Place using reference Verb to Verb.Root
		SO Verb with SQ Root = True
*/
        /// </summary>
        public static void RandomSQs()
        {
            //make a list of qualities.
            List<SensualQuality> qualities = new List<SensualQuality>();
            List<SensualQuality> printedqualities = new List<SensualQuality>();

            foreach (SensualObject ASO in BuildSOs.TheSOs)
            {
                foreach (SensualQuality ASQ in ASO.qualities)
                {
                    //exclude the inherit steps.
                    if (!ASQ.Name.Contains(BuildSOs.INHERITSO))
                      {
                        qualities.Add(ASQ);
                    }
                }
            }

            if (qualities.Count == 0) return;
            int count = 4000;
            
            //Pick a random SQ.
            int index = RandomInt(qualities.Count);
            //index = 4;
            Console.WriteLine("");
            Console.WriteLine("SOs RandomSQs ");
            Console.WriteLine("\tUsing " + qualities.Count.ToString() + " qualities and starting with index = " + index.ToString());

            SensualQuality SQ = qualities[index];
            SensualObject? SO;
            SensualQuality? SQ2= null;
            while (count > 0)
            {
                count--;
                if (SQ.SOParent != null)
                {
                    //SQ.SOParent.PrintSO("\t\t", " ");
                    //SQ.PrintQuality("\t\t", SQ.SOParent.Name);
                    Console.WriteLine("\t\tSO " + SQ.SOParent.Name + " with SQ " + SQ.Name + " = " + SQ.Value);
                    printedqualities.Add(SQ);

                    //find a SQ with the same Name but other SO which has not been printed yet.
                    SQ2 = GetSQ(SQ, qualities, printedqualities);
                }

                //switch to another referenced SO if there are no matching SQs.
                if (SQ2 == null)
                {
                    SO = SQ.SOParent;
                    if (SO != null)
                    {
                        if (SO.references.Count == 0)
                            break;
                        index = RandomInt(SO.references.Count);
                        SO = SO.references[index];
                        if (SO.qualities.Count == 0)
                            break;

                        //find an SQ which has not been printed yet.
                        foreach (SensualQuality ASQ in SO.qualities)
                        {
                            bool printed = false;
                            foreach (SensualQuality pSQ in printedqualities)
                            {
                                if (ASQ == pSQ)
                                {
                                    printed = true;
                                    break;                                    
                                }
                            }
                            if (printed == false && !ASQ.Name.Contains(BuildSOs.INHERITSO))
                            {
                                SQ2 = ASQ;
                                break;
                            }
                        }

                        //if a new SQ was found continue.
                        if (SQ2 != null)
                        {
                            if (SQ.SOParent != null && SQ2.SOParent != null)
                            {
                                Console.WriteLine("\t");
                                Console.WriteLine("\t" + "SWITCH from " + SQ.SOParent.Name + "." + SQ.Name + " using reference " + SO.Name + " to " + SQ2.SOParent.Name + "." + SQ2.Name);
                            }
                        }
                    }
                }
                if (SQ2 != null)
                    SQ = SQ2;
                else break;
            }
        }
    }
}
