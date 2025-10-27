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
        // most likely doesnt work.
        private static string _fileName = "DeepThought\\src\\DeepThought\\deepthought-jobs.json";
        private static List<Job> _jobs = [];

        public static void Load()
        {
            // load all current jobs to the runner
        }
        public static void SaveJobToDisk(Job Job)
        {
            _jobs.Add(Job);
            string JsonString = JsonConvert.SerializeObject(Job, Formatting.Indented);
            File.WriteAllText(_fileName, JsonString);
        }

        public static void UpdateJob(Job job)
        {
            
        }
        public static void UpdateCompletedJobToDisk(Job Job)
        {
            dynamic JsonObject = GetJsonStringFromFile();
            JsonObject[Job.JobId]["Status"] = "Completed";
            JsonObject[Job.JobId]["Progress"] = "100%";
            JsonObject[Job.JobId]["Result"] = JsonConvert.SerializeObject(Job.Result, Formatting.Indented);
            SaveJsonObjToDisk(JsonObject);
        }

        public static void UpdateCancelledJobToDisk(Job Job)
        {
            dynamic JsonObject = GetJsonStringFromFile();
            JsonObject[Job.JobId]["Status"] = "Canceled";
            JsonObject[Job.JobId]["Progress"] = "<100%";
            SaveJsonObjToDisk(JsonObject);
        }

        private static dynamic GetJsonStringFromFile()
        {
            string json = File.ReadAllText(_fileName);
            return JsonConvert.DeserializeObject(json);
        }
        public static void SaveJsonObjToDisk(dynamic JsonObject)
        {
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(JsonObject, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_fileName, output);
        }

    }
}