using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace PROGPOE
{
    public partial class ScaleRecipeWindow : Window
    {
        private Recipe _recipe;

        public ScaleRecipeWindow(Recipe recipe)
        {
            InitializeComponent();
            _recipe = recipe;
        }

        private void ScaleButton_Click(object sender, RoutedEventArgs e)
        {
            double scaleFactor = scaleSlider.Value;
            for (int i = 0; i < _recipe.Ingredients.Count; i++)
            {
                _recipe.Ingredients[i].Quantity *= scaleFactor;
            }
            _recipe.CalculateCalories();
            Close();
        }
    }
}