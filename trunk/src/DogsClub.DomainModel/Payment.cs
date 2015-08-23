using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.DomainModel
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal PaySize { get; set; }
        public System.DateTime PayDate { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int IdType { get; set; }
    }
}
