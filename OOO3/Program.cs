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
using OOOCL;
namespace OOO3
{
    static class Program
    {
    static void Main(string[] args)
        {
            string filepath="";
            if (args.Length > 0)
            {
                if (File.Exists(args[0]))
                {
                    filepath = args[0];
                }
                else
                    Console.WriteLine(args[0] + " not found.");
            }

            else
            {
                if (File.Exists("script.txt"))
                {
                    filepath = "script.txt";
                }
                else
                    Console.WriteLine("script.txt not found.");
            }

            if (filepath.Length > 0)
            {
                BuildSOs.CreateOutput(filepath);
            }
        }
    }
}
