//
// @Copyright 2025 Robin Baines
// Licensed under the MIT license. See MITLicense.txt file in the project root for details.
//

/*
The incoming stream is a collection of Events. 
An Event is a collection of 'events'.
script = {EVENT  | DateTime }
EVENT = String [ ':' String] '{' { OBJECT | QUALITY | DateTime } '}'
OBJECT = String [ ':' String] '{' { QUALITY | DateTime } '}'
QUALITY = String [ '=' String ] 
*/

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
            SOs.QuerySOSQ("Event");

            SOs.DisplaySOs("*");
            //SOs.DisplaySOs("Dog");

            ////Query Person present at an Event.
            //SOs.QuerySOs("Event", "Person");
            SOs.QuerySOs("Event", "Dog");

            for (int i = 0; i < 30; i++)
            {
                SOs.RandomSQs();
                Console.WriteLine("");
            }
        }

    }
}
