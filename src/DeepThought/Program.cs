using DeepThought.src.DeepThought.Services;
using DeepThought.src.DeepThought.Util;


namespace DeepThought
{ 

    // When i print jobs that have been loaded status =null progress = 0
    public class Program
    {
        // it should load existing jobs from deepthought-jobs.json
        public static async Task Main(string []args)
        {
            try
            {
                JobStore.Load();
            } catch (Exception ex)
            {
                ConsoleHelpers.ShowWarning("Failed to load saved jobs.");
                ConsoleHelpers.Log(ex);
            }
            
            int option = 0;
            while (option != AppConstants.Menu.Exit)
            {
                ConsoleHelpers.MenuPrompt();
                string? userInput = Console.ReadLine();
                if (int.TryParse(userInput, out option))
                {   
                    // do job based on what it is
                    switch (option)
                    {
                        case AppConstants.Menu.SubmitQuestion: await ConsoleHelpers.SubmitQuestion(); break;
                        case AppConstants.Menu.ListJobs: ConsoleHelpers.ListAllJobs(); break;
                        case AppConstants.Menu.ViewResultByJobId: ConsoleHelpers.PrintResultByJobId(); break;
                        case AppConstants.Menu.RelaunchLastIncompleteJob: await ConsoleHelpers.RelaunchLastUnfinishedJob(); break;
                        case AppConstants.Menu.Exit: { ConsoleHelpers.ExitApplication(); return; }
                        default: ConsoleHelpers.InvalidOption(); break;
                    }

                } else
                { // parsing failed don't throw exception
                    ConsoleHelpers.InvalidOption();
                }
            }
            
        }
    }
}