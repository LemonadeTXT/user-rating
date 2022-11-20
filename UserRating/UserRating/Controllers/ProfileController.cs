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
        private readonly IMapper _mapper;

        public ProfileController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
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

            var profileViewModel = _mapper.Map<User, ProfileViewModel>(user);

            return View(profileViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(ProfileViewModel profileViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.Get(int.Parse(User.Identity.Name));

                profileViewModel.Avatar = _userService.ConvertAvatarToByteArray(Request);

                if (!Equal(user, profileViewModel))
                {
                    var id = user.Id;

                    user = _mapper.Map<ProfileViewModel, User>(profileViewModel);

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
            if (user.FirstName != profileViewModel.FirstName ||
                user.LastName != profileViewModel.LastName ||
                user.Age != profileViewModel.Age ||
                user.City != profileViewModel.City ||
                user.AboutMe != profileViewModel.AboutMe ||
                (user.Avatar != profileViewModel.Avatar && profileViewModel.Avatar != null) ||
                user.Email != profileViewModel.Email ||
                user.Login != profileViewModel.Login ||
                user.Password != profileViewModel.Password)
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