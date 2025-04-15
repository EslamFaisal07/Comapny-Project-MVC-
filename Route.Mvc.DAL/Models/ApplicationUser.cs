using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Mvc.DAL.Models
{
    public class ApplicationUser :IdentityUser
    {

        public string FristName { get; set; } = null!;
        public string? LastName { get; set; }


    }
}
