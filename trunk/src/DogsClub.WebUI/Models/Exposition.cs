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
    public class Exposition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime DateStart { get; set; }
        public string Description { get; set; }
        //public ICollection<Dog> ExpositionMembers { get; set; }
        //public ICollection<Dog> ExpositionRequests { get; set; }
        //public ICollection<Dog> ExpositionWinners { get; set; }

        public DomainModel.Exposition ToDomainModel()
        {
            return Mapper.Map<DomainModel.Exposition>(this);
        }
        public List<models.ExpositionRequest> GetExpositionRequests
        {
            get
            {
                List<models.ExpositionRequest> requests = Mapper.Map<List<models.ExpositionRequest>>(ServiceFactory.Instance.ExpositionRequestsService.GetAll().ToList());
                return requests.Where(x => x.IdExposition == this.Id).ToList();
            }
        }


        public List<models.Dog> GetDogsExpositionRequests
        {
            get
            {
                List<models.ExpositionRequest> requests = Mapper.Map<List<models.ExpositionRequest>>(ServiceFactory.Instance.ExpositionRequestsService.GetAll().ToList());
                var dogs = requests.Where(x => x.IdExposition == this.Id).Select(t => t.IdDog).ToList();
                var domainDogs = ServiceFactory.Instance.DogsService.GetAll().Where(t => dogs.Contains(t.Id)).ToList();
                return new List<models.Dog>(Mapper.Map<IEnumerable<models.Dog>>(domainDogs));

            }
        }
        public List<models.Dog> GetDogsExpositionMembers
        {
            get
            {
                List<models.ExpositionMember> expositionMember = Mapper.Map<List<models.ExpositionMember>>(ServiceFactory.Instance.ExpositionMembersService.GetAll().ToList());
                var dogs = expositionMember.Where(x => x.IdExposition == this.Id).Select(t => t.IdDog).ToList();
                var domainDogs = ServiceFactory.Instance.DogsService.GetAll().Where(t => dogs.Contains(t.Id)).ToList();
                return new List<models.Dog>(Mapper.Map<IEnumerable<models.Dog>>(domainDogs));
            }
        }

        public List<models.ExpositionMember> GetMembers
        {
            get
            {
                List<models.ExpositionMember> AllRequest = Mapper.Map<List<models.ExpositionMember>>(ServiceFactory.Instance.ExpositionMembersService.GetAll().ToList());
                List<models.ExpositionMember> itemExp = AllRequest.Where(x => x.IdExposition == this.Id).ToList();
                return itemExp;
            }
        }
        //вернуть список победителей
        public List<models.ExpositionMember> GetWinners
        {
            get
            {
           
                List<models.ExpositionMember> allMember = Mapper.Map<List<models.ExpositionMember>>(ServiceFactory.Instance.ExpositionMembersService.GetAll().ToList());
                List<models.ExpositionMember> itemExp = allMember.Where(x => x.IdExposition == this.Id).ToList();
                var winners = Mapper.Map<List<models.ExpositionWinner>>(ServiceFactory.Instance.ExpositionWinnersService.GetAll());
                List<int> winnersIds = winners.Select(t => t.IdExpositionMember).ToList();

                List<models.ExpositionMember> listWinners = new List<models.ExpositionMember>();

                foreach (var item in itemExp)
                {
                    if(winnersIds.Contains(item.Id))
                    {
                        listWinners.Add(item);
                    }
                }
                return listWinners;
            }
        }

    }
}