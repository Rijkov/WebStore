namespace MyApp.BLL.DTO
{
    using System.ComponentModel.DataAnnotations;

    public class UserDTO
    {
        public int id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string age { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string email { get; set; }
        public string phnomber { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public System.DateTime date_regestry { get; set; }
        public System.Guid hash_num { get; set; }
    }
}
