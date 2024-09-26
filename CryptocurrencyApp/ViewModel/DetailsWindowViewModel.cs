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
using System.Web;
using System.Windows.Navigation;
using Wpf.Ui.Input;

namespace CryptocurrencyApp.ViewModel
{
    internal class DetailsWindowViewModel : BindableBase
    {
        private readonly CoinCapApiClient _coinCapApiClient = new CoinCapApiClient();
        private readonly HttpClient _httpClient;
        private readonly string _id;
        private List<string> _marketId;
       
        private CryptoDataDetail _cryptoDataDetail;

       // private ObservableCollection<MarketDataDetail> _cryptoMarketDetails;
        private ObservableCollection<CryptoMarketDetail> _cryptoMarketDetails;
        private ObservableCollection<CryptoMarkets2> _cryptoMarkets;

        public ICommand NavigateToHomeCommand { get; set; }
        public ICommand OpenLinkCommand { get; }

        private ObservableCollection<CombinedCryptoMarketData> _combinedCryptoMarketData = new ObservableCollection<CombinedCryptoMarketData>();
        public ObservableCollection<CombinedCryptoMarketData> CombinedCryptoMarketData
        {
            get { return _combinedCryptoMarketData; }
            set => SetProperty(ref _combinedCryptoMarketData, value);
        }
        public ObservableCollection<CryptoMarkets2> CryptoMarkets
        {
            get => _cryptoMarkets;
            set => SetProperty(ref _cryptoMarkets, value);
        }

        /*  public ObservableCollection<MarketDataDetail> CryptoMarketDetails
          {
              get => _cryptoMarketDetails;
              set => SetProperty(ref _cryptoMarketDetails, value);
          }*/

        public ObservableCollection<CryptoMarketDetail> CryptoMarketDetails
        {
            get => _cryptoMarketDetails;
            set => SetProperty(ref _cryptoMarketDetails, value);
        }
        public CryptoDataDetail CryptoDataDetail
        {
            get => _cryptoDataDetail;
            set => SetProperty(ref _cryptoDataDetail, value);
        }

        public DetailsWindowViewModel(string id)
        {
            // CryptoMarkets = new ObservableCollection<CryptoMarkets>();
           
            CryptoDataDetail = new CryptoDataDetail();
            CryptoMarkets = new ObservableCollection<CryptoMarkets2>();
            CryptoMarketDetails = new ObservableCollection<CryptoMarketDetail>();

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
            var result = await _coinCapApiClient.GetCurrencyDetailsAsync(_id);
            CryptoDataDetail = result;
           
        }
        /*   private async void LoadMarketDataAsync()
           {
               try
               {
                   var result = await _coinCapApiClient.GetCryptoMarketsAsync2(_id);
                   foreach (var data in result)
                   {
                       //var t = Uri.EscapeDataString(data.ExchangeId);


                       var marketDetail = await _coinCapApiClient.GetMarketDetail(data.ExchangeId);


                       var cryptoMarketDetail = new CryptoMarketDetail()
                       {
                           ExchangeId = data.ExchangeId,
                           PriceUsd = data.PriceUsd,
                           ExchangeUrl = marketDetail.ExchangeUrl,
                       };
                       CryptoMarketDetails.Add(cryptoMarketDetail);
                   }
               }
               catch (Exception ex)
               {
                   Debug.WriteLine($"Error loading market data: {ex.Message}");
               }
           }*/

        /*private async void LoadMarketDataAsync()
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
        }*/

        /*  private async void LoadMarketDetailDataAsync()
          {
              try
              {
                  var result = await _coinCapApiClient.GetCryptoMarketsAsync(_id);
                  var tasks = result.Select(async data =>
                  {
                      // Виклик для отримання деталей біржі за її ID
                      var marketDetail = await GetMarketDetailAsync(data.ExchangeId);

                      // Якщо marketDetail не null, додати в колекцію
                      if (marketDetail != null)
                      {
                          var cryptoMarketDetail = new CryptoMarketDetail()
                          {
                              ExchangeId = data.ExchangeId,
                              PriceUsd = data.PriceUsd,
                              ExchangeUrl = marketDetail.ExchangeUrl,
                          };
                          return cryptoMarketDetail;
                      }
                      return null; // Повертаємо null у разі помилки
                  });

                  // Очікуємо завершення всіх запитів
                  var cryptoMarketDetails = await Task.WhenAll(tasks);

                  // Додаємо до списку лише ті, що успішно повернули дані
                  CryptoMarketDetails.AddRange(cryptoMarketDetails.Where(m => m != null));
              }
              catch (Exception ex)
              {
                  Debug.WriteLine($"Error loading market data: {ex.Message}");
              }
          }*/

        private async void LoadMarketDataAsync()
        {
            try
            {
                var result = await _coinCapApiClient.GetCryptoMarketsAsync(_id);

                if (result != null)
                {
                    var exchangeIds = new List<string>();
                    foreach (var market in result)
                    {
                        CryptoMarkets.Add(market);

                        if (!string.IsNullOrEmpty(market.ExchangeId))
                        {
                            exchangeIds.Add(market.ExchangeId);
                        }
                    }

                    foreach (var exchangeId in exchangeIds)
                    {
                        var marketDetail = await _coinCapApiClient.GetMarketDetail(exchangeId);
                       

                        if (marketDetail != null)
                        {
                            var cryptoMarket = result.FirstOrDefault(m => m.ExchangeId == exchangeId);
                            // Додаємо дані в об'єднану колекцію
                            CombinedCryptoMarketData.Add(new CombinedCryptoMarketData
                            {
                                ExchangeId = exchangeId,
                                PriceUsd = cryptoMarket.PriceUsd,
                                ExchangeUrl = marketDetail.ExchangeUrl,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading market data: {ex.Message}");
            }
        }

      

        private void DisplayExchangeNames(List<string> exchangeNames)
        {
            // Ваша логіка для відображення назв бірж
            // Наприклад, прив'язка до DataGrid або ComboBox
            // В залежності від вашого UI
        }


        /*  private async void LoadMarketDetailAsync()
          {
              try
              {
                  var result = await _coinCapApiClient.GetMarketDetail(_marketId);

                  //MarketDataDetail = result;
              }
              catch (Exception ex)
              {
                  Debug.WriteLine($"Error loading market data: {ex.Message}");
              }
          }*/

        private void NavigateToHome()
        {
            var homePage = new HomePage();
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainFrame.Navigate(homePage);
        }
    }
}
