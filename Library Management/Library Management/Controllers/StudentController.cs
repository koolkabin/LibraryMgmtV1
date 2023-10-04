using Library_Management.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library_Management.Models;
using Microsoft.AspNetCore.Http;

namespace Library_Management.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public StudentController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _contextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            if (_contextAccessor.HttpContext.Session.GetInt32("userId") == null)
            {
                return RedirectToAction("Index", "Account");
            }
            if (_contextAccessor.HttpContext.Session.GetString("userType") == "Admin")
            {
                return RedirectToAction("ErrorPage", "Book");
            }
            var bookList = _context.Books.Include(x => x.BookAuthor).Include(x => x.BookCategory).ToList();
            return View(bookList);
        }

        public IActionResult MyRequestBook()
        {
            var myReqBookList = _context.RequestBooks.Where(x => x.UserId == _contextAccessor.HttpContext.Session.GetInt32("userId")).Include(x => x.Books).ToList();
            return View(myReqBookList);
        }
    }
}
