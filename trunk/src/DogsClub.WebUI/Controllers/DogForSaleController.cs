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
    public class DogForSaleController : Controller
    {
        // GET: DogForSale

        private List<models.Dog> GetAllDog() 
        {
            List<models.DogForSale> allDogForSale = Mapper.Map<List<models.DogForSale>>(ServiceFactory.Instance.DogSalesService.GetAll());
            List<int> idDogForSale = allDogForSale.Select(item => item.IdDog).ToList();
            List<models.Dog> allDogs = Mapper.Map<List<models.Dog>>(ServiceFactory.Instance.DogsService.GetAll());
            List<models.Dog> newListDog = new List<models.Dog>();
            foreach (var item in allDogs)
            {
                if (!idDogForSale.Contains(item.Id))
                {
                    newListDog.Add(item);
                }
            }
            return newListDog;
        }
        public ActionResult Index()
        {
            return PartialView();
        }
        public ActionResult Add()
        {
            ViewData["allDogs"] = GetAllDog();
            return PartialView();
        }


        [HttpPost]
        public ActionResult Add(models.DogForSale newDogForSale)
        {
            if (ModelState.IsValid)
            {
                ViewData["allDogs"] = GetAllDog();
                ServiceFactory.Instance.DogSalesService.Add(newDogForSale.ToDomainModel());
            }
            return PartialView();
        }

        public ActionResult GetAll()
        {

            List<models.DogForSale> allDogForSale = Mapper.Map<List<models.DogForSale>>(ServiceFactory.Instance.DogSalesService.GetAll());
            return PartialView(allDogForSale);
        }
        public ActionResult Remove(int Id)
        {
            if (Id != 0)
            {
                ServiceFactory.Instance.DogSalesService.Remove(Id);
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int Id)
        {
            if (Id != 0) 
            {
                models.DogForSale oldDogSale = Mapper.Map<models.DogForSale>(ServiceFactory.Instance.DogSalesService.GetById(Id));
                ViewData["allDogs"] = Mapper.Map<List<models.Dog>>(ServiceFactory.Instance.DogsService.GetAll()); 
                return View(oldDogSale);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Edit(models.DogForSale dogSale)
        {
            if (ModelState.IsValid)
            {
                ViewData["allDogs"] = Mapper.Map<List<models.Dog>>(ServiceFactory.Instance.DogsService.GetAll());
                ServiceFactory.Instance.DogSalesService.Edit(dogSale.ToDomainModel());
                return RedirectToAction("Index");
            }
            return View(dogSale);
        }
        public ActionResult GetAllDogForSaleHome()
        {
            List<models.DogForSale> allDogForSale = Mapper.Map<List<models.DogForSale>>(ServiceFactory.Instance.DogSalesService.GetAll());
            return View(allDogForSale);
        }
        public ActionResult FullShowItemDogForSale(int idDog)
        {
            if (idDog != 0)
            {
                models.Dog dbUSer = Mapper.Map<models.Dog>(ServiceFactory.Instance.DogsService.GetById(idDog));
               List<models.DogForSale> allDogsForSale = Mapper.Map<List<models.DogForSale>>(ServiceFactory.Instance.DogSalesService.GetAll());
               models.DogForSale itemDogSale = allDogsForSale.FirstOrDefault(item=>item.IdDog == idDog);
               ViewData["DogForSale"] = itemDogSale;
                return View(dbUSer);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}