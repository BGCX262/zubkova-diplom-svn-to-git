using DogsClub.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.Fake
{
	public class DogsFakeRepo : IDogsRepo
	{
		public void Edit(Dog item)
		{
			throw new NotImplementedException();
		}

		public void Add(Dog item)
		{
			throw new NotImplementedException();
		}

		public void Remove(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Dog> GetAll()
		{
			return FakeDataStore.GetDogs();
		}

		public Dog GetById(int id)
		{
			return FakeDataStore.GetDogs().FirstOrDefault(t => t.Id == id);
		}

		public void Save()
		{
			
		}

		public void Dispose()
		{
			
		}
	}
}
