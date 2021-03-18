namespace MyApp.DAL.Entities
{
    using System;

    public class Order
    {
        public int Id { get; private set; }
        public string OrderName { get; set; }
        public double TotalPrise { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid OrderNum {get; set;}

    }
}
