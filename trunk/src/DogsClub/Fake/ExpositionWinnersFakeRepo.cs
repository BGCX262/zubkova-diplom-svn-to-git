using DogsClub.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.Fake
{
	public class ExpositionWinnersFakeRepo : IExpositionWinnersRepo
	{
		public void Edit(ExpositionWinner item)
		{
			throw new NotImplementedException();
		}

		public void Add(ExpositionWinner item)
		{
			throw new NotImplementedException();
		}

		public void Remove(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<ExpositionWinner> GetAll()
		{
			return FakeDataStore.GetExpositionWinners();
		}

		public ExpositionWinner GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Save()
		{
			
		}

		public void Dispose()
		{
			
		}
	}
}
