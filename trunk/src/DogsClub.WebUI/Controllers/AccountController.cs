using AutoMapper;
using DogsClub.WebUI.Infrastracture;
using DogsClub.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using models = DogsClub.WebUI.Models;

namespace DogsClub.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IServicesFactory serviceFactory = ServiceFactory.Instance;
        public ActionResult Login(string ReturnUrl)
        {
            if (string.IsNullOrWhiteSpace(ReturnUrl))
            {
                ReturnUrl = "";
            }
            ViewData.Add("ReturnUrl", ReturnUrl);
            return View();
        }
        [HttpPost]
        public ActionResult Login(Authorization user, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                if (!user.AuthenticationUsers())
                {
                    if (!string.IsNullOrWhiteSpace(ReturnUrl))
                    {
                        ViewData["User"] = "Логин-пароль не совпадают";
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                   var oldUser =  ServiceFactory.Instance.UserService.GetByEmail(user.UserLogin);
                   Session["SessionEmail"] = oldUser.Email.ToString();
                   var q = Session["SessionEmail"].ToString();
                   if (oldUser!=null)
                       return RedirectToAction("PersonalCabinet", "Account", new { id = oldUser.Id });
                }
            }
            return View(user);
        }
        public ActionResult LogOut()
        {
          return View();
        }

        [HttpPost]
        public ActionResult LogOut(Confern model)
        {
            if (ModelState.IsValid)
            {
                if (model.Confirm)
                {
                    Authorization.LogOut();
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Registration()
        {
            ViewData["sexDog"] = models.SexDog.GetAllSexDogs();
            return View();
        }
        [HttpPost]
        public ActionResult Registration(models.User NewUser, string GetSexDog)
        {
            if (ModelState.IsValid && !serviceFactory.UserService.IsEmailExists(NewUser.Email) && GetSexDog!=null)
            {
                NewUser.RegistrationDate = DateTime.Now;
                NewUser.IdRole = (int)UserRolesEnum.User;
                NewUser.Sex=Convert.ToInt32(GetSexDog);
                serviceFactory.UserService.CreateUser(NewUser.ToDomainModel());
                return RedirectToAction("Login");
            }
            return View(NewUser);
        }

        [Authorize]
        public ActionResult PersonalCabinet()
        {
            if (User.Identity != null) 
            {
                ViewData["sexDog"] = models.SexDog.GetAllSexDogs();
                var dbUser = serviceFactory.UserService.GetByEmail(User.Identity.Name);
                DogsClub.WebUI.Models.User users = Mapper.Map<DogsClub.WebUI.Models.User>(dbUser);   
                if (users!=null)
                    return View(users);
            }
            return RedirectToAction("Index", "Account");
        }
        public ActionResult UserStatus()
        {
            return PartialView();
        }
        [Authorize(Roles="Администратор")]
        public ActionResult IsAdmin()
        {
            IEnumerable<models.User> allUser = Mapper.Map<IEnumerable<models.User>>(ServiceFactory.Instance.UserService.GetAll());
            return View(allUser);
        }

        public ActionResult Search(string valueString)
        {
            List<models.User> allUser = new List<models.User>();
            if (!string.IsNullOrWhiteSpace(valueString))
            {
                string[] split = valueString.Split(new Char[] { ' ' });
                List<models.User> allUsers = Mapper.Map<List<models.User>>(ServiceFactory.Instance.UserService.GetAll().ToList());
              
                if (split.Length < 3 && split.Length>0) 
                {
                    if (split.Length == 3) 
                    {
                        foreach (var item in allUsers)
                        {
                            if (split[0].Trim() != "" && item.LastName == split[0] && split[1].Trim() != "" && item.FirstName == split[1] && split[2].Trim() != "" && item.MiddleName == split[2]) 
                            {
                                allUser.Add(item);
                                return View(allUser);
                            }
                        }
                    }
                    if (split.Length == 2)
                    {
                        foreach (var item in allUsers)
                        {
                            if (split[0].Trim() != "" && item.LastName == split[0] && split[1].Trim() != "" && item.FirstName == split[1])
                            {
                                allUser.Add(item);
                                return View(allUser);
                            }
                        }
                    }
                    if (split.Length == 1)
                    {
                        foreach (var item in allUsers)
                        {
                            if (split[0].Trim() != "" && item.LastName == split[0] )
                            {
                                allUser.Add(item);
                                return View(allUser);
                            }
                        }
                    }
                    if (split.Length == 1)
                    {

                            if (split[0].Trim() != "")
                            {
                                ViewData["allDog"]= Mapper.Map<List<models.Dog>>(ServiceFactory.Instance.DogsService.GetAll().Where(item => item.Name == split[0]));
                                return View(allUser);
                            }
                    }                  
                }
            }
            return View(allUser);
        }
    }
}