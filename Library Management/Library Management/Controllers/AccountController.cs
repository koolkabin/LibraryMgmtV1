using Microsoft.AspNetCore.Mvc;
using Library_Management.Models;
using Library_Management.Data;

namespace Library_Management.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public AccountController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _contextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string email,string password)
        {
            if(email != "" && password != "")
            {
                User userData = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password );
                if(userData != null)
                {
                    _contextAccessor.HttpContext.Session.SetInt32("userId", userData.Id);
                    _contextAccessor.HttpContext.Session.SetString("userType", userData.UserType.ToString()); 

                    if (userData.UserType == EnumUserType.Admin)
                    {
                        return RedirectToAction("Index", "Book");
                    }
                    else
                    {
                        return RedirectToAction("Index","Student");
                    }
                }
                TempData["ErrorMessage"] = "User Not Found!!";
                return RedirectToAction("Index");


            }
            TempData["ErrorMessage"] = "Empty Fields!!";
            return RedirectToAction("Index");
        }


        public IActionResult Logout() 
        {
            _contextAccessor.HttpContext.Session.Remove("userId");
            _contextAccessor.HttpContext.Session.Remove("userType");
            _contextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
