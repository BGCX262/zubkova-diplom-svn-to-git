using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.DomainModel
{
    public class Vaccination
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> DateVacination { get; set; }
        public int IdDog { get; set; }
    }
}
