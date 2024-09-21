using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyAppTest.Model
{
    public class CryptoResponse
    {

        public List<CryptoData> Data { get; set; }
        public long Timestamp { get; set; }

    }
}
