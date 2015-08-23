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
    public class TestSetDataDB
    {
        public TestSetDataDB()
        {
            SetUserDB();
            SetAvatarUserDB();
            SetDogsTypeDB();
            SetDogsDB();
            SetAvatarDogsDB();
        }

        public void SetUserDB()
        {
            for (int i = 1; i <= 10; i++)
            {
                models.User user = new models.User();
                user.FirstName = "Александр" + i;
                user.LastName = "Фролов" + i;
                user.MiddleName = "Владиморович" + i;
                user.Password = "123456";
                user.Phone = "89034567" + i + "Э567";
                user.RegistrationDate = DateTime.Now; 
                user.Email = "User" + i + "@gmail.com";
                user.Address = "Саратов, ул.Чернышевская 123";
                user.IdRole = (int)UserRolesEnum.User;
                user.BirthDate = DateTime.Now;
                user.City = "Саратов" + i; ;
                ServiceFactory.Instance.UserService.CreateUser(user.ToDomainModel());

            }
        }
        public void SetAvatarUserDB()
        {
            var allUser = ServiceFactory.Instance.UserService.GetAll().ToList();
            for (int i = 1; i < allUser.Count; i++)
            {
                byte[] image = System.IO.File.ReadAllBytes("F:\\Зубкова\\src\\DogsClub.WebUI\\Images\\DBAvatarUser.png");
                ServiceFactory.Instance.UserService.ChangeAvatar(allUser[i].Email, image, "image/jpeg");
            }
        }
        public void SetDogsTypeDB() 
        {
           models.DogType dogType = new models.DogType();
           dogType.Name = "Овчарка";
           ServiceFactory.Instance.DogTypesService.Add(dogType.ToDomainModel());
           dogType.Name = "Боксер";
           ServiceFactory.Instance.DogTypesService.Add(dogType.ToDomainModel());
           dogType.Name = "Лабродор";
           ServiceFactory.Instance.DogTypesService.Add(dogType.ToDomainModel());
           dogType.Name = "Сенбернар";
           ServiceFactory.Instance.DogTypesService.Add(dogType.ToDomainModel());
           dogType.Name = "Хаски";
           ServiceFactory.Instance.DogTypesService.Add(dogType.ToDomainModel());
           dogType.Name = "Пекинес";
           ServiceFactory.Instance.DogTypesService.Add(dogType.ToDomainModel());
           dogType.Name = "Пудель";
           ServiceFactory.Instance.DogTypesService.Add(dogType.ToDomainModel());
           dogType.Name = "Бульдог";
           ServiceFactory.Instance.DogTypesService.Add(dogType.ToDomainModel());
           dogType.Name = "Доберман";
           ServiceFactory.Instance.DogTypesService.Add(dogType.ToDomainModel());
           dogType.Name = "Мопс";
           ServiceFactory.Instance.DogTypesService.Add(dogType.ToDomainModel());
        }
        public void SetDogsDB()
        {
           int[] allUsers = ServiceFactory.Instance.UserService.GetAll().Select(t=>t.Id).ToArray();
           int[] countDog = ServiceFactory.Instance.DogTypesService.GetAll().Select(t => t.Id).ToArray();
           for (int i = 0; i < allUsers.Length; i++)
            {
                models.Dog dog = new models.Dog();
                dog.Age = 12 + i;
                dog.Description = "sdfsdfssafsafsa" + i;
                dog.IdDogType = countDog[i];
                dog.Sex = i % 2 == 0 ? 0 : 1;
                dog.OwnerId = allUsers[i];
               dog.Name = "Джексон"+i;
                ServiceFactory.Instance.UserService.AddDog(dog.ToDomainModel(), ServiceFactory.Instance.UserService.GetById(allUsers[i]).Email);
            }
        }
        public void SetAvatarDogsDB()
        {
            int[] allUsers = ServiceFactory.Instance.DogsService.GetAll().Select(t => t.Id).ToArray();
            for (int i = 0; i < allUsers.Length; i++)
            {
                byte[] image = System.IO.File.ReadAllBytes("F:\\Зубкова\\src\\DogsClub.WebUI\\Images\\DBAvatarDog.png");
                ServiceFactory.Instance.DogsService.ChangeAvatar(allUsers[i], image, "image/jpeg");
            }
        }
    }
}