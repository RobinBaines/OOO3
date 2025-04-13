//
// @Copyright 2025 Robin Baines
// Licensed under the MIT license. See MITLicense.txt file in the project root for details.
//

/*
The incoming stream is a collection of Events. 
An Event is a collection of 'events'.
Optional => [ ]
1 or more => { }

script = {EVENT  | DATETIME }
EVENT = String [ ':' String] '{' { OBJECT | QUALITY | DATETIME } '}'
OBJECT = String [ ':' String] '{' { QUALITY | DATETIME } '}'
QUALITY = String [ '=' String ] 
DATETIME = 'Time' [ '=' ] 'yyyy-MM-dd HH:mm:ss.fff'
*/

using System.Reflection.Metadata;

namespace OOO3
{
    static class Program
    {
    static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (File.Exists(args[0]))
                {
                    BuildSOs.ProcessFile(args[0]);
                }
                else
                    Console.WriteLine(args[0] + " not found.");
            }
            else
            {
                if (File.Exists("script.txt"))
                {
                    BuildSOs.ProcessFile("script.txt");
                }
                else
                    Console.WriteLine("script.txt not found.");
            }

            //Look for SOFrom pointers in Events and create a script to add the quality to the subject.
            //SOs.QuerySOSQ("Event");
            foreach (string s in BuildSOs.GenerateScript)
            {
                SOs.GenerateAScript(s);
            }

            //BuildSOs.DisplaySOs is a list of output commands read from the script
            //string s is the DisplaySOs parameter for example
            //* or Dog.
            foreach (string s in BuildSOs.DisplaySOs)
            {
                SOs.DisplaySOs(s);
            }

            //BuildSOs.QuerySOs is a list of output commands read from the script.
            //string s is the QuerySOs parameter for example
            //Event, Dog
            foreach (string s in BuildSOs.QuerySOs)
            {
                string[] tokens = s.Split(',');
                if (tokens.Length > 1)
                {
                    SOs.QuerySOs(tokens[0].Trim(), tokens[1].Trim());
                }
            }

            //SOs.DisplaySOs("Dog");

            //Query Person present at an Event.
            //SOs.QuerySOs("Event", "Person");

            // SOs.QuerySOs("Event", "Dog");
            foreach (string s in BuildSOs.RandomWalk)
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

            ////A number of 'walks' through the SOs.
            //for (int i = 0; i < 30; i++)
            //{
            //    Console.WriteLine("///////////////// Random started connected walk through SQs /////////////////");
            //    SOs.RandomSQs();
            //    Console.WriteLine("");
            //}
        }

    }
}
