using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.Abstract
{
	public interface IUsersRepo : IBaseRepo<User>
	{
		bool IsEmailExists(string email);
		string[] GetUserRoles(string email);
		bool VerifyUser(string password, string email);
		void AddDog(Dog dog, string userEmail);
        string[] GetRolesAsStringArray();
        IEnumerable<Role> GetRoles();
        string[] GetRolesForUser(string email);
        DogsClub.User GetByEmail(string email);
	}
}
