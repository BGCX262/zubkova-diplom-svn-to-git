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
    public class ExpositionWinnerController : Controller
    {
        public ActionResult Add(int idExpoition)
      {
            if (idExpoition != 0)
            {
                IEnumerable<models.Exposition> oldExp = Mapper.Map<IEnumerable<models.Exposition>>(ServiceFactory.Instance.ExpositionService.GetAll());
                models.Exposition itemExposition = oldExp.FirstOrDefault(t => t.Id == idExpoition);
                ViewData["allMember"] = itemExposition.GetDogsExpositionMembers;

                List<models.Avard> allAvard = Mapper.Map<List<models.Avard>>(ServiceFactory.Instance.AvardsService.GetAll());
                ViewData["allAvard"] = allAvard;

                ViewData["idExposition"] = itemExposition.Id;

            }
            return View();
        }

        [HttpPost]
        public ActionResult Add(models.ExpositionWinner winner, string idExposition, int idMember)
        {
            if (ModelState.IsValid && idExposition != null && idMember != 0)
            {

                models.Exposition oldExp = Mapper.Map<models.Exposition>(ServiceFactory.Instance.ExpositionService.GetById(Convert.ToInt32(idExposition)));
                List<models.ExpositionWinner> res = new List<models.ExpositionWinner>();

                models.ExpositionMember member = oldExp.GetMembers.FirstOrDefault(item => item.IdExposition == Convert.ToInt32(idExposition) && item.IdDog == idMember);
                ViewData["allMember"] = oldExp.GetDogsExpositionMembers;

                List<models.Avard> allAvard = Mapper.Map<List<models.Avard>>(ServiceFactory.Instance.AvardsService.GetAll());
                ViewData["allAvard"] = allAvard;

                res = member.GetExpositionWinner.Where(item => item.IdExpositionMember == member.Id).ToList();
                if (res.Count != 0)
                {
                    ViewData["SecondVavue"] = "SecondVavue";
                    return View();
                }

                if (oldExp.GetDogsExpositionMembers.Any(t => t.Id == winner.IdExpositionMember))
                {
                    ViewData["NotMember"] = "NullMember";
                    return View();
                }

                winner.IdExpositionMember = member.Id;

                ServiceFactory.Instance.ExpositionWinnersService.Add(winner.ToDomainModel());

                return RedirectToAction("GetAllDogs", new { idExp = Convert.ToInt32(idExposition) });
            }
            return View();
        }

        public ActionResult GetAllDogs(int idExp)
        {
            if (idExp != 0)
            {
                models.Exposition oldExp = Mapper.Map<models.Exposition>(ServiceFactory.Instance.ExpositionService.GetById(idExp));

                return View(oldExp);
            }
            return View("Index", "Exposition");
        }

        public ActionResult Delete(int id)
        {
            if (id == 0)
                throw new ArgumentNullException();
            models.ExpositionWinner oldExpWin = Mapper.Map<models.ExpositionWinner>(ServiceFactory.Instance.ExpositionWinnersService.GetById(id));

            if (oldExpWin == null)
                throw new ArgumentNullException();

            ServiceFactory.Instance.ExpositionWinnersService.Remove(oldExpWin.Id);
            return RedirectToAction("GetAllDogs", new { idExp = oldExpWin.Id });
        }

        public ActionResult Edit(int id, int idExposition)
        {
            if (id != 0 && idExposition != 0)
            {
                models.ExpositionWinner oldWinner = Mapper.Map<models.ExpositionWinner>(ServiceFactory.Instance.ExpositionWinnersService.GetById(id));
                models.Exposition oldExp = Mapper.Map<models.Exposition>(ServiceFactory.Instance.ExpositionService.GetById(idExposition));
                ViewData["allMember"] = oldExp.GetDogsExpositionMembers;
                List<models.Avard> allAvard = Mapper.Map<List<models.Avard>>(ServiceFactory.Instance.AvardsService.GetAll());
                ViewData["allAvard"] = allAvard;
                return View(oldWinner);
            }
            return RedirectToAction("GetAllDogs", new { idExp = Convert.ToInt32(idExposition) });
        }

        [HttpPost]
        public ActionResult Edit(models.ExpositionWinner winner, string idExposition, int idMember)
        {
            if (ModelState.IsValid && idExposition != null && idMember != 0)
            {
                models.Exposition oldExp = Mapper.Map<models.Exposition>(ServiceFactory.Instance.ExpositionService.GetById(Convert.ToInt32(idExposition)));
                models.ExpositionMember member = oldExp.GetMembers.FirstOrDefault(item => item.IdExposition == Convert.ToInt32(idExposition) && item.IdDog == idMember);
                ViewData["allMember"] = oldExp.GetDogsExpositionMembers;

                List<models.Avard> allAvard = Mapper.Map<List<models.Avard>>(ServiceFactory.Instance.AvardsService.GetAll());
                ViewData["allAvard"] = allAvard;

                if (oldExp.GetDogsExpositionMembers.Any(t => t.Id == winner.IdExpositionMember))
                {
                    ViewData["NotMember"] = "NullMember";
                    return View();
                }
                winner.IdExpositionMember = member.Id;
                ServiceFactory.Instance.ExpositionWinnersService.Edit(winner.ToDomainModel());
                return RedirectToAction("GetAllDogs", new { idExp = Convert.ToInt32(idExposition) });
            }
            return View("Index", "Exposition");
        }
    }
}