namespace MyApp.DAL.Entities
{
    using MyApp.DAL.Interfaces;
    using System;

    public class User : IEntity
    {
        public int id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string age { get; set; }
        public string email { get; set; }
        public string phnomber{ get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public DateTime date_regestry { get; set; }
        public Guid hash_num { get; set; }               
    }
}