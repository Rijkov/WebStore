using MyApp.BLL.DTO;
using MyApp.BLL.DTO.EmailData;
using MyApp.BLL.Serveces;
using System.Web.Mvc;

namespace MyApp.PL.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email

        EmailData emdata;

        public ActionResult FeedBack()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FeedBack(UserDTO usr)
        {
            // usr.login - Comments
            if (usr.login == null && usr.phnomber == null && usr.fname == null && usr.email == null)
            {
                ViewBag.Empty = "You must pass all fields!";
                return View();
            }
            else if (usr.login == null || usr.phnomber == null || usr.fname == null || usr.email == null)
            {
                ViewBag.Empty = "You must pass all fields!";
                return View();
            }
            else
            {
                emdata = new EmailData();
                emdata.MsgContent = $"Спасибо  {usr.fname}! Скоро наш менеджер с Вами свяжеться!";
                emdata.RecipientMail = usr.email;
                emdata.Subject = "Cообщение с моего интернет магазина.";
                emdata.Text = usr.login;
                try
                {
                    new EmailSender().SendMessageAsync(emdata, HomeController.path);
                    return Json($"Thank you {usr.fname} for your request! We will contact you soon!");
                }
                catch (System.Exception ex) { ViewBag.exception = ex.Message; return View(); }
            }
        }
    }
}