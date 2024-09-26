using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyApp.Model
{
    public class CombinedCryptoMarketData
    {
        public string ExchangeId { get; set; }
        public string PriceUsd { get; set; }
        public string ExchangeUrl { get; set; }
    }
}
