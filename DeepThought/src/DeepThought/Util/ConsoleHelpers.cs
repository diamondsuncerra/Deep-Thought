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

        public static bool CheckUltimateQuestion(string? UltimateQuestion)
        {
            return UltimateQuestion != null && UltimateQuestion.Length <= 200;
        }
        public static bool CheckAlgorithmKey (string? AlgorithmKey)
        {
            return AlgorithmKey != null && (AlgorithmKey.Equals("Trivial") || AlgorithmKey.Equals("SlowCount") ||
            AlgorithmKey.Equals("RandomGuess"));
        }
        public static void InvalidOption()
        {
            Console.WriteLine("The value is not valid. Please try again.");
        }


        public static void DoOption1()
        {

            // Checking the values are correct might move to separate methods to be SOLID.
            Console.WriteLine("Please submit your Ultimate Questions for which we definitely have an answer.");
            string? UltimateQuestion = Console.ReadLine();

            if (!CheckUltimateQuestion(UltimateQuestion))
            {
                Console.WriteLine("Unfortunately, questions is not suitable. Try shorter.. or maybe something at all.");
                return;
            }

            Console.WriteLine("Which algorithm should Deep Thought use?");
            string? AlgorithmKey = Console.ReadLine();
            if (!CheckAlgorithmKey(AlgorithmKey))
            {
                Console.WriteLine("Unfortunately, that algorithm is not currently supported by Deep Thought. Try one of the provided options.");
                return;
            }

            // now do the job
            
        }
        public static void DoOption2()
        {
            Console.WriteLine("Printing jobs..");

        }
        public static void DoOption3()
        {
            Console.WriteLine("Please submit the JobId.");
            string? JobIdString = Console.ReadLine();
            if (int.TryParse(JobIdString, out int JobId))
            {
                Console.WriteLine("Printing Job Result..");
            } else return;
            
        }
        public static void DoOption4()
        {
            Console.WriteLine("You've already found the answer, haven't you?");
            Console.WriteLine("Forever deleting the Ultimate Job..");
        }
         public static void DoOption5()
        {
            Console.WriteLine("Thank you for using Deep Thought. 42 ms until termination.");
            //TODO: maybe implement a timer
        }
    }
}