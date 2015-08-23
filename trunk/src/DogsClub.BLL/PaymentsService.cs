using DogsClub.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using domain = DogsClub.DomainModel;
using dal = DogsClub;
using DogsClub.BLL.Abstract;

namespace DogsClub.BLL
{
    public class PaymentsService : BaseService<IPaymentsRepo, domain.Payment, dal.Payment>, IPaymentsService
    {
    }
}
