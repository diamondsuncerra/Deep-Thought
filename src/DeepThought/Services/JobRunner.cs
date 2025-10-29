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
                // JobStore.UpdateJobsToDisk(Job); => this updates in real time but it cause concurent blah blah on relaunch
                Console.Write($"\r{spinner} Progress: {p}%  ");

                Console.Write("\nPress ^C or 4 to cancel current job.");
            });

            try
            {
                Job.Status = "Running"; 
                string Result = await Job.DoJob(cancelToken, progress);
                Job.Status = "Completed";
                Job.Progress = 100; 
            }
            catch (Exception ex)
            {
                Job.Status = "Canceled";
                return; 
            }

            finally
            {
                JobStore.UpdateJobsToDisk(Job);
            }

        }

    }
}