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
    public class ExpositionMemberController : Controller
    {
        public ActionResult Add(int ExpId,int DogId)
        {
            if (ExpId != 0 && DogId!=0)
            {
                IEnumerable<models.ExpositionRequest> oldExpReq = Mapper.Map<IEnumerable<models.ExpositionRequest>>(ServiceFactory.Instance.ExpositionRequestsService.GetAll());
                models.ExpositionRequest item = oldExpReq.FirstOrDefault(t => t.IdDog == DogId && t.IdExposition == ExpId);
                
                if(item == null)
                    ViewData["Null"] = "null";

               models.ExpositionMember dbMember = Mapper.Map<models.ExpositionMember>(ServiceFactory.Instance.ExpositionMembersService.GetAll().ToList().FirstOrDefault(m => m.IdDog == DogId && m.IdExposition == ExpId));
              if (dbMember != null) 
                {
                    ViewData["SecondVeue"] = "SecondValue";
                    return RedirectToAction("GetAllDogs", "ExpositionRequests", new { idExp = ExpId });
                }

                models.ExpositionMember expMem = new models.ExpositionMember();
                expMem.IdDog = DogId;
                expMem.IdExposition = ExpId;
                ServiceFactory.Instance.ExpositionMembersService.Add(expMem.ToDomainModel());
                ServiceFactory.Instance.ExpositionRequestsService.Remove(item.Id);

                return RedirectToAction("GetAllDogs", "ExpositionRequests", new { idExp = ExpId });
            }
            return RedirectToAction("Index", "Exposition");
        }

        public ActionResult GetAllDogs(int idExp)
        {
            if (idExp != 0)
            {
                models.Exposition oldExp = Mapper.Map<models.Exposition>(ServiceFactory.Instance.ExpositionService.GetById(idExp));

                if (oldExp == null && oldExp.GetDogsExpositionMembers.Count == 0)
                    throw new ArgumentNullException();

                return PartialView(oldExp);
            }
            return PartialView("Index", "Exposition");
        }

        public ActionResult Delete(int id)
        {
            if (id == 0)
                throw new ArgumentNullException();
            models.ExpositionMember oldExpMem = Mapper.Map<models.ExpositionMember>(ServiceFactory.Instance.ExpositionMembersService.GetById(id));
            
            if (oldExpMem == null)
                throw new ArgumentNullException();
            ServiceFactory.Instance.ExpositionMembersService.Remove(oldExpMem.Id);
            return RedirectToAction("GetAllDogs", new { idExp = oldExpMem.IdExposition});
        }
    }
}
