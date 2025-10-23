using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeepThought.src.DeepThought.Domain;

namespace DeepThought.src.DeepThought.Strategies
{
    public class SlowCountStrategy
    {
        public string AnswerQuestion(string QuestionText)
        {
            for (int i = 0; i <= 15; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine("Generating Ultimate Answer...");
            }

            return "42";
        }
    }
}