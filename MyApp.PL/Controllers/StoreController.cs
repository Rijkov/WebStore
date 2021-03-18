namespace MyApp.PL.Controllers
{
    using MyApp.BLL.DTO;
    using System.Web.Mvc;
    using MyApp.BLL.Serveces;
    using System.Configuration;
    using System.Collections.Generic;
    using System;
    using System.Web;
    using System.IO;

    public class StoreController : Controller
    {
        OrderService db;
        public static List<ProductDTO> shop_cart = new List<ProductDTO>();
        static int quantity;

        public StoreController()
        {
            db = new OrderService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public ActionResult StorePage(int? Id)
        {
            IEnumerable<ProductDTO> result = InitProducts();
            PutItemCart(Id);

            return View(result);
        }

        [HttpPost]
        public ActionResult StorePage(ProductDTO product)
        {
            return View();
        }

        IEnumerable<ProductDTO> InitProducts()
        {
            string welcm = string.Empty;
            IEnumerable<ProductDTO> listProds = db.GetAllProducts();
            List<string> stocks = new List<string>();
            foreach (var p in listProds)
            {
                if (p.IsStock)
                {
                    stocks.Add("In Stock");
                }
                else stocks.Add("Out of Stock");
            }
            object[] datas = new object[] { welcm, stocks };
            if (AccountController.welcome != "")
            {
                welcm = AccountController.welcome;
                ViewBag.welcome = welcm;
            }
            ViewBag.Stock = stocks;
            return listProds;
        }

        void PutItemCart(int? Id)
        {
            if (Id != null)
            {
                ProductDTO item = db.GetProduct(Id);
                shop_cart.Add(item);
                quantity = shop_cart.Count;
                ViewBag.quantity = quantity;
            }
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fName;
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testFiles = file.FileName.Split(new char[] { '\\' });
                            fName = testFiles[testFiles.Length - 1];
                        }
                        else
                        {
                            fName = file.FileName;
                        }
                        fName = Path.Combine(Server.MapPath("~/Uploaded_Files/"), fName);
                        file.SaveAs(fName);
                    }
                    return Json("File has been Upload Successfully!!!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else // if file NOT selected
            {
                return Json("No file Selected...");
            }
        }
    }
}