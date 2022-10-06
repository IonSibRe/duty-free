using DutyFree.Web.Data;
using DutyFree.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DutyFree.Web.Controllers
{
    public class SecurityController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SecurityController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<User> objUserList = _db.Users.ToList();
            return View(objUserList);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(int userId)
        {
            var user = _db.Users.Find(userId); 
            var claims = new List<Claim>
                {
                    new Claim("UserId", user.UserId.ToString(), ClaimValueTypes.Integer),
                    new Claim("Name", user.Name),
                    new Claim("Email", user.Email),
                    new Claim("ImageUrl", user.ImageUrl ?? string.Empty),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties();
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
