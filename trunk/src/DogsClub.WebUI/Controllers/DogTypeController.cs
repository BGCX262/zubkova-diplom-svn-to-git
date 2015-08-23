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
    public class DogTypeController : Controller
    {
        // GET: DogType

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAll()
        {
            IEnumerable <models.DogType> listDogType = Mapper.Map<IEnumerable<models.DogType>>(ServiceFactory.Instance.DogTypesService.GetAll());
            return PartialView(listDogType);
        }

        public ActionResult Add()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Add(models.DogType dogType)
        {
            if (ModelState.IsValid) 
            {
                ServiceFactory.Instance.DogTypesService.Add(dogType.ToDomainModel());
            }
            return PartialView(dogType);
        }

      public ActionResult Edit(int id)
        {
            if (id == 0)
                throw new ArgumentNullException();
                models.DogType dt = Mapper.Map<models.DogType>(ServiceFactory.Instance.DogTypesService.GetById(id));
                return View(dt);
        }
        [HttpPost]
        public ActionResult Edit(models.DogType dog)
        {
            if (ModelState.IsValid)
            {
                ServiceFactory.Instance.DogTypesService.Edit(dog.ToDomainModel());
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            if (id == 0)
               throw new ArgumentNullException();
            ServiceFactory.Instance.DogTypesService.Remove(id);
            return RedirectToAction("Index","DogType");
        }
        public ActionResult GetAllDogTypesHome()
        {
            List<models.DogType> TopDog = Mapper.Map<List<models.DogType>>(ServiceFactory.Instance.DogTypesService.GetAll().Take(10));
            return PartialView(TopDog);
        }



    }
}