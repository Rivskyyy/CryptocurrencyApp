using CryptocurrencyApp.APIs;
using CryptocurrencyApp.Model;
using CryptocurrencyApp.View;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Prism.Commands;
using System.Collections.ObjectModel;

namespace CryptocurrencyApp.ViewModel
{
    internal class DetailsWindowViewModel : BindableBase
    {
        private readonly CoinCapApiClient _coinCapApiClient = new CoinCapApiClient();
        private readonly HttpClient _httpClient;
        private readonly string _id;
        private CryptoDataDetail _cryptoDataDetail;
        private ObservableCollection<CryptoMarkets> _cryptoMarkets;
        public ICommand NavigateToHomeCommand { get; set; }

        public ObservableCollection<CryptoMarkets> CryptoMarkets
        {
            get => _cryptoMarkets;
            set => SetProperty(ref _cryptoMarkets, value);
        }
        public CryptoDataDetail CryptoDataDetail
        {
            get => _cryptoDataDetail;
            set => SetProperty(ref _cryptoDataDetail, value);
        }

        public DetailsWindowViewModel(string id)
        {
            CryptoMarkets = new ObservableCollection<CryptoMarkets>();
            _id = id;
            LoadDetailsDataAsync();
            LoadMarketDataAsync();
            NavigateToHomeCommand = new DelegateCommand(NavigateToHome);
        }

        private async void LoadDetailsDataAsync()
        {
            var result = await _coinCapApiClient.GetCurrencyDetailsAsync(_id);
            CryptoDataDetail = result;
           
        }
        private async void LoadMarketDataAsync()
        {
            try
            {
                var result = await _coinCapApiClient.GetCryptoMarketsAsync(_id);

                // Перевірка на null перед додаванням
                if (result != null)
                {
                    foreach (var market in result)
                    {
                        CryptoMarkets.Add(market);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading market data: {ex.Message}");
            }
        }

        private void NavigateToHome()
        {
            var homePage = new HomePage();
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.Navigate(homePage);
        }
    }
}
