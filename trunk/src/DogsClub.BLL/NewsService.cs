using DogsClub.BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using domain = DogsClub.DomainModel;
using dal = DogsClub;
using StructureMap;
using DogsClub.Abstract;
using AutoMapper;
using DogsClub.DomainModel;

namespace DogsClub.BLL
{
    public class NewsService : INewsService
    {
        public IEnumerable<DomainModel.News> GetNewsForPeriod(DomainModel.NewsPeriodEnum newsPeriod)
        {
            using (var newsRepo = ObjectFactory.GetInstance<INewsRepo>())
            {
                DateTime date = DateTime.Now;
                switch(newsPeriod)
                {
                    case NewsPeriodEnum.Month:
                        date.AddMonths(-1);
                        break;
                    case NewsPeriodEnum.Week:
                        date.AddDays(-7);
                        break;
                    case NewsPeriodEnum.Year:
                        date.AddYears(-1);
                        break;
                    case NewsPeriodEnum.All:
                        return Mapper.Map<IEnumerable<domain.News>>(newsRepo.GetAll());
                    default:
                        throw new InvalidOperationException("unknown newsPeriodEnum type");
                }
                var news = newsRepo.GetAll().Where(t => t.NewsDate >= date).ToList();
                return Mapper.Map<IEnumerable<domain.News>>(news);
            }
        }
    }
}
