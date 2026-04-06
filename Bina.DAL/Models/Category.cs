using System.Collections.Generic;

namespace Bina.DAL.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string IconUrl { get; set; }

        public int? ParentId { get; set; }
        public Category Parent { get; set; }
        public ICollection<Category> Children { get; set; } = new List<Category>();

        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}