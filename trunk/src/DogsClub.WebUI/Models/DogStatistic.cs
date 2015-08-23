using AutoMapper;
using DogsClub.WebUI.Infrastracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using models = DogsClub.WebUI.Models;
namespace DogsClub.WebUI.Models
{
    public class DogStatistic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DogType { get; set; }
        public int NumOfVictories { get; set; }

        public models.Dog GetDog 
        {
            get
            {
                return Mapper.Map<models.Dog>(ServiceFactory.Instance.DogsService.GetById(this.Id));
            
            }
        
        }
    }

}