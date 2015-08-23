using DogsClub.DomainModel.ReportsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.BLL.Abstract
{
	public interface IReportsService
	{
		UserPaymentsStatistic GetUserPaymentStatistic(DateTime startPeriod, DateTime endPeriod, int idUser);
		IEnumerable<UserPaymentsStatistic> GetAllUsersPaymentStatistic(DateTime startPeriod, DateTime endPeriod);
		DogStatistic GetDogVaccinationStatistic(DateTime startPeriod, DateTime endPeriod, int idDog);
		IEnumerable<DogStatistic> GetAllDogVaccinationsStatistic(DateTime startPeriod, DateTime endPeriod);
        PaymentTypeStatistic StatisticByPaymentType(DateTime startPeriod, DateTime endPeriod, int idPaymentType);
	}
}
