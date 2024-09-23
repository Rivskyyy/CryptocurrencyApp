using CryptocurrencyApp.APIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyApp.ViewModel
{
    internal class DetailsWindowViewModel : BaseViewModel
    {
        private readonly CoinCapApiClient _coinCapApiClient = new CoinCapApiClient();
        private readonly HttpClient _httpClient;
        public Command AddCommand { get; }

    }
}
