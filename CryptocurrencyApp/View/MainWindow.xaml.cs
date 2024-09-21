using CryptocurrencyApp.ViewModel;
using System.Windows;

namespace CryptocurrencyApp.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel viewModel = new MainWindowViewModel();
            DataContext = viewModel;
        }
    }
}