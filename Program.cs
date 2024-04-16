using System;
using System.Collections.Generic; // Part 2: Added for using List<T>
using System.Linq; // Part 2: Added for using LINQ methods like OrderBy and Sum

using System.Text;
using System.Threading.Tasks;

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
        static void Main(string[] args)
        {
            RecipeApp recipeApp = new RecipeApp();
            recipeApp.CalorieExceeded += RecipeApp_CalorieExceeded; // Part 2: Subscribe to the CalorieExceeded event

            while (true)
            {
                Console.WriteLine("Recipe App Menu:");
                Console.WriteLine("1. Enter a new recipe");
                Console.WriteLine("2. Display recipe list"); // Part 2: Added option to display recipe list
                Console.WriteLine("3. Select a recipe"); // Part 2: Added option to select a recipe
                Console.WriteLine("4. Scale recipe");
                Console.WriteLine("5. Reset recipe");
                Console.WriteLine("6. Clear recipe");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice (1-7): ");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        recipeApp.GetRecipeDetails();
                        break;
                    case 2:
                        recipeApp.DisplayRecipeList(); // Part 2: Display the list of recipes
                        break;
                    case 3:
                        Console.Write("Enter the recipe name: ");
                        string recipeName = Console.ReadLine();
                        recipeApp.DisplayRecipe(recipeName); // Part 2: Display a specific recipe by name
                        recipeApp.NotifyExceedingCalories(recipeName); // Part 2: Notify if the recipe exceeds 300 calories
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