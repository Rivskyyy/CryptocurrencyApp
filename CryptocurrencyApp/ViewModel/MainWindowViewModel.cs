using CryptocurrencyApp.APIs;
using CryptocurrencyApp.Model;
using System.Diagnostics;
using System.Net.Http;
using System.Windows.Input;
using CryptocurrencyApp.View;
using Prism.Mvvm;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Configuration;
using Wpf.Ui.Controls;
using System.Windows;

namespace CryptocurrencyApp.ViewModel
{
    class MainWindowViewModel : BindableBase
    {
        private readonly NavigationView _navigationView;
        private object _currentView;
        public ICommand NavigateToSettingsCommand { get; set; }
        public ICommand NavigateToHomeCommand { get; set; }

        public MainWindowViewModel()
        {
            NavigateToSettingsCommand = new DelegateCommand(NavigateToSettings);
            NavigateToHomeCommand = new DelegateCommand(NavigateToHome);
            NavigateToHome();
        }

        private void NavigateToHome()
        {
                var homePage = new HomePage();
                var mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.MainFrame.Navigate(homePage);
        }

        private void NavigateToSettings()
        {
                var settingsPage = new SettingsPage();
                var mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.MainFrame.Navigate(settingsPage);
        }
    }
}
