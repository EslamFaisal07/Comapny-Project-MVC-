using MailKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route.Mvc.DAL.Models;
using Route.Mvc.PL.Utilites;
using Route.Mvc.PL.ViewModels;
using Route.Mvc.PL.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;


namespace Route.Mvc.PL.Controllers
{
    public class AccountController(Route.Mvc.PL.Helpers.IMailService _mailService, UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager) : Controller
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


        public IActionResult GoogleLogin()
        {
            var prop = new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")

            };
            return Challenge(prop,GoogleDefaults.AuthenticationScheme);
        }



        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(c=>new
            {
                c.Issuer,
                c.OriginalIssuer,
                c.Type,
                c.Value
            });

            return RedirectToAction("Index" , "Home");


        }













        #endregion

        public new IActionResult SignOut()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(Login));
        }



        #region forgetPassword



        [HttpGet]

        public IActionResult ForgetPassword() => View();



        [HttpPost]

        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {

                var user = _userManager.FindByEmailAsync(ViewModel.Email).Result;

                if (user is not null)
                {

                    var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var resetPasswordLink = Url.Action("ResetPassword", "Account", new
                    {
                        email = ViewModel.Email,
                        token
                    } , Request.Scheme);





                    var email = new Email()
                    {
                        To = ViewModel.Email,
                        Subject = "Reset Password",
                        Body = resetPasswordLink 


                    };

                    //EmailSettings.SendEmail(email);

                    _mailService.Send(email);


                    return RedirectToAction("CheckYourInbox");



                }




            }


            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(nameof(ForgetPassword), ViewModel);




        }


        [HttpGet]
        public IActionResult CheckYourInbox() => View();






        [HttpGet]

        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]

        public IActionResult ResetPassword(ResetPasswordViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            string email = TempData["email"]as string ?? string.Empty;
            string token = TempData["token"] as string ?? string.Empty;

            var user = _userManager.FindByEmailAsync(email).Result;


            if (user is not null)
            {
         var result =    _userManager.ResetPasswordAsync( user ,token , viewModel.Password ).Result;


                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("" ,error.Description);
                    }
                }


            }



            return View(nameof(ResetPassword),viewModel);






        }



        #endregion






    }
}

