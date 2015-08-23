using AutoMapper;
using DogsClub.WebUI.Infrastracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using models = DogsClub.WebUI.Models; 

namespace DogsClub.WebUI.Models
{
    public class UserStatistic
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int NumOfDogsWinners { get; set; }

         public models.User GetUser
         {
             get 
             {
                 return Mapper.Map<models.User>(ServiceFactory.Instance.UserService.GetById(this.Id));
             }
         
         }
    }

}