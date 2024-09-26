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

                //await Task.Delay(2000);

                var cryptoResponse = await response.Content.ReadFromJsonAsync<CryptoResponse>();

                Debug.WriteLine($"Loaded Data:{cryptoResponse.Data}");

                return cryptoResponse.Data;
            }
            catch (HttpRequestException e)
            {
                //delete
                MessageBox.Show("Could not load data.", $"Error:{e}", MessageBoxButton.OK, MessageBoxImage.Error);

                return new List<CryptoData>();
            }
        }

        public async Task<CryptoDataDetail?> GetCurrencyDetailsAsync(string currencyId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"assets/{currencyId}");
                response.EnsureSuccessStatusCode();

                Debug.WriteLine("Data loaded successfully.");

                var cryptoDetailResponse = await response.Content.ReadFromJsonAsync<CryptoDetailResponse>();


                return cryptoDetailResponse.Data;
            }
            catch (HttpRequestException e)
            {

                MessageBox.Show("Could not load data.", $"Error:{e}", MessageBoxButton.OK, MessageBoxImage.Error);

                return null;
            }
        }

        public async Task<List<CryptoMarkets2>?> GetCryptoMarketsAsync(string currencyId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"assets/{currencyId}/markets");
                response.EnsureSuccessStatusCode();

                Debug.WriteLine("Data loaded successfully.");

                var cryptoMarketResponse = await response.Content.ReadFromJsonAsync<CryptoMarketsResponse2>();


                return cryptoMarketResponse.Data;
            }
            catch (HttpRequestException e)
            {

               // MessageBox.Show("Could not load data.", $"Error:{e}", MessageBoxButton.OK, MessageBoxImage.Error);

                return null;
            }
        }

     /*   public async Task<List<CryptoMarkets2>?> GetCryptoMarketsAsync2(string currencyId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"markets?assetId={currencyId}");
                response.EnsureSuccessStatusCode();

                Debug.WriteLine("Data loaded successfully.");

                var cryptoMarketResponse = await response.Content.ReadFromJsonAsync<CryptoMarketsResponse2>();


                return cryptoMarketResponse.Data;
            }
            catch (HttpRequestException e)
            {

                MessageBox.Show("Could not load data.", $"Error:{e}", MessageBoxButton.OK, MessageBoxImage.Error);

                return null;
            }
        }*/
        
        public async Task<MarketDataDetail?> GetMarketDetail(string exchangeId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"exchanges/{exchangeId}");
                response.EnsureSuccessStatusCode();

                var marketResponse = await response.Content.ReadFromJsonAsync<MarketResponse>();

                return marketResponse.Data;
            }
            catch (HttpRequestException e)
            {

                //MessageBox.Show("Could not load data.", $"Error:{e}", MessageBoxButton.OK, MessageBoxImage.Error);

                return null;
            }
}
    }
}
