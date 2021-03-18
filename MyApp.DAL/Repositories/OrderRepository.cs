namespace MyApp.DAL.Repositories
{
    using MyApp.DAL.Interfaces;
    using System.Data.Entity;
    using MyApp.DAL.Entities;
    using MyApp.DAL.EF;
    using System;

    public class OrderRepository : IRepository<Order>
    {
        MyContext db;

        public OrderRepository(MyContext db)
        {
            this.db = db;
        }

        public void Create(Order prod)
        {
            db.Orders.Add(prod);
        }

        public void Delete(int id)
        {
            Order founded = db.Orders.Find(id);
            if (founded != null)
            {
                db.Orders.Remove(founded);
            }
        }

        public Order Get(int id)
        {
            return db.Orders.Find(id);
        }

        public Order Get(int i, Guid id)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<Order> GetAll()
        {
            return db.Orders;
        }

        public void Update(Order pr)
        {
            db.Entry(pr).State = EntityState.Modified;
        }
    }
}
