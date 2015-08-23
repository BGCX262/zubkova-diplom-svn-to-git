using DogsClub.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.Concrete
{
	public class UsersRepo : BaseRepo<User>, IUsersRepo
	{
		public UsersRepo()
			: base() { }

		public bool IsEmailExists(string email)
		{
			return _dbSet.Any(t => t.Email.ToUpper() == email.ToUpper());
		}

		public string[] GetUserRoles(string email)
		{
			var user = _dbSet.FirstOrDefault(t => t.Email.ToUpper() == email.ToUpper());
			if (user == null) return new string[0];

			return user.UsersRoles.Select(t => t.Role.Name).ToArray();
		}

		public bool VerifyUser(string password, string email)
		{
			if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email)) return false;

			var user = _dbSet.FirstOrDefault(t => t.Email.ToUpper() == email.ToUpper());
			if (user == null) return false;

			return user.Password == password;
		}

		public void AddDog(Dog dog, string userEmail)
		{
			if (dog == null) throw new ArgumentNullException("dog");

			if (string.IsNullOrEmpty(userEmail)) throw new ArgumentException("userEmail");

			var user = _dbSet.FirstOrDefault(t => t.Email.ToUpper() == userEmail.ToUpper());
			if (user == null) throw new InvalidOperationException("Не найден пользователь с email " + userEmail);

			user.Dogs.Add(dog);
		}

        public string[] GetRolesAsStringArray()
        {
            return _context
                .Roles
                .Select(t => t.Name)
                .ToArray();
        }

        public IEnumerable<Role> GetRoles()
        {
            return _context
                .Roles;
        }

        public string[] GetRolesForUser(string email)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException("email");

            var user = _dbSet.FirstOrDefault(t => t.Email.ToUpper() == email.ToUpper());
            if (user == null) throw new InvalidOperationException("Пользователь с адресом " + email + " не найден");

            return user
                .UsersRoles
                .Select(t => t.Role.Name)
                .ToArray();
        }

        public DogsClub.User GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentException("email");

            return _dbSet.FirstOrDefault(t => t.Email.ToUpper() == email.ToUpper());
        }
    }
}
