using DogsClub.WebUI.Infrastracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using models = DogsClub.WebUI.Models;

namespace DogsClub.WebUI.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public static List<models.Role> GetAllRoles
        //{
        //    get 
        //    {
        //        List<models.Role> allrole = new List<Role>();
        //        string [] res = ServiceFactory.Instance.UserService.GetAllRoles();
        //        for (int i = 0; i < res.Length; i++)
        //        {
        //            allrole.Add(
        //                new models.Role{
        //                Id = i+1,
        //                Name = res[i]
        //                });
			 
        //        }
        //        return allrole;
        //    }
        //}
    }
}