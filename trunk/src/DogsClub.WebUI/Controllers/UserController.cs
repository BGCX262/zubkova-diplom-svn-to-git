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
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserInfo()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Edit(models.User newUser, string GetSexDog)
        {
            if (ModelState.IsValid) 
            {
                newUser.Sex = Convert.ToInt32(GetSexDog);
                ServiceFactory.Instance.UserService.Edit(newUser.ToDomainModel());
            }
            return RedirectToAction("PersonalCabinet", "Account", new { email = newUser.Email});
        }
        public ActionResult Delete(int id)
        {
            if (id!=0)
            {
                var oldUSer = ServiceFactory.Instance.UserService.GetById(id);
            }
            return RedirectToAction("Index","Home");
        }

        public ActionResult DeleteUsers(int IdUser)
        {
            if (IdUser == 0)
                throw new ArgumentNullException();

            ServiceFactory.Instance.UserService.Remove(IdUser);
            return RedirectToAction("IsAdmin","Account");
        }
        public ActionResult DefaultPassword(int IdUser)
        {
            if (IdUser == 0)
                throw new ArgumentNullException();
             models.User old = Mapper.Map<models.User>(ServiceFactory.Instance.UserService.GetById(IdUser));
             old.Password = "123456";
             ServiceFactory.Instance.UserService.Edit(old.ToDomainModel());
            return RedirectToAction("IsAdmin","Account");
        }

        public ActionResult EditUserAdmin(int IdUser)
        {
            if (IdUser == 0)
                throw new ArgumentNullException();
            models.User editUser = Mapper.Map<models.User>(ServiceFactory.Instance.UserService.GetById(IdUser));
            if (editUser == null)
                throw new ArgumentNullException();
             ViewData["AllRoles"] = Mapper.Map<IEnumerable<models.Role>>(ServiceFactory.Instance.UserService.GetAllRolesEntities());
            ViewData["sexDog"] = models.SexDog.GetAllSexDogs();
            return View(editUser);
        }
        [HttpPost]
        public ActionResult EditUserAdmin(models.User user, int[] checkedRole, string GetSexDog)
        {
            if (ModelState.IsValid && GetSexDog!=null)
            {
                user.Sex = Convert.ToInt32(GetSexDog);
                ServiceFactory.Instance.UserService.Edit(user.ToDomainModel());
                ServiceFactory.Instance.UserService.SetUserRoles(checkedRole, user.Id);
                ViewData["sexDog"] = models.SexDog.GetAllSexDogs();
                ViewData["AllRoles"] = Mapper.Map<IEnumerable<models.Role>>(ServiceFactory.Instance.UserService.GetAllRolesEntities());
                return RedirectToAction("IsAdmin","Account"); 
            }
            return View(user);
        }
        public ActionResult TopUser()
        {
            IEnumerable<models.UserStatistic> topUser = Mapper.Map<IEnumerable<models.UserStatistic>>(ServiceFactory.Instance.StatisticService.GetUsersStatistic());
            return PartialView(topUser);
        }
        public ActionResult FullShowItemUser(int idUser)
        {
            if (idUser != 0) 
            {
                models.User dbUSer = Mapper.Map<models.User>(ServiceFactory.Instance.UserService.GetById(idUser));
                return View(dbUSer);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult FullShowAllUsers()
        {
            List<models.User> allUsers = Mapper.Map<List<models.User>>(ServiceFactory.Instance.UserService.GetAll());
            return View(allUsers);
        }

        public ActionResult Payment()
        {

            if (User != null && User.Identity.IsAuthenticated)
            {
                models.User allUsers = Mapper.Map<models.User>(ServiceFactory.Instance.UserService.GetByEmail(User.Identity.Name));
                return View(allUsers.GetPayment);
            }
            return RedirectToAction("Login", "Account");

        }
    }
}