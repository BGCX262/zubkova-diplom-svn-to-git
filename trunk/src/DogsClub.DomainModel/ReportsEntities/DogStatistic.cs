using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.DomainModel.ReportsEntities
{
	public class DogStatistic
	{
		public int IdDog { get; set; }
		public string Name { get; set; }
		public string Sex { get; set; }
		public int Age { get; set; }
		public string DogType { get; set; }
		public int OwnerId { get; set; }
		public string OwnerFirstName { get; set; }
		public string OwnerMiddleName { get; set; }
		public string OwnerLastName { get; set; }

		public IEnumerable<DogVaccinationStatistic> Vaccinations { get; set; }
	}
}
