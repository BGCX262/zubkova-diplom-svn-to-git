using AutoMapper;
using DogsClub.Abstract;
using DogsClub.DomainModel.ReportsEntities;
using DogsClub.Fake;
using DogsClub.WebUI.Infrastracture;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using models = DogsClub.WebUI.Models;

namespace DogsClub.WebUI.Controllers
{
    public class ReportsController : Controller
    {
		private readonly IServicesFactory _factory = ServiceFactory.Instance;

        //public ReportsController()
        //{
        //    ObjectFactory.Initialize(x =>
        //    {
        //        x.For<IUsersRepo>().Use<UsersFakeRepo>();
        //        x.For<IDogsRepo>().Use<DogsFakeRepo>();
        //        x.For<IPaymentTypesRepo>().Use<PayTypesFakeRepo>();
        //    });
        //}

        public ActionResult Index()
        {
            return PartialView();
        }

        #region all dogs vaccinations
        public ActionResult AllDogsVaccination()
		{
			return View();
		}
	
        [HttpPost]
		public ActionResult AllDogsVaccinationData(DateTime? startPeriod, DateTime? endPeriod)
		{
            if(startPeriod == null || endPeriod == null)
            {
                return RedirectToAction("AllDogsVaccination");
            }
             List<models.Vaccination> allVaccination  = Mapper.Map<List<models.Vaccination>>(ServiceFactory.Instance.VaccinationsService.GetAll());
             List<models.Vaccination> itemAllVac = allVaccination.Where(item => item.DateVacination >= startPeriod && item.DateVacination <= endPeriod).ToList();
             ViewData["startPeriod"] = startPeriod;
             ViewData["endPeriod"] = endPeriod;
            return View(itemAllVac);
		}
        #endregion

        #region concrette dog vaccination
        public ActionResult DogVaccination()
        {
            List<models.Dog> allDog = Mapper.Map<List<models.Dog>>(ServiceFactory.Instance.DogsService.GetAll());
            ViewData["allDog"] = allDog;
            return View();
        }

        [HttpPost]
        public ActionResult DogVacccinationData(DateTime? startPeriod, DateTime? endPeriod, int idDog)
        {
            if(startPeriod == null || endPeriod == null || idDog == null)
            {
                return RedirectToAction("DogVaccination");
            }
            //var data = _factory.ReportsService.GetDogVaccinationStatistic(startPeriod.Value, endPeriod.Value, idDog.Value);
            List<models.Vaccination> allVaccionation = Mapper.Map<List<models.Vaccination>>(ServiceFactory.Instance.VaccinationsService.GetAll());
            List<models.Vaccination> itemAllVac = allVaccionation.Where(item => item.DateVacination >= startPeriod && item.DateVacination <= endPeriod && item.IdDog == idDog).ToList();
            models.Dog itemDog = Mapper.Map<models.Dog>(ServiceFactory.Instance.DogsService.GetById(idDog));
            ViewData["itemDog"] = itemDog;
            ViewData["startPeriod"] = startPeriod;
            ViewData["endPeriod"] = endPeriod;
            var jsonData = itemAllVac
                .Select(t => new
                {
                    Year = t.DateVacination.Value.Year,
                    Name = t.Name
                })
                .GroupBy(t => t.Year)
                .Select(t => new
                {
                    label = t.Key + " Год",
                    value = t.Count()
                });

            ViewData["jsonData"] = Json(jsonData.ToArray());

            return View(itemAllVac);
        }

        #endregion

        #region concrette user payments
        public ActionResult UserPayment()
        {
            List<models.User> allUser = Mapper.Map<List<models.User>>(ServiceFactory.Instance.UserService.GetAll());
            ViewData["allUsers"] = allUser;
            return View();
        }

