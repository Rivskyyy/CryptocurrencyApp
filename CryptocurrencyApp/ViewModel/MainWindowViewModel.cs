using CryptocurrencyApp.APIs;
using CryptocurrencyApp.Model;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

namespace CryptocurrencyApp.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        private readonly CoinCapApiClient _coinCapApiClient = new CoinCapApiClient();
        private readonly HttpClient _httpClient;
        private List<CryptoData> _cryptoCurrency;
        private bool _isLoading = false;
        private string _buttonText = "Refresh";

        public Command AddCommand { get; }
        public List<CryptoData> CryptoCurrency
        {
            get => _cryptoCurrency;
            set
            {
                _cryptoCurrency = value;
                OnPropertyChanged(nameof(CryptoCurrency));
            }
        }

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

        public ICommand RefreshDataCommand { get; set; }

        public MainWindowViewModel()
        {
            RefreshDataCommand = new Command(LoadDataAsync, () => !IsLoading);
        }

        private async Task LoadDataAsync()
        {
            IsLoading = true;
            //ButtonText = "Refreshing...";
            try
            {
                var result = await _coinCapApiClient.GetCurrenciesAsync();
                CryptoCurrency = result;
            }
            finally
            {
                IsLoading = false;
                //ButtonText = "Refresh";
            }
        }

        private async Task LoadCryptoCurrencyAsync()
        {
            Debug.WriteLine("Starting to load cryptocurrency data...");
            try
            {

                string url = "https://api.coincap.io/v2/assets";
                var response = await _httpClient.GetStringAsync(url);

                Debug.WriteLine("Data loaded successfully.");

                var cryptoResponse = JsonConvert.
                    DeserializeObject<CryptoResponse>(response);

                CryptoCurrency = cryptoResponse.Data;



                Debug.WriteLine($"Loaded Data:{cryptoResponse.Data}");


            }

            catch (HttpRequestException e)
            {

                MessageBox.Show("Could not load data.", $"Error:{e}", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
