using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROGPOE
//_________________________________________Part 2 _______________________________________________
{
    public class
        CalorieExceededEventArgs : EventArgs
    {
        public string RecipeName { get; set; }
        public int Calories { get; set; }
    }
}
