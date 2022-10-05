using DutyFree.Web.Data;
using DutyFree.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DutyFree.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objProductList = _db.Products.ToList();
            return View(objProductList);
        }
    }
}
