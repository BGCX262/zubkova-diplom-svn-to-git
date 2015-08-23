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
	public class User : IViewModel<DogsClub.DomainModel.User>
	{
		public int Id { get; set; }
        [Required(ErrorMessage = "Не допустимо пустое значение")]
		public string FirstName { get; set; }
         [Required(ErrorMessage = "Не допустимо пустое значение")]
		public string LastName { get; set; }
         [Required(ErrorMessage = "Не допустимо пустое значение")]
		public string MiddleName { get; set; }
          [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime BirthDate { get; set; }
         [Required(ErrorMessage = "Не допустимо пустое значение")]
         [DataType(DataType.EmailAddress)]
		public string Email { get; set; }
         [Required(ErrorMessage = "Не допустимо пустое значение")]
         [DataType(DataType.PhoneNumber)]
		public string Phone { get; set; }
         [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime RegistrationDate { get; set; }
        [Required(ErrorMessage = "Не допустимо пустое значение")]
        [StringLength(16,MinimumLength=6,ErrorMessage="Не менее 6 и не более 16")]
        [DataType(DataType.Password,ErrorMessage="Должен быть пароль")]
		public string Password { get; set; }
         [Required(ErrorMessage = "Не допустимо пустое значение")]
		public string City { get; set; }
         [Required(ErrorMessage = "Не допустимо пустое значение")]
		public string Address { get; set; }
         public byte[] Avatar { get; set; }
         public string AvatarMimeType { get; set; }
         public int IdRole { get; set; }
         public  ICollection<models.Dog> Dogs { get; set; }

         public int Sex { get; set; }
		public DomainModel.User ToDomainModel()
		{
			return Mapper.Map<DomainModel.User>(this);
		}

        public List<models.Role> GetRoleUser
        {
            get
            {
                List<models.Role> usRole = new List<models.Role>();
                models.Role rol = new Role();
                rol.Id = 1;
                rol.Name = "Пользователь";
                usRole.Add(rol);
                models.Role rol2 = new Role();
                rol2.Id = 2;
                rol2.Name = "Модератор";
                usRole.Add(rol2);
                models.Role rol3 = new Role();
                rol3.Id = 3;
                rol3.Name = "Администратор";
                usRole.Add(rol3);
                return usRole;
            }
        }
        public string GetSex
        {
            get
            {
                if (this.Sex == 1)
                {
                    return "Мужской";
                }
                else if (this.Sex == 0)
                {
                    return "Женский";
                }
                return "";
            }

        }
        public List<models.ExpositionMember> GetWinners
        {
            get
            {
                List<models.ExpositionMember> allMember = Mapper.Map<List<models.ExpositionMember>>(ServiceFactory.Instance.ExpositionMembersService.GetAll().ToList());
                List<int> itemDog = this.Dogs.Select(m => m.Id).ToList();
                List<models.ExpositionMember> itemAllDog = new List<ExpositionMember>();
                foreach (var item in allMember)
                {
                    if (itemDog.Contains(item.IdDog))
                    {
                        itemAllDog.Add(item);
                    }
                }
                var winners = Mapper.Map<List<models.ExpositionWinner>>(ServiceFactory.Instance.ExpositionWinnersService.GetAll());
                List<int> winnersIds = winners.Select(t => t.IdExpositionMember).ToList();

                List<models.ExpositionMember> listWinners = new List<models.ExpositionMember>();

                foreach (var item in itemAllDog)
                {
                    if (winnersIds.Contains(item.Id))
                    {
                        listWinners.Add(item);
                    }
                }
                return listWinners;
            }
        }
        public List<models.Avard> GetAvards
        {
            get
            {
                List<models.ExpositionWinner> allWinners =Mapper.Map<List<models.ExpositionWinner>>(ServiceFactory.Instance.ExpositionWinnersService.GetAll().ToList());
                List<int> idWinner = this.GetWinners.Select(item => item.Id).ToList();
                List<models.Avard> userAllAvard = new List<Avard>();
                foreach (var item in allWinners)
                { 
                    if(idWinner.Contains(item.IdExpositionMember))
                    {
                        userAllAvard.Add(item.GetAvard);                    
                    }
                }
                return userAllAvard;
            }

        }
        public List<models.Payment> GetPayment
        {
            get
            {
                List<models.Payment> allPaiment = Mapper.Map<List<models.Payment>>(ServiceFactory.Instance.PaymentsService.GetAll().ToList());
                return allPaiment.Where(item=>item.UserId == this.Id).ToList();
            }
        }
   	}
}