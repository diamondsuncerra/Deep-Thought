using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeepThought.src.DeepThought.Domain;
using DeepThought.src.DeepThought.Strategies;

namespace DeepThought.src.DeepThought.Services
{
    public class JobRunner
    {
        public static async Task RunJob(Job job, CancellationToken cancelToken)
        {

            var progress = new Progress<int>(p =>
            {
                job.Progress = p;
                Console.WriteLine($"Progress: {p}%"); // this will be later saved to json
            });

            try
            {
                job.Status = "Running"; // maybe pending
                string result = await job.DoJob(cancelToken, progress); // this will be saved to json
                job.Status = "Completed";
                Console.WriteLine("Completed, answer is: " + result);

            } catch(Exception ex)
            {
                job.Status = "Cancelled";
                Console.WriteLine("Job was CANCELED!");
                return;
            }
           

        }

    }
}