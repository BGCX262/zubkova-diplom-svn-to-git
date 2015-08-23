using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogsClub.WebUI.Infrastracture
{
	public interface IViewModel <TDomainModel>
	{
		TDomainModel ToDomainModel();
	}
}