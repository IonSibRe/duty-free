using DutyFree.Web.Data;
using DutyFree.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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

        public async Task<IActionResult> DeleteOrder(int orderId, int productId)
        {

            var order = _db.Orders.Where(order => order.OrderId == orderId).FirstOrDefault();

            _db.Orders.Remove(order);
            _db.Products.ToList().Find(x => x.ProductId == productId).Quantity += 1;


            _db.SaveChangesAsync();

            return View();
        }

        public async Task<IActionResult> ShowTableAsync(DateTime dateFrom, DateTime dateTo)
        {
            var user = await _db.Users.FindAsync(
               int.Parse(User.FindFirst("UserId").Value)
           );

            var query = _db.Orders
                .Where(o => dateFrom <= o.DateCreated && o.DateCreated <= dateTo.AddDays(1) && o.UserId == user.UserId);

            var orders = await query
                .OrderByDescending(o => o.DateCreated)
                .ToListAsync();

            return PartialView("UserOrdersTable", orders);
        }
    }
}
