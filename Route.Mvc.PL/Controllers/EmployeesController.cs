using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Route.Mvc.BusinessLL.DataTransferObjects.Employee;
using Route.Mvc.BusinessLL.Services.Classes;
using Route.Mvc.BusinessLL.Services.Interfaces;
using Route.Mvc.DAL.Models.EmployeeModel;
using Route.Mvc.DAL.Models.Shared;
using Route.Mvc.PL.ViewModels.EmployessViewModel;

namespace Route.Mvc.PL.Controllers
{
    [Authorize]
    public class EmployeesController(IEmployeeService _employeeService , ILogger<EmployeesController> _logger , IWebHostEnvironment _environment) : Controller
    {


        public IActionResult Index(string? EmployeeSearchName)
        {
            var employees = _employeeService.GetAllEmployees(EmployeeSearchName);
            return View(employees);
        }









        #region Create Employee
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]

        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var createdEmployeeDto = new CreatedEmployeeDto()
                    {
                        Name = employeeViewModel.Name,
                        Age = employeeViewModel.Age,
                        Address = employeeViewModel.Address,
                        Salary = employeeViewModel.Salary,
                        IsActive = employeeViewModel.IsActive,
                        Email = employeeViewModel.Email,
                        PhoneNumber = employeeViewModel.PhoneNumber,
                        EmployeeType = employeeViewModel.EmployeeType,
                        Gender= employeeViewModel.Gender,
                        HiringDate = employeeViewModel.HiringDate,
                        DepartmentId = employeeViewModel.DepartmentId,
                        Image = employeeViewModel.Image


                    };
                    string message;
                    int result = _employeeService.AddEmployee(createdEmployeeDto);
                    if (result > 0)
                    {
                        message = $"Employee {employeeViewModel.Name} Created Successfully";
                        TempData["message"] = message;

                    }
                    else
                    {
                       message = $"Employee {employeeViewModel.Name}  Cannot be Created";
                        TempData["message"] = message;
                    }
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError("", ex.Message);

                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }



                }
            }

            return View(employeeViewModel);


        }


        #endregion



        #region Details

        [HttpGet]

        public IActionResult Details(int? id )
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
                
            

            return employee is null ? NotFound() :  View(employee);

        }




        #endregion



        #region Edit Employee
        [HttpGet]

        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null)
            {
                return NotFound();
            }
            var employeeDto = new EmployeeViewModel()
            {
                //Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age.Value,
                Address = employee.Address,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                DepartmentId = employee.DepartmentId
            };
            return View(employeeDto);

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]

        public IActionResult Edit(EmployeeViewModel employeeViewModel, [FromRoute] int? id )
        {
            if (!id.HasValue) return BadRequest();  

            if (ModelState.IsValid)
            {
                try
                {

                    var updatedEmployeeDto = new UpdatedEmployeeDto()
                    {
                        Id = id.Value,
                        Name = employeeViewModel.Name,
                        Age = employeeViewModel.Age,
                        Address = employeeViewModel.Address,
                        Salary = employeeViewModel.Salary,
                        IsActive = employeeViewModel.IsActive,
                        Email = employeeViewModel.Email,
                        PhoneNumber = employeeViewModel.PhoneNumber,
                        HiringDate =employeeViewModel.HiringDate,
                        Gender = employeeViewModel.Gender,
                        EmployeeType = employeeViewModel.EmployeeType,
                        DepartmentId = employeeViewModel.DepartmentId,
                        Image = employeeViewModel.Image


                    };


                    int result = _employeeService.UpdateEmployee(updatedEmployeeDto);
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Employee not updated");
                        return View(updatedEmployeeDto);
                    }


                }
                catch(Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError("", ex.Message);
                        return View(employeeViewModel);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                        return View("ErrorView", ex);
                            
                    }
                }

            }



            return View(employeeViewModel);

        }


        #endregion



        #region Delete Employee


        [HttpPost]
        //[ValidateAntiForgeryToken]
          
        public IActionResult Delete(int id )
        {
            if (id == 0) return BadRequest();
            try
            {
                bool deleted = _employeeService.DeleteEmployee(id);
                if (deleted) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee is not Deleted");
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


        #endregion




    }
}
