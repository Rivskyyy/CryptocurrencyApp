using Prism.Mvvm;

namespace CryptocurrencyApp.ViewModel
{
    public class CryptoMarketDetail : BindableBase
    {
        public string ExchangeId { get; set; }
        public string PriceUsd { get; set; }
        public string ExchangeUrl { get; set; }
    }
}
