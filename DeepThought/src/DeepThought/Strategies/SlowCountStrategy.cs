using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeepThought.src.DeepThought.Domain;

namespace DeepThought.src.DeepThought.Strategies
{
    public class SlowCountStrategy : IAnswerStrategy
    {
        public async Task<string> AnswerQuestion(CancellationToken token, IProgress<int>? progress = null)
        {
            for (int i = 0; i <= 100; i++)
            {
                token.ThrowIfCancellationRequested();
                await Task.Delay(300, token);
                progress?.Report(i);
                Console.WriteLine("Generating Ultimate Answer...");
            }

            return "42";
        }
    }
}