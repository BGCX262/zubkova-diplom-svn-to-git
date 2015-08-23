using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using domain = DogsClub.DomainModel;

namespace DogsClub.BLL.Abstract
{
    public interface IPaymentTypesService : IBaseService<DogsClub.DomainModel.PaymentType>
    {
        IEnumerable<domain.PaymentsPeriodType> GetAllPaymentPeriods();
    }
}
