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
    public class MedicalCare
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Поле не может быть пустым")]
        public string Name { get; set; }
         [Required(ErrorMessage = "Поле не может быть пустым")]
         [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime DateCare { get; set; }
        public string Description { get; set; }
        public int IdDog { get; set; }
        public models.Dog GetDog 
        {
            get 
            {
               return Mapper.Map<models.Dog>(ServiceFactory.Instance.DogsService.GetById(this.IdDog));
            }
        }
        public DomainModel.MedicalCare ToDomainModel()
        {
            return Mapper.Map<DomainModel.MedicalCare>(this);
        }
    }
}