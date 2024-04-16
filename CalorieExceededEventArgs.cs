using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROGPOE
//__________________________________________Part 2 _____________________________________________
{
    public class
        CalorieExceededEventArgs : EventArgs
    {
        public string RecipeName { get; set; }
        public int Calories { get; set; }
    }
}
