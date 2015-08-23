using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.DomainModel
{
	public class DogsPhoto
	{
		public int Id { get; set; }
		public byte[] PhotoData { get; set; }
		public string MimeType { get; set; }
		public int DogId { get; set; }
	}
}
