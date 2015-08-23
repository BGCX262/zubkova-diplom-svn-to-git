using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DogsClub.BLL;
using System.Web.Security;

namespace DogsClub.WebUI.Models
{
    public class Authorization
    {
       [Required(ErrorMessage = "Не допустимо пустое значение")]
        public string UserLogin { get; set; }

       [Required(ErrorMessage = "Не допустимо пустое значение")]
       [StringLength(16, MinimumLength = 6, ErrorMessage = "Не менее 6 и не более 16")]
       [DataType(DataType.Password, ErrorMessage = "Должен быть пароль")]
        public string UserPassword { get; set; }

       internal bool AuthenticationUsers()
       {
           DogsClub.BLL.UsersService isUser = new DogsClub.BLL.UsersService();
           if (isUser.VerifyUser(this.UserPassword,this.UserLogin)) 
           {
               FormsAuthentication.RedirectFromLoginPage(this.UserLogin, true);
               return true;
           }
           return false;
       }
       internal static void LogOut()
       {
           FormsAuthentication.SignOut();
       }

    }
}