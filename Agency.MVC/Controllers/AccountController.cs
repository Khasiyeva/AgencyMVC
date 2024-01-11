using Agency.Business.ViewModels;
using Agency.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Agency.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = new AppUser()
            {
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                Email = registerVM.Email,
                UserName = registerVM.UserName
            };

            var result = await _userManager.CreateAsync(user, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();

            }
            await _signInManager.SignInAsync(user, false);


            return RedirectToAction(nameof(Index), "Home");
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var user = await _userManager.FindByEmailAsync(loginVM.EmailOrUsername);
            if (user == null)
            {
                ModelState.AddModelError("", "Not Found");
                return View();
            }

            var result = _signInManager.CheckPasswordSignInAsync(user, loginVM.Password, true).Result;
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Not Found");
                return View();
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Waiting");
                return View();
            }

            await _signInManager.SignInAsync(user, true);

            return RedirectToAction(nameof(Index), "Home");
            return View();
        }

        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
