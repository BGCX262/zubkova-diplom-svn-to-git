using DogsClub.Abstract;
using DogsClub.BLL.Abstract;
using DogsClub.DomainModel.ReportsEntities;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.BLL
{
    public class ReportsService : IReportsService
    {
        public UserPaymentsStatistic GetUserPaymentStatistic(DateTime startPeriod, DateTime endPeriod, int idUser)
        {
            using (var usersRepo = ObjectFactory.GetInstance<IUsersRepo>())
            {
                var user = usersRepo.GetById(idUser);
                return CreateUserPaymentsStatisticEntity(user, startPeriod, endPeriod);
            }
        }

        public IEnumerable<UserPaymentsStatistic> GetAllUsersPaymentStatistic(DateTime startPeriod, DateTime endPeriod)
        {
            using (var usersRepo = ObjectFactory.GetInstance<IUsersRepo>())
            {
                return usersRepo
                    .GetAll()
                    .ToList()
                    .Select(t => CreateUserPaymentsStatisticEntity(t, startPeriod, endPeriod))
                    .Where(t => t.Payments != null && t.Payments.Any());
            }
        }

        private UserPaymentsStatistic CreateUserPaymentsStatisticEntity(User user, DateTime startPeriod, DateTime endPeriod)
        {
            return new UserPaymentsStatistic()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                StartPeriod = startPeriod,
                EndPeriod = endPeriod,
                Payments = new List<UserPayment>(user
                    .Payments
                    .Where(t => t.PayDate >= startPeriod && t.PayDate <= endPeriod)
                    .Select(t => new UserPayment()
                    {
                        PayDate = t.PayDate,
                        PaySize = t.PaySize,
                        PayType = t.PaymentType.Name
                    }))
            };
        }

        public DogStatistic GetDogVaccinationStatistic(DateTime startPeriod, DateTime endPeriod, int idDog)
        {
            using (var dogsRepo = ObjectFactory.GetInstance<IDogsRepo>())
            {
                var dog = dogsRepo.GetById(idDog);
                return CreateDogStatisticEntity(startPeriod, endPeriod, dog);
            }
        }

        public IEnumerable<DogStatistic> GetAllDogVaccinationsStatistic(DateTime startPeriod, DateTime endPeriod)
        {
            using (var dogsRepo = ObjectFactory.GetInstance<IDogsRepo>())
            {
                return dogsRepo
                    .GetAll()
                    .ToList()
                    .Select(t => CreateDogStatisticEntity(startPeriod, endPeriod, t))
                    .Where(t => t.Vaccinations != null && t.Vaccinations.Any());
            }
        }

        private DogStatistic CreateDogStatisticEntity(DateTime startPeriod, DateTime endPeriod, Dog dog)
        {
            return new DogStatistic()
            {
                Age = dog.Age,
                DogType = dog.DogType.Name,
                IdDog = dog.Id,
                Name = dog.Name,
                OwnerFirstName = dog.User.FirstName,
                OwnerId = dog.User.Id,
                OwnerLastName = dog.User.LastName,
                OwnerMiddleName = dog.User.MiddleName,
                Sex = dog.Sex == null ? "Не указан" : (dog.Sex.Value == 1 ? "Кабель" : "Сучка"),
                Vaccinations = new List<DogVaccinationStatistic>(dog
                    .Vaccinations
                    .Where(t => t.DateVacination >= startPeriod && t.DateVacination <= endPeriod)
                    .Select(t => new DogVaccinationStatistic()
                {
                    Date = t.DateVacination,
                    Name = t.Name
                }))
            };
        }

        public PaymentTypeStatistic StatisticByPaymentType(DateTime startPeriod, DateTime endPeriod, int idPaymentType)
        {
            using (var paymentTypesRepo = ObjectFactory.GetInstance<IPaymentTypesRepo>())
            {
                var paymentType = paymentTypesRepo.GetById(idPaymentType);
                if (paymentType == null)
                {
                    throw new InvalidOperationException("Payment Type not found");
                }

                return new PaymentTypeStatistic()
                    {
                        EndPeriod = endPeriod,
                        IdPaymentType = paymentType.Id,
                        PaymentName = paymentType.Name,
                        StartPeriod = startPeriod,
                        Payments = paymentType
                                    .Payments
                                    .Select(t => new ConcretteUserPay()
                                    {
                                        IdUser = t.UserId,
                                        LastName = t.User.LastName,
                                        MiddleName = t.User.MiddleName,
                                        Name = t.User.FirstName,
                                        PayDate = t.PayDate,
                                        PaySize = t.PaySize
                                    })
                    };
            }
        }
    }
}
