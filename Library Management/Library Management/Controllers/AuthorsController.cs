using Library_Management.Data;
using Library_Management.Filters;
using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
    [ServiceFilter(typeof(AdminAuthorizeFilter))]
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AuthorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var Authors = _context.BookAuthors.ToList();
            return View(Authors);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(BookAuthor authors)
        {
            BookAuthor value = new BookAuthor();
            value.Name = authors.Name;
            _context.BookAuthors.Add(value);
            _context.SaveChanges();
            return RedirectToAction("Index", "Authors");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            BookAuthor authors = _context.BookAuthors.Find(id);
            _context.BookAuthors.Remove(authors);
            _context.SaveChanges();
            return RedirectToAction("Index", "Authors");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            BookAuthor author = _context.BookAuthors.Find(id);
            return View(author);
        }


        [HttpPost]
        public IActionResult Edit(BookAuthor author, int id)
        {
            BookAuthor value = _context.BookAuthors.Find(id);
            value.Name = author.Name;
            _context.SaveChanges();

            return RedirectToAction("Index", "Authors");
        }
    }
}
