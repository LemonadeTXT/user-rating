using Microsoft.AspNetCore.Mvc;
using UserRating.Infrastructure.ServiceInterfaces;

namespace UserRating.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult HomePage()
        {
            return View(_userService.GetAll());
        }
    }
}