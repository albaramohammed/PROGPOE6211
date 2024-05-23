using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PROGPOE
{
    public class Ingredient : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        private double _quantity;
        public double Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        private string _unit;
        public string Unit
        {
            get { return _unit; }
            set
            {
                _unit = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayText));
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

        private string _foodGroup;
        public string FoodGroup
        {
            get { return _foodGroup; }
            set
            {
                _foodGroup = value;
                OnPropertyChanged();
            }
        }

        public string DisplayText => $"{Quantity} {Unit} of {Name}";

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}