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
        public static async Task RunJob(Job Job, CancellationToken cancelToken)
        {

            var progress = new Progress<int>(p =>
            {
                Job.Progress = p;
                Console.WriteLine($"Progress: {p}%"); // this will be later saved to json
            });

            try
            {
                Job.Status = "Running"; // maybe pending
                string result = await Job.DoJob(cancelToken, progress); // this will be saved to json
                Job.Status = "Completed";
                Console.WriteLine("Completed, answer is: " + result);
                Console.WriteLine("Job result is: " + Job.Result.ToString());

            } catch(Exception ex)
            {
                Job.Status = "Canceled";
                Console.WriteLine("Job was CANCELED!");
                return;
            }
            finally
            {
                JobStore.UpdateJobsToDisk(Job);
            }

        }

    }
}