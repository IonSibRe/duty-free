using DutyFree.Web.Data;
using DutyFree.Web.Models;
using DutyFree.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DutyFree.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _db = db;
            _logger = logger;
            //_currentUserAccessor = currentUserAccessor;
        }

        public IActionResult Index()
        {
            IEnumerable <Product> objProductList = _db.Products.ToList();
            return View(objProductList);
        }
        public async Task<IActionResult> AddOrder(int productId)
        {
            var product = _db.Products.Find(productId);
            var user = await _db.Users.FindAsync(
                int.Parse(User.FindFirst("UserId").Value)
            );

            var order = new Order() {
                Name = product.Name,
                Price = product.Price,
                UserId = user.UserId,
                ProductId = product.ProductId
            };

            _db.Orders.Add(order);
            _db.SaveChanges();
            return RedirectToAction("Index", "UserOrders");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}