using System.Windows;
using System.Windows.Input;
using PROGPOE;

namespace PROGPOE
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new RecipeAppViewModel();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = (RecipeAppViewModel)DataContext;
            viewModel.LoadRecipes();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var viewModel = (RecipeAppViewModel)DataContext;
            viewModel.SaveRecipes();
        }
    }
}