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
    public class DogType
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Поле не должно быть пустым")]
        public string Name { get; set; }

        public DomainModel.DogType ToDomainModel()
        {
            return Mapper.Map<DomainModel.DogType>(this);
        }

        public static  List<models.DogType> GetAllDogType()
        {
            List<models.DogType> DogTypes = new List<models.DogType>();
            var dbTypeDogs = ServiceFactory.Instance.DogTypesService.GetAll();
            foreach (var item in dbTypeDogs)
            {
                DogTypes.Add(Mapper.Map<models.DogType>(item));
            }
            return DogTypes;
        }
     }
}