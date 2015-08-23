using AutoMapper;
using DogsClub.Abstract;
using DogsClub.Fake;
using NUnit.Framework;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using domain = DogsClub.DomainModel;
using dal = DogsClub;

namespace DogsClub.BLL.Tests
{
	[TestFixture]
	public class ReportsServiceTests
	{
		[TestFixtureSetUp]
		public void Init()
		{
			ObjectFactory.Initialize(x =>
			{
				x.For<IUsersRepo>().Use<UsersFakeRepo>();
				x.For<IDogsRepo>().Use<DogsFakeRepo>();
			});

            Mapper.CreateMap<dal.User, domain.User>();
			Mapper.CreateMap<dal.Dog, domain.Dog>()
				 .ForMember(dest => dest.ExpositionsMember, src =>
				 {
					 src.MapFrom(t =>
						 t.ExpositionMembers.Select(x => x.Exposition)
					 );
				 })
				 .ForMember(dest => dest.WinsExpositions, src =>
				 {
					 src.MapFrom(t =>
						 t.ExpositionMembers
						 .Where(q => t.ExpositionMembers.SelectMany(qq => qq.ExpositionWinners).Select(y => y.IdExpositionMember).Contains(q.Id))
						 .ToList() == null
						 ? new domain.Exposition[0]
						 :
						 t.ExpositionMembers
						 .Where(q => t.ExpositionMembers.SelectMany(qq => qq.ExpositionWinners).Select(y => y.IdExpositionMember).Contains(q.Id))
						 .ToList()
						 .Select(g => Mapper.Map<dal.Exposition, domain.Exposition>(g.Exposition))
					 );
				 });

            Mapper.CreateMap<dal.DogType, domain.DogType>();

            Mapper.CreateMap<dal.Exposition, domain.Exposition>();
		}

		[Test]
		public void OneUserStatisticTestLimitValues()
		{
			DateTime startDate = new DateTime(2012, 11, 11);
			DateTime endDate = new DateTime(2014, 11, 11);
			int idUser = 1;

			ReportsService reports = new ReportsService();
			var userStatistic = reports.GetUserPaymentStatistic(startDate, endDate, idUser);

			decimal paymentSum = userStatistic
				.Payments
				.Sum(t => t.PaySize);

			Assert.AreEqual(3000, paymentSum);
		}

		[Test]
		public void OneUserStatisticTestAvgValues()
		{
			DateTime startDate = new DateTime(2013, 10, 11);
			DateTime endDate = new DateTime(2014, 11, 11);
			int idUser = 1;

			ReportsService reports = new ReportsService();
			var userStatistic = reports.GetUserPaymentStatistic(startDate, endDate, idUser);

			decimal paymentSum = userStatistic
				.Payments
				.Sum(t => t.PaySize);

			Assert.AreEqual(2000, paymentSum);
		}

		[Test]
		public void OneUserStatisticTestZeroValues()
		{
			DateTime startDate = new DateTime(1991, 10, 11);
			DateTime endDate = new DateTime(1995, 11, 11);
			int idUser = 1;

			ReportsService reports = new ReportsService();
			var userStatistic = reports.GetUserPaymentStatistic(startDate, endDate, 1);

			decimal paymentSum = userStatistic
				.Payments
				.Sum(t => t.PaySize);

			Assert.AreEqual(0, paymentSum);
		}

		[Test]
		public void AllUsersStatisticTestLimitValues()
		{
			DateTime startDate = new DateTime(2012, 11, 11);
			DateTime endDate = new DateTime(2014, 11, 11);
			int idUser1 = 1;
			int idUser2 = 2;

			ReportsService reports = new ReportsService();
			var usersStatistic = reports.GetAllUsersPaymentStatistic(startDate, endDate);

			decimal user1pays = usersStatistic
				.FirstOrDefault(t => t.Id == idUser1)
				.Payments
				.Sum(t => t.PaySize);

			Assert.AreEqual(3000, user1pays);

			decimal user2pays = usersStatistic
				.FirstOrDefault(t => t.Id == idUser2)
				.Payments
				.Sum(t => t.PaySize);

			Assert.AreEqual(1000, user2pays);
		}

