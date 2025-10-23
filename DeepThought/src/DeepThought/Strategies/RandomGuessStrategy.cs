using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using DeepThought.src.DeepThought.Domain;

namespace DeepThought.src.DeepThought.Strategies
{
    public class RandomGuessStrategy
    {   
        public string AnswerQuestion(string QuestionText)
        {
            Console.Write("Hmm.. Let me think..");
            Thread.Sleep(1000);
            var list = new int[] { 42 };
            return Enumerable.Range(0, 0).Select(e => list[new Random().Next(list.Length)]).ToString() ?? "42";
        }
    }
}