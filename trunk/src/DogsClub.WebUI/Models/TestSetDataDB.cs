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
            SetPhotoDogsDB();
            SetExpositionDB();
            SetVaccinationDB();
            SetMedicalCareDB();
            SetExpositionRequestDB();
            SetExpositionMemberDB();
            SetAdverdDB();
            SetExpositionWinnerDB();
            SetDogForSaleDB();
            SetPaymentTypeForSaleDB();
        }

        public void SetUserDB()
        {
            var res = ServiceFactory.Instance.UserService.GetAllRoles();
            for (int j = 0; j < 2; j++)
            {
                for (int i = 1; i <= 10; i++)
                {
                    models.User user = new models.User();
                    user.FirstName = "Александр" + i + j;
                    user.LastName = "Фролов" + i + j;
                    user.MiddleName = "Владиморович" + i + j;
                    user.Password = "123456";
                    user.Phone = "89034567" + i + j + "Э567";
                    user.RegistrationDate = DateTime.Now;
                    user.Email = "User" + i + j + "@gmail.com";
                    user.Address = "Саратов, ул.Чернышевская 123";
                    user.IdRole = (int)UserRolesEnum.User;
                    user.BirthDate = DateTime.Now.Date;
                    user.City = "Саратов" + i; ;
                    user.Sex = i % 2 == 0 ? 0 : 1;
                    ServiceFactory.Instance.UserService.CreateUser(user.ToDomainModel());
                }
            }
        }
        public void SetAvatarUserDB()
        {
            var allUser = ServiceFactory.Instance.UserService.GetAll().ToList();
            for (int i = 1; i < allUser.Count; i++)
            {
                byte[] image = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Images/DBAvatarUser.png"));
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
            int[] allUsers = ServiceFactory.Instance.UserService.GetAll().Select(t => t.Id).ToArray();
            int[] countDog = ServiceFactory.Instance.DogTypesService.GetAll().Select(t => t.Id).ToArray();
            for (int m = 0; m < 2; m++)
            {
                for (int i = 0; i < allUsers.Length; i++)
                {
                    models.Dog dog = new models.Dog();
                    dog.Age = 12 + i + m;
                    dog.Description = "sdfsdfssafsafsa" + i;
                    if (i < 5)
                    {
                        dog.IdDogType = countDog[1];
                    }
                    else if (i < 10)
                    {
                        dog.IdDogType = countDog[2];
                    }
                    else if (i < 14)
                    {
                        dog.IdDogType = countDog[3];
                    }
                    else if (i < 18)
                    {
                        dog.IdDogType = countDog[0];
                    }
                    else if (i < 22)
                    {
                        dog.IdDogType = countDog[4];
                    }
                    else if (i < 27)
                    {
                        dog.IdDogType = countDog[6];
                    }
                    else if (i < 30)
                    {
                        dog.IdDogType = countDog[8];
                    }
                    else if (i < 35)
                    {
                        dog.IdDogType = countDog[9];
                    }
                    dog.Sex = i % 2 == 0 ? 0 : 1;
                    dog.OwnerId = allUsers[i];
                    dog.Name = "Джексон" + i + m;
                    ServiceFactory.Instance.UserService.AddDog(dog.ToDomainModel(), ServiceFactory.Instance.UserService.GetById(allUsers[i]).Email);
                }
            }
        }
        public void SetAvatarDogsDB()
        {
            int[] allUsers = ServiceFactory.Instance.DogsService.GetAll().Select(t => t.Id).ToArray();
            for (int i = 0; i < allUsers.Length; i++)
            {
                byte[] image = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Images/DBAvatarDog.png"));
                ServiceFactory.Instance.DogsService.ChangeAvatar(allUsers[i], image, "image/jpeg");
            }
        }
        public void SetPhotoDogsDB()
        {
            List<models.Dog> newDogsList = models.Dog.GetAllDog().ToList();
            for (int r = 0; r < 5; r++)
            {
                for (int i = 0; i < newDogsList.Count / 2; i++)
                {
                    models.DogsPhoto newPhoto = new models.DogsPhoto();
                    newPhoto.PhotoData = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Images/PhotoDogs.jpg"));
                    newPhoto.MimeType = "image/jpeg";
                    newPhoto.DogId = newDogsList[i].Id;
                    ServiceFactory.Instance.DogsPhotoService.Add(newPhoto.ToDomainModel());
                }
            }
        }
        public void SetExpositionDB()
        {
            int CountUsers = ServiceFactory.Instance.UserService.GetAll().Count();
            for (int i = 0; i < 6; i++)
            {
                models.Exposition exp = new models.Exposition();
                exp.DateStart = DateTime.Now.Date;
                exp.Description = "ОписаниеМнгого ОписаниеМнгого ОписаниеМнгогоОписание Мнгого ОписаниеМнгогоОписа ниеМнгог ОписаниеМнг гоОписаниеМ нгогоОпис ниеМнго гоОписа ниеМнгого";
                exp.Name = "Выставка" + i;
                ServiceFactory.Instance.ExpositionService.Add(exp.ToDomainModel());
            }
        }

        public void SetVaccinationDB()
        {
            List<models.Dog> AllDogs = Mapper.Map<List<models.Dog>>(ServiceFactory.Instance.DogsService.GetAll().ToList());
            for (int r = 0; r < 2; r++)
            {
                for (int i = 0; i < AllDogs.Count(); i++)
                {
                    models.Vaccination vac = new models.Vaccination();
                    vac.IdDog = AllDogs[i].Id;
                    vac.Name = "Прививка" + i + r;
                    vac.DateVacination = DateTime.Now.Date;
                    ServiceFactory.Instance.VaccinationsService.Add(vac.ToDomainModel());
                    r++;
                }
            }
        }
        public void SetMedicalCareDB()
        {
            List<models.Dog> AllDogs = Mapper.Map<List<models.Dog>>(ServiceFactory.Instance.DogsService.GetAll().ToList());
            for (int r = 0; r < 2; r++)
            {
                for (int i = 0; i < AllDogs.Count()/2; i++)
                {
                    models.MedicalCare vac = new models.MedicalCare();
                    vac.IdDog = AllDogs[i].Id;
                    vac.Name = "Осмотр" + i + r;
                    vac.DateCare = DateTime.Now.Date;
                    vac.Description = "Описание" + i * r;
                    ServiceFactory.Instance.MedicalCaresService.Add(vac.ToDomainModel());
                }
            }
        }

        public void SetExpositionRequestDB()
        {
            List<models.Dog> AllDogs = Mapper.Map<List<models.Dog>>(ServiceFactory.Instance.DogsService.GetAll().ToList());
            List<models.Exposition> allExposition = Mapper.Map<List<models.Exposition>>(ServiceFactory.Instance.ExpositionService.GetAll().ToList());
            for (int r = 1; r < 3; r++)
            {
                for (int i = 0; i < allExposition.Count(); i++)
                {
                    models.ExpositionRequest vac = new models.ExpositionRequest();
                    if (r % 2 == 1)
                    {
                        vac.IdDog = AllDogs[i * r +1].Id;
                    }
                    else 
                    {
                        vac.IdDog = AllDogs[i+r+1].Id;
                    }
                    vac.IdExposition = allExposition[i].Id;
                    ServiceFactory.Instance.ExpositionRequestsService.Add(vac.ToDomainModel());
                }
            }
        }
        public void SetExpositionMemberDB()
        {
            List<models.Dog> AllDogs = Mapper.Map<List<models.Dog>>(ServiceFactory.Instance.DogsService.GetAll().ToList());
            List<models.Exposition> allExposition = Mapper.Map<List<models.Exposition>>(ServiceFactory.Instance.ExpositionService.GetAll().ToList());

            for (int r = 0; r < 2; r++)
            {
                for (int i = 0; i < allExposition.Count(); i++)
                {
                    models.ExpositionMember vac = new models.ExpositionMember();
                    if (r % 2 == 0)
                    {
                        vac.IdDog = AllDogs[i].Id;
                    }
                    else 
                    {
                        vac.IdDog = AllDogs[i + r].Id;
                    }
                    vac.IdExposition = allExposition[i].Id;
                    ServiceFactory.Instance.ExpositionMembersService.Add(vac.ToDomainModel());
                }
            }
        }
        public void SetAdverdDB()
        {
            for (int r = 0; r < 6; r++)
            {
                models.Avard newAvard = new models.Avard();
                newAvard.Name = "Награда" + r;
                newAvard.Description = "Описание"+ r;
                ServiceFactory.Instance.AvardsService.Add(newAvard.ToDomainModel());
            }
        }
        public void SetExpositionWinnerDB()
        {
            List<models.Exposition> AllExposition = Mapper.Map<List<models.Exposition>>(ServiceFactory.Instance.ExpositionService.GetAll().ToList());
            List<models.Avard> allAvard =Mapper.Map<List<models.Avard>>(ServiceFactory.Instance.AvardsService.GetAll());

            for (int i = 0; i < AllExposition.Count; i++)
            {
               List<models.ExpositionMember> itemMember = AllExposition[i].GetMembers;
               for (int y = 0; y < 1; y++)
               {
                   models.ExpositionWinner newWinner = new models.ExpositionWinner();
                   newWinner.IdExpositionMember = itemMember[y].Id;
                   newWinner.WinnerPlace = i;
                   newWinner.IdAvard = allAvard[i].Id;
                   ServiceFactory.Instance.ExpositionWinnersService.Add(newWinner.ToDomainModel());
               }
            }
        }
        public void SetDogForSaleDB()
        {
            List<models.Dog> allDog = Mapper.Map<List<models.Dog>>(ServiceFactory.Instance.DogsService.GetAll());
            for (int i = 0; i < allDog.Count/3; i++)
            {
                models.DogForSale newSale = new models.DogForSale();
                newSale.IdDog = allDog[i].Id;
                newSale.Price = 100000;
                newSale.ClubCommission = 5000;
                ServiceFactory.Instance.DogSalesService.Add(newSale.ToDomainModel());
            }
        }
        public void SetPaymentTypeForSaleDB()
        {
           var allPeriod  = ServiceFactory.Instance.PaymentTypesService.GetAllPaymentPeriods();
           List<models.PaymentsPeriodType> allPeriods = new List<models.PaymentsPeriodType>();
           foreach (var item in allPeriod)
           {
               allPeriods.Add(new models.PaymentsPeriodType() { Id=item.Id, Name=item.Name});
           }

           models.PaymentType paym = new models.PaymentType();
           paym.IdPeriod = allPeriods[0].Id;
           paym.Name = "Членский взнос";
           ServiceFactory.Instance.PaymentTypesService.Add(paym.ToDomainModel());

           models.PaymentType paym2 = new models.PaymentType();
           paym2.IdPeriod = allPeriods[0].Id;
           paym2.Name = "Прививки";
           ServiceFactory.Instance.PaymentTypesService.Add(paym2.ToDomainModel());
        }
    }
}