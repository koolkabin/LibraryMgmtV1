using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
