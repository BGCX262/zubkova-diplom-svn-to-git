using DogsClub.Abstract;
using DogsClub.BLL.Abstract;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using dal = DogsClub;
using domain = DogsClub.DomainModel;

namespace DogsClub.BLL
{
    public class DogSalesService : BaseService<IDogSalesRepo, domain.DogForSale, dal.DogsForSale>, IDogSalesService
    {
        public override void Add(domain.DogForSale item)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                base.Add(item);
                using (var newsRepo = ObjectFactory.GetInstance<INewsRepo>())
                {
                    using (var dogsRepo = ObjectFactory.GetInstance<IDogsRepo>())
                    {
                        var dog = dogsRepo.GetById(item.IdDog);
                        newsRepo.Add(new News()
                        {
                            NewsDate = DateTime.Now,
                            Title = "Продажа питомца",
                            NewsText = string.Format(@"Питомец по кличке {0} выставлен на продажу. Подробности в разделе 'Продажа'", dog.Name)
                        });
                        newsRepo.Save();
                    }
                }

                transaction.Complete();
            }
        }
    }
}
