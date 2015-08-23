using DogsClub.BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogsClub.WebUI.Infrastracture
{
	public interface IServicesFactory
	{
		IDogsService DogsService { get; }
		IUserService UserService { get; }
		IDogTypesService DogTypesService { get; }
		IExpositionsService ExpositionService { get; }
		IStatisticService StatisticService { get; }
		IReportsService ReportsService { get; }
        IVaccinationsService VaccinationsService { get; }
        IDogsPhotoService DogsPhotoService { get; }
        IExpositionMembersService ExpositionMembersService { get; }
        IExpositionsRequestsService ExpositionRequestsService { get; }
        IMedicalCaresService MedicalCaresService { get; }
        IAvardsService AvardsService { get; }
        IExpositionWinnersService ExpositionWinnersService { get; }
        IDogSalesService DogSalesService { get; }
        IPaymentsService PaymentsService { get; }
        IPaymentTypesService PaymentTypesService { get; }
        INewsService NewsService { get; }
	}
}