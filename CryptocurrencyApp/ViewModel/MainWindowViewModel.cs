using CryptocurrencyApp.APIs;
using CryptocurrencyApp.Model;
using System.Diagnostics;
using System.Net.Http;
using System.Windows.Input;
using CryptocurrencyApp.View;

namespace CryptocurrencyApp.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        private readonly CoinCapApiClient _coinCapApiClient = new CoinCapApiClient();
        private readonly HttpClient _httpClient;
        public ICommand OpenDetailsWindowCommand { get; set; }
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
            }
        }

        public MainWindowViewModel()
        {
            RefreshDataCommand = new Command(LoadDataAsync, () => !IsLoading);
            OpenDetailsWindowCommand = new Command(OpenDetailsWindow, () => true);

            LoadDataAsync();
            

        }
        private void OpenDetailsWindow(string id)
        {
            Debug.WriteLine("OpenDetailsWindow command executed");
            DetailsWindow detailsWindow = new DetailsWindow();
            detailsWindow.Show();

        }

        private async Task LoadDataAsync()
        {
            IsLoading = true;
          
            if (IsLoading== true && CryptoCurrency != null)
            {
                CryptoCurrency = null;                              
                OnPropertyChanged(nameof(CryptoCurrency));          
            }
            
            try
            {
                var result = await _coinCapApiClient.GetCurrenciesAsync();
                CryptoCurrency = result;

                Debug.WriteLine($"Loaded {CryptoCurrency} items.");
            }
            finally
            {
                IsLoading = false;
                
            }
        }
    }
}
