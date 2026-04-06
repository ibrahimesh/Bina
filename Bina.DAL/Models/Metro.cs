using System.Collections.Generic;

namespace Bina.DAL.Models
{
    public class Metro
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}