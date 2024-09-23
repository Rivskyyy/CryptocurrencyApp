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
            MainWindowViewModel viewModel = new MainWindowViewModel();
            DataContext = viewModel;
        }

        private void btnOpenDetails_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button Open Clicked");
        }
    }
}