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
    public class ExpositionRequest
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Поле не должно быть пустым")]
        public int IdExposition { get; set; }
       [Required(ErrorMessage = "Поле не должно быть пустым")]
        public int IdDog { get; set; }

       public DomainModel.ExpositionRequest ToDomainModel()
       {
           return Mapper.Map<DomainModel.ExpositionRequest>(this);
       }


    }
}