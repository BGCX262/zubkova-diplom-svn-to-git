using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.DomainModel
{
	public class User
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public DateTime BirthDate { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public DateTime RegistrationDate { get; set; }
		public string Password { get; set; }
		public string City { get; set; }
		public string Address { get; set; }
		public byte[] Avatar { get; set; }
        public string AvatarMimeType { get; set; }

        public int Sex { get; set; }

		public virtual ICollection<Dog> Dogs { get; set; }
		//public virtual ICollection<Payment> Payments { get; set; }
	}
}
