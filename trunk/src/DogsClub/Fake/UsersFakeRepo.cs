using DogsClub.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.Fake
{
	public class UsersFakeRepo : IUsersRepo
	{
		public bool IsEmailExists(string email)
		{
			throw new NotImplementedException();
		}

		public string[] GetUserRoles(string email)
		{
			throw new NotImplementedException();
		}

		public bool VerifyUser(string password, string email)
		{
			throw new NotImplementedException();
		}

		public void AddDog(Dog dog, string userEmail)
		{
			throw new NotImplementedException();
		}

		public string[] GetRolesAsStringArray()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Role> GetRoles()
		{
			throw new NotImplementedException();
		}

		public string[] GetRolesForUser(string email)
		{
            string[] arr = new string[1];
			return arr;
		}

		public User GetByEmail(string email)
		{
			throw new NotImplementedException();
		}

		public void Edit(User item)
		{
			throw new NotImplementedException();
		}

		public void Add(User item)
		{
			throw new NotImplementedException();
		}

		public void Remove(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<User> GetAll()
		{
			return FakeDataStore.GetUsers();
		}

		public User GetById(int id)
		{
			return FakeDataStore.GetUsers().FirstOrDefault(t => t.Id == id);
		}

		public void Save()
		{
			
		}

		public void Dispose()
		{
			
		}
	}
}
