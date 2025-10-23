using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeepThought.src.DeepThought.Domain
{
    public class Job
    {
        public string JobId { get; set; }
        public string QuestionText { get; set; }
        public string AlgorithmKey { get; set; }
        public string Status { get; set; }
        public string Progress { get; set; }
        // timestamps and optional result
        
    }
}