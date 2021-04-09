namespace MyApp.PL.Models
{
    public class MyGenerator
    {
        public static string Connection() =>
            System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    }
}