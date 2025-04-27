using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route.Mvc.DAL.Models;
using Route.Mvc.PL.ViewModels.UserVM;

namespace Route.Mvc.PL.Controllers
{
    public class UserController(UserManager<ApplicationUser> _userManager , ILogger<UserController> _logger , IWebHostEnvironment _environment) : Controller
    {



        [HttpGet]
        public IActionResult Index(string? SearchName)
        {
            var users = _userManager.Users.ToList();
            if (!string.IsNullOrWhiteSpace(SearchName))
            {
                users = _userManager.Users.Where(e => e.FristName.ToLower().Contains(SearchName.ToLower())).ToList();
            }







            var userVM = users.Select(user => new UserViewModel
            {
                Id = user.Id,
                FName = user.FristName,
                LName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = _userManager.GetRolesAsync(user).Result
            }).ToList();

            return View(userVM);








        }



        [HttpGet]

        public IActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();
            var user = _userManager.FindByIdAsync(id.ToString());


            if (user is null)
            {
                return NotFound();
            }

            var userVM = new UserViewModel
            {
                Id = user.Result.Id,
                FName = user.Result.FristName,
                LName = user.Result.LastName,
                Email = user.Result.Email,
                PhoneNumber = user.Result.PhoneNumber,
                Roles = _userManager.GetRolesAsync(user.Result).Result
            };


            return View(userVM);



        }



        [HttpGet]

        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();
            var user = _userManager.FindByIdAsync(id.ToString());
            if (user is null)
            {
                return NotFound();
            }
            var userVM = new EditUserVM
            {
                Id = user.Result.Id,
                FName = user.Result.FristName,
                LName = user.Result.LastName,
                Email = user.Result.Email,
                PhoneNumber = user.Result.PhoneNumber,

            };
            return View(userVM);


        }



        [HttpPost]
        public IActionResult Edit(EditUserVM userViewModel ,[FromRoute] string id )
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var user = _userManager.FindByIdAsync(id);
            if (user is null)
            {
                return NotFound();
            }

            user.Result.FristName = userViewModel.FName;
            user.Result.LastName = userViewModel.LName;
            user.Result.Email = userViewModel.Email;
            user.Result.PhoneNumber = userViewModel.PhoneNumber;



            var result = _userManager.UpdateAsync(user.Result);

            if (result.Result.Succeeded)
            {
            
              
          return   RedirectToAction("Index");
            }

            foreach (var error in result.Result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(userViewModel);

        }





        [HttpPost]

        public IActionResult Delete(string id) 
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();


            try
            {
                var user = _userManager.FindByIdAsync(id);
                if (user is null)
                {
                    return NotFound();
                }
              
                    
                var result = _userManager.DeleteAsync(user.Result);
                if (result.Result.Succeeded) 
                {


                    return RedirectToAction(nameof(Index));
                
                
                }
                else
                {
                    foreach (var error in result.Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                        return RedirectToAction(nameof(Delete), new { id });
                }
              
            }
            catch (Exception ex)
            {

                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }

            }

        }













    }
}
