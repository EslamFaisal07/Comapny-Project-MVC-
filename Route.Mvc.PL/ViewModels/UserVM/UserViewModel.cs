using System.ComponentModel.DataAnnotations;

namespace Route.Mvc.PL.ViewModels.UserVM
{
    public class UserViewModel
    {


        public string Id { get; set; } = default!;

        public string FName { get; set; } = default!;
        public string LName { get; set; } = default!;
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = default!;

        public IEnumerable<string> Roles { get; set; } = new List<string>();


    }
}
