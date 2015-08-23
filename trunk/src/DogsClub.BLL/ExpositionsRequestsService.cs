﻿using DogsClub.Abstract;
using DogsClub.BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal = DogsClub;
using domain = DogsClub.DomainModel;

namespace DogsClub.BLL
{
    public class ExpositionsRequestsService : BaseService<IExpositionRequests, domain.ExpositionRequest, dal.ExpositionsRequest>
        , IExpositionsRequestsService
    {
    }
}