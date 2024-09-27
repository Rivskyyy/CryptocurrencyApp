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
        //Get the top cryptocurrencies 
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
                
                Debug.WriteLine($"Error loading data: {e.Message}");
                return new List<CryptoData>();
            }
        }
        //Get the details about crypto
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
                Debug.WriteLine($"Error loading data: {e.Message}");
                return null;
            }
        }
        // Get the exchanges by id
        public async Task<List<CryptoMarkets>?> GetCryptoMarketsAsync(string currencyId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"assets/{currencyId}/markets");
                response.EnsureSuccessStatusCode();

                Debug.WriteLine("Data loaded successfully.");

                var cryptoMarketResponse = await response.Content.ReadFromJsonAsync<CryptoMarketsResponse>();


                return cryptoMarketResponse.Data;
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"Error loading data: {e.Message}");
                return null;
            }
        }
        //Get the exchange's url
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

                Debug.WriteLine($"Error loading data: {e.Message}");
                return null;
            }   
        }
    }
}
