﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.Abstract
{
    public interface IPaymentTypesRepo : IBaseRepo<PaymentType>
    {
        IEnumerable<PaymentsPeriodType> GetPaymentPeriods();
    }
}
