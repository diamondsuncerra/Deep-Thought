using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeepThought.src.DeepThought.Util
{
    public class ConsoleHelpers
    {
        public static void MenuPrompt()
        {
            Console.WriteLine("Please select an option:");
            Console.WriteLine("(1) Submit Question");
            Console.WriteLine("(2) List Jobs");
            Console.WriteLine("(3) View Result by JobId");
            Console.WriteLine("(4) Cancel Running Job");
            Console.WriteLine("(5) Exit");
        }

        public static void InvalidOption()
        {
            Console.WriteLine("The value is not valid. Please try again.");
        }

        public static void DoOption1()
        {

        }
        public static void DoOption2()
        {

        }
        public static void DoOption3()
        {

        }
        public static void DoOption4()
        {

        }
         public static void DoOption5()
        {
            
        }
    }
}