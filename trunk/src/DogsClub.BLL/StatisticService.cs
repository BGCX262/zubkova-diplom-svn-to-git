using DogsClub.Abstract;
using DogsClub.BLL.Abstract;
using DogsClub.DomainModel;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.BLL
{
	public class StatisticService : IStatisticService
	{
		public IEnumerable<DomainModel.UserStatistic> GetUsersStatistic()
		{
			using(var usersRepo = ObjectFactory.GetInstance<IUsersRepo>())
			{
				using (var winnersRepo = ObjectFactory.GetInstance<IExpositionWinnersRepo>())
				{

					var winners = winnersRepo
						.GetAll()
						.Select(t => t.ExpositionMember.Dog)
						.GroupBy(t => t.OwnerId)
						.Select(t => new
						{
							idOwner = t.Key,
							winsNum = t.Count(),
						})
						.ToList();

                    var owners = winners.Select(t => t.idOwner).ToList();

					return usersRepo
						.GetAll()
                        .Where(t => owners.Contains(t.Id))
						.Select(t => new UserStatistic
						{
							Id = t.Id,
							FirstName = t.FirstName,
							LastName = t.LastName,
							MiddleName = t.MiddleName,
							NumOfDogsWinners = winners.FirstOrDefault(x => x.idOwner == t.Id).winsNum
						})
						.ToList(); 
				}
			}
		}

		public IEnumerable<DomainModel.DogStatistic> GetDogsStatistic()
		{
			using(var winnersRepo = ObjectFactory.GetInstance<IExpositionWinnersRepo>())
			{
				var winners = winnersRepo
					.GetAll()
					.Select(t => t.ExpositionMember.Dog)
					.ToList();

				var winnesCount = winners
					.GroupBy(t => t.Id)
					.Select(t => new
					{
						idDog = t.Key,
						winnsCount = t.Count()
					})
					.ToList();

				return winnesCount
					.Select(t => new DogStatistic
					{
						Id = t.idDog,
						Name = winners.First(x => x.Id == t.idDog).Name,
						DogType = winners.First(x => x.Id == t.idDog).DogType.Name,
						NumOfVictories = t.winnsCount
					})
                    .ToList();
			}
		}
	}
}
