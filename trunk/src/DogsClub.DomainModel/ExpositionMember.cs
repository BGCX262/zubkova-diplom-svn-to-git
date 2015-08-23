using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.DomainModel
{
    public class ExpositionMember
    {
        public int Id { get; set; }
        public int IdExposition { get; set; }
        public int IdDog { get; set; }
    }
}