		[Test]
		public void AllUsersStatisticTestLimitValuesAll()
		{
			DateTime startDate = new DateTime(2012, 11, 11);
			DateTime endDate = new DateTime(2016, 11, 11);
			int idUser1 = 1;
			int idUser2 = 2;

			ReportsService reports = new ReportsService();
			var usersStatistic = reports.GetAllUsersPaymentStatistic(startDate, endDate);

			decimal user1pays = usersStatistic
				.FirstOrDefault(t => t.Id == idUser1)
				.Payments
				.Sum(t => t.PaySize);

			Assert.AreEqual(3000, user1pays);

			decimal user2pays = usersStatistic
				.FirstOrDefault(t => t.Id == idUser2)
				.Payments
				.Sum(t => t.PaySize);

			Assert.AreEqual(5000, user2pays);
		}

		[Test]
		public void AllUsersStatisticTestEmptyPayments()
		{
			DateTime startDate = new DateTime(2012, 11, 11);
			DateTime endDate = new DateTime(2016, 11, 11);
			int idUser3 = 3;

			ReportsService reports = new ReportsService();
			var usersStatistic = reports.GetAllUsersPaymentStatistic(startDate, endDate);

			var userStatistic = usersStatistic.FirstOrDefault(t => t.Id == idUser3);
			Assert.IsNull(userStatistic);
		}

		[Test]
		public void GetDogVaccinationStatisticTest()
		{
			ReportsService reports = new ReportsService();
			DateTime startPeriod = new DateTime(2011, 10, 11);
			DateTime endPeriod = new DateTime(2012, 10, 11);
			int idDog = 1;
			var dogsStat = reports.GetDogVaccinationStatistic(startPeriod, endPeriod, idDog);

			Assert.IsNotNull(dogsStat);
			Assert.AreEqual(2, dogsStat.Vaccinations.Count());
		}

		[Test]
		public void GetDogVaccinationStatisticTestShortPeriod()
		{
			ReportsService reports = new ReportsService();
			DateTime startPeriod = new DateTime(2011, 10, 11);
			DateTime endPeriod = new DateTime(2011, 10, 12);
			int idDog = 1;
			var dogsStat = reports.GetDogVaccinationStatistic(startPeriod, endPeriod, idDog);

			Assert.IsNotNull(dogsStat);
			Assert.AreEqual(1, dogsStat.Vaccinations.Count());
		}

		[Test]
		public void GetAllDogsVaccinationStatisticTest()
		{
			ReportsService reports = new ReportsService();
			DateTime startPeriod = new DateTime(2011, 10, 11);
			DateTime endPeriod = new DateTime(2014, 10, 11);

			int idDog1 = 1;
			int idDog2 = 2;

			var dogsStat = reports.GetAllDogVaccinationsStatistic(startPeriod, endPeriod).ToList();

			Assert.AreEqual(2, dogsStat.Count);

			var firstDogVaccinations = dogsStat.FirstOrDefault(t => t.IdDog == idDog1);
			Assert.AreEqual(2, firstDogVaccinations.Vaccinations.Count());

			var secondDogVaccinations = dogsStat.FirstOrDefault(t => t.IdDog == idDog2);
			Assert.AreEqual(1, secondDogVaccinations.Vaccinations.Count());
		}

		[Test]
		public void GetAllDogsVaccinationStatisticTestNotAllPeriod()
		{
			ReportsService reports = new ReportsService();
			DateTime startPeriod = new DateTime(2012, 9, 11);
			DateTime endPeriod = new DateTime(2014, 10, 11);

			int idDog1 = 1;
			int idDog2 = 2;

			var dogsStat = reports.GetAllDogVaccinationsStatistic(startPeriod, endPeriod).ToList();

			Assert.AreEqual(2, dogsStat.Count);

			var firstDogVaccinations = dogsStat.FirstOrDefault(t => t.IdDog == idDog1);
			Assert.AreEqual(1, firstDogVaccinations.Vaccinations.Count());

			var secondDogVaccinations = dogsStat.FirstOrDefault(t => t.IdDog == idDog2);
			Assert.AreEqual(1, firstDogVaccinations.Vaccinations.Count());
		}

        [Test]
        public void Test()
        {
            DogsService dogsService = new DogsService();
            var dogs = dogsService.GetAll().ToList();
        }
	}
}
