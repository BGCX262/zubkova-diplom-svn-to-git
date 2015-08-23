using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.DomainModel.ReportsEntities
{
	public class UserPaymentsStatistic
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }

        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }

        public string FullName 
        { 
            get
            {
                return string.Format("{0} {1} {2}", LastName, FirstName, MiddleName);
            }
        }

		public IEnumerable<UserPayment> Payments { get; set; }
	}
}
