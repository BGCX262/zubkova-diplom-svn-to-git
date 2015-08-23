using DogsClub.Abstract;
using DogsClub.BLL.Abstract;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain = DogsClub.DomainModel;

namespace DogsClub.BLL
{
	public class DogsService : BaseService<IDogsRepo, domain.Dog, DogsClub.Dog>, IDogsService 
	{
		public override void Edit(domain.Dog entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");

			using (var dogsRepo = ObjectFactory.GetInstance<IDogsRepo>())
			{
				var dbDog = dogsRepo.GetById(entity.Id);
				if (dbDog == null) throw new InvalidOperationException("Dog not found");

				entity.Avatar = dbDog.Avatar;
				entity.AvatarMimeType = dbDog.AvatarMimeType;
				base.Edit(entity);
			}
		}

		public void ChangeAvatar(int idDog, byte[] avatar, string avatarMimeType)
		{
			using (var dogsRepo = ObjectFactory.GetInstance<IDogsRepo>())
			{
				var dbDog = dogsRepo.GetById(idDog);
				if (dbDog == null) throw new InvalidOperationException("В базе не найден питомец с id " + idDog);

				dbDog.Avatar = avatar;
				dbDog.AvatarMimeType = avatarMimeType;
				dogsRepo.Save();
			}
		}
	}
}
