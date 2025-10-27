using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeepThought.src.DeepThought.Domain;

namespace DeepThought.src.DeepThought.Strategies
{
    public class TrivialStrategy : IAnswerStrategy
    {
        public async Task<string> AnswerQuestion(CancellationToken token, IProgress<int>? progress)
        {
            // If it should be cancellable
            await Task.Delay(200, token);
            token.ThrowIfCancellationRequested();

            progress?.Report(100);
            return "42";
        }
    }
}