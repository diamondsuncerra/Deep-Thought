using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeepThought.src.DeepThought.Domain;

namespace DeepThought.src.DeepThought.Strategies
{
    public interface IAnswerStrategy
    {
        Task<string> AnswerQuestion(CancellationToken token, IProgress<int>? progress = null);
    }
}