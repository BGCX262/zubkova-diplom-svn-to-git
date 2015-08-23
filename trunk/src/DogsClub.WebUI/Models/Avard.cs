using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DogsClub.WebUI.Models
{
    public class Avard
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Поле не может быть пустым")]
        public string Name { get; set; }
        public string Description { get; set; }
        public DomainModel.Avard ToDomainModel()
        {
            return Mapper.Map<DomainModel.Avard>(this);
        }

    }
}