namespace MyApp.DAL.Repositories
{
    using MyApp.DAL.EF;
    using MyApp.DAL.Entities;
    using MyApp.DAL.Interfaces;
    public class EFUnitOfWork : IUnitOfWork
    {
        ProductRepository productRepository;
        UserRepository userRepository;
        OrderRepository orderRepository;
        MyContext db;

        public EFUnitOfWork(string connect) 
        {
            db = new MyContext(connect);
        }

        public IRepository<Product> Products
        {
            get
            {
                if(productRepository == null)
                {
                    productRepository = new ProductRepository(db);
                }
                return productRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if(userRepository == null)
                {
                    userRepository = new UserRepository(db);
                }
                return userRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                {
                    orderRepository = new OrderRepository(db);
                }
                return orderRepository;
            }
        }


        bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    db.Dispose();

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
