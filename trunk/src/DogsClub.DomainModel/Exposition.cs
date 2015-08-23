using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.DomainModel
{
	public class Exposition
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public System.DateTime DateStart { get; set; }
		public string Description { get; set; }

        //public ICollection<Dog> ExpositionMembers { get; set; }
        //public ICollection<Dog> ExpositionRequests { get; set; }
        //public ICollection<Dog> ExpositionWinners { get; set; }
	}
}
