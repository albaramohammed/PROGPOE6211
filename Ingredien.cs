using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROGPOE
{
    //_________________________________________PART1________________________________________
    /// Represents an ingredient in a recipe, including its name, quantity, and unit of measurement.
    public class Ingredient
    {
        /// The name of the ingredient.
        public string Name { get; set; }
        /// The quantity of the ingredient.
        public double Quantity { get; set; }
        /// The unit of measurement for the ingredient.
        public string Unit { get; set; }
    


/*_____________________________________references_______________________________________
* C# documentation: https://docs.microsoft.com/en-us/dotnet/csharp/
* C# programming examples: https://www.dotnetperls.com/
* Console application examples in C#: https://www.csharp-console-examples.com/
* Object-oriented programming in C#: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/object-oriented-programming
* Code Helper: https://chat.openai.com/
*/
//_________________________________________PART1________________________________________

//_________________________________________Part 2_______________________________________
public int Calories { get; set; }
public string FoodGroup { get; set; }
    }
}