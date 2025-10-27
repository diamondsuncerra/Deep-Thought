using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using DeepThought.src.DeepThought.Strategies;

namespace DeepThought.src.DeepThought.Domain
{
    public class Job
    {
        public string JobId { get; set; }
        public string QuestionText { get; set; }
        public string AlgorithmKey { get; set; }
        public string Status { get; set; }
        public int Progress { get; set; }
        private IAnswerStrategy _strategy;
        public string Answer { get; set; } = string.Empty;

        public JobResult Result {get; set;}

        public DateTime? CreatedUtc { get; set; }
        public Job(string JobId, string QuestionText, string AlgorithmKey, string Status = "Pending", int Progress = 0)
        {
            this.JobId = JobId;
            this.QuestionText = QuestionText;
            this.AlgorithmKey = AlgorithmKey;
            CreatedUtc = DateTime.UtcNow;
            if (AlgorithmKey.Equals("Trivial"))
                _strategy = new TrivialStrategy();
            if (AlgorithmKey.Equals("SlowCount"))
                _strategy = new SlowCountStrategy();
            if (AlgorithmKey.Equals("RandomGuess"))
                _strategy = new RandomGuessStrategy();
        }


        public async Task<string> DoJob(CancellationToken token, IProgress<int>? progress)
        {
            Stopwatch stopwatch = new Stopwatch(); // for duration
            stopwatch.Start();
            Answer = await _strategy.AnswerQuestion(token, progress);
            stopwatch.Stop();
            Result = new JobResult(Answer, "To be Implemented", stopwatch.ElapsedMilliseconds);
            Console.WriteLine("Do we ever get here? " + Result.ToString());
            return Answer;
        }

    
    }
}