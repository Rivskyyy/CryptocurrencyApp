using CryptocurrencyApp.Model;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;

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

        /// <summary>
        /// Get the top cryptocurrencies 
        /// </summary>
        public async Task<List<CryptoData>> GetCurrenciesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("assets");

                Debug.WriteLine("Data loaded successfully.");

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

        /// <summary>
        /// Get the details about crypto
        /// </summary>
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

        /// <summary>
        /// Get the exchanges by id
        /// </summary>
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
        
        /// <summary>
        /// Get the exchange's url
        /// </summary>
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
