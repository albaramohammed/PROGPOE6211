using System;
using System.Collections.Generic; // Part 2: Added for using List<T>
using System.Linq; // Part 2: Added for using LINQ methods like OrderBy and Sum
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/*_____________________________________references_______________________________________
* C# documentation: https://docs.microsoft.com/en-us/dotnet/csharp/
* C# programming examples: https://www.dotnetperls.com/
* Console application examples in C#: https://www.csharp-console-examples.com/
* Object-oriented programming in C#: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/object-oriented-programming
* Code Helper: https://chat.openai.com/
*/

namespace PROGPOE
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
            app.Run();
        }
    }
}
        /*
        static void Main(string[] args)
        {
            RecipeApp recipeApp = new RecipeApp();
            recipeApp.CalorieExceeded += RecipeApp_CalorieExceeded; // Part 2: Subscribe to the CalorieExceeded event

            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Black; // Set the background color to black
                Console.ForegroundColor = ConsoleColor.White; // Set the text color to white
                Console.WriteLine("Recipe App Menu:");
                Console.ForegroundColor = ConsoleColor.Yellow; // Set the text color to yellow for menu options
                Console.WriteLine("1. Enter a new recipe");
                Console.WriteLine("2. Display recipe list");
                Console.WriteLine("3. Select a recipe");
                Console.WriteLine("4. Scale recipe");
                Console.WriteLine("5. Reset recipe");
                Console.WriteLine("6. Clear recipe");
                Console.WriteLine("7. Exit");
                Console.ForegroundColor = ConsoleColor.White; // Set the text color back to white
                Console.Write("Enter your choice (1-7): ");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        recipeApp.GetRecipeDetails();
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Cyan; // Set the text color to cyan for displaying recipe list
                        recipeApp.DisplayRecipeList();
                        Console.ForegroundColor = ConsoleColor.White; // Set the text color back to white
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Green; // Set the text color to green for user input
                        Console.Write("Enter the recipe name: ");
                        Console.ForegroundColor = ConsoleColor.White; // Set the text color back to white
                        string recipeName = Console.ReadLine();
                        recipeApp.DisplayRecipe(recipeName);
                        recipeApp.NotifyExceedingCalories(recipeName);
                        break;        
                    case 4:
                        Console.Write("Enter the recipe name: ");
                        recipeName = Console.ReadLine();
                        Console.Write("Enter the scale factor: ");
                        double scaleFactor = double.Parse(Console.ReadLine());
                        recipeApp.ScaleRecipe(recipeName, scaleFactor); // Part 2: Scale a specific recipe by name
                        break;
                    case 5:
                        Console.Write("Enter the recipe name: ");
                        recipeName = Console.ReadLine();
                        recipeApp.ResetRecipe(recipeName); // Part 2: Reset a specific recipe by name
                        break;
                    case 6:
                        Console.Write("Enter the recipe name: ");
                        recipeName = Console.ReadLine();
                        recipeApp.ClearRecipe(recipeName); // Part 2: Clear a specific recipe by name
                        break;
                    case 7:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        // Part 2: Event handler for the CalorieExceeded event
        private static void RecipeApp_CalorieExceeded(object sender, CalorieExceededEventArgs e)
        {
            Console.WriteLine($"Alert: Recipe '{e.RecipeName}' exceeds 300 calories! Total calories: {e.Calories}");
        }
    }
}
        */