using System.ComponentModel.DataAnnotations;

namespace Route.Mvc.PL.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Frist Name Can't Be Null")]
        [MaxLength(50)]
        public string FristName { get; set; } = null!;
        [Required(ErrorMessage = "Last Name Can't Be Null")]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "User Name Can't Be Null")]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;
        public bool IsAgree { get; set; }
    }
}
