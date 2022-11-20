using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserRating.Models;
using UserRating.ViewModels;
using UserRating.Infrastructure.ServiceInterfaces;

namespace UserRating.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthenticationController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("HomePage", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_authService.IsLogin(loginViewModel.Login, loginViewModel.Password, out int id))
                {
                    var user = _userService.Get(id);

                    await Authenticate(user);

                    return RedirectToAction("Profile", "Profile");
                }
            }

            return View(loginViewModel);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_authService.IsRegistration(signUpViewModel.Login, signUpViewModel.Email))
                {
                    return View(signUpViewModel);
                }

                var user = new User
                {
                    Email = signUpViewModel.Email,
                    Login = signUpViewModel.Login,
                    Password = signUpViewModel.Password,
                    Role = 0
                };

                _userService.Create(user);

                user.Id = _userService.GetLastId();

                await Authenticate(user);

                return RedirectToAction("Edit", "Profile");
            }

            return View(signUpViewModel);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("HomePage", "Home");
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var claimIdentity =
                new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentity));
        }
    }
}