using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading.Tasks;
using DeepThought.src.DeepThought.Domain;
using DeepThought.src.DeepThought.Services;

namespace DeepThought.src.DeepThought.Util
{
    public class ConsoleHelpers
    {
        private static int _jobs = 0;
        public static void MenuPrompt()
        {
            Console.WriteLine("Please select an option:");
            Console.WriteLine("(1) Submit Question");
            Console.WriteLine("(2) List Jobs");
            Console.WriteLine("(3) View Result by JobId");
            Console.WriteLine("(4) Cancel Running Job");
            Console.WriteLine("(5) Relaunch Last Incomplete Job");
            Console.WriteLine("(6) Exit");
        }

        public static bool CheckQuestionText(string? QuestionText)
        {
            return QuestionText != null && QuestionText.Length <= 200;
        }
        public static bool CheckAlgorithmKey(string? AlgorithmKey)
        {
            return AlgorithmKey != null && (AlgorithmKey.Equals("Trivial") || AlgorithmKey.Equals("SlowCount") ||
            AlgorithmKey.Equals("RandomGuess"));
        }
        public static void InvalidOption()
        {
            Console.WriteLine("The value is not valid. Please try again.");
        }


        public static async Task SubmitQuestion()
        {   // TODO: Separate in methods for more SOLID aproach
            Console.WriteLine("Please submit your Ultimate Questions for which we definitely have an answer.");
            string? QuestionText = Console.ReadLine();

            if (!CheckQuestionText(QuestionText))
            {
                Console.WriteLine("Unfortunately, question is not suitable. Try shorter.. or maybe something at all.");
                return;
            }

            Console.WriteLine("Which algorithm should Deep Thought use?");
            Console.WriteLine("Trivial | RandomGuess | SlowCount");

            string? AlgorithmKey = Console.ReadLine();
            if (!CheckAlgorithmKey(AlgorithmKey))
            {
                Console.WriteLine("Unfortunately, that algorithm is not currently supported by Deep Thought. Try one of the provided options.");
                return;
            }

            Guid JobId = Guid.NewGuid();
            Console.WriteLine("Job queued: " + JobId);

            Job Job = new(JobId.ToString(), QuestionText, AlgorithmKey, "Pending", 0);
            JobStore.UpdateJobsToDisk(Job); // job created 

            await RunJob(Job);
        }

        public static void ListAllJobs()
        {
            JobStore.ListAllJobs();
        }
        public static void PrintResultByJobId()
        {
            Console.WriteLine("Please submit the JobId.");
            string? JobIdString = Console.ReadLine();
            if (JobIdString != null)
            {
                Console.WriteLine(JobStore.GetResultByJobId(JobIdString));
            }
        }
        public static void DoOption4()
        {
            Console.WriteLine("You've already found the answer, haven't you?");
            Console.WriteLine("Forever deleting the Ultimate Job..");
        }
        public static void ExitApplication()
        {
            Console.WriteLine("Thank you for using Deep Thought. 42 ms until termination.");
            Thread.Sleep(42);
        }
        public async static Task RelaunchLastUnfinishedJob()
        {
            // Resume last unfinished job
            JobStore.GetFirstUnfinishedJob(out Job? Job);
            if (Job == null)
            {
                Console.WriteLine("There is no unfinished job, unfortunately...");
            }
            else await RunJob(Job);
        }

        public async static Task RunJob(Job Job)
        {
            using var cts = new CancellationTokenSource();

            Console.CancelKeyPress += (sender, e) =>
            {
                Console.WriteLine("\nCtrl+C detected — cancelling the job...");
                e.Cancel = true;
                cts.Cancel();
            };

            // Launch a background process to listen for 4 and cancel job similar to ^c
            _ = Task.Run(() =>
            {
                while (!cts.IsCancellationRequested)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true).Key;
                        if (key == ConsoleKey.D4 || key == ConsoleKey.NumPad4)
                            cts.Cancel();
                    }
                    Thread.Sleep(100);
                }
            });

            await JobRunner.RunJob(Job, cts.Token);
        }
   
    }


}