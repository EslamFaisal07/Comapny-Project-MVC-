using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route.Mvc.DAL.Models;
using Route.Mvc.PL.ViewModels.RoleVM;

namespace Route.Mvc.PL.Controllers
{
    public class RoleController(RoleManager<IdentityRole> _roleManager, UserManager<ApplicationUser> _userManager , ILogger<RoleController> _logger , IWebHostEnvironment _environment) : Controller
    {

        [HttpGet]
        public IActionResult Index(string? SearchName)
        {
            var role = _roleManager.Roles.ToList();
            if (!string.IsNullOrWhiteSpace(SearchName))
            {
                role = _roleManager.Roles.Where(e => e.Name.ToLower().Contains(SearchName.ToLower())).ToList();
            }
            var roleVM = role.Select(r => new RoleViewModel()
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();
            return View(roleVM);




        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
              var result=  _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = roleViewModel.Name
                }).Result;
                return RedirectToAction(nameof(Index));

            }

            return View(roleViewModel);



        }



        [HttpGet]
        public IActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var role = _roleManager.FindByIdAsync(id).Result;
            if (role == null) return NotFound();
            var roleVM = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(roleVM);

        }





        [HttpGet]
        public IActionResult Edit(string? id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role is null) return NotFound();
            var roleVM = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(roleVM);
        }




        [HttpPost]
        public IActionResult Edit(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = _roleManager.FindByIdAsync(roleViewModel.Id).Result;

                if (role == null) return NotFound();


                role.Name = roleViewModel.Name;

              var result =   _roleManager.UpdateAsync(role);
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
                }
            }
            return View(roleViewModel);
        }





        [HttpPost]



        public IActionResult Delete( string id)
        {
            if(string.IsNullOrEmpty(id)) return NotFound();
            try
            {
                var role = _roleManager.FindByIdAsync(id);
                if (role is null)
                {
                    return NotFound();
                }


                var result = _roleManager.DeleteAsync(role.Result);
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
