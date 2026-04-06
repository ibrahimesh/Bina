using System.Collections.Generic;

namespace Bina.DAL.Models
{
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Qesebe { get; set; } // ?lav? edil?n xass?
        
        public int CityId { get; set; }
        public City City { get; set; }

        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}