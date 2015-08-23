using AutoMapper;
using DogsClub.WebUI.Infrastracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using models = DogsClub.WebUI.Models;
namespace DogsClub.WebUI.Models
{
    public class DogsPhoto
    {
        public int Id { get; set; }
        public byte[] PhotoData { get; set; }
        public string MimeType { get; set; }
        public int DogId { get; set; }

        public  models.DogsPhoto GetDogPhoto()
        {
            return  Mapper.Map<models.DogsPhoto>(ServiceFactory.Instance.DogsService.GetById(this.DogId));
        }
        public models.Dog GetDog()
        {
            return Mapper.Map<models.Dog>(ServiceFactory.Instance.DogsService.GetById(this.DogId));
        }
        public DomainModel.DogsPhoto ToDomainModel()
        {
            return Mapper.Map<DomainModel.DogsPhoto>(this);
        }

    }
}