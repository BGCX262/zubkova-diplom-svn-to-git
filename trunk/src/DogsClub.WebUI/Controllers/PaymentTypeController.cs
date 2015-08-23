using AutoMapper;
using DogsClub.WebUI.Infrastracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using models = DogsClub.WebUI.Models;

namespace DogsClub.WebUI.Controllers
{
    public class PaymentTypeController : Controller
    {



        private List<models.PaymentsPeriodType> GetPeriod()
        {
            var res = ServiceFactory.Instance.PaymentTypesService.GetAllPaymentPeriods();
            List<models.PaymentsPeriodType> all = new List<models.PaymentsPeriodType>();

            foreach (var item in res)
            {
                all.Add(new models.PaymentsPeriodType() { Id = item.Id, Name = item.Name });
            }
            return all;
        }
        // GET: PaymentType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            IEnumerable<models.PaymentType> allPaymentType = Mapper.Map<IEnumerable<models.PaymentType>>(ServiceFactory.Instance.PaymentTypesService.GetAll());
            return PartialView(allPaymentType);
        }


        public ActionResult Add()
        {
            ViewData["allPeriod"] = GetPeriod();
            return PartialView();
        }
        [HttpPost]
        public ActionResult Add(models.PaymentType itemPaymentType)
        {
            ViewData["allPeriod"] = GetPeriod();
            if (ModelState.IsValid )
            {
                IEnumerable<models.PaymentType> allPaymentType = Mapper.Map<IEnumerable<models.PaymentType>>(ServiceFactory.Instance.PaymentTypesService.GetAll());
                var avard = allPaymentType.FirstOrDefault(t => t.Name == itemPaymentType.Name && t.IdPeriod == itemPaymentType.IdPeriod);
                if (avard != null)
                {
                    ViewData["ValueAvard"] = "notNull";
                    return RedirectToAction("Index");
                }
                ServiceFactory.Instance.PaymentTypesService.Add(itemPaymentType.ToDomainModel());
                IEnumerable<models.Dog> modelsVac = Mapper.Map<IEnumerable<models.Dog>>(ServiceFactory.Instance.DogsService.GetAll());
                return PartialView();
            }
            return PartialView();
        }

        public ActionResult Edit(int idPaymentType)
        {
            if (idPaymentType != 0)
            {
                models.PaymentType payment = Mapper.Map<models.PaymentType>(ServiceFactory.Instance.PaymentTypesService.GetById(idPaymentType));
                ViewData["allPeriod"] = GetPeriod();
                return View(payment);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(models.PaymentType itemPaymentType)
        {
            ViewData["allPeriod"] = GetPeriod();
            if (ModelState.IsValid )
            {
                ServiceFactory.Instance.PaymentTypesService.Edit(itemPaymentType.ToDomainModel());
                return RedirectToAction("Index");
            }
            return View(itemPaymentType);
        }
        public ActionResult Remove(int idPaymentType)
        {
            if (idPaymentType != 0)
            {
                models.PaymentType oldvac = Mapper.Map<models.PaymentType>(ServiceFactory.Instance.PaymentTypesService.GetById(idPaymentType));
                if (oldvac == null)
                    throw new ArgumentNullException();
                ServiceFactory.Instance.PaymentTypesService.Remove(oldvac.Id);
            }
            return RedirectToAction("Index");
        }
    }
}