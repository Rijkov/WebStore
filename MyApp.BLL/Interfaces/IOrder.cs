namespace MyApp.BLL.Interfaces
{
    using MyApp.BLL.DTO;
    using System.Collections.Generic;

    public interface IOrder
    {
        IEnumerable<ProductDTO> GetAllProducts();
        IEnumerable<UserDTO> GetAllUsers();
        UserDTO GetUser(string f_name);
        UserDTO GetUser(int? id); 
        ProductDTO GetProduct(int? id);
        void AddProduct(ProductDTO add);
        void AddUser(UserDTO add);
        string MakeOrder(OrderDTO ord, string conn, int num_tbl);
        int CreateNewTable(int num_tbl, string name = "Order");
        void Save();
        void Dispose();
    }
}
