using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            char[] spinnerChars = { '|', '/', '-', '\\' };
            int spinnerIndex = 0;

            var progress = new Progress<int>(p =>
            {
                Job.Progress = p;
                var spinner = spinnerChars[spinnerIndex++ % 4];
                Console.Write($"\r{spinner} Progress: {p}%  ");
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
                return; // what if the ^c is done twice or three times.
            }

            finally
            {
                JobStore.UpdateJobsToDisk(Job);
            }

        }

    }
}