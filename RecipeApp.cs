using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PROGPOE
{
    public class RecipeApp
    {
        // Part 2: Changed from a single recipe to a list of recipes
        private List<Recipe> _recipes = new List<Recipe>();

        // Part 2: Added delegate and event for calorie notification
        public delegate void CalorieExceededEventHandler(object sender, CalorieExceededEventArgs e);
        public event CalorieExceededEventHandler CalorieExceeded;
        public void GetRecipeDetails()
        {
            Recipe newRecipe = new Recipe();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter the recipe name: ");
            Console.ResetColor();
            newRecipe.Name = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter the number of ingredients: ");
            Console.ResetColor();
            int numIngredients = int.Parse(Console.ReadLine());

            // Part 2: Changed from arrays to generic collections
            newRecipe.Ingredients = new List<Ingredient>();
            newRecipe.OriginalIngredientQuantities = new List<double>();
            // Part 2: Added food groups list
            newRecipe.FoodGroups = new List<string>();

            for (int i = 0; i < numIngredients; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Ingredient {i + 1}:");

                Console.Write("  Name: ");
                string name = Console.ReadLine();

                Console.Write("  Quantity: ");
                double quantity = double.Parse(Console.ReadLine());

                Console.Write("  Unit: ");
                string unit = Console.ReadLine();

                // Part 2: Added calories input
                Console.Write("  Calories: ");
                int calories = int.Parse(Console.ReadLine());

                // Part 2: Added food group input
                Console.Write("  Food Group: ");
                string foodGroup = Console.ReadLine();

                newRecipe.Ingredients.Add(new Ingredient
                {
                    Name = name,
                    Quantity = quantity,
                    Unit = unit,
                    // Part 2: Added calories and food group properties
                    Calories = calories,
                    FoodGroup = foodGroup
                });

                newRecipe.OriginalIngredientQuantities.Add(quantity);
                // Part 2: Added food group to the list
                newRecipe.FoodGroups.Add(foodGroup);
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter the number of steps: ");
            Console.ResetColor();
            int numSteps = int.Parse(Console.ReadLine());

            // Part 2: Changed from array to generic collection
            newRecipe.Steps = new List<string>();

            // Part 2: Modified to handle multi-line step
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Enter the steps (press Enter twice to finish):");
            Console.ResetColor();
            StringBuilder stepBuilder = new StringBuilder();
            string line;
            while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
            {
                stepBuilder.AppendLine(line);
            }
            string stepDescription = stepBuilder.ToString().TrimEnd();
            newRecipe.Steps.Add(stepDescription);

            // Part 2: Calculate total calories of the recipe
            newRecipe.Calories = newRecipe.Ingredients.Sum(i => i.Calories);

            // Part 2: Add the new recipe to the list of recipes
            _recipes.Add(newRecipe);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Recipe added successfully!");
            Console.ResetColor();
        }

        // Part 2: Added method to display the list of recipes
        public void DisplayRecipeList()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Recipe List:");
            Console.ResetColor();
            foreach (var recipe in _recipes.OrderBy(r => r.Name))
            {
                Console.WriteLine($"- {recipe.Name}");
            }
            Console.WriteLine();
        }

        // Part 2: Updated to display a specific recipe by name
        public void DisplayRecipe(string recipeName)
        {
            Recipe recipe = _recipes.Find(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

            if (recipe != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Recipe: {recipe.Name}");
                Console.ForegroundColor = ConsoleColor.Cyan;
                // Part 2: Display total calories and food groups
                Console.WriteLine($"Total Calories: {recipe.Calories}");
                Console.WriteLine("Food Groups:");
                foreach (var foodGroup in recipe.FoodGroups.Distinct())
                {
                    Console.WriteLine($"- {foodGroup}");
                }
                Console.WriteLine("Ingredients:");
                foreach (var ingredient in recipe.Ingredients)
                {
                    Console.WriteLine($"- {ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}");
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Steps:");
                Console.ResetColor();
                // Part 2: Modified to display multi-line step descriptions
                Console.WriteLine(recipe.Steps[0]);
                Console.WriteLine();
            }
            else
            {
                Console.ResetColor();
                Console.WriteLine($"Recipe '{recipeName}' not found.");
                Console.ResetColor();
            }
        }

        // Part 2: Updated to scale a specific recipe by name
        public void ScaleRecipe(string recipeName, double factor)
        {
            Recipe recipe = _recipes.Find(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

            if (recipe != null)
            {
                Console.ResetColor();
                Console.WriteLine($"Scaling recipe '{recipe.Name}' by a factor of {factor}");
                Console.ResetColor();
                for (int i = 0; i < recipe.Ingredients.Count; i++)
                {
                    recipe.Ingredients[i].Quantity *= factor;
                }
            }
            else
            {
                Console.ResetColor();
                Console.WriteLine($"Recipe '{recipeName}' not found.");
                Console.ResetColor();
            }
        }
        // Part 2: Updated to reset a specific recipe by name
        public void ResetRecipe(string recipeName)
        {
            Recipe recipe = _recipes.Find(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

            if (recipe != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Resetting recipe '{recipe.Name}' to original quantities...");
                Console.ResetColor();
                for (int i = 0; i < recipe.Ingredients.Count; i++)
                {
                    recipe.Ingredients[i].Quantity = recipe.OriginalIngredientQuantities[i];
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Recipe '{recipeName}' not found.");
                Console.ResetColor();
            }
        }
        // Part 2: Updated to clear a specific recipe by name
        public void ClearRecipe(string recipeName)
        {
            Recipe recipe = _recipes.Find(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

            if (recipe != null)
            {
                _recipes.Remove(recipe);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Recipe '{recipeName}' cleared.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Recipe '{recipeName}' not found.");
                Console.ResetColor();
            }
        }

        // Part 2: Added method to raise the CalorieExceeded event
        protected virtual void OnCalorieExceeded(string recipeName, int calories)
        {
            CalorieExceeded?.Invoke(this, new CalorieExceededEventArgs { RecipeName = recipeName, Calories = calories });
        }

        // Part 2: Added method to notify when a recipe exceeds 300 calories
        public void NotifyExceedingCalories(string recipeName)
        {
            Recipe recipe = _recipes.Find(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

            if (recipe != null && recipe.Calories > 300)
            {
                OnCalorieExceeded(recipe.Name, recipe.Calories);
            }
        }
    }
}