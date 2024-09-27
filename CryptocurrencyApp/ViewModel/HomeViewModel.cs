using CryptocurrencyApp.APIs;
using CryptocurrencyApp.Model;
using CryptocurrencyApp.View;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace CryptocurrencyApp.ViewModel
{
    class HomeViewModel : BindableBase
    {
        private readonly CoinCapApiClient _coinCapApiClient = new CoinCapApiClient();
        private ObservableCollection<CryptoData> _cryptoCurrency;
        private ObservableCollection<CryptoData> _filteredCryptoCurrency;
        private string _searchText;
        private bool _isLoading;

        public ICommand OpenDetailsWindowCommand { get; set; }
        public ICommand RefreshDataCommand { get; set; }
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                SearchCryptocurrency();
                Console.WriteLine($"Search query changed: {value}");
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public ObservableCollection<CryptoData> CryptoCurrency
        {
            get => _cryptoCurrency;
            set => SetProperty(ref _cryptoCurrency, value);
        }

        public ObservableCollection<CryptoData> FilteredCryptoCurrency
        {
            get => _filteredCryptoCurrency;
            set => SetProperty(ref _filteredCryptoCurrency, value);
        }

        public HomeViewModel()
        {
            RefreshDataCommand = new DelegateCommand(LoadDataAsync);
            OpenDetailsWindowCommand = new DelegateCommand<string>(NavigateToDetails);
            CryptoCurrency = new ObservableCollection<CryptoData>();
            FilteredCryptoCurrency = new ObservableCollection<CryptoData>();
            LoadDataAsync();
           
        }
        private void SearchCryptocurrency()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                FilteredCryptoCurrency = new ObservableCollection<CryptoData>(CryptoCurrency);
            }
            else
            {
                var filtered = CryptoCurrency.Where(c => c.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)
                    || c.Id.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
                FilteredCryptoCurrency = new ObservableCollection<CryptoData>(filtered);
            }
        }

        private void NavigateToDetails(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var detailsPage = new DetailsPage(id);
                var mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.MainFrame.Navigate(detailsPage);
            }
        }

        private async void LoadDataAsync()
        {
            IsLoading = true;

            FilteredCryptoCurrency.Clear();
            CryptoCurrency.Clear();

            try
            {
                var result = await _coinCapApiClient.GetCurrenciesAsync();
                CryptoCurrency.AddRange(result);
                FilteredCryptoCurrency = new ObservableCollection<CryptoData>(CryptoCurrency);

                Debug.WriteLine($"Loaded {CryptoCurrency} items.");
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not load data.", $"Error:{e}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
