namespace Bina.DAL.Models
{
    public class Translation
    {
        public int Id { get; set; }
        
        // e.g. "Category", "City", "Property"
        public string EntityType { get; set; } 
        
        public int EntityId { get; set; }
        
        // e.g. "Name", "Description"
        public string PropertyName { get; set; }
        
        // e.g. "az", "en", "ru"
        public string LanguageCode { get; set; }
        
        public string Value { get; set; }
    }
}