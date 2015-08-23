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
    public class AvardController : Controller
    {
        // GET: Avard

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            IEnumerable<models.Avard> modelsVac = Mapper.Map<IEnumerable<models.Avard>>(ServiceFactory.Instance.AvardsService.GetAll());
            return PartialView(modelsVac);
        }


        public ActionResult Add()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Add(models.Avard add)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<models.Avard> allAvard = Mapper.Map<IEnumerable<models.Avard>>(ServiceFactory.Instance.AvardsService.GetAll());
                var avard = allAvard.FirstOrDefault(t => t.Name == add.Name);
                if (avard != null) 
                {
                    ViewData["ValueAvard"] = "notNull";
                    return RedirectToAction("Index");
                }
                ServiceFactory.Instance.AvardsService.Add(add.ToDomainModel());
                IEnumerable<models.Dog> modelsVac = Mapper.Map<IEnumerable<models.Dog>>(ServiceFactory.Instance.DogsService.GetAll());
                return PartialView();
            }
            return PartialView();
        }

        public ActionResult Edit(int idAvard)
        {
            if (idAvard != 0)
            {
                models.Avard avard = Mapper.Map<models.Avard>(ServiceFactory.Instance.AvardsService.GetById(idAvard));
                return View(avard);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(models.Avard avard)
        {
            if (ModelState.IsValid)
            {
                ServiceFactory.Instance.AvardsService.Edit(avard.ToDomainModel());
            }
            return RedirectToAction("Index");
        }
        public ActionResult Remove(int avard)
        {
            if (avard != 0)
            {
                models.Avard oldvac = Mapper.Map<models.Avard>( ServiceFactory.Instance.AvardsService.GetById(avard));
                if (oldvac == null)
                    throw new ArgumentNullException();
                ServiceFactory.Instance.AvardsService.Remove(oldvac.Id);
            }
            return RedirectToAction("Index");
        }
        public ActionResult GetAllAvardHome()
        {
            List<models.Avard> allAvard = Mapper.Map<List<models.Avard>>(ServiceFactory.Instance.AvardsService.GetAll().Take(10));
            return PartialView(allAvard);
        }


    }
}