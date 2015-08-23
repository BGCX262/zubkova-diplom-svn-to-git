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
    public class ExpositionRequestsController : Controller
    {
        [Authorize]
        public ActionResult Add(int idExposition)
        {

            if (User != null && User.Identity.IsAuthenticated && idExposition!= 0) 
            {
                models.User authorUSer = Mapper.Map<models.User>(ServiceFactory.Instance.UserService.GetByEmail(User.Identity.Name));
                models.Exposition exposition = Mapper.Map<models.Exposition>(ServiceFactory.Instance.ExpositionService.GetById(idExposition));
                List<models.ExpositionMember> allMember = exposition.GetMembers;
                List<models.ExpositionRequest> allRequest = exposition.GetExpositionRequests;
                
                List<int> IdReq = allRequest.Select(t=>t.IdDog).ToList();
                foreach (var item in allMember) { IdReq.Add(item.IdDog); };
              
                List<models.Dog> newDog = new List<models.Dog>();
                
                List<models.Dog> newList = new List<models.Dog>();
                foreach (var item in authorUSer.Dogs)
                {
                    if(!IdReq.Contains(item.Id))
                    {
                        newDog.Add(item);
                    }
                }
                
                ViewData["Dogs"] = newDog;
                ViewData["idExposition"] = idExposition;

                return View();        
            }
            return RedirectToAction("Index","Home");
        }
        [Authorize]
        [HttpPost]
        public ActionResult Add(int idDog, int IdExposition)
        {
            if (idDog != 0 && IdExposition!=0)
            {
                models.ExpositionRequest newRequer = new models.ExpositionRequest();
                newRequer.IdDog = idDog;
                newRequer.IdExposition = IdExposition;
                ServiceFactory.Instance.ExpositionRequestsService.Add(newRequer.ToDomainModel());
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult GetAllDogs(int idExp)
        {
            if (idExp!=0)
            {
               models.Exposition oldExp = Mapper.Map<models.Exposition>(ServiceFactory.Instance.ExpositionService.GetById(idExp));
               return PartialView(oldExp);
            }  
            return PartialView("Index","Exposition");
        }


        public ActionResult Remove(int idDog, int ExpId)
        {
            if (idDog == 0 && ExpId!=0)
                throw new ArgumentNullException();

            models.Exposition oldExpReq = Mapper.Map<models.Exposition>(ServiceFactory.Instance.ExpositionService.GetById(ExpId));
            models.ExpositionRequest itemReque = oldExpReq.GetExpositionRequests.FirstOrDefault(m => m.IdDog == idDog);

            ServiceFactory.Instance.ExpositionRequestsService.Remove(itemReque.Id);
            return RedirectToAction("GetAllDogs", new { idExp = ExpId });
        }
    }
}