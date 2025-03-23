using Microsoft.AspNetCore.Mvc;

namespace Route.Mvc.PL.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
