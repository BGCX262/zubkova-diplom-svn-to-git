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
    public class MedicalCareController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
            IEnumerable<models.Dog> allDog = Mapper.Map<IEnumerable<models.Dog>>(ServiceFactory.Instance.DogsService.GetAll());
            ViewData["AllDog"] = allDog;
            return PartialView();
        }

       [HttpPost]
        public ActionResult Add( models.MedicalCare med, int IdDog)
        {
            if (ModelState.IsValid && IdDog != 0)
            {
                med.IdDog = IdDog;
                ServiceFactory.Instance.MedicalCaresService.Add(med.ToDomainModel());
            }
            return PartialView();
        }

           public ActionResult GetAll()
           {
               IEnumerable<models.MedicalCare> allMed = Mapper.Map<IEnumerable<models.MedicalCare>>(ServiceFactory.Instance.MedicalCaresService.GetAll());
               return PartialView(allMed);
           }

        public ActionResult Edit(int id)
        {
            if (id != 0)
            {
                IEnumerable<models.Dog> allDog = Mapper.Map<IEnumerable<models.Dog>>(ServiceFactory.Instance.DogsService.GetAll());
                ViewData["AllDog"] = allDog;

                models.MedicalCare med = Mapper.Map<models.MedicalCare>(ServiceFactory.Instance.MedicalCaresService.GetById(id));
                return View(med);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int idDog, models.MedicalCare med)
        {
            if (ModelState.IsValid && idDog != 0)
            { 
                med.IdDog=idDog;
                ServiceFactory.Instance.MedicalCaresService.Edit(med.ToDomainModel());
            }
           return View();
        }

        public ActionResult Delete(int id)
        {
            if (id == 0)
                throw new ArgumentNullException();
            models.MedicalCare med = Mapper.Map<models.MedicalCare>(ServiceFactory.Instance.MedicalCaresService.GetById(id));

            if (med != null)
            {
                ServiceFactory.Instance.MedicalCaresService.Remove(med.Id);
            }
            
            return RedirectToAction("Index");
        }
    }
}
