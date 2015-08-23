using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.DomainModel
{
    public class PaymentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdPeriod { get; set; }
    }
}
