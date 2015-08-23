using DogsClub.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain = DogsClub.DomainModel;
using dal = DogsClub;
using DogsClub.BLL.Abstract;
using StructureMap;
using AutoMapper;
using System.Collections;

namespace DogsClub.BLL
{
    public class PaymentTypesService : BaseService<IPaymentTypesRepo, domain.PaymentType, dal.PaymentType>, IPaymentTypesService
    {
        public IEnumerable<domain.PaymentsPeriodType> GetAllPaymentPeriods()
        {
            using(var repo = ObjectFactory.GetInstance<IPaymentTypesRepo>())
            {
                return Mapper.Map<IEnumerable<domain.PaymentsPeriodType>>(repo.GetPaymentPeriods());
            }
        }
    }
}
