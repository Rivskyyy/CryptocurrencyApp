using CryptocurrencyApp.APIs;
using CryptocurrencyApp.Model;
using CryptocurrencyApp.View;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace CryptocurrencyApp.ViewModel
{
    internal class DetailsWindowViewModel : BindableBase
    {
        private readonly CoinCapApiClient _coinCapApiClient = new CoinCapApiClient();
        private readonly string _id;
        private CryptoDataDetail _cryptoDataDetail;

        private ObservableCollection<CryptoMarkets> _cryptoMarkets;

        public ICommand NavigateToHomeCommand { get; set; }
        public ICommand OpenLinkCommand { get; }

        private ObservableCollection<CombinedCryptoMarketData> _combinedCryptoMarketData = new ObservableCollection<CombinedCryptoMarketData>();
        public ObservableCollection<CombinedCryptoMarketData> CombinedCryptoMarketData
        {
            get { return _combinedCryptoMarketData; }
            set => SetProperty(ref _combinedCryptoMarketData, value);
        }
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
           
            CryptoDataDetail = new CryptoDataDetail();
            CryptoMarkets = new ObservableCollection<CryptoMarkets>();

            _id = id;
            LoadDetailsDataAsync();
            LoadMarketDataAsync();
            NavigateToHomeCommand = new DelegateCommand(NavigateToHome);
            OpenLinkCommand = new DelegateCommand<string>(OpenLink);
        }
        private void OpenLink(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
        }
        private async void LoadDetailsDataAsync()
        {
            try
            {
                var result = await _coinCapApiClient.GetCurrencyDetailsAsync(_id);
                CryptoDataDetail = result;
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not load data.", $"Error:{e}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadMarketDataAsync()
        {
            try
            {
                var result = await _coinCapApiClient.GetCryptoMarketsAsync(_id);

                if (result != null)
                {
                    var exchangeIds = new HashSet<string>(); // hashmap for unique values

                    foreach (var market in result)
                    {
                        if (!string.IsNullOrEmpty(market.ExchangeId) && exchangeIds.Add(market.ExchangeId))  // add if not null && unique
                        {
                            CryptoMarkets.Add(market);
                        }
                    }

                    foreach (var exchangeId in exchangeIds)
                    {
                        var marketDetail = await _coinCapApiClient.GetMarketDetail(exchangeId);

                        if (marketDetail != null)
                        {
                            var cryptoMarket = result.FirstOrDefault(m => m.ExchangeId == exchangeId);

                            if (cryptoMarket != null) 
                            {
                               
                                CombinedCryptoMarketData.Add(new CombinedCryptoMarketData    // combined list for only 1 dataGrid
                                {
                                    ExchangeId = exchangeId,
                                    PriceUsd = cryptoMarket.PriceUsd,
                                    ExchangeUrl = marketDetail.ExchangeUrl,
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error loading data: {e.Message}");
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
