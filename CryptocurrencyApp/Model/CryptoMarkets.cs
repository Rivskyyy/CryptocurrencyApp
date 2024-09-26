﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyApp.Model
{
    public class CryptoMarkets
    {
        public string ExchangeId { get; set; }
        public string BaseId { get; set; }
        public string QuoteId { get; set; }
        public string BaseSymbol { get; set; }
        public string QuoteSymbol { get; set; }
        public decimal VolumeUsd24Hr { get; set; }
        public decimal PriceUsd { get; set; }
        public decimal VolumePercent { get; set; }
    }

    public class CryptoMarkets2
    {
        public string ExchangeId { get; set; }
        public string PriceUsd { get; set; }
    }
}
