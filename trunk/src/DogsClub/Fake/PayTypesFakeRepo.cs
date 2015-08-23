using DogsClub.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.Fake
{
    public class PayTypesFakeRepo : IPaymentTypesRepo
    {
        public void Edit(PaymentType item)
        {
            throw new NotImplementedException();
        }

        public void Add(PaymentType item)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PaymentType> GetAll()
        {
            return FakeDataStore.GetPaymentTypes();
        }
        
        public PaymentType GetById(int id)
        {
            return FakeDataStore.GetPaymentTypes().FirstOrDefault(t => t.Id == id);
        }

        public void Save()
        {
            
        }

        public void Dispose()
        {
            
        }

        public IEnumerable<PaymentsPeriodType> GetPaymentPeriods()
        {
            throw new NotImplementedException();
        }
    }
}
