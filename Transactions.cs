using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaTest
{
    [Serializable]
    public class Transactions
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string MerchantName { get; set; }
    }
}
