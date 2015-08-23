using DogsClub.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using domain = DogsClub.DomainModel;

namespace DogsClub.BLL.Abstract
{
	public interface INewsService
	{
		IEnumerable<domain.News> GetNewsForPeriod(NewsPeriodEnum newsPeriod);
	}
}
