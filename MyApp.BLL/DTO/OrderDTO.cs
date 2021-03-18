namespace MyApp.BLL.DTO
{
    using System;
    public class OrderDTO
    {
        public int Id;
        public string OrderName;
        public double TotalPrice;
        public DateTime OrderDate;
        public Guid OrderNum;
    }
}
