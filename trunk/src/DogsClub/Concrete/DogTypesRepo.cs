using DogsClub.Abstract;
using DogsClub.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub
{
	public class DogTypesRepo : BaseRepo<DogType>, IDogTypesRepo
	{
		public DogTypesRepo()
			: base() { }
	}
}
