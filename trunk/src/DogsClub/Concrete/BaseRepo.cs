using DogsClub.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.Concrete
{
	public class BaseRepo<T> : IBaseRepo<T>
		where T : class
	{
		protected DbSet<T> _dbSet;
		protected DClubEntities _context;
		public BaseRepo()
		{
			DClubEntities context = new DClubEntities();
			_dbSet = context.Set<T>();
			_context = context;
		}

		public void Add(T item)
		{
			if (item == null) throw new ArgumentNullException("item");
			_dbSet.Add(item);
		}

		public virtual void Remove(int id)
		{
			var oldEntity = this.GetById(id);
			_dbSet.Remove(oldEntity);
		}

		public virtual T GetById(int id)
		{
			return _dbSet.Find(id);
		}

		public void Dispose()
		{
			if (_context != null) _context.Dispose();
		}

		public void Save()
		{
			_context.SaveChanges();
		}

		IEnumerable<T> IBaseRepo<T>.GetAll()
		{
			return _dbSet.AsEnumerable<T>();
		}

        public void Edit(T item)
        {
            if (item == null) throw new ArgumentNullException("item");
            using (var context = new DClubEntities())
            {
                var dbSet = context.Set<T>();
                var pKey = dbSet.Create().GetType().GetProperty("Id").GetValue(item);
                var oldEntity = dbSet.Find(pKey);
                context.Entry<T>(oldEntity).CurrentValues.SetValues(item);
                context.SaveChanges();
            }
        }
    }
}
