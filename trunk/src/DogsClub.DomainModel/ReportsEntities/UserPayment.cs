using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.DomainModel.ReportsEntities
{
	public class UserPayment
	{
		public DateTime PayDate { get; set; }
		public string PayType { get; set; }
		public decimal PaySize { get; set; }
	}
}
