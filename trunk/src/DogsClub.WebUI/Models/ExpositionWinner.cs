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
    public class ExpositionWinner
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Поле не может быть пустым")]
        public int IdExpositionMember { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public int IdAvard { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public int WinnerPlace { get; set; }
        public DomainModel.ExpositionWinner ToDomainModel()
        {
            return Mapper.Map<DomainModel.ExpositionWinner>(this);
        }
        public models.Avard GetAvard
          {
              get
              {
                  List<models.Avard> AllRequest = Mapper.Map<List<models.Avard>>(ServiceFactory.Instance.AvardsService.GetAll().ToList());
                  models.Avard getAvard = AllRequest.FirstOrDefault(t => t.Id == this.IdAvard);
                  return getAvard;
            }
        }

        public models.Dog GetDog
        {
            get
            {
                var member = ServiceFactory.Instance.ExpositionMembersService.GetById(this.IdExpositionMember);
                return Mapper.Map<models.Dog>(ServiceFactory.Instance.DogsService.GetById(member.IdDog));
            }
        }

        //public int idDog
        //{
        //    get
        //    {
        //        var member = ServiceFactory.Instance.ExpositionMembersService.GetById(this.IdExpositionMember);
        //        return Mapper.Map<models.Dog>(ServiceFactory.Instance.DogsService.GetById(member.IdDog)).Id;
        //    }
        //}
        //public models.Exposition GetDog
        //{
        //    get
        //    {
        //        var member = ServiceFactory.Instance.ExpositionMembersService.GetById(this.);
        //        return Mapper.Map<models.Dog>(ServiceFactory.Instance.DogsService.GetById(member.IdDog));
        //    }
        //}
    }
}