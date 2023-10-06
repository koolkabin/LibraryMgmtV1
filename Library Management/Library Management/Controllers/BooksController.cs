using Library_Management.Data;
using Library_Management.Filters;
using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Library_Management.Controllers
{
    [ServiceFilter(typeof(AdminAuthorizeFilter))]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [ServiceFilter(typeof(AdminAuthorizeFilter))]
        public IActionResult Index()
        {
            var bookList = _context.Books.Include(x => x.BookAuthor).Include(x => x.BookCategory).ToList();
            return View(bookList);
        }

        //Get for ADD New Data
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AuthorList = new SelectList(_context.BookAuthors.ToList(), "Id", "Name");
            ViewBag.CategroyList = new SelectList(_context.BookCategories.ToList(), "Id", "Name");
            return View();
        }

        //Post for ADD New Data
        [HttpPost]
        public IActionResult Create(Books books)
        {
            Books value = new Books();
            value.Name = books.Name;
            value.AuthorId = books.AuthorId;
            value.ISBN = books.ISBN;
            value.CategoryId = books.CategoryId;
            value.Level = books.Level;
            value.Count = books.Count;
            value.Publication = books.Publication;
            value.UpdateDate = DateTime.Now;
            _context.Books.Add(value);
            _context.SaveChanges();
            return RedirectToAction("Index", "Books");

        }

        //Post for Delete
        //Or use Post Method for using the Form Method
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Books value = _context.Books.Find(id);
            _context.Books.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Index", "Books");
        }

        //GET for Edit Data
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Books book = _context.Books.Where(x => x.Id == id).First();
            return View(book);
        }

        //Post for Edit Data
        [HttpPost]
        public IActionResult Edit(Books books, int id)
        {
            Books value = _context.Books.Find(id);
            value.Name = books.Name;
            value.AuthorId = books.AuthorId;
            value.ISBN = books.ISBN;
            value.CategoryId = books.CategoryId;
            value.Level = books.Level;
            value.Count = books.Count;
            value.Publication = books.Publication;
            _context.SaveChanges();
            return RedirectToAction("Index", "Books");

        }
    }
}
