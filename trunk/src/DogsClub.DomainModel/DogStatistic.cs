using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.DomainModel
{
	public class DogStatistic
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string DogType { get; set; }
		public int NumOfVictories { get; set; }
	}
}
