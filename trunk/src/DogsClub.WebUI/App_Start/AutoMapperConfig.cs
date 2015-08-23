using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using domain = DogsClub.DomainModel;
using models = DogsClub.WebUI.Models;
using dal = DogsClub;

namespace DogsClub.WebUI.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            FromDomainModel();
            FromViewModels();
            FromDalToDomain();
        }

        private static void FromViewModels()
        {
            Mapper.CreateMap<models.User, domain.User>();
            Mapper.CreateMap<models.Dog, domain.Dog>();
            Mapper.CreateMap<models.DogType, domain.DogType>();
            Mapper.CreateMap<models.DogsPhoto, domain.DogsPhoto>();
            Mapper.CreateMap<models.Exposition, domain.Exposition>();
            Mapper.CreateMap<models.Role, domain.Role>();
            Mapper.CreateMap<models.Vaccination, domain.Vaccination>();
            Mapper.CreateMap<models.ExpositionRequest, domain.ExpositionRequest>();
            Mapper.CreateMap<models.ExpositionMember, domain.ExpositionMember>();
            Mapper.CreateMap<models.ExpositionWinner, domain.ExpositionWinner>();
            Mapper.CreateMap<models.MedicalCare, domain.MedicalCare>();
            Mapper.CreateMap<models.DogStatistic, domain.DogStatistic>();
            Mapper.CreateMap<models.UserStatistic, domain.UserStatistic>();
            Mapper.CreateMap<models.DogForSale, domain.DogForSale>();
            Mapper.CreateMap<models.Avard, domain.Avard>();
            Mapper.CreateMap<models.PaymentType, domain.PaymentType>();
            Mapper.CreateMap<models.Payment, domain.Payment>();


            Mapper.CreateMap<domain.User, models.User>();
            Mapper.CreateMap<domain.Dog, models.Dog>();
            Mapper.CreateMap<domain.DogType, models.DogType>();
            Mapper.CreateMap<domain.DogsPhoto, models.DogsPhoto>();
            Mapper.CreateMap<domain.Exposition, models.Exposition>();
            Mapper.CreateMap<domain.Role, models.Role>();
            Mapper.CreateMap<domain.Vaccination, models.Vaccination>();
            Mapper.CreateMap<domain.ExpositionMember, models.ExpositionMember>();
            Mapper.CreateMap<domain.ExpositionRequest, models.ExpositionRequest>();
            Mapper.CreateMap<domain.ExpositionWinner, models.ExpositionWinner>();
            Mapper.CreateMap<domain.Avard, models.Avard>();
            Mapper.CreateMap<domain.MedicalCare, models.MedicalCare>();
            Mapper.CreateMap<domain.DogStatistic, models.DogStatistic>();
            Mapper.CreateMap<domain.UserStatistic, models.UserStatistic>();
            Mapper.CreateMap<domain.DogForSale, models.DogForSale>();
            Mapper.CreateMap<domain.Payment, models.Payment>();
            Mapper.CreateMap<domain.PaymentType, models.PaymentType>();
        }

        private static void FromDomainModel()
        {
            Mapper.CreateMap<domain.PaymentsPeriodType, dal.PaymentsPeriodType>();
            Mapper.CreateMap<domain.User, dal.User>();
            Mapper.CreateMap<domain.Dog, dal.Dog>();
            Mapper.CreateMap<domain.DogType, dal.DogType>();
            Mapper.CreateMap<domain.Exposition, dal.Exposition>();
            Mapper.CreateMap<domain.Vaccination, dal.Vaccination>();
            Mapper.CreateMap<domain.DogsPhoto, dal.DogsPhoto>();
            Mapper.CreateMap<domain.Role, dal.Role>();
            Mapper.CreateMap<domain.ExpositionMember, dal.ExpositionMember>();
            Mapper.CreateMap<domain.ExpositionRequest, dal.ExpositionsRequest>();
            Mapper.CreateMap<domain.ExpositionWinner, dal.ExpositionWinner>();
            Mapper.CreateMap<domain.Avard, dal.Avard>();
            Mapper.CreateMap<domain.MedicalCare, dal.MedicalCare>();
            Mapper.CreateMap<domain.DogForSale, dal.DogsForSale>();
            Mapper.CreateMap<domain.Payment, dal.Payment>();
            Mapper.CreateMap<domain.PaymentType, dal.PaymentType>();
        }

        private static void FromDalToDomain()
        {
            Mapper.CreateMap<dal.News, domain.News>();
            Mapper.CreateMap<dal.PaymentsPeriodType, domain.PaymentsPeriodType>();
            Mapper.CreateMap<dal.PaymentType, domain.PaymentType>();
            Mapper.CreateMap<dal.Payment, domain.Payment>();
            Mapper.CreateMap<dal.DogsForSale, domain.DogForSale>();
            Mapper.CreateMap<dal.ExpositionMember, domain.ExpositionMember>();
            Mapper.CreateMap<dal.ExpositionsRequest, domain.ExpositionRequest>();
            Mapper.CreateMap<dal.ExpositionWinner, domain.ExpositionWinner>();
            Mapper.CreateMap<dal.Avard, domain.Avard>();
            Mapper.CreateMap<dal.MedicalCare, domain.MedicalCare>();

            Mapper.CreateMap<dal.Role, domain.Role>();
            Mapper.CreateMap<dal.Vaccination, domain.Vaccination>();
            Mapper.CreateMap<dal.DogsPhoto, domain.DogsPhoto>();

            Mapper.CreateMap<dal.User, domain.User>();
            Mapper.CreateMap<dal.Dog, domain.Dog>()
                .ForMember(dest => dest.ExpositionsMember, src =>
                {
                    src.MapFrom(t =>
                        t.ExpositionMembers.Select(x => x.Exposition)
                    );
                })
                .ForMember(dest => dest.WinsExpositions, src =>
                {
                    src.MapFrom(t =>
                        t.ExpositionMembers
                        .Where(q => t.ExpositionMembers.SelectMany(qq => qq.ExpositionWinners).Select(y => y.IdExpositionMember).Contains(q.Id))
                        .ToList() == null
                        ? new domain.Exposition[0]
                        :
                        t.ExpositionMembers
                        .Where(q => t.ExpositionMembers.SelectMany(qq => qq.ExpositionWinners).Select(y => y.IdExpositionMember).Contains(q.Id))
                        .ToList()
                        .Select(g => Mapper.Map<dal.Exposition, domain.Exposition>(g.Exposition))
                    );
                });

            Mapper.CreateMap<dal.DogType, domain.DogType>();

            Mapper.CreateMap<dal.Exposition, domain.Exposition>();
                //.ForMember(dest => dest.ExpositionMembers, src =>
                //{
                //    src.MapFrom(t => t.ExpositionMembers == null
                //        ? new domain.Dog[0]
                //        : t.ExpositionMembers.Select(x => Mapper.Map<dal.Dog, domain.Dog>(x.Dog)));
                //})
                //.ForMember(dest => dest.ExpositionRequests, src =>
                //{
                //    src.MapFrom(t => t.ExpositionsRequests == null
                //        ? new domain.Dog[0]
                //        : t.ExpositionsRequests.Select(x => Mapper.Map<dal.Dog, domain.Dog>(x.Dog)));
                //})
                //.ForMember(dest => dest.ExpositionWinners, src =>
                //{
                //    src.MapFrom(t => t.ExpositionMembers == null
                //        ? new domain.Dog[0]
                //        : t.ExpositionMembers.Select(wins => wins.ExpositionWinners) == null
                //            ? new domain.Dog[0]
                //            : t.ExpositionMembers
                //                .SelectMany(x => x.ExpositionWinners)
                //                .Select(q => Mapper.Map<dal.Dog, domain.Dog>(q.ExpositionMember.Dog))
                //    );
                //});
        }
    }
}