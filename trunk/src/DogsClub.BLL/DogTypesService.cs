using DogsClub.Abstract;
using DogsClub.BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain = DogsClub.DomainModel;

namespace DogsClub.BLL
{
	public class DogTypesService : BaseService<IDogTypesRepo, domain.DogType, DogsClub.DogType>, IDogTypesService
	{
	}
}
