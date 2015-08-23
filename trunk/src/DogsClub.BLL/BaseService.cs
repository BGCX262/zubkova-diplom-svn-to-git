using AutoMapper;
using DogsClub.Abstract;
using DogsClub.BLL.Abstract;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.BLL
{
    public class BaseService<TBaseRepo, TDomainEntity, TDalEntity>: IBaseService<TDomainEntity>
        where TBaseRepo : IBaseRepo<TDalEntity>
    {
        public IEnumerable<TDomainEntity> GetAll()
        {
            using(var repository = ObjectFactory.GetInstance<TBaseRepo>())
            {
                var dbEntities = repository.GetAll().ToList();
                var mappedEntities = Mapper.Map<IEnumerable<TDalEntity>, IEnumerable<TDomainEntity>>(dbEntities);
                return mappedEntities;
            }
        }

        public TDomainEntity GetById(int id)
        {
            using(var repository = ObjectFactory.GetInstance<TBaseRepo>())
            {
                var dbEntity = repository.GetById(id);
                var mappedEntity = Mapper.Map<TDomainEntity>(dbEntity);
                return mappedEntity;
            }
        }

        public virtual void Edit(TDomainEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            using(var repository = ObjectFactory.GetInstance<TBaseRepo>())
            {
                var mappedEntity = Mapper.Map<TDalEntity>(entity);
                repository.Edit(mappedEntity);
                repository.Save();
            }
        }

        public void Remove(int id)
        {
            using(var repository = ObjectFactory.GetInstance<TBaseRepo>())
            {
                repository.Remove(id);
                repository.Save();
            }
        }

		public virtual void Add(TDomainEntity item)
		{
			using (var repository = ObjectFactory.GetInstance<TBaseRepo>())
			{
				var mappedEntity = Mapper.Map<TDalEntity>(item);
				repository.Add(mappedEntity);
				repository.Save();
			}
		}
	}
}
