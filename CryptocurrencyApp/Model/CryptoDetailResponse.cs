using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyApp.Model
{
    public class CryptoDetailResponse
    {
        public List<CryptoDataDetail> Data { get; set; }
        public long Timestamp { get; set; }
    }
}
