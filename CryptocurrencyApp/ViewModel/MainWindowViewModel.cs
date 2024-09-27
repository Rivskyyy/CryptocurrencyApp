using CryptocurrencyApp.View;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows;
using System.Windows.Input;
using Wpf.Ui.Controls;

namespace CryptocurrencyApp.ViewModel
{
    class MainWindowViewModel : BindableBase
    {
        private readonly HomePage _homePage;
        private readonly SettingsPage _settingsPage;
        private readonly NavigationView _navigationView;
        private object _currentView;
     
        public ICommand NavigateToSettingsCommand { get; set; }
        public ICommand NavigateToHomeCommand { get; set; }

        public MainWindowViewModel()
        {
            _homePage = new HomePage();
            _settingsPage = new SettingsPage();

            NavigateToSettingsCommand = new DelegateCommand(NavigateToSettings);
            NavigateToHomeCommand = new DelegateCommand(NavigateToHome);
            NavigateToHome();
        }

        private void NavigateToHome()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.Navigate(_homePage);
        }

        private void NavigateToSettings()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.Navigate(_settingsPage);
        }
    }
}
