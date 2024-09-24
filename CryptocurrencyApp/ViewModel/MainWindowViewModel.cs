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
        private readonly CoinCapApiClient _coinCapApiClient = new CoinCapApiClient();
        private readonly NavigationView _navigationView;
        private ObservableCollection<CryptoData> _cryptoCurrency;
        private ObservableCollection<CryptoData> _filteredCryptoCurrency;
        private object _currentView;
        private string _searchText;
        private bool _isPlaceholderVisible = true;
        private bool _isLoading;
        private string _buttonText = "Refresh";

        public ICommand OpenDetailsWindowCommand { get; set; }
        public ICommand RefreshDataCommand { get; set; }
        public ICommand NavigateToSettingsCommand { get; set; }
        public ICommand NavigateToHomeCommand { get; set; }

        public ObservableCollection<CryptoData> CryptoCurrency
        {
            get => _cryptoCurrency;
            set => SetProperty(ref _cryptoCurrency, value);
        }
        public ObservableCollection <CryptoData> FilteredCryptoCurrency
        {
            get => _filteredCryptoCurrency;
            set => SetProperty(ref _filteredCryptoCurrency, value);
        }
        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                SearchCryptocurrency();
                Console.WriteLine($"Search query changed: {value}");
                IsPlaceholderVisible = string.IsNullOrEmpty(value);
            }
        }

        public bool IsPlaceholderVisible
        {
            get => _isPlaceholderVisible;
            set => SetProperty(ref _isPlaceholderVisible, value);
        }
        public string ButtonText
        {
            get => _buttonText;
            set => SetProperty(ref _buttonText, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }


        public MainWindowViewModel()
        {

            RefreshDataCommand = new DelegateCommand(LoadDataAsync, () => true);
            OpenDetailsWindowCommand = new DelegateCommand<string>(OpenDetailsWindow, _ => true);
            NavigateToSettingsCommand = new DelegateCommand(NavigateToSettings);
           // NavigateToHomeCommand = new DelegateCommand(NavigateToHome);
            CryptoCurrency = new ObservableCollection<CryptoData>();

            LoadDataAsync();
        }
        private void NavigateToSettings()
        {
            
            Application.Current.Dispatcher.Invoke(() =>
            {
                var settingsPage = new SettingsPage();
                var mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.MainFrame.Navigate(settingsPage);
            });
        }
        
        private void SearchCryptocurrency()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                FilteredCryptoCurrency = new ObservableCollection<CryptoData>(CryptoCurrency);
            }
            else
            {
                var filtered = CryptoCurrency.Where( c => c.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) || 
                c.Id.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
                FilteredCryptoCurrency = new ObservableCollection<CryptoData>(filtered);
            }
        }

        private void OpenDetailsWindow(string id)
        {
            var detailsViewModel = new DetailsWindowViewModel(id);
            DetailsWindow detailsWindow = new DetailsWindow(id);
            detailsWindow.Show();
        }

        private async void LoadDataAsync()
        {
            IsLoading = true;

            CryptoCurrency.Clear();

            try
            {
                var result = await _coinCapApiClient.GetCurrenciesAsync();
                CryptoCurrency.AddRange(result);
                FilteredCryptoCurrency = new ObservableCollection<CryptoData>(CryptoCurrency);

                Debug.WriteLine($"Loaded {CryptoCurrency} items.");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
