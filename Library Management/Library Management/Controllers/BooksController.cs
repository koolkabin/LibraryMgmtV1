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


        public IActionResult Index()
        {
            var bookList = _context.Books.Include(x => x.BookAuthor).Include(x => x.BookCategory).Include(x => x.Publication).Include(x => x.BookLevel).ToList();
            return View(bookList);
        }

        //Get for ADD New Data
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AuthorList = new SelectList(_context.BookAuthors.ToList(), "Id", "Name");
            ViewBag.LevelList = new SelectList(_context.BookLevels.ToList(), "Id", "Name");
            ViewBag.CategroyList = new SelectList(_context.BookCategories.ToList(), "Id", "Name");
            ViewBag.PublicationList = new SelectList(_context.Publications.ToList(), "Id", "Name");
            return View();
        }

        //Post for ADD New Data
        [HttpPost]
        public IActionResult Create(Book books)
        {
            Book value = new Book();
            value.Name = books.Name;
            value.AuthorId = books.AuthorId;
            value.ISBN = books.ISBN;
            value.CategoryId = books.CategoryId;
            value.LevelId = books.LevelId;
            value.Count = books.Count;
            value.PublicationID = books.PublicationID;
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
            Book value = _context.Books.Find(id);
            _context.Books.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Index", "Books");
        }

        //GET for Edit Data
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Book book = _context.Books.Where(x => x.Id == id).First();
            return View(book);
        }

        //Post for Edit Data
        [HttpPost]
        public IActionResult Edit(Book books, int id)
        {
            Book value = _context.Books.Find(id);
            value.Name = books.Name;
            value.AuthorId = books.AuthorId;
            value.ISBN = books.ISBN;
            value.CategoryId = books.CategoryId;
            value.LevelId = books.LevelId;
            value.Count = books.Count;
            value.Publication = books.Publication;
            _context.SaveChanges();
            return RedirectToAction("Index", "Books");

        }
    }
}
