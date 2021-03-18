namespace MyApp.PL.Models
{
    public class MyGenerator
    {
        public static string Connection()
        {
            string conn;
            conn = System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
            return conn;
        }
    }
}