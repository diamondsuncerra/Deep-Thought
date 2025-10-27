using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeepThought.src.DeepThought.Domain
{
  public class JobResult
    {
        public string Answer { get; set; }
        public string Summary { get; set; }
        public long DurationMs { get; set; }

        public JobResult(string answer, string summary, long durationMs)
        {
            Answer = answer;
            Summary = summary;
            DurationMs = durationMs;
        }

        public override string ToString() =>
            $"Answer is: {Answer}, Summary is: {Summary}, Duration is: {DurationMs} ms";
    }
}