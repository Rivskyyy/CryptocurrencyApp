using CryptocurrencyApp.ViewModel;
using System.Diagnostics;
using System.Windows;

namespace CryptocurrencyApp.View
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


           
            MainWindowViewModel viewModel = new MainWindowViewModel();
        
            this.DataContext = viewModel;
            
        }
    }
}