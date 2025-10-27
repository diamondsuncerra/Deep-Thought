using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeepThought.src.DeepThought.Domain
{
    public class JobResult(string Answer, string Summary, long DurationMs)
    {
        // for printing job result
        public string Answer { get; set; }
        public string Summary { get; set; }
        public long DurationMs { get; set; }
    }
}