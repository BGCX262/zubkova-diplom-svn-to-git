using AutoMapper;
using DogsClub.WebUI.Infrastracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using models = DogsClub.WebUI.Models;
namespace DogsClub.WebUI.Models
{
    public class PaymentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdPeriod { get; set; }

        public DomainModel.PaymentType ToDomainModel()
        {
            return Mapper.Map<DomainModel.PaymentType>(this);
        }

        public models.PaymentsPeriodType GetPeriod
        {
            get
            {
                var rew = ServiceFactory.Instance.PaymentTypesService.GetAllPaymentPeriods().FirstOrDefault(item => item.Id == this.IdPeriod);
                return new PaymentsPeriodType() { Id = rew.Id, Name = rew.Name};
            }
        }
    }
}