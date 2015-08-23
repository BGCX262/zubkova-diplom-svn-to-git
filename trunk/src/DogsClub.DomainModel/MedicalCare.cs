using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.DomainModel
{
    public class MedicalCare
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime DateCare { get; set; }
        public string Description { get; set; }
        public int IdDog { get; set; }
    }
}
