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
    public class PaymentController : Controller
    {
        // GET: Payment

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
            List<models.PaymentType> allPaymentType = Mapper.Map<List<models.PaymentType>>(ServiceFactory.Instance.PaymentTypesService.GetAll());
            List<models.User> allUser = Mapper.Map<List<models.User>>(ServiceFactory.Instance.UserService.GetAll());
            ViewData["allPaymentType"] = allPaymentType;
            ViewData["allUser"] = allUser;
            return PartialView();
        }

        [HttpPost]
        public ActionResult Add(models.Payment payment)
        {
            List<models.PaymentType> allPaymentType = Mapper.Map<List<models.PaymentType>>(ServiceFactory.Instance.PaymentTypesService.GetAll());
            List<models.User> allUser = Mapper.Map<List<models.User>>(ServiceFactory.Instance.UserService.GetAll());
            ViewData["allPaymentType"] = allPaymentType;
            ViewData["allUser"] = allUser;
            if (ModelState.IsValid)
            {
                List<models.Payment> allPayment = Mapper.Map<List<models.Payment>>(ServiceFactory.Instance.PaymentsService.GetAll());
                ServiceFactory.Instance.PaymentsService.Add(payment.ToDomainModel());
            }
            return PartialView(payment);
        }

        public ActionResult GetAll()
        {
            IEnumerable<models.Payment> allMed = Mapper.Map<IEnumerable<models.Payment>>(ServiceFactory.Instance.PaymentsService.GetAll());
            return PartialView(allMed);
        }

        public ActionResult Edit(int id)
        {
            if (id != 0)
            {
                List<models.PaymentType> allPaymentType = Mapper.Map<List<models.PaymentType>>(ServiceFactory.Instance.PaymentTypesService.GetAll());
                List<models.User> allUser = Mapper.Map<List<models.User>>(ServiceFactory.Instance.UserService.GetAll());
                ViewData["allPaymentType"] = allPaymentType;
                ViewData["allUser"] = allUser;

                models.Payment payment = Mapper.Map<models.Payment>(ServiceFactory.Instance.PaymentsService.GetById(id));
                return View(payment);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(models.Payment payment)
        {
            List<models.PaymentType> allPaymentType = Mapper.Map<List<models.PaymentType>>(ServiceFactory.Instance.PaymentTypesService.GetAll());
            List<models.User> allUser = Mapper.Map<List<models.User>>(ServiceFactory.Instance.UserService.GetAll());
            ViewData["allPaymentType"] = allPaymentType;
            ViewData["allUser"] = allUser;
            if (ModelState.IsValid)
            {
                ServiceFactory.Instance.PaymentsService.Edit(payment.ToDomainModel());
                return RedirectToAction("Index");
            }
            return View(payment);
        }

        public ActionResult Delete(int id)
        {
            if (id == 0)
                throw new ArgumentNullException();

            models.Payment payment = Mapper.Map<models.Payment>(ServiceFactory.Instance.PaymentsService.GetById(id));

            if (payment != null)
            {
                ServiceFactory.Instance.MedicalCaresService.Remove(payment.Id);
            }

            return RedirectToAction("Index");
        }
    }
}