        [HttpPost]
        public ActionResult UserPaymentReport(DateTime? startPeriod, DateTime? endPeriod, int idUser)
        {
            if(startPeriod == null || endPeriod == null || idUser == null)
            {
                return RedirectToAction("UserPayment");
            }
            //UserPaymentsStatistic report = _factory.ReportsService.GetUserPaymentStatistic(startPeriod.Value, endPeriod.Value, idUser.Value);
            ViewData["startPeriod"] = startPeriod;
            ViewData["endPeriod"] = endPeriod;
            List<models.Payment> allPayment = Mapper.Map<List<models.Payment>>(ServiceFactory.Instance.PaymentsService.GetAll()).ToList();
            List<models.Payment> itemUserAllPayments = allPayment.Where(t => t.UserId == idUser).ToList();
            models.User user = Mapper.Map<models.User>(ServiceFactory.Instance.UserService.GetById(idUser));
            ViewData["user"] = user;
            var jsonData = itemUserAllPayments
                .GroupBy(t => t.IdType)
                .Select(t => new
                {
                    label = t.Key,
                    value = t.Sum(x => x.PaySize)
                })
                .ToArray();
            var yearsGrouping = itemUserAllPayments
                .GroupBy(t => t.PayDate.Year)
                .Select(t => new
                {
                    y = t.Key.ToString(),
                    a = t.Sum(x => x.PaySize)
                })
                .ToArray();
            ViewData["jsonData"] = jsonData;
            ViewData["yearsGrouping"] = yearsGrouping;
            return View(itemUserAllPayments);




    //        var jsonData = report
    //.Payments
    //.GroupBy(t => t.PayType)
    //.Select(t => new
    //{
    //    label = t.Key,
    //    value = t.Sum(x => x.PaySize)
    //})
    //.ToArray();
    //        var yearsGrouping = report
    //            .Payments
    //            .GroupBy(t => t.PayDate.Year)
    //            .Select(t => new
    //            {
    //                y = t.Key.ToString(),
    //                a = t.Sum(x => x.PaySize)
    //            })
    //            .ToArray();
    //        ViewData["jsonData"] = jsonData;
    //        ViewData["yearsGrouping"] = yearsGrouping;
    //        return View(report);
        }
        #endregion

        #region all users payments
        public ActionResult AllUsersPayments()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AllUsersPaymentsReport(DateTime? startPeriod, DateTime? endPeriod)
        {
            if(startPeriod == null || endPeriod == null)
            {
                return RedirectToAction("AllUsersPayments");
            }
            ViewData["startPeriod"] = startPeriod;
            ViewData["endPeriod"] = endPeriod;
            List<models.Payment> allPayment = Mapper.Map<List<models.Payment>>(ServiceFactory.Instance.PaymentsService.GetAll());
            List<models.Payment> checkPayment = allPayment.Where(m => m.PayDate >= startPeriod && m.PayDate <= endPeriod).ToList();
            return View(checkPayment);
        }
        #endregion

        public ActionResult PaymentTypeReport()
        {
            List<models.PaymentType> allPaymentType = Mapper.Map<List<models.PaymentType>>(ServiceFactory.Instance.PaymentTypesService.GetAll());
            ViewData["allPaymentType"] = allPaymentType;
            return View();
        }

        public ActionResult PaymentTypeReportData(DateTime? startPeriod, DateTime? endPeriod, int idPaymentType)
        {
            if(startPeriod == null || endPeriod == null || idPaymentType == null)
            {
                return RedirectToAction("PaymentTypeReport");
            }
            List<models.Payment> allPayment = Mapper.Map<List<models.Payment>>(ServiceFactory.Instance.PaymentsService.GetAll());
            List<models.Payment> checkPaiment = allPayment.Where(item => item.PayDate >= startPeriod && item.PayDate <= endPeriod && item.IdType == idPaymentType).ToList();
            ViewData["startPeriod"] = startPeriod;
            ViewData["endPeriod"] = endPeriod;
            models.PaymentType itemType = Mapper.Map<models.PaymentType>(ServiceFactory.Instance.PaymentTypesService.GetById(idPaymentType));
            ViewData["itemType"] = itemType;
            return View(checkPaiment);
        }
    }
}




  //#region concrette dog vaccination
  //      public ActionResult DogVaccination()
  //      {
  //          return View();
  //      }

  //      [HttpPost]
  //      public ActionResult DogVacccinationData(DateTime? startPeriod, DateTime? endPeriod, int? idDog)
  //      {
  //          if(startPeriod == null || endPeriod == null || idDog == null)
  //          {
  //              return RedirectToAction("DogVaccination");
  //          }
  //          var data = _factory.ReportsService.GetDogVaccinationStatistic(startPeriod.Value, endPeriod.Value, idDog.Value);
  //          var jsonData = data
  //              .Vaccinations
  //              .Select(t => new
  //              {
  //                  Year = t.Date.Value.Year,
  //                  Name = t.Name
  //              })
  //              .GroupBy(t => t.Year)
  //              .Select(t => new
  //              {
  //                  label = t.Key + " Год",
  //                  value = t.Count()
  //              });

  //          ViewData["jsonData"] = Json(jsonData.ToArray());

  //          return View(data);
  //      }

  //      #endregion