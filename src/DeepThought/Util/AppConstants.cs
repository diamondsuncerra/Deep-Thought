namespace DeepThought.src.DeepThought.Util
{
    public static class AppConstants
    {
        public static class Menu
        {
            public const int SubmitQuestion = 1;
            public const int ListJobs = 2;
            public const int ViewResultByJobId = 3;

            public const int RelaunchLastIncompleteJob = 4;
            public const int Exit = 5;
        }

        public static class Limits
        {
            public const int MaxQuestionLength = 200;
        }

        public static class Algorithms
        {
            public const string Trivial = "Trivial";
            public const string RandomGuess = "RandomGuess";
            public const string SlowCount = "SlowCount";

   
            public static readonly string[] All = { Trivial, RandomGuess, SlowCount };
        }

        public static class Status
        {
            public const string Pending = "Pending";

            public const string Completed = "Completed";
            public const string Canceled = "Canceled";
        }

        public static class Warnings
        {
            public const string InvalidQuestion = "This question is too long or empty. Please retry.";
            public const string InvalidAlgorithm = "Unfortunately, that algorithm is not currently supported by Deep Thought. Try one of the provided options.";
            public const string JobSavedFailed = "Failed to save job.";
            public const string InvalidJobId = "The JobId you provided is invalid. Try again.";
            public const string GeneralFail = "Something went wrong!";

        }
        
        
        public static class Messages
        {
            public const string InvalidOption = "This option is not valid.";
            public const string EnterJobId = "Please submit the JobId:";
            public const string EnterQuestion = "Please submit your Ultimate Question (max 200 chars):";
            public const string EnterAlgorithm = "Which algorithm should Deep Thought use? Trivial | RandomGuess | SlowCount";

            public const string NoUnfinishedJob = "There is no unfinished job, unfortunately...";
            public const string Goodbye = "Thank you for using Deep Thought. 42 ms until termination.";
            public const string ControlCDetected = "\nCtrl+C detected — cancelling the job...";
        }
    }
}
