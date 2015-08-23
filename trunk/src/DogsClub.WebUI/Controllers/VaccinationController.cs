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
    public class VaccinationController : Controller
    {
        // GET: Vaccination
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            IEnumerable<models.Dog> modelsVac = Mapper.Map<IEnumerable<models.Dog>>(ServiceFactory.Instance.DogsService.GetAll());
            return PartialView(modelsVac);
        }


        public ActionResult Add()
        {
            IEnumerable<models.Dog> modelsVac = Mapper.Map<IEnumerable<models.Dog>>(ServiceFactory.Instance.DogsService.GetAll());
            ViewData["allDogs"] = modelsVac;
                return PartialView();
        }
        [HttpPost]
        public ActionResult Add(models.Vaccination add, int idDog)
        {
            if (ModelState.IsValid && idDog != 0)
            {
                add.IdDog = idDog;
                ServiceFactory.Instance.VaccinationsService.Add(add.ToDomainModel());
                IEnumerable<models.Dog> modelsVac = Mapper.Map<IEnumerable<models.Dog>>(ServiceFactory.Instance.DogsService.GetAll());
                ViewData["allDogs"] = modelsVac;
                return PartialView();

            }
            return PartialView(add);
        }

        public ActionResult Edit(int idVaccionation)
        {
            if (idVaccionation != 0)
            {
                models.Vaccination modelsVac = Mapper.Map<models.Vaccination>(ServiceFactory.Instance.VaccinationsService.GetById(idVaccionation));
                return View(modelsVac);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(models.Vaccination edit)
        {
            if (ModelState.IsValid)
            {
                ServiceFactory.Instance.VaccinationsService.Edit(edit.ToDomainModel());
                return RedirectToAction("GetItemDogAllVaccinations", new { idDog = edit.IdDog });

            }
            return RedirectToAction("Index");
        }
        public ActionResult Remove(int idVaccionation)
        {
            if (idVaccionation != 0)
            {
                var oldvac =ServiceFactory.Instance.VaccinationsService.GetById(idVaccionation);
                if (oldvac == null)
                    throw new ArgumentNullException();
                ServiceFactory.Instance.VaccinationsService.Remove(idVaccionation);
                return RedirectToAction("GetItemDogAllVaccinations", new { idDog = oldvac.IdDog });
            }
            return RedirectToAction("Index");
        }
        public ActionResult GetItemDogAllVaccinations(int idDog)
        {
            if(idDog!=0)
            {
               models.Dog oldDog = Mapper.Map<models.Dog>(ServiceFactory.Instance.DogsService.GetById(idDog));

                if(oldDog==null && oldDog.GetDogVaccinations==null)
                    throw new ArgumentNullException();

                return View(oldDog.GetDogVaccinations);
            }
            return RedirectToAction("Index");
        }
        public ActionResult GetAllVaccinationHome()
        {
            List<models.Vaccination> allVac = Mapper.Map<List<models.Vaccination>>(ServiceFactory.Instance.VaccinationsService.GetAll().Take(10));
            return PartialView(allVac);
        }
    }
}