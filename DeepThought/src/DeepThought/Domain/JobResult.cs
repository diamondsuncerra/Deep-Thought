using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeepThought.src.DeepThought.Domain
{
    public class JobResult
    {
        // for printing job result
        public string JobId { get; set; }
        public string Answer { get; set; }
        public string Summary { get; set; }
        public string DurationMs { get; set; }
    }
}