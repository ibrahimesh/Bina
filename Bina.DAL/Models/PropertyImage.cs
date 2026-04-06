namespace Bina.DAL.Models
{
    public class PropertyImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public int OrderIndex { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
}