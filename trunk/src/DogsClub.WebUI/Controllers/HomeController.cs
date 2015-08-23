using DogsClub.DomainModel;
using DogsClub.WebUI.Infrastracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using models = DogsClub.WebUI.Models;

namespace DogsClub.WebUI.Controllers
{
    public class HomeController : Controller
    {
		private readonly IServicesFactory serviceFactory = ServiceFactory.Instance;

        public HomeController()
        {
            if (ServiceFactory.Instance.UserService.GetAll().ToList().Count < 2)
            {
                TestSetDataDB setdb = new TestSetDataDB();
            }
        }
        public ActionResult Index()
        {
			//пример использования
            //var items = serviceFactory.NewsService.GetNewsForPeriod(NewsPeriodEnum.All).ToList();
            return View();
        }

        //[HttpPost]
        //public ActionResult Index(models.User user)
        //{
        //    serviceFactory.UserService.CreateUser(user.ToDomainModel());
        //    return View(user);
        //}

        public ActionResult TestDataPicker()
        {
            return View();
        }
    }
}