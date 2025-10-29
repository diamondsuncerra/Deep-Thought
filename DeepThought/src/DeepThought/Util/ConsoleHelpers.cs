using DeepThought.src.DeepThought.Domain;
using DeepThought.src.DeepThought.Services;

namespace DeepThought.src.DeepThought.Util
{
    public static class ConsoleHelpers
    {
        private const int MaxQuestionLength = 200;
        public static void MenuPrompt()
        {
            Console.WriteLine("Please select an option:");
            Console.WriteLine($"({AppConstants.Menu.SubmitQuestion}) Submit Question");
            Console.WriteLine($"({AppConstants.Menu.ListJobs}) List Jobs");
            Console.WriteLine($"({AppConstants.Menu.ViewResultByJobId}) View Result by JobId");
            Console.WriteLine($"({AppConstants.Menu.RelaunchLastIncompleteJob}) Relaunch Last Incomplete Job");
            Console.WriteLine($"({AppConstants.Menu.Exit}) Exit");
        }

        public static bool CheckQuestionText(string? questionText)
        {
            //return questionText != null && questionText.Length <= MaxQuestionLength; 
            if (string.IsNullOrWhiteSpace(questionText)) return false;
            // IsNullOrWhiteSpace for null, empty, spaceonly
            return questionText.Length <= MaxQuestionLength;
        }
        public static bool CheckAlgorithmKey(string? algorithmKey)
        {
            if (string.IsNullOrWhiteSpace(algorithmKey)) return false;
           
            var key = algorithmKey.Trim();

            // Compare against AppConstants, case-insensitively
            return key.Equals(AppConstants.Algorithms.Trivial, StringComparison.OrdinalIgnoreCase)
                || key.Equals(AppConstants.Algorithms.SlowCount, StringComparison.OrdinalIgnoreCase)
                || key.Equals(AppConstants.Algorithms.RandomGuess, StringComparison.OrdinalIgnoreCase);
        }
        public static void InvalidOption()
        {
            ShowWarning(AppConstants.Messages.InvalidOption);
        }


        private static string? ReadQuestion()
        {
            Console.WriteLine(AppConstants.Messages.EnterQuestion);
            string? questionText = Console.ReadLine();

            if (!CheckQuestionText(questionText))
            {
                ShowWarning(AppConstants.Warnings.InvalidQuestion);
                return null;
            }
            return questionText;
        }
        
        private static string? ReadAlgorithm()
        {
            Console.WriteLine(AppConstants.Messages.EnterAlgorithm);
            string? algorithmKey = Console.ReadLine();
            if (!CheckAlgorithmKey(algorithmKey))
            {
                ShowWarning(AppConstants.Warnings.InvalidAlgorithm);
                return null;
            }
            return algorithmKey;
        }
        public static async Task SubmitQuestion()
        {   
            string? questionText = ReadQuestion();
            string? algorithmKey = ReadAlgorithm();

            Guid jobId = Guid.NewGuid();
            Console.WriteLine("Job queued: " + jobId);

            Job job = new(jobId.ToString(), questionText, algorithmKey, AppConstants.Status.Pending, 0);
            try
            {
                JobStore.UpdateJobsToDisk(job);
            }
            catch (Exception ex)
            {
                ShowWarning(AppConstants.Warnings.JobSavedFailed);
                Log(ex);
                return;
            }

            await RunJob(job);
        }

        public static void ListAllJobs()
        {
            JobStore.ListAllJobs();
        }
        public static void PrintResultByJobId()
        {
            Console.WriteLine(AppConstants.Messages.EnterJobId);
            string? jobIdString = Console.ReadLine();
            if (jobIdString != null)
            {
                Console.WriteLine(JobStore.GetResultByJobId(jobIdString));
            }
        }

        public static void ExitApplication()
        {
            Console.WriteLine(AppConstants.Messages.Goodbye);
            Thread.Sleep(42);
        }
        public async static Task RelaunchLastUnfinishedJob()
        {
            // Resume last unfinished job
            JobStore.GetFirstUnfinishedJob(out Job? Job);
            if (Job == null)
            {
                Console.WriteLine(AppConstants.Messages.NoUnfinishedJob);
            }
            else await RunJob(Job);
        }

        public async static Task RunJob(Job Job)
        {
            // To avoid Object Disposed Exception using a handler            
            using var cts = new CancellationTokenSource();

            ConsoleCancelEventHandler handler = (sender, e) =>
            {
                Console.WriteLine(AppConstants.Messages.ControlCDetected);
                e.Cancel = true;
                if (!cts.IsCancellationRequested) cts.Cancel();
            };

            Console.CancelKeyPress += handler;

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
            try
            {
                await JobRunner.RunJob(Job, cts.Token);
            }
            finally
            {
                Console.CancelKeyPress -= handler;
            }
        }

        internal static void ShowWarning(string v)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[Warning] {v}");
            Console.ResetColor();
        }

        internal static void Log(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"[Error] {ex.GetType().Name}: {ex.Message}");
            if (ex.StackTrace is not null) Console.Error.WriteLine(ex.StackTrace);
            Console.ResetColor();
        }
    }


}