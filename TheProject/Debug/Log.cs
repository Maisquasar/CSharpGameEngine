using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheProject.Debug
{
    class Log
    {
        public static void PrintError(string log)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(log);
            Console.BackgroundColor = ConsoleColor.White;
        }

        public static void PrintWarning(string log)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine(log);
            Console.BackgroundColor = ConsoleColor.White;
        }

        public static void PrintLog(string log)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(log);
            Console.BackgroundColor = ConsoleColor.White;
        }
    }
}
