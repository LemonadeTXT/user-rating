using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserRating.Infrastructure.ServiceInterfaces;
using UserRating.ViewModels;
using UserRating.Models;
using AutoMapper;

namespace UserRating.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View(_userService.Get(int.Parse(User.Identity.Name)));
        }

        [Authorize]
        public IActionResult Edit()
        {
            var user = _userService.Get(int.Parse(User.Identity.Name));

            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, ProfileViewModel>());

            var mapper = new Mapper(config);

            var profileViewModel = mapper.Map<User, ProfileViewModel>(user);

            return View(profileViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(ProfileViewModel profileViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.Get(int.Parse(User.Identity.Name));

                foreach (var file in Request.Form.Files)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);

                        profileViewModel.Avatar = memoryStream.ToArray();
                    }
                }

                if (!Equal(user, profileViewModel))
                {
                    var id = user.Id;

                    var config = new MapperConfiguration(cfg => cfg.CreateMap<ProfileViewModel, User>());

                    var mapper = new Mapper(config);

                    user = mapper.Map<ProfileViewModel, User>(profileViewModel);

                    user.Id = id;

                    _userService.Edit(user);

                    return RedirectToAction(nameof(Profile));
                }
            }

            return View(profileViewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult RemoveAvatar()
        {
            var user = _userService.Get(int.Parse(User.Identity.Name));

            if (user.Avatar != null)
            {
                _userService.RemoveAvatar(int.Parse(User.Identity.Name));

                return RedirectToAction(nameof(Profile));
            }

            return RedirectToAction(nameof(Edit));
        }

        private bool Equal(User user, ProfileViewModel profileViewModel)
        {
            if (user.FirstName != profileViewModel.FirstName)
            {
                return false;
            }
            else if (user.LastName != profileViewModel.LastName)
            {
                return false;
            }
            else if (user.Age != profileViewModel.Age)
            {
                return false;
            }
            else if (user.City != profileViewModel.City)
            {
                return false;
            }
            else if (user.AboutMe != profileViewModel.AboutMe)
            {
                return false;
            }
            else if (user.Avatar != profileViewModel.Avatar && profileViewModel.Avatar != null)
            {
                return false;
            }
            else if (user.Email != profileViewModel.Email)
            {
                return false;
            }
            else if (user.Login != profileViewModel.Login)
            {
                return false;
            }
            else if (user.Password != profileViewModel.Password)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}