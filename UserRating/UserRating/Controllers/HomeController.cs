using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UserRating.Models;

namespace UserRating.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult HomePage()
        {
            return View();
        }
    }
}