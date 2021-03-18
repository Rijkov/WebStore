namespace MyApp.PL.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using MyApp.BLL.DTO;
    using System.Web.Mvc;
    using MyApp.BLL.Serveces;
    using System.Collections.Generic;
    using MyApp.PL.Models;

    //придумать что можно добавить ещё на сайт
    public class HomeController : Controller
    {
        UserDTO user;
        OrderService db;
        List<ProductDTO> cart;
        public static string path;
        readonly string admn, usr;
        public static string welcome;
        public static bool unlogin = false;

        public HomeController()
        {
            admn = "admin";
            usr = "user";        
            db = new OrderService(MyGenerator.Connection());
            if (AccountController.id != 0)
                user = db.GetUser(AccountController.id);
        }

        [HttpGet]
        public ActionResult Cart()
        {
            string empty = string.Empty;
            object[][] o = new object[2][];
            bool check_empty = false;
            cart = StoreController.shop_cart;

            if (cart == null || cart.Count == 0)
            {
                check_empty = true;
                empty = "The cart is EMPTY... Try to fill out it!!!";

                o[0] = new object[] { check_empty };
                o[1] = new object[] { empty };
                ViewBag.Empty = o;
                return View(cart);
            }
            else
            {
                double total = 0;
                for (int i = 0; i < cart.Count; i++)
                {
                    total = total + cart[i].Price;
                }
                ViewBag.TotalPay = total;
                o[0] = new object[] { check_empty };
                o[1] = new object[] { "" };
                ViewBag.Empty = o;
                return View(cart);
            }
        }

        [HttpPost]
        public ActionResult Cart(ProductDTO p)
        {
            if (user == null)  // Если пользователь не вошел
            {
                unlogin = true;
                return RedirectToAction("../Account/SignIn");
            }
            else
            {
                double total = 0;
                string prod_names = "", msg = "";
                foreach (ProductDTO r in StoreController.shop_cart)
                {
                    total += r.Price;
                    prod_names += r.Name + " | ";
                }
                try
                {
                    msg = db.MakeOrder(new OrderDTO
                    {
                        OrderName = prod_names,
                        OrderNum = Guid.NewGuid(),
                        OrderDate = DateTime.Now,
                        TotalPrice = total
                    }, MyGenerator.Connection(), user.id);
                    db.Save();
                }
                catch (Exception) { }

                path = Path.Combine(Server.MapPath("~/Files/checklist.txt"));
                if (System.IO.File.Exists(path))
                    ViewBag.total = StreamTxt.WriteToFile(path, true, prod_names, msg, user, total);
                else
                    ViewBag.total = StreamTxt.WriteToFile(path, false, prod_names, msg, user, total);

                return RedirectToAction("../Success/SuccessPage");
            }
        }

        public ActionResult CartDelete(int? id)
        {
            ProductDTO del = db.GetProduct(id);
            cart.Remove(del);
            return RedirectToAction("../Home/Cart");
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Store", "StorePage");
        }

        [HttpPost]
        public ActionResult Index(UserDTO user)
        {
            bool flag = false;
            List<UserDTO> users = db.GetAllUsers().ToList();
            string role = string.Empty;

            if (user != null)
            {
                for (int i = 0; i < users.Count; i++)
                {
                    if (user.login.Equals(users[i].login) && user.password.Equals(users[i].password) && users[i].role.Equals(admn))
                    {
                        flag = true;
                        role = users[i].role;
                        welcome = "Welcome - " + users[i].fname;
                        return RedirectToAction("../Store/StorePage");

                    }
                    else if (user.login.Equals(users[i].login) && user.password.Equals(users[i].password) && users[i].role.Equals(usr))
                    {
                        welcome = users[i].fname;
                        flag = true;
                        role = users[i].role;
                        break;
                    }
                    else
                    {
                        // if input user doesn't exist..
                        flag = false;
                    }
                }
                if (!flag)
                {
                    ViewBag.error = "This user does NOT exist... try again!";
                }
                else if (flag && role.Equals("admin"))
                {
                    // Go to tha main page as administrator...
                    return RedirectToAction("../Page/MainPage");
                }
                else if (flag && role.Equals("user"))
                {
                    // ..as user
                    return RedirectToAction("../MainPage/PageController");
                }
            }

            return View();
        }
    }
}