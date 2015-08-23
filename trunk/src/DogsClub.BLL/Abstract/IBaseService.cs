using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.BLL.Abstract
{
    public interface IBaseService<TDomainEntity>
    {
		void Add(TDomainEntity item);
        IEnumerable<TDomainEntity> GetAll();
        TDomainEntity GetById(int id);
        void Edit(TDomainEntity entity);
        void Remove(int id);
    }
}
