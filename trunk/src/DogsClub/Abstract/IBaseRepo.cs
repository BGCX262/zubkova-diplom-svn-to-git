using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.Abstract
{
	public interface IBaseRepo<T> : IDisposable
	{
        void Edit(T item);
		void Add(T item);
		void Remove(int id);
		IEnumerable<T> GetAll();
		T GetById(int id);
		void Save();
	}
}
