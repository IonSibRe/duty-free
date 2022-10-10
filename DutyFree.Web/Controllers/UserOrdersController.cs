using Microsoft.AspNetCore.Mvc;

namespace DutyFree.Web.Controllers
{
    public class UserOrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
