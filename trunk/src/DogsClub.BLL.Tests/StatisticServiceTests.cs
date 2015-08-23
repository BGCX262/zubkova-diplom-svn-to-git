using DogsClub.Abstract;
using DogsClub.Fake;
using NUnit.Framework;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.BLL.Tests
{
	[TestFixture]
    public class StatisticServiceTests
    {
		[TestFixtureSetUp]
		public void Init()
		{
			ObjectFactory.Initialize(x =>
				{
					x.For<IUsersRepo>().Use<UsersFakeRepo>();
					x.For<IExpositionWinnersRepo>().Use<ExpositionWinnersFakeRepo>();
				});
		}

		[Test]
		public void UsersStatisticTest()
		{
			StatisticService statistic = new StatisticService();
			var usersStatistic = statistic.GetUsersStatistic().ToList();

			int winnersFirstUser = usersStatistic.First(t => t.Id == 1).NumOfDogsWinners;
			Assert.AreEqual(2, winnersFirstUser);

			int winnersSecondUser = usersStatistic.First(t => t.Id == 2).NumOfDogsWinners;
			Assert.AreEqual(1, winnersSecondUser);
		}

		[Test]
		public void DogsStatisticTests()
		{
			StatisticService statistic = new StatisticService();
			var dogsStatistic = statistic.GetDogsStatistic().ToList();

			int winners3Dog = dogsStatistic.First(t => t.Id == 3).NumOfVictories;
			Assert.AreEqual(2, winners3Dog);

			int winners2Dog = dogsStatistic.First(t => t.Id == 2).NumOfVictories;
			Assert.AreEqual(1, winners2Dog);

			int winners1Dog = dogsStatistic.First(t => t.Id == 1).NumOfVictories;
			Assert.AreEqual(1, winners1Dog);
		}
    }
}
