using System.Collections.Generic;

namespace Bina.BLL.DTOs.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string IconUrl { get; set; }
        public List<CategoryDto> Children { get; set; } = new List<CategoryDto>();
    }
}