using AutoMapper;
using DogsClub.Abstract;
using DogsClub.BLL.Abstract;
using DogsClub.Concrete;
using DogsClub.Shared.Exceptions;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using domain = DogsClub.DomainModel;

namespace DogsClub.BLL
{
	public class UsersService : BaseService<IUsersRepo, domain.User, DogsClub.User>, IUserService
	{
		public void CreateUser(DogsClub.DomainModel.User user)
		{
			if (user == null) throw new ArgumentNullException("user");
			if (string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Email)) throw new InvalidAccountException("Пароль, либо email не могут быть пустыми");
			using (TransactionScope transaction = new TransactionScope())
			{
				using (var usersRepo = ObjectFactory.GetInstance<IUsersRepo>())
				{
					if (usersRepo.IsEmailExists(user.Email)) throw new EmailAlreadyExistsException(string.Format("Email {0} уже зарегистрирован", user.Email));

					var mapedEntity = Mapper.Map<DogsClub.User>(user);
					mapedEntity.RegistrationDate = DateTime.Now;
					usersRepo.Add(mapedEntity);
					usersRepo.Save();
				}

				using (var newsRepo = ObjectFactory.GetInstance<INewsRepo>())
				{
					newsRepo.Add(new News()
						{
							NewsDate = DateTime.Now,
							Title = string.Format("В системе зарегистрирован новый пользователь!"),
							NewsText = string.Format("{0} {1} {2}, Поздравляем с успешной регистрацией в системе!", user.LastName, user.FirstName, user.MiddleName)
						});
					newsRepo.Save();
				}

				transaction.Complete();
			}
		}

		public bool VerifyUser(string password, string email)
		{
			if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email)) return false;

			using (var usersRepo = ObjectFactory.GetInstance<IUsersRepo>())
			{
				return usersRepo.VerifyUser(password, email);
			}
		}

		public void AddDog(DogsClub.DomainModel.Dog dog, string userEmail)
		{
			if (dog == null) throw new ArgumentNullException("dog");
			if (string.IsNullOrEmpty("userEmail")) throw new ArgumentException("userEmail");

			using (var usersRepo = ObjectFactory.GetInstance<IUsersRepo>())
			{
				var mappedDog = Mapper.Map<DogsClub.Dog>(dog);
				usersRepo.AddDog(mappedDog, userEmail);
				usersRepo.Save();
			}
		}

		public string[] GetAllRoles()
		{
			using (var usersRepo = ObjectFactory.GetInstance<IUsersRepo>())
			{
				return usersRepo.GetRolesAsStringArray();
			}
		}

		public string[] GetUserRoles(string email)
		{
			if (string.IsNullOrEmpty(email)) return new string[0];

			using (var usersRepo = ObjectFactory.GetInstance<IUsersRepo>())
			{
				return usersRepo.GetRolesForUser(email);
			}
		}

		public bool IsEmailExists(string email)
		{
			using (var usersRepo = ObjectFactory.GetInstance<IUsersRepo>())
			{
				return usersRepo.IsEmailExists(email);
			}
		}

		public DomainModel.User GetById(int id)
		{
			using (var usersRepo = ObjectFactory.GetInstance<IUsersRepo>())
			{
				var dbUser = usersRepo.GetById(id);
				var mapped = Mapper.Map<DomainModel.User>(dbUser);
				return mapped;
			}
		}

		public DomainModel.User GetByEmail(string email)
		{
			using (var usersRepo = ObjectFactory.GetInstance<IUsersRepo>())
			{
				var dbUser = usersRepo.GetByEmail(email);
				var mapped = Mapper.Map<DomainModel.User>(dbUser);
				return mapped;
			}
		}

		public IEnumerable<DomainModel.User> GetAll()
		{
			using (var usersRepo = ObjectFactory.GetInstance<IUsersRepo>())
			{
				var dbUsers = usersRepo.GetAll().ToList();
				var mappedUsers = Mapper.Map<IEnumerable<DogsClub.User>, IEnumerable<DomainModel.User>>(dbUsers);
				return mappedUsers;
			}
		}

		public void ChangeAvatar(string userEmail, byte[] avatarData, string mimeType)
		{
			using (var usersRepo = ObjectFactory.GetInstance<IUsersRepo>())
			{
				var dbUser = usersRepo.GetByEmail(userEmail);
				if (dbUser == null) throw new InvalidOperationException("В базе не найден пользователь с адресом " + userEmail); ;

				dbUser.Avatar = avatarData;
				dbUser.AvatarMimeType = mimeType;
				usersRepo.Save();
			}
		}

		public override void Edit(domain.User entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");

			using (var usersRepo = ObjectFactory.GetInstance<IUsersRepo>())
			{
				var dbUser = usersRepo.GetByEmail(entity.Email);
				if (dbUser == null) throw new InvalidOperationException("User not found");

				entity.Avatar = dbUser.Avatar;
				entity.AvatarMimeType = dbUser.AvatarMimeType;
				base.Edit(entity);
			}
		}

		public void SetUserRoles(int[] rolesIds, int idUser)
		{
			if (rolesIds == null || !rolesIds.Any()) throw new ArgumentException("Список идентификаторов ролей не может быть пустым");

			using (var usersRepo = ObjectFactory.GetInstance<IUsersRepo>())
			{
				var roles = usersRepo
					.GetRoles()
					.Where(t => rolesIds.Contains(t.Id))
					.ToList();

				if (!roles.Any()) throw new InvalidOperationException("Осутствуют роли с такими идентификаторами");

				var user = usersRepo.GetById(idUser);
				user.UsersRoles.Clear();
				usersRepo.Save();
				foreach (var item in roles)
				{
					user.UsersRoles.Add(new UsersRole()
						{
							IdRole = item.Id,
							IdUser = idUser
						});
				}
				usersRepo.Save();
			}
		}

		IEnumerable<domain.Role> IUserService.GetAllRolesEntities()
		{
			using (var usersRepo = ObjectFactory.GetInstance<IUsersRepo>())
			{
				var roles = usersRepo
					.GetRoles()
					.ToList();
				return Mapper.Map<IEnumerable<domain.Role>>(roles);
			}
		}
	}
}
