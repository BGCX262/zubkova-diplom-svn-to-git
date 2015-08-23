using AutoMapper;
using DogsClub.WebUI.Infrastracture;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using models = DogsClub.WebUI.Models;

namespace DogsClub.WebUI.Models
{
    public class Dog : IViewModel<DogsClub.DomainModel.Dog>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым!")]
        public string Name { get; set; }
        [Required(ErrorMessage="Поле не должно быть пустым")]
        public int Age { get; set; }
        public string Description { get; set; }
        public byte[] Avatar { get; set; }
        public string AvatarMimeType { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public int OwnerId { get; set; }
        public Nullable<bool> WasSold { get; set; }
        public Nullable<int> Sex { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public int IdDogType { get; set; }
        public  User User { get; set; }
        public  ICollection<DogsPhoto> DogsPhotos { get; set; }
        public DogType DogType { get; set; }

        public IEnumerable<models.Vaccination> GetDogVaccinations
        {
            get
            {
                IEnumerable<models.Vaccination> modelsVac = Mapper.Map<IEnumerable<models.Vaccination>>(ServiceFactory.Instance.VaccinationsService.GetAll().Where(m => m.IdDog == this.Id));
                if (modelsVac == null)
                    return null;
                return modelsVac;
            }
        }
        
        public string GetSex 
        {
            get
            { 
                if(this.Sex==1)
                {
                    return "Мужской";
                }
                else if (this.Sex == 0)
                {
                    return "Женский";
                }
                return "";
            }
        
        }

        public DomainModel.Dog ToDomainModel()
        {
            return Mapper.Map<DomainModel.Dog>(this);
        }

        public static IEnumerable<models.Dog> GetAllDog()
        {
            var dbTypeDogs = ServiceFactory.Instance.DogsService.GetAll();
            return Mapper.Map<IEnumerable<models.Dog>>(dbTypeDogs);
        }

        public models.User GetUser 
        {
            get
            {
            return Mapper.Map<models.User>(ServiceFactory.Instance.UserService.GetById(this.OwnerId));
            }
        }

        public List<models.ExpositionMember> GetExpositionMember
        {
            get
            {
                List<models.ExpositionMember> allExpMem = Mapper.Map<List<models.ExpositionMember>>(ServiceFactory.Instance.ExpositionMembersService.GetAll());
                return allExpMem.Select(m=>m).Where(t => t.IdDog == this.Id).ToList();
            }
        }
        public List<models.MedicalCare> GetMedicalCare
        {
            get
            {
                List<models.MedicalCare> allExpMem = Mapper.Map<List<models.MedicalCare>>(ServiceFactory.Instance.MedicalCaresService.GetAll());
                return allExpMem.Where(t => t.IdDog == this.Id).ToList();
            }
        }
        public List<models.ExpositionMember> GetWinner
        {
            get
            {
                List<models.ExpositionMember> itemMem = Mapper.Map<List<models.ExpositionMember>>(ServiceFactory.Instance.ExpositionMembersService.GetAll().Where(t=>t.IdDog==this.Id));
                List<models.ExpositionWinner> allWinners = Mapper.Map<List<models.ExpositionWinner>>(ServiceFactory.Instance.ExpositionWinnersService.GetAll());
                List<int> idWinner = allWinners.Select(m=>m.IdExpositionMember).ToList();
                List<models.ExpositionMember> itemWinner = new List<ExpositionMember>();

                foreach (var item in itemMem)
                {
                    if (idWinner.Contains(item.Id)) 
                    {
                        itemWinner.Add(item);
                    }
                }

                return itemWinner;
            }
        }
        public List<models.Vaccination> GetVaccinations
        {
            get
            {
                List<models.Vaccination> allExpMem = Mapper.Map<List<models.Vaccination>>(ServiceFactory.Instance.VaccinationsService.GetAll());
                return allExpMem.Where(t => t.IdDog == this.Id).ToList();
            }
        }
        
    }
}