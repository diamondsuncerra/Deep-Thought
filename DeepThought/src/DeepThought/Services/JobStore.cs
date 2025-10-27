using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DeepThought.src.DeepThought.Domain;
using Newtonsoft.Json;
namespace DeepThought.src.DeepThought.Services
{
    public class JobStore
    {
        // do something about this
        private static string _fileName = "C:\\Endava\\EndevLocal\\Deep Thought\\DeepThought\\src\\DeepThought\\deepthought-jobs.json";
        // The problem was that List was just puttin them all in order
        public static Dictionary<string, Job> Jobs { get; private set; } = new(StringComparer.OrdinalIgnoreCase);

        public static void Load()
        {
            // load all current jobs to the runner TODO
        }

        public static void UpdateJobsToDisk(Job Job)
        {
            Jobs[Job.JobId] = Job;
            Jobs[Job.JobId].Result = Job.Result;
            SaveJobsToDisk();
        }

        public static void SaveJobsToDisk()
        {
            if (Jobs.Count == 0) return;

            string output = JsonConvert.SerializeObject(Jobs, Formatting.Indented);
            File.WriteAllText(_fileName, output);
        }

        public static string GetResultByJobId(String JobId)
        {
            if (JobId == string.Empty)
                return "Oh well, looks like we do not have that in store.";
                
            string json = File.ReadAllText(_fileName);
            Dictionary<string, Job> AllJobs = JsonConvert.DeserializeObject<Dictionary<string, Job>>(json);
            Job CurrentJob = AllJobs[JobId];
            return CurrentJob.Result.ToString();
        }
    }
}