using CurrencyAppTest.ViewModel;
using System.Diagnostics;
using System.Windows;

namespace CurrencyAppTest.View
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