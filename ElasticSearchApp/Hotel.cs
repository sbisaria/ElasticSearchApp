using System.Collections.Generic;
using System.Linq;

namespace ElasticSearchApp
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public static Hotel Get(int id)
        {
            return GetAll().SingleOrDefault(x => x.Id.Equals(id));
        }
        public static List<Hotel> GetAll()
        {
            return new List<Hotel> {
                new Hotel {Id = 1, Name = "Hayat",Type="Hotel", Description = "A Five Star Hotel"},
                new Hotel {Id = 2, Name = "Conrad",Type="Hotel", Description = "Luxury Hotel"},
                new Hotel {Id = 3, Name = "Holiday-In",Type="Hotel", Description = "A Five Star Hotel"},
                new Hotel {Id = 4, Name = "Country-In",Type="Hotel", Description = "Luxury Hotel"},
                new Hotel {Id = 5, Name = "Clarks-In",Type="Hotel", Description = "Luxury Hotel"}
            };
        }
    }
}
