using DogsClub.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DogsClub.Concrete
{
    public class PaymentTypesRepo : BaseRepo<PaymentType>, IPaymentTypesRepo
    {
        public PaymentTypesRepo()
            : base() { }

        public override PaymentType GetById(int id)
        {
            //return _dbSet.Include(t => t.)            
                return _dbSet
                    .Include(t => t.Payments)
                    .Include(t => t.Payments.Select(x => x.User))
                    .FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<PaymentsPeriodType> GetPaymentPeriods()
        {
            using(var context = new DClubEntities())
            {
                return context
                    .PaymentsPeriodTypes
                    .ToList();
            }
        }
    }
}
