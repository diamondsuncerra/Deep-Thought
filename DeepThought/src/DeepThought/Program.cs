using DeepThought.src.DeepThought.Util;

namespace DeepThought
{
    public class Program
    {
        public static void Main(string []args)
        {
            int option = 0;
            while (option != 5)
            {
                ConsoleHelpers.MenuPrompt();
                string? UserInput = Console.ReadLine();
                if (int.TryParse(UserInput, out option))
                {   
                    // do job based on what it is
                    switch (option)
                    {
                        case 1: ConsoleHelpers.DoOption1(); break;
                        case 2: ConsoleHelpers.DoOption2(); break;
                        case 3: ConsoleHelpers.DoOption3(); break;
                        case 4: ConsoleHelpers.DoOption4(); break;
                        case 5: ConsoleHelpers.DoOption5(); break;
                    }

                } else
                { // parsing failed don't throw exception
                    ConsoleHelpers.InvalidOption();
                }
            }
            
        }
    }
}