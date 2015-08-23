using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.DomainModel
{
	public class News
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime NewsDate { get; set; }
		public string NewsText { get; set; }
	}
}
