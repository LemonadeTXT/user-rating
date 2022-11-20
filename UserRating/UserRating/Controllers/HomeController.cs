using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserRating.Infrastructure.ServiceInterfaces;
using UserRating.Models;

namespace UserRating.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProfileService _profileService;

        public HomeController(IUserService userService, IProfileService profileService)
        {
            _userService = userService;
            _profileService = profileService;
        }

        public IActionResult HomePage()
        {
            var users = new List<User>(_userService.GetAll());

            return View(RemoveIncompleteUsers(users));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Like(int id)
        {
            var appraiser = _userService.Get(int.Parse(User.Identity.Name));

            var likedUser = _userService.Get(id);

            if (likedUser.Id != appraiser.Id)
            {
                _profileService.Like(likedUser.Id, appraiser.Id);
            }

            return RedirectToAction(nameof(HomePage));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Dislike(int id)
        {
            var appraiser = _userService.Get(int.Parse(User.Identity.Name));

            var likedUser = _userService.Get(id);

            if (likedUser.Id != appraiser.Id)
            {
                _profileService.Dislike(likedUser.Id, appraiser.Id);
            }

            return RedirectToAction(nameof(HomePage));
        }

        private List<User> RemoveIncompleteUsers(List<User> users)
        {
            foreach (var item in users.ToList())
            {
                if (string.IsNullOrEmpty(item.FirstName) ||
                    string.IsNullOrEmpty(item.LastName) ||
                    item.Age is 0 ||
                    string.IsNullOrEmpty(item.City) ||
                    string.IsNullOrEmpty(item.AboutMe))
                {
                    users.Remove(item);
                }
            }

            return users;
        }
    }
}