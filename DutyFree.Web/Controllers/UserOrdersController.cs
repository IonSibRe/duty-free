using DutyFree.Web.Data;
using DutyFree.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> ShowTableAsync(string dateFrom, string dateTo)
        {
            var user = await _db.Users.FindAsync(
               int.Parse(User.FindFirst("UserId").Value)
           );

            long dateFromLong = long.Parse(dateFrom);
            var dateFromConverted = new DateTime(dateFromLong, DateTimeKind.Local);

            long dateToLong = long.Parse(dateTo);
            var dateToConverted = new DateTime(dateToLong, DateTimeKind.Local);

            var query = _db.Orders
                .Where(o => dateFromConverted <= o.DateCreated && o.DateCreated <= dateToConverted.AddDays(1) && o.UserId == user.UserId);

            var orders = await query
                .OrderByDescending(o => o.DateCreated)
                .ToListAsync();
  
            return PartialView("UserOrdersTable", orders);
        }
    }
}
