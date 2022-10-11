using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserRating.Data;

namespace UserRating.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProfileController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userLogin = User?.Identity?.Name;

            var user = _db.Users.Where(x => x.Login == userLogin).FirstOrDefaultAsync().Result;

            return View(user);
        }

        [Authorize]
        public async Task<IActionResult> ProfileChanger()
        {
            return View();
        }
    }
}
