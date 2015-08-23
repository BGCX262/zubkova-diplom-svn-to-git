using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.DomainModel
{
    public class ExpositionWinner
    {
        public int Id { get; set; }
        public int IdExpositionMember { get; set; }
        public int IdAvard { get; set; }
        public int WinnerPlace { get; set; }
    }
}
