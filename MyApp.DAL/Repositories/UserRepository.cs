namespace MyApp.DAL.Repositories
{
    using MyApp.DAL.Interfaces;
    using MyApp.DAL.Entities;
    using System.Data.Entity;
    using MyApp.DAL.EF;
    using System;

    public class UserRepository : IRepository<User>
    {
        MyContext db;

        public UserRepository(MyContext db) {
            this.db = db;
        }

        public void Create(User user)
        {
            db.Users.Add(user);
        }

        public void Delete(int id)
        {
            User founded = db.Users.Find(id);
            if (founded != null)
            {
                db.Users.Remove(founded);
            }
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public System.Collections.Generic.IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public void Update(User pr)
        {
            db.Entry(pr).State = EntityState.Modified;
        }
    }
}
