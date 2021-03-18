namespace MyApp.DAL.Entities
{
    using MyApp.DAL.Interfaces;

    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public bool IsStock { get; set; }
        public int AgeRelease { get; set; }
        public string Manufacturer { get; set; }
    }
}