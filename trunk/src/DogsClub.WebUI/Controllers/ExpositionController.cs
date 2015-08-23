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
    public class ExpositionController : Controller
    {
        // GET: Exposition
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
              return PartialView();
        }

        [HttpPost]
        public ActionResult Add(models.Exposition exp)
        {
            if (ModelState.IsValid) 
            {
                ServiceFactory.Instance.ExpositionService.Add(exp.ToDomainModel());
                return PartialView();
            }
            return PartialView(exp);
        }
        public ActionResult GetAll()
        {
             IEnumerable<models.Exposition> allExposition = Mapper.Map<IEnumerable<models.Exposition>>(ServiceFactory.Instance.ExpositionService.GetAll());
             return PartialView(allExposition);
        }

        public ActionResult Edit(int Id) 
        {
            if (Id != 0) 
            {
                models.Exposition mdexp = Mapper.Map<models.Exposition>(ServiceFactory.Instance.ExpositionService.GetById(Id));
                return View(mdexp);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(models.Exposition exp)
        {
            if (ModelState.IsValid)
            {
                ServiceFactory.Instance.ExpositionService.Edit(exp.ToDomainModel());
            }
            return View();
        }

        public ActionResult Remove(int Id)
        {
            if (Id == 0)
                throw new ArgumentNullException();
            ServiceFactory.Instance.ExpositionService.Remove(Id);
            return View("Index");
        }

        public ActionResult GetAllExpositionHome()
        {
            IEnumerable<models.Exposition> getAllExposition = Mapper.Map<IEnumerable< models.Exposition>>(ServiceFactory.Instance.ExpositionService.GetAll());
            return PartialView(getAllExposition);
        }

        public ActionResult FullShowItemExposition(int idExposition)
        {
            if (idExposition!=0)
            {
                models.Exposition exp= Mapper.Map<models.Exposition>(ServiceFactory.Instance.ExpositionService.GetById(idExposition));
                return View(exp);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}