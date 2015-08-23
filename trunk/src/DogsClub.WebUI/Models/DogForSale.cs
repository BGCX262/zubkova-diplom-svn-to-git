using AutoMapper;
using DogsClub.WebUI.Infrastracture;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using models = DogsClub.WebUI.Models;

namespace DogsClub.WebUI.Models
{
    public class DogForSale
    {
        public int Id { get; set; }
         [DisplayName("Sale")]
        [Required(ErrorMessage = "Не допустимо пустое значение")]
        public decimal Price { get; set; }
        [DisplayName("%")]
        public decimal ClubCommission { get; set; }
        public int IdDog { get; set; }
        public DomainModel.DogForSale ToDomainModel()
        {
            return Mapper.Map<DomainModel.DogForSale>(this);
        }
        public models.Dog GetDog
        {
            get {  return Mapper.Map<models.Dog>(ServiceFactory.Instance.DogsService.GetById(this.IdDog)); }
        }
    }
}