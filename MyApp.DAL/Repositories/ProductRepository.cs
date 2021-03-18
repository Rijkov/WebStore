namespace MyApp.DAL.Repositories
{
    using MyApp.DAL.Interfaces;
    using System.Data.Entity;
    using MyApp.DAL.Entities;
    using MyApp.DAL.EF;
    using System;

    public class ProductRepository : IRepository<Product>
    {
        MyContext db;

        public ProductRepository(MyContext db) 
        {
            this.db = db;
        }

        public void Create (Product prod)
        {
            db.Products.Add(prod);
        }

        public void Delete (int id)
        {
            Product founded = db.Products.Find(id);
            if(founded != null)
            {
                db.Products.Remove(founded);
            }
        }

        public Product Get(int id)
        {
            return db.Products.Find(id);
        }

        public System.Collections.Generic.IEnumerable<Product> GetAll()
        {
            return db.Products;
        }

        public void Update(Product pr)
        {
            db.Entry(pr).State = EntityState.Modified;
        }
    }
}
