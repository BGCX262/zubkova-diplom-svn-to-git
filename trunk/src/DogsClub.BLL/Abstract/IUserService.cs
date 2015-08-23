using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using domain = DogsClub.DomainModel;

namespace DogsClub.BLL.Abstract
{
	public interface IUserService : IBaseService<domain.User>
	{
		void CreateUser(DogsClub.DomainModel.User user);

		bool VerifyUser(string password, string email);

		void AddDog(DogsClub.DomainModel.Dog dog, string userEmail);

        string[] GetAllRoles();

        string[] GetUserRoles(string email);

        bool IsEmailExists(string email);

        domain.User GetByEmail(string email);

        void ChangeAvatar(string userEmail, byte[] avatarData, string mimeType);

        void SetUserRoles(int[] rolesIds, int idUser);

		IEnumerable<domain.Role> GetAllRolesEntities();
	}
}
