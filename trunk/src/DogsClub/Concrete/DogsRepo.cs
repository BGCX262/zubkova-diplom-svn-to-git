using DogsClub.Abstract;
using DogsClub.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub
{
    public class DogsRepo : BaseRepo<Dog>, IDogsRepo
    {
        public DogsRepo()
            : base() { }
    }
}
