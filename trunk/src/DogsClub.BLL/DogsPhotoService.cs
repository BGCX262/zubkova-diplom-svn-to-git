using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using domain = DogsClub.DomainModel;
using dal = DogsClub;
using DogsClub.Abstract;
using DogsClub.BLL.Abstract;

namespace DogsClub.BLL
{
    public class DogsPhotoService : BaseService<IDogsPhotosRepo, domain.DogsPhoto, dal.DogsPhoto>, IDogsPhotoService
    {
    }
}
