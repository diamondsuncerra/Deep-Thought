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
                Console.WriteLine($"Progress: {p}%");
            });

            try
            {
                Job.Status = "Running"; 
                string Result = await Job.DoJob(cancelToken, progress);
                Job.Status = "Completed";
            }
            catch (Exception ex)
            {
                Job.Status = "Canceled";
                return; // what if the ^c is done twice or three times. also for option 4
            }

            finally
            {
                JobStore.UpdateJobsToDisk(Job);
            }

        }

    }
}