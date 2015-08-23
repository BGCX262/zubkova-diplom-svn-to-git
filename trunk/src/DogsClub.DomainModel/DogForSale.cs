using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.DomainModel
{
    public class DogForSale
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal ClubCommission { get; set; }
        public int IdDog { get; set; }
    }
}
