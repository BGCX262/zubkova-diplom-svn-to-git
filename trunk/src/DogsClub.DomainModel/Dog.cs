using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.DomainModel
{
	public class Dog
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public string Description { get; set; }
		public byte[] Avatar { get; set; }
		public string AvatarMimeType { get; set; }
		public int OwnerId { get; set; }
		public Nullable<bool> WasSold { get; set; }

        public Nullable<int> Sex { get; set; }

        public int IdDogType { get; set; }

		public User User { get; set; }
		
		public ICollection<DogsPhoto> DogsPhotos { get; set; }

        public DogType DogType { get; set; }
		public IEnumerable<Exposition> WinsExpositions { get; set; }
		public IEnumerable<Exposition> ExpositionsMember { get; set; }
        public ICollection<Vaccination> Vaccinations { get; set; }
		//public virtual ICollection<ExpositionMember> ExpositionMembers { get; set; }		
		//public virtual ICollection<Vaccination> Vaccinations { get; set; }
	}
}
