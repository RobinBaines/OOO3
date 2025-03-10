//
// @ Copyright 2025 Robin Baines
// Licensed under the MIT license. See LICENSE file in the project root for details.
//

/*
The incoming stream is a collection of Events. 
An Event is a collection of detailed 'events'.
{InterfaceEvent {ADD_SENSUALOBJECT |  INHERIT_SENSUALOBJECT | AddQuality | DateTime }
*/

namespace OOO3
{
    static class Program
    {
    static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                BuildSOs.ProcessFile(args[0]);
            }
            else
            {

            }

            //Look for SOFrom pointers in Events and create a script to add the quality to the subject.
            SOs.QuerySOSQ("Event");

            ///
            // SOs.PrintSOs(2);
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


            //SOs.QuerySOs("Event", "Animal");
            //SOs.PrintInput();

        }

    }
}
