using DutyFree.Web.Data;
using DutyFree.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DutyFree.Web.Controllers
{
    public class UserOrdersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserOrdersController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _db.Users.FindAsync(
                int.Parse(User.FindFirst("UserId").Value)
            );

            IEnumerable<Order> objOrderList = _db.Orders.Where(order => order.UserId == user.UserId);
            return View(objOrderList);
        }

        public IActionResult DeleteOrder(int productId)
        {
            var order = _db.Orders.Where(order => order.ProductId == productId).FirstOrDefault();
            _db.Orders.Remove(order);

            _db.SaveChanges();
            return RedirectToAction("Index", "UserOrders");
        }

        public async Task<IActionResult> ShowTableAsync()
        {
            var user = await _db.Users.FindAsync(
               int.Parse(User.FindFirst("UserId").Value)
           );

            IEnumerable<Order> objOrderList = _db.Orders.Where(order => order.UserId == user.UserId);        
            return PartialView("UserOrdersTable", objOrderList);
        }
    }
}
