using CryptocurrencyApp.ViewModel;
using System.Diagnostics;
using System.Windows;
using Wpf.Ui.Controls;

namespace CryptocurrencyApp.View
{
    public partial class MainWindow : FluentWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel viewModel = new MainWindowViewModel(MainFrame);
            DataContext = viewModel;
        }
    }
}