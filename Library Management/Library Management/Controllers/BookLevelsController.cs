using Library_Management.Data;
using Library_Management.Filters;
using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
    [ServiceFilter(typeof(AdminAuthorizeFilter))]
    public class BookLevels : Controller
    {
        private readonly ApplicationDbContext _context;
        public BookLevels(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var BookLevels = _context.BookLevels.ToList();
            return View(BookLevels);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(BookLevel bookLevels)
        {
            BookLevel value = new BookLevel();
            value.Name = bookLevels.Name;
            _context.BookLevels.Add(value);
            _context.SaveChanges();
            return RedirectToAction("Index", "BookLevels");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            BookLevel bookLevels = _context.BookLevels.Find(id);
            _context.BookLevels.Remove(bookLevels);
            _context.SaveChanges();
            return RedirectToAction("Index", "BookLevels");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            BookLevel bookLevels = _context.BookLevels.Find(id);
            return View(bookLevels);
        }


        [HttpPost]
        public IActionResult Edit(BookLevel bookLevels, int id)
        {
            BookLevel value = _context.BookLevels.Find(id);
            value.Name = bookLevels.Name;
            _context.SaveChanges();

            return RedirectToAction("Index", "BookLevels");
        }
    }
}
