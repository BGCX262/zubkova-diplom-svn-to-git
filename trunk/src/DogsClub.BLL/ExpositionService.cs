using DogsClub.Abstract;
using DogsClub.BLL.Abstract;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using domain = DogsClub.DomainModel;

namespace DogsClub.BLL
{
	public class ExpositionService : BaseService<IExpositionsRepo, domain.Exposition, DogsClub.Exposition>, IExpositionsService
	{
		public override void Add(domain.Exposition item)
		{
			using (TransactionScope transaction = new TransactionScope())
			{
				base.Add(item);
				using (var newsRepo = ObjectFactory.GetInstance<INewsRepo>())
				{					
					newsRepo.Add(new News()
						{
							NewsDate = DateTime.Now,
							Title = "Внимание! Новая выставка!",
							NewsText = string.Format("С {0} планируется проведение выставки {1}. Подробная информация в разделе 'Выставки'.", item.DateStart.ToString("dd.MM.yyyy"), item.Name)
						});
					newsRepo.Save();
				}

				transaction.Complete();
			}
		}
	}
}
