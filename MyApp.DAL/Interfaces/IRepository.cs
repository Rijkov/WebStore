namespace MyApp.DAL.Interfaces
{
    using System.Collections.Generic;

    public interface IRepository<IEntity> where IEntity : class
    {
        void Create(IEntity item);
        void Update(IEntity item);
        void Delete(int id);
        IEntity Get(int id);
        IEnumerable<IEntity> GetAll();
    }
}
