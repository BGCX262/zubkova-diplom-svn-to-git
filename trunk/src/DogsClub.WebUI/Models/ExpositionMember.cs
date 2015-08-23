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
    public class ExpositionMember
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Поле не должно быть пустым")]
        public int IdExposition { get; set; }
         [Required(ErrorMessage = "Поле не должно быть пустым")]
        public int IdDog { get; set; }

         public DomainModel.ExpositionMember ToDomainModel()
         {
             return Mapper.Map<DomainModel.ExpositionMember>(this);
         }
         public models.Exposition GetExposition
         {
             get
             {
                 List<models.Exposition> AllRequest = Mapper.Map<List<models.Exposition>>(ServiceFactory.Instance.ExpositionService.GetAll().ToList());
                 return AllRequest.FirstOrDefault(x=>x.Id==this.IdExposition);
             }
         }
         public List<models.ExpositionWinner> GetExpositionWinner
         {
             get
             {
                 List<models.ExpositionWinner> AllRequest = Mapper.Map<List<models.ExpositionWinner>>(ServiceFactory.Instance.ExpositionWinnersService.GetAll().ToList());
                 var res = AllRequest.Where(m => m.IdExpositionMember == this.Id).ToList();
                 return res;
             }
         }
         public models.Dog GetDog
         {
             get
             {
                 List<models.Dog> AllDog = Mapper.Map<List<models.Dog>>(ServiceFactory.Instance.DogsService.GetAll().ToList());
                 return AllDog.FirstOrDefault(m => m.Id == this.IdDog);
             }
         }

         public models.ExpositionWinner GetWinner
         {
             get
             {
                 List<models.ExpositionWinner> AllRequest = Mapper.Map<List<models.ExpositionWinner>>(ServiceFactory.Instance.ExpositionWinnersService.GetAll().ToList());
                 var res = AllRequest.FirstOrDefault(m => m.IdExpositionMember == this.Id);
                 return res;
             }
         }


    }
}