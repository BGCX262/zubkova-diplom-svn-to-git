using DogsClub.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.Concrete
{
    public class DogsPhotosRepo : BaseRepo<DogsPhoto>, IDogsPhotosRepo
    {
        public DogsPhotosRepo()
            : base() { }
    }
}
