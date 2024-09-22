using CryptocurrencyApp.APIs;
using CryptocurrencyApp.Model;
using System.Diagnostics;
using System.Net.Http;
using System.Windows.Input;

namespace CryptocurrencyApp.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        private readonly CoinCapApiClient _coinCapApiClient = new CoinCapApiClient();
        private readonly HttpClient _httpClient;
        public Command AddCommand { get; }
        public ICommand RefreshDataCommand { get; set; }
        private List<CryptoData> _cryptoCurrency;
        public List<CryptoData> CryptoCurrency
        {
            get => _cryptoCurrency;
            set
            {
                _cryptoCurrency = value;
                OnPropertyChanged(nameof(CryptoCurrency));
            }
        }

        private string _buttonText = "Refresh";
        public string ButtonText
        {
            get => _buttonText;

            set
            {
                _buttonText = value;
                OnPropertyChanged(nameof(ButtonText));
                Debug.WriteLine($"ButtonText set to: {_buttonText}");
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
                //ButtonText = _isLoading ? "Refreshing..." : "Refresh";
            }
        }


        public MainWindowViewModel()
        {
            RefreshDataCommand = new Command(LoadDataAsync, () => !IsLoading);
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            IsLoading = true;
            //ButtonText = "Refreshing...";
            try
            {
                var result = await _coinCapApiClient.GetCurrenciesAsync();
                CryptoCurrency = result;
                Debug.WriteLine($"Loaded {CryptoCurrency} items.");
            }
            finally
            {
                IsLoading = false;
                //ButtonText = "Refresh";
            }
        }
    }
}
