using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PROGPOE
{
    public class Recipe : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Ingredient> _ingredients;
        public ObservableCollection<Ingredient> Ingredients
        {
            get { return _ingredients; }
            set
            {
                _ingredients = value;
                OnPropertyChanged();
                CalculateCalories();
            }
        }

        private ObservableCollection<string> _steps;
        public ObservableCollection<string> Steps
        {
            get { return _steps; }
            set
            {
                _steps = value;
                OnPropertyChanged();
            }
        }

        private int _calories;
        public int Calories
        {
            get { return _calories; }
            set
            {
                _calories = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _foodGroups;
        public ObservableCollection<string> FoodGroups
        {
            get { return _foodGroups; }
            set
            {
                _foodGroups = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<double> _originalIngredientQuantities;
        public ObservableCollection<double> OriginalIngredientQuantities
        {
            get { return _originalIngredientQuantities; }
            set
            {
                _originalIngredientQuantities = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Recipe()
        {
            Ingredients = new ObservableCollection<Ingredient>();
            Steps = new ObservableCollection<string>();
            FoodGroups = new ObservableCollection<string>();
            OriginalIngredientQuantities = new ObservableCollection<double>();
        }

        public void CalculateCalories()
        {
            Calories = Ingredients.Sum(i => i.Calories);
        }

        public void ResetIngredientQuantities()
        {
            for (int i = 0; i < Ingredients.Count; i++)
            {
                Ingredients[i].Quantity = OriginalIngredientQuantities[i];
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}