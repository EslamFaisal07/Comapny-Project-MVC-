using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route.Mvc.DAL.Models;
using Route.Mvc.PL.ViewModels;

namespace Route.Mvc.PL.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager) : Controller
    {
        #region Register
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {

            if (!ModelState.IsValid)
                return View(registerViewModel);

            var User = new ApplicationUser()
            {
                FristName = registerViewModel.FristName,
                Email = registerViewModel.Email,
                LastName = registerViewModel.LastName,
                UserName = registerViewModel.UserName,
            };

            var Result = _userManager.CreateAsync(User, registerViewModel.Password).Result;

            if (Result.Succeeded)
                return RedirectToAction(nameof(Login));
            else
            {
                foreach (var Error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, Error.Description);
                }
                return View(registerViewModel);
            }
        }



        #endregion

        #region Login

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]

        public IActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            else
            {
                var user = _userManager.FindByEmailAsync(viewModel.Email).Result;
                if (user != null)
                {
                    var Flag = _userManager.CheckPasswordAsync(user, viewModel.Password).Result;
                    if (Flag)
                    {

                        var result = _signInManager.PasswordSignInAsync(user, viewModel.Password, viewModel.RememberMe, false).Result;

                        if (result.Succeeded)

                            return RedirectToAction(nameof(HomeController.Index), "Home");




                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "form is not found");

                    }
                }
                return View(viewModel);
            }

        }

        #endregion

        public new IActionResult SignOut()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(Login));
        }








    }
}
