using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using DeepThought.src.DeepThought.Domain;

namespace DeepThought.src.DeepThought.Strategies
{
    public class RandomGuessStrategy : IAnswerStrategy
    {   //an a short summary??? todo
        private static readonly int[] Answers = { 42 };

        public async Task<string> AnswerQuestion(CancellationToken token, IProgress<int>? progress)
        {
            Console.Write("Hmm.. Let me think..");
            for (int i = 0; i <= 100; i+=20)
            {
                token.ThrowIfCancellationRequested();
                await Task.Delay(300, token);
                 progress?.Report(i);
                Console.Write(".");
            }

            int answer = Answers[new Random().Next(Answers.Length)];
            return answer.ToString();
        }
    }
}