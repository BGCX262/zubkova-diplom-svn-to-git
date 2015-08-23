using DogsClub.Abstract;
using DogsClub.Concrete;
using DogsClub.WebUI.Infrastracture;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogsClub.WebUI.App_Start
{
	public class StructureMapConfig
	{
		public static void Configure()
		{
			ObjectFactory.Initialize(x =>
				{
					x.For<IUsersRepo>().Use<UsersRepo>();
					x.For<IDogsRepo>().Use<DogsRepo>();
					x.For<IDogTypesRepo>().Use<DogTypesRepo>();
					x.For<IExpositionMembersRepo>().Use<ExpositionMembersRepo>();
					x.For<IExpositionRequests>().Use<ExpositionRequestsRepo>();
					x.For<IExpositionsRepo>().Use<ExpositionsRepo>();
					x.For<IExpositionWinnersRepo>().Use<ExpositionWinnersRepo>();
                    x.For<IDogsPhotosRepo>().Use<DogsPhotosRepo>();
                    x.For<IVaccinationsRepo>().Use<VaccinationsRepo>();
                    x.For<IMedicalCaresRepo>().Use<MedicalCaresRepo>();
                    x.For<IAvardsRepo>().Use<AvardsRepo>();
                    x.For<IDogSalesRepo>().Use<DogSalesRepo>();
                    x.For<IPaymentTypesRepo>().Use<PaymentTypesRepo>();
                    x.For<IPaymentsRepo>().Use<PaymentsRepo>();
					x.For<INewsRepo>().Use<NewsRepo>();
				});
		}
	}
}