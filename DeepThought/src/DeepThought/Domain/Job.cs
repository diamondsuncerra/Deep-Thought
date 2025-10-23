using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeepThought.src.DeepThought.Domain
{
    public class Job(string JobId, string QuestionText, string AlgorithmKey, string Status="Pending", string Progress = "0%")
    {
        public string JobId { get; set; }
        public string QuestionText { get; set; }
        public string AlgorithmKey { get; set; }
        public string Status { get; set; }
        public string Progress { get; set; }
    }
}