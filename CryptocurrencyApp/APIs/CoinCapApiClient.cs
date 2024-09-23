using CryptocurrencyApp.Model;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace CryptocurrencyApp.APIs
{
    internal class CoinCapApiClient
    {
        private readonly HttpClient _httpClient;

        public CoinCapApiClient()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://api.coincap.io/v2/")
            };
        }

        public async Task<List<CryptoData>> GetCurrenciesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("assets");

                Debug.WriteLine("Data loaded successfully.");

                await Task.Delay(2000);

                var cryptoResponse = await response.Content.ReadFromJsonAsync<CryptoResponse>();

                Debug.WriteLine($"Loaded Data:{cryptoResponse.Data}");

                return cryptoResponse.Data;
            }
            catch (HttpRequestException e)
            {

                MessageBox.Show("Could not load data.", $"Error:{e}", MessageBoxButton.OK, MessageBoxImage.Error);

                return new List<CryptoData>();
            }
        }

        public async Task<CryptoDataDetail> GetCurrencyDetailsAsync(int currencyId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"assets/{currencyId}");

                Debug.WriteLine("Data loaded successfully.");

                var cryptoDetailResponse = await response.Content.ReadFromJsonAsync<CryptoDetailResponse>();

                // Debug.WriteLine($"Loaded Data:{cryptoResponse.Data}");

                return cryptoDetailResponse.Data.FirstOrDefault();
            }
            catch (HttpRequestException e)
            {

                MessageBox.Show("Could not load data.", $"Error:{e}", MessageBoxButton.OK, MessageBoxImage.Error);

                return null;
            }
        }
    }
}
