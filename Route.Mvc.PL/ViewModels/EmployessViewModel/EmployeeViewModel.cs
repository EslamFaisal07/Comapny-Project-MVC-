using Route.Mvc.DAL.Models.EmployeeModel;
using Route.Mvc.DAL.Models.Shared;
using System.ComponentModel.DataAnnotations;

namespace Route.Mvc.PL.ViewModels.EmployessViewModel
{
    public class EmployeeViewModel
    {


        [Required]
        [MaxLength(50, ErrorMessage = "Max length should be 50 characters")]
        [MinLength(5, ErrorMessage = "Min length should be 5 characters")]
        public string Name { get; set; } = null!;

        [Range(22, 35)]
        public int? Age { get; set; }

        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string? Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmployeeType EmployeeType { get; set; }

        [Display(Name = "Department")]

        public int? DepartmentId { get; set; }







    }
}
