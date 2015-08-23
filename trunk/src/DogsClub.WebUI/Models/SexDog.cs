using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using models = DogsClub.WebUI.Models;
namespace DogsClub.WebUI.Models
{
    public class SexDog
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static  List<models.SexDog> GetAllSexDogs()
        {
            List<models.SexDog> sexDog = new List<models.SexDog>();
            sexDog.Add(new models.SexDog() { Id = 1, Name = "Мальчик" });
            sexDog.Add(new models.SexDog() { Id = 0, Name = "Девочка" });
            return sexDog;
        }
    }
}