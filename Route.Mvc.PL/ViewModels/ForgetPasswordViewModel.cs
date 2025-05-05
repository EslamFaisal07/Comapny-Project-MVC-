using System.ComponentModel.DataAnnotations;

namespace Route.Mvc.PL.ViewModels
{
    public class ForgetPasswordViewModel
    {

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Email is Required")]

        public string Email { get; set; } = null!;


    }
}
