namespace MyApp.BLL.Serveces
{
    using System;
    using System.Linq;
    using MyApp.BLL.DTO;
    using MyApp.DAL.Entities;
    using MyApp.BLL.Interfaces;
    using MyApp.DAL.Interfaces;
    using MyApp.DAL.Repositories;
    using MyApp.BLL.Infostructure;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class OrderService : IOrder
    {
        IUnitOfWork Db { get; set; }
        string conn;

        public OrderService(string conn)
        {
            this.conn = conn;
            Db = new EFUnitOfWork(conn);
        }

        public void AddProduct(ProductDTO add)
        {
            Product product = new Product
            {
                Id = add.Id,
                AgeRelease = add.AgeRelease,
                Description = add.Description,
                IsStock = add.IsStock,
                Manufacturer = add.Manufacturer,
                Name = add.Name,
                Photo = add.Photo,
                Price = add.Price
            };
            Db.Products.Create(product);
        }

        public void AddUser(UserDTO add) 
        {
            User user = new User
            {
                id = add.id,
                fname = add.fname,
                lname = add.lname,
                age = add.age,
                email = add.email,
                phnomber = add.phnomber,
                login = add.login,
                password = add.password,
                role = add.role,
                date_regestry = DateTime.Now,
                hash_num = Guid.NewGuid()
            };
            Db.Users.Create(user);
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            List<Product> Products = Db.Products.GetAll().ToList();
            List<ProductDTO> PDTO = new List<ProductDTO>();
            for(int i =0; i<Products.Count; i++)
            {
                PDTO.Add(new ProductDTO());
                PDTO[i].AgeRelease = Products[i].AgeRelease;
                PDTO[i].Description = Products[i].Description;
                PDTO[i].Id = Products[i].Id;
                PDTO[i].IsStock = Products[i].IsStock;
                PDTO[i].Manufacturer = Products[i].Manufacturer;
                PDTO[i].Name = Products[i].Name;
                PDTO[i].Photo = Products[i].Photo;
                PDTO[i].Price = Products[i].Price;
            }
            return PDTO;
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            List<User> Users = Db.Users.GetAll().ToList();
            List<UserDTO> UsersDTO = new List<UserDTO> ();
            for(int i=0; i<Users.Count; i++)
            {
                UsersDTO.Add(new UserDTO());
                UsersDTO[i].age = Users[i].age;
                UsersDTO[i].date_regestry = Users[i].date_regestry;
                UsersDTO[i].email = Users[i].email;
                UsersDTO[i].fname = Users[i].fname;
                UsersDTO[i].hash_num = Users[i].hash_num;
                UsersDTO[i].id = Users[i].id;
                UsersDTO[i].lname = Users[i].lname;
                UsersDTO[i].login = Users[i].login;
                UsersDTO[i].password = Users[i].password;
                UsersDTO[i].phnomber = Users[i].phnomber;
                UsersDTO[i].role = Users[i].role;
            }
            return UsersDTO;
        }

        public ProductDTO GetProduct(int? id) // добавить
        {
            if (id == null)
            {
                throw new Validation("This product ID NOT correct...", "");
            }

            Product found = Db.Products.Get(id.Value);
            if(found == null)
            {
                throw new Validation("This product doesn't exist", "");
            }

            ProductDTO prdDto = new ProductDTO();
            prdDto.Id = found.Id;
            prdDto.Name = found.Name;
            prdDto.Price = found.Price;
            prdDto.Description = found.Description;
            prdDto.Photo = found.Photo;
            prdDto.IsStock = found.IsStock;
            prdDto.AgeRelease = found.AgeRelease;
            prdDto.Manufacturer = found.Manufacturer;
            return prdDto;
        }

        public UserDTO GetUser(string f_name)
        {
            User found = Db.Users.GetAll().Where(n=>n.fname == f_name).FirstOrDefault();

            return new UserDTO
            {
                id = found.id,
                age = found.age,
                date_regestry = found.date_regestry,
                email = found.email,
                fname = found.fname,
                hash_num = found.hash_num,
                lname = found.lname,
                login = found.login,
                password = found.password,
                phnomber = found.phnomber,
                role = found.role
            };
        }

        public UserDTO GetUser(int? id)
        {
            if (id == null)
            {
                throw new Validation("This id NOT correct..", "");
            }
            User found = Db.Users.Get(id.Value);
            if(found == null)
            {
                throw new Validation("This user doesn't exist", "");
            }
            return new UserDTO
            {
                id = found.id,
                age = found.age,
                date_regestry = found.date_regestry,
                email = found.email,
                fname = found.fname,
                hash_num = found.hash_num,
                lname = found.lname,
                login = found.login,
                password = found.password,
                phnomber = found.phnomber,
                role = found.role
            };
        }

        public UserDTO GetUserbyEmail(string email)
        {
            User found = Db.Users.GetAll().Where(n => n.email == email).FirstOrDefault();

            return new UserDTO
            {
                id = found.id,
                age = found.age,
                date_regestry = found.date_regestry,
                email = found.email,
                fname = found.fname,
                hash_num = found.hash_num,
                lname = found.lname,
                login = found.login,
                password = found.password,
                phnomber = found.phnomber,
                role = found.role
            };
        }

        public string MakeOrder(OrderDTO ord, string conn, int num_tbl)
        {
            string query = $"insert into Order_{num_tbl}(OrderName, TotalPrice, OrderDate, OrderNum) " +
                $"Values(@OrderName, @TotalPrice, @OrderDate, @OrderNum)", msg = string.Empty;
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@OrderName", ord.OrderName);
                    cmd.Parameters.AddWithValue("@TotalPrice", ord.TotalPrice);
                    cmd.Parameters.AddWithValue("@OrderDate", ord.OrderDate);
                    cmd.Parameters.AddWithValue("@OrderNum", ord.OrderNum);

                    int exec_result = cmd.ExecuteNonQuery();
                    if (exec_result == 1)
                        msg = $"\t\t\t\t\tСпасибо за заказ ";
                    else msg = "\t\t\t\tЧто то пошло не так.....";
                }
            }
            return msg;
        }

        public int CreateNewTable(int num_tbl, string name = "Order")
        {
            int result = new DAL.EF.MyContext(conn).Database.ExecuteSqlCommand
            (
                $"CREATE TABLE [dbo].[{name}_{num_tbl}]" +
                "([Id] INT NOT NULL PRIMARY KEY IDENTITY," +
                "[OrderName] NVARCHAR(255) NULL," +
                "[TotalPrice] INT NULL," +
                "[OrderDate] DATETIME NULL," +
                "[OrderNum] UNIQUEIDENTIFIER NULL)"
            );
            return result;
        }

        public void Save() => Db.Save();
    }
}
