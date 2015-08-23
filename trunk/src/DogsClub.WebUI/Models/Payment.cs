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
    public class Payment
    {
            public int Id { get; set; }
            //   [Required(ErrorMessage="Пустое значение!")]
            public decimal PaySize { get; set; }
        [Required(ErrorMessage = "Пустое значение!")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
            public System.DateTime PayDate { get; set; }
            public string Description { get; set; }
            public int UserId { get; set; }
            public int IdType { get; set; }

            public DomainModel.Payment ToDomainModel()
            {
                return Mapper.Map<DomainModel.Payment>(this);
            }

            public models.PaymentType GetType 
            {
                get 
                {
                   List<models.PaymentType> item = Mapper.Map<List<models.PaymentType>>(ServiceFactory.Instance.PaymentTypesService.GetAll());
                   return item.FirstOrDefault(t => t.Id == this.IdType);
                }           
            }
            public models.User GetUser
            {
                get
                {
                    List<models.User> item = Mapper.Map<List<models.User>>(ServiceFactory.Instance.UserService.GetAll());
                    return item.FirstOrDefault(t => t.Id == this.UserId);
                }
            }



    }
}