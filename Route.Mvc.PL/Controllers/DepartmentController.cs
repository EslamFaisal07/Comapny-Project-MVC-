
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Route.Mvc.BusinessLL.DataTransferObjects.Department;
using Route.Mvc.BusinessLL.Services.Interfaces;
using Route.Mvc.PL.ViewModels.DepartmentsViewModels;


namespace Route.Mvc.PL.Controllers
{
    [Authorize]
    public class DepartmentController(IDepartmentService _departmentService
        , ILogger<DepartmentController> _logger, IWebHostEnvironment _environment) : Controller
    {


        #region Index()


        public IActionResult Index()
        {

            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }



        #endregion

        #region Create Department

        [HttpGet]
        public IActionResult Create()
        {

            return View();

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentViewModel departmentViewModel)
        {

            if (ModelState.IsValid)
            {

                try
                {

                    var departmentDto = new CreatedDepartmentDto()
                    {
                        Name = departmentViewModel.Name,
                        Code = departmentViewModel.Code,
                        Description = departmentViewModel.Description,
                        DateOfCreation = departmentViewModel.DateOfCreation,
                    };

                    string message;

                    var result = _departmentService.AddDepartment(departmentDto);
                    if (result > 0)
                        message = $"Department {departmentViewModel.Name} Created Successfully";
                    else
                    {
                        message = $"Department {departmentViewModel.Name} Cannot be Created";
                    }
                    TempData["message"] = message;
                    return RedirectToAction(nameof(Index));


                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);


                    }
                    else
                    {
                        _logger.LogError(ex.Message);


                    }



                }



            }

            return View(departmentViewModel);







        }



        #endregion


        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {

            if (!id.HasValue)
            {
                return BadRequest();

            }
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
            {
                return NotFound();
            }
            return View(department);

        }











        #endregion




        #region Edit

        [HttpGet]

        public ActionResult Edit(int? id)
        {

            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null) return NotFound();
            var departmentViewModel = new DepartmentViewModel()
            {
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = department.CreatedOn,

            };
            return View(departmentViewModel);



        }


        [HttpPost]
        //[ValidateAntiForgeryToken]

        public IActionResult Edit(DepartmentViewModel viewModel, [FromRoute] int id)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var updatedDepartment = new UpdatedDepartmentDto()
                    {
                        Id = id,
                        Name = viewModel.Name,
                        Code = viewModel.Code,
                        Description = viewModel.Description,
                        DateOfCreation = viewModel.DateOfCreation,

                    };
                    var result = _departmentService.UpdateDepartment(updatedDepartment);

                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Cannot be Updated");

                    }





                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);



                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                        return View("ErrorView", ex);
                    }

                }
            }
            return View(viewModel);

        }



        #endregion


        #region Delete

        [HttpPost]
        //[ValidateAntiForgeryToken]

        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool deleted = _departmentService.DeleteDepartment(id);
                if (deleted) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department is not Deleted");
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





        #endregion



