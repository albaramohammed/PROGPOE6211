using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Xml.Serialization;

namespace PROGPOE
{
    public class RecipeAppViewModel : INotifyPropertyChanged
    {
        private const string RecipesFilePath = "recipes.xml";

        private ObservableCollection<Recipe> _recipes;
        public ObservableCollection<Recipe> Recipes
        {
            get { return _recipes; }
            set
            {
                _recipes = value;
                OnPropertyChanged(nameof(Recipes));
            }
        }

        private Recipe _selectedRecipe;
        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
                _selectedRecipe = value;
                OnPropertyChanged(nameof(SelectedRecipe));
                UpdateCurrentRecipe();
            }
        }

        private int _currentRecipeIndex = -1;
        public int CurrentRecipeIndex
        {
            get { return _currentRecipeIndex; }
            set
            {
                _currentRecipeIndex = value;
                OnPropertyChanged(nameof(CurrentRecipeIndex));
                UpdateCurrentRecipe();
            }
        }

        private bool isUpdatingCurrentRecipe = false;

        private void UpdateCurrentRecipe()
        {
            if (isUpdatingCurrentRecipe)
                return;

            isUpdatingCurrentRecipe = true;

            if (Recipes.Count > 0 && CurrentRecipeIndex >= 0 && CurrentRecipeIndex < Recipes.Count)
            {
                SelectedRecipe = Recipes[CurrentRecipeIndex];
                NewRecipeName = SelectedRecipe.Name;
                NewRecipeIngredients = new ObservableCollection<Ingredient>(SelectedRecipe.Ingredients);
                NewRecipeSteps = SelectedRecipe.Steps[0];
            }
            else
            {
                SelectedRecipe = null;
                NewRecipeName = string.Empty;
                NewRecipeIngredients.Clear();
                NewRecipeSteps = string.Empty;
            }

            isUpdatingCurrentRecipe = false;
        }


        private string _filterText;
        public string FilterText
        {
            get { return _filterText; }
            set
            {
                _filterText = value;
                OnPropertyChanged(nameof(FilterText));
                ApplyFilter();
            }
        }

       
        private FilterOption _selectedFilterOption;
        public FilterOption SelectedFilterOption
        {
            get { return _selectedFilterOption; }
            set
            {
                _selectedFilterOption = value;
                OnPropertyChanged(nameof(SelectedFilterOption));
                ApplyFilter();
            }
        }

        public ObservableCollection<FilterOption> FilterOptions { get; set; }

        private string _ingredientFilterText;
        public string IngredientFilterText
        {
            get { return _ingredientFilterText; }
            set
            {
                _ingredientFilterText = value;
                OnPropertyChanged(nameof(IngredientFilterText));
                ApplyFilter();
            }
        }

        private string _selectedFoodGroup;
        public string SelectedFoodGroup
        {
            get { return _selectedFoodGroup; }
            set
            {
                _selectedFoodGroup = value;
                OnPropertyChanged(nameof(SelectedFoodGroup));
                ApplyFilter();
            }
        }

        public class FilterOption
        {
            public string Name { get; set; }
            public FilterType FilterType { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }

        private int? _maxCalories;
        public int? MaxCalories
        {
            get { return _maxCalories; }
            set
            {
                _maxCalories = value;
                OnPropertyChanged(nameof(MaxCalories));
                ApplyFilter();
            }
        }

        private string _newRecipeName;
        public string NewRecipeName
        {
            get { return _newRecipeName; }
            set
            {
                _newRecipeName = value;
                OnPropertyChanged(nameof(NewRecipeName));
            }
        }

        private ObservableCollection<Ingredient> _newRecipeIngredients;
        public ObservableCollection<Ingredient> NewRecipeIngredients
        {
            get { return _newRecipeIngredients; }
            set
            {
                _newRecipeIngredients = value;
                OnPropertyChanged(nameof(NewRecipeIngredients));
            }
        }

        private string _newRecipeSteps;
        public string NewRecipeSteps
        {
            get { return _newRecipeSteps; }
            set
            {
                _newRecipeSteps = value;
                OnPropertyChanged(nameof(NewRecipeSteps));
            }
        }

        public ICommand AddRecipeCommand { get; set; }
        public ICommand AddIngredientCommand { get; set; }
        public ICommand SaveRecipeCommand { get; set; }
        public ICommand ScaleRecipeCommand { get; set; }
        public ICommand ResetRecipeCommand { get; set; }
        public ICommand ClearRecipeCommand { get; set; }
        public ICommand PreviousRecipeCommand { get; set; }
        public ICommand NextRecipeCommand { get; set; }
        public ICommand SearchRecipesCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public RecipeAppViewModel()
        {
            Recipes = new ObservableCollection<Recipe>();
            NewRecipeIngredients = new ObservableCollection<Ingredient>();

            FilterOptions = new ObservableCollection<FilterOption>
            {
                new FilterOption { Name = "Name", FilterType = FilterType.Name },
                new FilterOption { Name = "Ingredient", FilterType = FilterType.Ingredient },
                new FilterOption { Name = "Food Group", FilterType = FilterType.FoodGroup }
            };

            AddRecipeCommand = new RelayCommand(AddRecipe);
            AddIngredientCommand = new RelayCommand(AddIngredient);
            SaveRecipeCommand = new RelayCommand(SaveRecipe);
            ScaleRecipeCommand = new RelayCommand(ScaleRecipe);
            ResetRecipeCommand = new RelayCommand(ResetRecipe);
            ClearRecipeCommand = new RelayCommand(ClearRecipe);
            PreviousRecipeCommand = new RelayCommand(PreviousRecipeMethod);
            NextRecipeCommand = new RelayCommand(NextRecipeMethod);
            SearchRecipesCommand = new RelayCommand(SearchRecipesMethod);

            LoadRecipes();
        }

        public void LoadRecipes()
        {
            if (File.Exists(RecipesFilePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Recipe>));
                using (FileStream fs = new FileStream(RecipesFilePath, FileMode.Open))
                {
                    Recipes = (ObservableCollection<Recipe>)serializer.Deserialize(fs);
                }
            }
        }

        public void SaveRecipes()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Recipe>));
            using (FileStream fs = new FileStream(RecipesFilePath, FileMode.Create))
            {
                serializer.Serialize(fs, Recipes);
            }
        }

        private void AddRecipe(object obj)
        {
            if (!string.IsNullOrWhiteSpace(NewRecipeName))
            {
                Recipe newRecipe = new Recipe
                {
                    Name = NewRecipeName,
                    Ingredients = new ObservableCollection<Ingredient>(NewRecipeIngredients),
                    Steps = new ObservableCollection<string> { NewRecipeSteps }
                };

                newRecipe.CalculateCalories();
                Recipes.Add(newRecipe);
                NewRecipeName = string.Empty;
                NewRecipeIngredients.Clear();
                NewRecipeSteps = string.Empty;
            }
        }

        private void AddIngredient(object obj)
        {
            NewRecipeIngredients.Add(new Ingredient());
        }

        private void SaveRecipe(object obj)
        {
            if (SelectedRecipe != null)
            {
                SelectedRecipe.Ingredients = new ObservableCollection<Ingredient>(NewRecipeIngredients);
                SelectedRecipe.Steps = new ObservableCollection<string> { NewRecipeSteps };
                SelectedRecipe.CalculateCalories();
            }
        }

        private void ScaleRecipe(object obj)
        {
            if (SelectedRecipe != null)
            {
                ScaleRecipeWindow scaleWindow = new ScaleRecipeWindow(SelectedRecipe);
                scaleWindow.ShowDialog();
            }
        }

        private void ResetRecipe(object obj)
        {
            if (SelectedRecipe != null)
            {
                SelectedRecipe.ResetIngredientQuantities();
            }
        }

        private void ClearRecipe(object obj)
        {
            if (SelectedRecipe != null)
            {
                Recipes.Remove(SelectedRecipe);
                SelectedRecipe = null;
                CurrentRecipeIndex = -1;
            }
        }

        private void PreviousRecipeMethod(object obj)
        {
            if (CurrentRecipeIndex > 0)
                CurrentRecipeIndex--;
        }

        private void NextRecipeMethod(object obj)
        {
            if (CurrentRecipeIndex < Recipes.Count - 1)
                CurrentRecipeIndex++;
        }

        private void SearchRecipesMethod(object obj)
        {
            // Implement your search logic here
        }

  
        private void ApplyFilter()
        {
            if (string.IsNullOrWhiteSpace(FilterText) && SelectedFilterOption == null &&
                string.IsNullOrWhiteSpace(IngredientFilterText) && string.IsNullOrWhiteSpace(SelectedFoodGroup) && !MaxCalories.HasValue)
            {
                Recipes = new ObservableCollection<Recipe>(_recipes);
            }
            else
            {
                IEnumerable<Recipe> filteredRecipes = _recipes.Where(r => FilterRecipe(r));
                Recipes = new ObservableCollection<Recipe>(filteredRecipes);
            }
        }


        private bool FilterRecipe(Recipe recipe)
        {
            if (string.IsNullOrWhiteSpace(FilterText) && SelectedFilterOption == null &&
                string.IsNullOrWhiteSpace(IngredientFilterText) && string.IsNullOrWhiteSpace(SelectedFoodGroup) && !MaxCalories.HasValue)
                return true;

            bool matchesNameFilter = string.IsNullOrWhiteSpace(FilterText) ||
                recipe.Name.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0;

            bool matchesIngredientFilter = string.IsNullOrWhiteSpace(IngredientFilterText) ||
                recipe.Ingredients.Any(i => i.Name.IndexOf(IngredientFilterText, StringComparison.OrdinalIgnoreCase) >= 0);

            bool matchesFoodGroupFilter = string.IsNullOrWhiteSpace(SelectedFoodGroup) ||
                recipe.FoodGroups.Any(fg => fg.IndexOf(SelectedFoodGroup, StringComparison.OrdinalIgnoreCase) >= 0);

            bool matchesCalorieFilter = !MaxCalories.HasValue || recipe.Calories <= MaxCalories.Value;

            if (SelectedFilterOption == null)
                return matchesNameFilter && matchesIngredientFilter && matchesFoodGroupFilter && matchesCalorieFilter;

            switch (SelectedFilterOption.FilterType)
            {
                case FilterType.Name:
                    return matchesNameFilter && matchesIngredientFilter && matchesFoodGroupFilter && matchesCalorieFilter;
                case FilterType.Ingredient:
                    return recipe.Ingredients.Any(i => i.Name.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0) &&
                           matchesIngredientFilter && matchesFoodGroupFilter && matchesCalorieFilter;
                case FilterType.FoodGroup:
                    return recipe.FoodGroups.Any(fg => fg.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0) &&
                           matchesIngredientFilter && matchesFoodGroupFilter && matchesCalorieFilter;
                default:
                    return matchesNameFilter && matchesIngredientFilter && matchesFoodGroupFilter && matchesCalorieFilter;
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class FilterOption
    {
        public string Name { get; set; }
        public FilterType FilterType { get; set; }
    }

    public enum FilterType
    {
        Name,
        Ingredient,
        FoodGroup
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}