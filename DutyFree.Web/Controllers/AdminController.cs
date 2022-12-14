using DutyFree.Web.Data;
using DutyFree.Web.Models;
using DutyFree.Web.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace DutyFree.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _db;

        public AdminController(IWebHostEnvironment environment, ApplicationDbContext db)
        {
            _db = db;
            _environment = environment;
        }
        public IActionResult Index()
        {           
            var vm = new AdminViewModel();
            vm.Products = _db.Products.ToList();

            List<string> categories = new List<string>();
            foreach (Category category in _db.Categories)
            {
                categories.Add(category.CategoryName);
            }

            ViewData["Categories"] = categories;

            return View(vm.Products);
        }

        [HttpPost]
        public IActionResult Insert()
        {
            string name = Request.Form["Name"];
            int price = Convert.ToInt32(Request.Form["Price"]);
            int qty = Convert.ToInt32(Request.Form["Quantity"]);
            string category = Request.Form["Category"];
            IFormFile image = Request.Form.Files[0];
            int priceAfter = new int();
            bool isNew = Convert.ToBoolean(Request.Form["New"]);
            
            string imgUrl = String.Empty;

            try 
            {
                priceAfter = Convert.ToInt32(Request.Form["Price-After"]);
            }
            catch(Exception)
            {
                priceAfter = 0;
            }

            try
            {
               string fileName = Path.GetFileName(image.FileName);
               imgUrl = $"\\Images\\{fileName}";

                using (FileStream stream = new FileStream(Path.Combine(_environment.WebRootPath + "\\Images", fileName), FileMode.Create))
                {
                    image.CopyTo(stream);
                    stream.Close();
                }                             
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            _db.Products.Add(new Product { Name = name, Price = price, Quantity = qty, ImageUrl = imgUrl, CreatedBy = 1, Discount = priceAfter, CategoryName = category, isNew = isNew });
            _db.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        public IActionResult Edit()
        {
            string name = Request.Form["Name"];
            int price = Convert.ToInt32(Request.Form["Price"]);
            int qty = Convert.ToInt32(Request.Form["Quantity"]);
            IFormFile? image;
            int id = Convert.ToInt32(Request.Form["Id"]);
            int priceAfter = new int();

            try
            {
                priceAfter = Convert.ToInt32(Request.Form["Price-After"]);
            }
            catch (Exception)
            {
                priceAfter = 0;
            }

            try {
                image = Request.Form.Files[0]; 
            }
            catch (Exception) {
                image = null;
            }

            string imgUrl = String.Empty;

            try
            {
                if (image != null)
                {
                    string fileName = Path.GetFileName(image.FileName);
                    imgUrl = $"\\Images\\{fileName}";

                    using (FileStream stream = new FileStream(Path.Combine(_environment.WebRootPath + "\\Images", fileName), FileMode.Create))
                    {
                        image.CopyTo(stream);
                        stream.Close();
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            _db.Products.ToList().Find(x => x.ProductId == id).Name = name;
            _db.Products.ToList().Find(x => x.ProductId == id).Price = price;
            _db.Products.ToList().Find(x => x.ProductId == id).Quantity = qty;
            _db.Products.ToList().Find(x => x.ProductId == id).DateUpdated = DateTime.Now;
            _db.Products.ToList().Find(x => x.ProductId == id).UpdatedBy = 1;
            _db.Products.ToList().Find(x => x.ProductId == id).Discount = priceAfter;

            if (image != null)
            {
                _db.Products.ToList().Find(x => x.ProductId == id).ImageUrl = imgUrl;
            }

            _db.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        public IActionResult Delete(int id)
        {
            var item = _db.Products.Find(id);
            item.DateUpdated = DateTime.Now;
            item.isDeleted = true;
            _db.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }
    }
}
