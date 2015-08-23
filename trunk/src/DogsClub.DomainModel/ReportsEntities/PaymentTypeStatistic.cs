using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.DomainModel.ReportsEntities
{
    public class PaymentTypeStatistic
    {
        public int IdPaymentType { get; set; }
        public string PaymentName { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public IEnumerable<ConcretteUserPay> Payments { get; set; }
    }
}
