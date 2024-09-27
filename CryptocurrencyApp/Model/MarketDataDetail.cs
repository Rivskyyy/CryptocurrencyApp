namespace CryptocurrencyApp.Model
{
    public class MarketDataDetail
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public decimal PercentTotalVolume { get; set; }
        public decimal VolumeUsd { get; set; }
        public int TradingPairs { get; set; }
        public bool Socket { get; set; }
        public string ExchangeUrl { get; set; }
        public long Updated { get; set; }
    }
}
