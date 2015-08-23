using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.DomainModel.ReportsEntities
{
    public class ConcretteUserPay
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName 
        { 
            get
            {
                return string.Format("{0} {1} {2}", LastName, Name, MiddleName);
            }
        }

        public DateTime PayDate { get; set; }
        public decimal PaySize { get; set; }
        public string PayDescription { get; set; }
    }
}
