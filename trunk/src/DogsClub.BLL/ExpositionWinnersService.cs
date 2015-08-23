using DogsClub.Abstract;
using DogsClub.BLL.Abstract;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using dal = DogsClub;
using domain = DogsClub.DomainModel;

namespace DogsClub.BLL
{
	public class ExpositionWinnersService : BaseService<IExpositionWinnersRepo, domain.ExpositionWinner, dal.ExpositionWinner>
		, IExpositionWinnersService
	{
		public override void Add(domain.ExpositionWinner item)
		{
			using (TransactionScope transaction = new TransactionScope())
			{
				base.Add(item);
				using (var newsRepo = ObjectFactory.GetInstance<INewsRepo>())
				{
					using(var expositionMembersRepo = ObjectFactory.GetInstance<IExpositionMembersRepo>())
					{
						var expositionMember = expositionMembersRepo.GetById(item.IdExpositionMember);
						newsRepo.Add(new News()
							{
								NewsDate = DateTime.Now,
								Title = "Победитель в выставке!!!",
								NewsText = string.Format("{0} {1} {2} поздравляем Вас и вашего питомца по кличке {3} в победе на выставке '{4}'", expositionMember.Dog.User.LastName, expositionMember.Dog.User.FirstName
								, expositionMember.Dog.User.MiddleName, expositionMember.Dog.Name, expositionMember.Exposition.Name)
							});
						newsRepo.Save();
					}
				}

				transaction.Complete();
			}
		}
	}
}
