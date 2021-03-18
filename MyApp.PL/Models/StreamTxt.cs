namespace MyApp.PL.Models
{
    using System.IO;
    using MyApp.BLL.DTO;
    using MyApp.PL.Controllers;

    public class StreamTxt
    {
        public static double WriteToFile(string path, bool is_append, string prod_names, string msg, UserDTO user, double total)
        {
            using (StreamWriter writer = new StreamWriter(path, is_append))
            {
                writer.WriteLine($"{msg} {user.fname}!");
                foreach (ProductDTO i in StoreController.shop_cart)
                {
                    writer.WriteLine(new string('-', 100));
                    writer.WriteLine($"Назoвание продукта: {i.Name}");
                    writer.WriteLine($"Цена: {i.Price}");
                    writer.WriteLine($"Описание: {i.Description}");
                    writer.WriteLine($"Прозводитель: {i.Manufacturer}");
                    writer.WriteLine($"Дата выпуска: {i.AgeRelease}");
                    writer.WriteLine($"В наличии: {i.IsStock}");
                    writer.WriteLine($"Фото: {i.Photo}");
                    writer.WriteLine(new string('-', 100));
                    prod_names += i.Name + " | ";
                }
                writer.WriteLine($"\t\t\t Всего к оплате: {total}");
            }
            return total;
        }
    }
}