using AutoMapper;
using DogsClub.WebUI.Infrastracture;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using models = DogsClub.WebUI.Models;

namespace DogsClub.WebUI.Controllers
{
    public class ImagesController : Controller
    {
        // GET: Images
        public ActionResult Galery()
        {
            return View();
        }

        private byte[] ConvertHttpPostedFileBaseTOByte(HttpPostedFileBase file)
        {
            byte[] data;
            using (Stream inputStream = file.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }
            return data;
        }
        public FileResult GetAvatar(string NameUser)
        {
            ViewData.Add("ReturnididUser", NameUser);
            var dbUser = ServiceFactory.Instance.UserService.GetByEmail(NameUser);
            DogsClub.WebUI.Models.User user = Mapper.Map<DogsClub.WebUI.Models.User>(dbUser);
            if (user == null) throw new InvalidOperationException("user not found");
            if (user.Avatar == null || user.Avatar.Length == 0)
            {
                byte[] image = System.IO.File.ReadAllBytes(Server.MapPath("~/Images/avatar.jpg"));
                return File(image, "image/jpeg");
            }
            return File(user.Avatar, user.AvatarMimeType);
        }

        [HttpPost]
        public ActionResult GetAvatar(HttpPostedFileBase file, string NameUser)
        {
            string[] TypeFormat = new string[] { "image/jpeg", "image/jpg", "image/png" };
            if (TypeFormat.Contains(file.ContentType))
                ServiceFactory.Instance.UserService.ChangeAvatar(NameUser, ConvertHttpPostedFileBaseTOByte(file), file.ContentType);
            return RedirectToAction("PersonalCabinet", "Account", new { email = NameUser });
        }
        public FileResult GetDogsAvatar(int IdDog)
        {
            if (IdDog == 0)
                throw new ArgumentNullException();

            var db = ServiceFactory.Instance.DogsService.GetById(IdDog);
            DogsClub.WebUI.Models.Dog dog = Mapper.Map<DogsClub.WebUI.Models.Dog>(db);
            if (dog.Avatar == null || dog.Avatar.Length == 0)
            {
                byte[] image = System.IO.File.ReadAllBytes(Server.MapPath("~/Images/AvatarDogs.jpg"));
                return File(image, "image/jpeg");
            }
            return File(dog.Avatar, dog.AvatarMimeType);
        }

        [HttpPost]
        public ActionResult GetDogsAvatar(HttpPostedFileBase file, int IdDog)
        {
            if (IdDog == 0)
                throw new ArgumentNullException();

            string[] TypeFormat = new string[] { "image/jpeg", "image/jpg", "image/png" };
            if (TypeFormat.Contains(file.ContentType))
                ServiceFactory.Instance.DogsService.ChangeAvatar(IdDog, ConvertHttpPostedFileBaseTOByte(file), file.ContentType);
            return RedirectToAction("Edit", "Dogs", new { Id = IdDog });
        }

        public ActionResult AllPhotoDogsUser()
        {
            var user = ServiceFactory.Instance.UserService.GetByEmail(User.Identity.Name);
            models.User newUser = Mapper.Map<models.User>(user);
            ViewData["AllDogs"] = newUser.Dogs;
            return View();
        }

        [HttpPost]
        public ActionResult AllPhotoDogsUser(int IdDog)
        {
            if (User.Identity.Name == null)
                throw new ArgumentNullException();
            var user = ServiceFactory.Instance.UserService.GetByEmail(User.Identity.Name);
            DogsClub.WebUI.Models.User newUser = Mapper.Map<DogsClub.WebUI.Models.User>(user);
            ViewData["AllDogs"] = newUser.Dogs;

            models.Dog checkDog = newUser.Dogs.ToList().FirstOrDefault(m => m.Id == IdDog);

            if (checkDog != null)
            {
                return View(checkDog.DogsPhotos.ToList());
            }
            return View();
        }
        public FileResult GetPhotoDogs(int IdPhoto)
        {
            models.DogsPhoto photo = Mapper.Map<models.DogsPhoto>(ServiceFactory.Instance.DogsPhotoService.GetById(IdPhoto));
            if (photo == null) throw new InvalidOperationException("user not found");
            return File(photo.PhotoData, photo.MimeType);
        }
        public ActionResult PrintPhoto(int IdPhoto)
        {
            if (IdPhoto == 0)
                throw new ArgumentNullException();
            models.DogsPhoto newPhoto = Mapper.Map<models.DogsPhoto>(ServiceFactory.Instance.DogsPhotoService.GetById(IdPhoto));

            if (newPhoto == null)
                throw new ArgumentNullException();

            return View(newPhoto);
        }

