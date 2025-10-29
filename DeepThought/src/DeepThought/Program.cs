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
            while (option != 6)
            {
                ConsoleHelpers.MenuPrompt();
                string? UserInput = Console.ReadLine();
                if (int.TryParse(UserInput, out option))
                {   
                    // do job based on what it is
                    switch (option)
                    {
                        case 1: await ConsoleHelpers.SubmitQuestion(); break;
                        case 2: ConsoleHelpers.ListAllJobs(); break;
                        case 3: ConsoleHelpers.PrintResultByJobId(); break;
                        case 4: ConsoleHelpers.DoOption4(); break;
                        case 5: await ConsoleHelpers.RelaunchLastUnfinishedJob(); break;
                        case 6: { ConsoleHelpers.ExitApplication(); return; }
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