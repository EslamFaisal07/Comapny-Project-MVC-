using Microsoft.AspNetCore.Mvc;
using Route.Mvc.BusinessLL.DataTransferObjects.Employee;
using Route.Mvc.BusinessLL.Services.Classes;
using Route.Mvc.BusinessLL.Services.Interfaces;
using Route.Mvc.DAL.Models.EmployeeModel;
using Route.Mvc.DAL.Models.Shared;

namespace Route.Mvc.PL.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService , ILogger<EmployeesController> _logger , IWebHostEnvironment _environment) : Controller
    {
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();
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

        public IActionResult Create(CreatedEmployeeDto createdEmployeeDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int result = _employeeService.AddEmployee(createdEmployeeDto);
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Employee not created");
                    }

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

            return View(createdEmployeeDto);


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
            var employeeDto = new UpdatedEmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
            };
            return View(employeeDto);

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]

        public IActionResult Edit(UpdatedEmployeeDto updatedEmployeeDto, [FromRoute] int? id )
        {
            if (!id.HasValue || id != updatedEmployeeDto.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
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
                        return View(updatedEmployeeDto);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                        return View("ErrorView", ex);
                            
                    }
                }

            }



            return View(updatedEmployeeDto);

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
