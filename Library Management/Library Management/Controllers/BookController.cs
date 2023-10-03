using Library_Management.Data;
using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
    public class BookController : Controller
    {

        private readonly ApplicationDbContext _context;
        public BookController (ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var bookList = _context.Books.ToList();
            return View(bookList);
        }
        
        //Get for ADD New Data
        [HttpGet]
        public IActionResult Create()
        {
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
            value.CatagoryId = books.CatagoryId;
            value.Level = books.Level; 
            value.Count = books.Count;
            value.Publication = books.Publication;
            _context.Books.Add(value);
            _context.SaveChanges();
            return RedirectToAction("Index","Book");

        }

        //Post for Delete
        //Or use Post Method for using the Form Method
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Books value = _context.Books.Find(id);
            _context.Books.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Index","Book");
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
        public IActionResult Edit(Books books,int id)
        {
            Books value = _context.Books.Find(id);
            value.Name = books.Name;
            value.AuthorId = books.AuthorId;
            value.ISBN = books.ISBN;
            value.CatagoryId = books.CatagoryId;
            value.Level = books.Level;
            value.Count = books.Count;
            value.Publication = books.Publication;
            _context.SaveChanges();
            return RedirectToAction("Index", "Book");

        }

    }
}
