using PROGPOE;
using System;
using System.Collections.Generic;
using System.Linq;
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
    //_________________________________________PART1________________________________________
    /// Represents a recipe, including its name, ingredients, and steps.
    public class Recipe
    {
        /// The name of the recipe.
        public string Name { get; set; }
        /// The ingredients for the recipe.
        public List<Ingredient> Ingredients { get; set; }
        /// The steps to prepare the recipe.
        public List<string> Steps { get; set; }
        /// The original quantities of the ingredients.
        public List<double> OriginalIngredientQuantities { get; set; }

        //_________________________________________PART1________________________________________

        //_________________________________________Part 2 ______________________________________

        public int Calories { get; set; }
        public List<string> FoodGroups { get; set; }
    }
}