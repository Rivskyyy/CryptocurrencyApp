using CryptocurrencyApp.APIs;
using CryptocurrencyApp.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptocurrencyApp.ViewModel
{
    internal class DetailsWindowViewModel : BindableBase
    {
        private readonly CoinCapApiClient _coinCapApiClient = new CoinCapApiClient();
        private readonly HttpClient _httpClient;
        private readonly string _id;

        private CryptoDataDetail _cryptoDataDetail;

        public CryptoDataDetail CryptoDataDetail
        {
            get => _cryptoDataDetail;
            set => SetProperty(ref _cryptoDataDetail, value);
        }
        public ICommand AddCommand { get; }

        public DetailsWindowViewModel(string id)
        {
            _id = id;
            LoadDetailsDataAsync();
        }

        private async void LoadDetailsDataAsync()
        {
            var result = await _coinCapApiClient.GetCurrencyDetailsAsync(_id);
            CryptoDataDetail = result; 
        }
    }
}