        public ActionResult Delete(int IdPhoto)
        {
            if (IdPhoto == 0)
                throw new ArgumentNullException();
            models.DogsPhoto photo = Mapper.Map<models.DogsPhoto>(ServiceFactory.Instance.DogsPhotoService.GetById(IdPhoto));
            return View(photo);
        }
        [HttpPost]
        public ActionResult Delete(models.Confern check, int Id)
        {
            if (check == null || Id == 0)
                throw new ArgumentNullException();

            if (check.Confirm == true)
                ServiceFactory.Instance.DogsPhotoService.Remove(Id);

            return RedirectToAction("AllPhotoDogsUser");
        }
        public ActionResult ChengePhoto(int IdPhoto)
        {
            if (IdPhoto == 0)
                throw new ArgumentNullException();
            ViewData["AllDogs"] = Mapper.Map<IEnumerable<models.Dog>>(ServiceFactory.Instance.DogsService.GetAll());
            models.DogsPhoto photoDog = Mapper.Map<models.DogsPhoto>(ServiceFactory.Instance.DogsPhotoService.GetById(IdPhoto));
            return View(photoDog);
        }
        [HttpPost]
        public ActionResult ChengePhoto(models.DogsPhoto photo, int CheckDog)
        {
            if (ModelState.IsValid && CheckDog != 0)
            {
                models.DogsPhoto oldPhoto = Mapper.Map<models.DogsPhoto>(ServiceFactory.Instance.DogsPhotoService.GetById(photo.Id));
                oldPhoto.DogId = CheckDog;
                ServiceFactory.Instance.DogsPhotoService.Edit(oldPhoto.ToDomainModel());
                return RedirectToAction("PrintPhoto", new { IdPhoto = photo.Id });
            }
            return View(photo);
        }
        public ActionResult AddPhotoDogs()
        {
            if (User.Identity.Name == null)
                throw new ArgumentNullException();
            
           models.User allDogs = Mapper.Map<models.User>(ServiceFactory.Instance.UserService.GetByEmail(User.Identity.Name));

           ViewData["AllDog"] = allDogs.Dogs;
            return View();
        }
        [HttpPost]
        public ActionResult AddPhotoDogs(HttpPostedFileBase file, int idDog)
        {
            string[] TypeFormat = new string[] { "image/jpeg", "image/jpg", "image/png" };
            if (file != null && idDog != null)
            {
                if (TypeFormat.Contains(file.ContentType))
                {
                    models.DogsPhoto newPhoto = new models.DogsPhoto();
                    newPhoto.DogId = idDog;
                    newPhoto.MimeType = file.ContentType;
                    newPhoto.PhotoData = ConvertHttpPostedFileBaseTOByte(file);
                    ServiceFactory.Instance.DogsPhotoService.Add(newPhoto.ToDomainModel());
                    return RedirectToAction("AllPhotoDogsUser");
                }
                return View();
            }
            return View();
        }

        public ActionResult GaleryPhoto()
        {
            IEnumerable<models.DogsPhoto> allPhoto = Mapper.Map<IEnumerable<models.DogsPhoto>>(ServiceFactory.Instance.DogsPhotoService.GetAll());
            return View(allPhoto.Take(12));
        }
    }
}

