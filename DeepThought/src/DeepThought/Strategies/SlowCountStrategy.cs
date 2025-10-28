using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeepThought.src.DeepThought.Domain;

namespace DeepThought.src.DeepThought.Strategies
{
    public class SlowCountStrategy : IAnswerStrategy
    {
        private TimeSpan _zero;

        public SlowCountStrategy(TimeSpan? zero = null) // if user doesn't provide value is null
        {
            this._zero = zero ?? TimeSpan.FromMilliseconds(300);
        }

        public async Task<string> AnswerQuestion(CancellationToken token, IProgress<int>? progress)
        {
            for (int i = 0; i <= 100; i++)
            {
                token.ThrowIfCancellationRequested();
                await Task.Delay(_zero, token);
                progress?.Report(i);
            }

            return "42";
        }
    }
}