namespace MyApp.DAL.Interfaces
{
    using MyApp.DAL.Entities;
    using System;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<User> Users { get; }
        IRepository<Order> Orders { get; }
        void Save();
    }
}
