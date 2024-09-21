using CryptocurrencyApp.Model;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Windows;

namespace CryptocurrencyApp.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        private readonly HttpClient _httpClient;              // Ініціалізація http-клієнта

        public List<CryptoData> _cryptoCurrency;
        
        public List<CryptoData> CryptoCurrency { get => _cryptoCurrency;

            set
            {
                _cryptoCurrency = value;
                OnPropertyChanged(nameof(CryptoCurrency));

            }
        }

        public MainWindowViewModel()
        {

            _httpClient = new HttpClient();
            LoadCryptoCurrencyAsync();             //  Завантажуємо крипту при створенні ViewModel

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
                    DeserializeObject<CryptoResponse>(response);     //десереалізація json в об єкт CryptoResponse

                CryptoCurrency = cryptoResponse.Data;                 //Зберігаємо дані про крипту

               // Debug.WriteLine($"Loaded Data: {_cryptoCurrency.Name}");

                Debug.WriteLine($"Loaded Data:{cryptoResponse.Data}");

                     
            }

            catch (HttpRequestException e)
            {
                Debug.WriteLine($"Error loading data: {e.Message}"); // Помилка при завантаженні
                MessageBox.Show("Could not load data.", $"Error:{e}", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
