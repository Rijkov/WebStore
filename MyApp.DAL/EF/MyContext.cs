namespace MyApp.DAL.EF
{
    using MyApp.DAL.Entities;
    using System.Data.Entity;

    public class MyContext : DbContext
    {
        public MyContext(string conn) : base(conn) { }

        public virtual DbSet<Product> Products { get; set; } 
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
    }
}