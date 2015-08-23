using DogsClub.BLL;
using DogsClub.BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogsClub.WebUI.Infrastracture
{
    public sealed class ServiceFactory : IServicesFactory
    {
        private ServiceFactory() { }

        static ServiceFactory() { }

        private static ServiceFactory _instance = new ServiceFactory();

        public static ServiceFactory Instance
        {
            get
            {
                return _instance;
            }
        }

        private IDogsService _dogsService = null;
        private IUserService _usersService = null;
        private IDogTypesService _dogTypesService = null;
        private IExpositionsService _expositionService = null;
        private IStatisticService _statisticService = null;
        private IReportsService _reportsService = null;
        private IVaccinationsService _vaccinationsService = null;
        private IDogsPhotoService _dogsPhotoService = null;
        private IExpositionMembersService _expositionMembersService = null;
        private IExpositionsRequestsService _expositionRequestsService = null;
        private IExpositionWinnersService _expositionWinnersService = null;
        private IAvardsService _avardsService = null;
        private IMedicalCaresService _medcalCaresService = null;
        private IDogSalesService _dogSalesService = null;
        private IPaymentsService _paymentsService = null;
        private IPaymentTypesService _paymentTypesService = null;
        private INewsService _newsService = null;

        public IDogsService DogsService
        {
            get
            {
                if (_dogsService == null)
                {
                    _dogsService = new DogsService();
                }
                return _dogsService;
            }
        }

        public IUserService UserService
        {
            get
            {
                if (_usersService == null)
                {
                    _usersService = new UsersService();
                }
                return _usersService;
            }
        }

        //породы собак
        public IDogTypesService DogTypesService
        {
            get
            {
                if (_dogTypesService == null)
                {
                    _dogTypesService = new DogTypesService();
                }
                return _dogTypesService;
            }
        }

        //Выставки
        public IExpositionsService ExpositionService
        {
            get
            {
                if (_expositionService == null)
                {
                    _expositionService = new ExpositionService();
                }
                return _expositionService;
            }
        }

        public IStatisticService StatisticService
        {
            get
            {
                if (_statisticService == null)
                {
                    _statisticService = new StatisticService();
                }
                return _statisticService;
            }
        }

        public IReportsService ReportsService
        {
            get
            {
                if (_reportsService == null)
                {
                    _reportsService = new ReportsService();
                }
                return _reportsService;
            }
        }

        public IVaccinationsService VaccinationsService
        {
            get
            {
                if (_vaccinationsService == null)
                {
                    _vaccinationsService = new VaccinationsService();
                }
                return _vaccinationsService;
            }
        }

        public IDogsPhotoService DogsPhotoService
        {
            get
            {
                if (_dogsPhotoService == null)
                {
                    _dogsPhotoService = new DogsPhotoService();
                }
                return _dogsPhotoService;
            }
        }


        public IExpositionMembersService ExpositionMembersService
        {
            get
            {
                if (_expositionMembersService == null)
                {
                    _expositionMembersService = new ExpositionMembersService();
                }
                return _expositionMembersService;
            }
        }

        public IExpositionsRequestsService ExpositionRequestsService
        {
            get
            {
                if (_expositionRequestsService == null)
                {
                    _expositionRequestsService = new ExpositionsRequestsService();
                }
                return _expositionRequestsService;
            }
        }

        public IMedicalCaresService MedicalCaresService
        {
            get
            {
                if (_medcalCaresService == null)
                {
                    _medcalCaresService = new MedicalCaresService();
                }
                return _medcalCaresService;
            }
        }

        public IAvardsService AvardsService
        {
            get
            {
                if (_avardsService == null)
                {
                    _avardsService = new AvardsService();
                }
                return _avardsService;
            }
        }

        public IExpositionWinnersService ExpositionWinnersService
        {
            get
            {
                if (_expositionWinnersService == null)
                {
                    _expositionWinnersService = new ExpositionWinnersService();
                }
                return _expositionWinnersService;
            }
        }

        public IDogSalesService DogSalesService
        {
            get
            {
                if (_dogSalesService == null)
                {
                    _dogSalesService = new DogSalesService();
                }
                return _dogSalesService;
            }
        }


        public IPaymentsService PaymentsService
        {
            get
            {
                if (_paymentsService == null)
                {
                    _paymentsService = new PaymentsService();
                }
                return _paymentsService;
            }
        }

        public IPaymentTypesService PaymentTypesService
        {
            get
            {
                if (_paymentTypesService == null)
                {
                    _paymentTypesService = new PaymentTypesService();
                }
                return _paymentTypesService;
            }
        }

        public INewsService NewsService
        {
            get
            {
                if (_newsService == null)
                {
                    _newsService = new NewsService();
                }
                return _newsService;
            }
        }
    }
}