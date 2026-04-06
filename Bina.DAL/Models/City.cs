using System.Collections.Generic;

namespace Bina.DAL.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<District> Districts { get; set; } = new List<District>();
        public ICollection<Metro> Metros { get; set; } = new List<Metro>();
        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}