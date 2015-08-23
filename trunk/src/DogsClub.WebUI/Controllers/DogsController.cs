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
    public class DogsController : Controller
    {
        public ActionResult Add()
        {
             ViewData["DogType"] =models.DogType.GetAllDogType();
             ViewData["sexDog"] = models.SexDog.GetAllSexDogs();
            return View();
        }

        [HttpPost]
        public ActionResult Add(models.Dog newDog, string GetTypeDog, string GetSexDog)
        {
            if (GetTypeDog != null && GetSexDog != null) 
            {
                var user = ServiceFactory.Instance.UserService.GetByEmail(User.Identity.Name);
                newDog.OwnerId = user.Id;
                newDog.IdDogType = Convert.ToInt32(GetTypeDog);
                newDog.Sex = Convert.ToInt32(GetSexDog);
                if (ModelState.IsValid && user!=null)
                {
                    ServiceFactory.Instance.UserService.AddDog(newDog.ToDomainModel(), User.Identity.Name);
                    return RedirectToAction("GetAllDogsUSer", "Dogs");
                }
                return View(newDog);         
            }
            return View(newDog); 
         }
        public ActionResult GetAllDogsUSer()
        {
            string userEmail = User.Identity.Name;
            if (string.IsNullOrWhiteSpace(userEmail))
                throw new ArgumentNullException();
            var dbUser = ServiceFactory.Instance.UserService.GetByEmail(userEmail);
            if (dbUser != null) 
            {
                models.User newUser = Mapper.Map<models.User>(dbUser);
                return View(newUser.Dogs);
            }
            return View();
        }
        public ActionResult Edit(int Id)
        {
            models.Dog modelDog=Mapper.Map<models.Dog>(ServiceFactory.Instance.DogsService.GetById(Id));
            if (modelDog != null) 
            {
                ViewData["DogType"] = models.DogType.GetAllDogType();
                ViewData["sexDog"] = models.SexDog.GetAllSexDogs();
                return View(modelDog);
            }
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public ActionResult Edit(models.Dog editDog, string GetTypeDogs, string GetSexDog)
        {
            if (GetTypeDogs != null && GetSexDog != null)
            {
                var user = ServiceFactory.Instance.UserService.GetByEmail(User.Identity.Name);
                editDog.OwnerId = user.Id;
                editDog.IdDogType = Convert.ToInt32(GetTypeDogs);
                editDog.Sex = Convert.ToInt32(GetSexDog);
                if (ModelState.IsValid && user != null)
                {
                    ServiceFactory.Instance.DogsService.Edit(editDog.ToDomainModel());
                    return RedirectToAction("GetAllDogsUSer", "Dogs");
                }
            }
            return View(editDog.Id);
        }

        public ActionResult Delete(int Id)
        {
            ViewData["Delete"] = Mapper.Map<models.Dog>(ServiceFactory.Instance.DogsService.GetById(Id));
            return View(Mapper.Map<models.Dog>(ServiceFactory.Instance.DogsService.GetById(Id)));
        }

        [HttpPost]
        public ActionResult Delete(DogsClub.WebUI.Models.Confern value, string Id)
        {
            if (value != null && Convert.ToInt16(Id)!= 0)
            {
                if (value.Confirm == false)
                    return RedirectToAction("Delete", (Id));
                ServiceFactory.Instance.DogsService.Remove(Convert.ToInt16(Id));
                return RedirectToAction("GetAllDogsUSer");
            }
            return RedirectToAction("GetAllDogsUSer");
        }

        public ActionResult Print(int Id) 
        {
            ViewData["DogType"] = models.DogType.GetAllDogType();
            ViewData["sexDog"] = models.SexDog.GetAllSexDogs();
            if (Id==0)
                 throw new ArgumentNullException();
           models.Dog db = Mapper.Map<models.Dog>(ServiceFactory.Instance.DogsService.GetById(Id));
           return View(db);
        }


        public ActionResult TopDog() 
        {
            IEnumerable<models.DogStatistic> allTopDog = Mapper.Map<IEnumerable<models.DogStatistic>>(ServiceFactory.Instance.StatisticService.GetDogsStatistic());
            return PartialView(allTopDog);
        }

        public ActionResult FullShowItemDog(int idDog)
        {
            if (idDog != 0)
            {
                models.Dog dbUSer = Mapper.Map<models.Dog>(ServiceFactory.Instance.DogsService.GetById(idDog));
                return View(dbUSer);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult FullShowAllDogs()
        {
            List<models.Dog> allUsers = Mapper.Map<List<models.Dog>>(ServiceFactory.Instance.DogsService.GetAll());
            return View(allUsers);
        }
    }
}