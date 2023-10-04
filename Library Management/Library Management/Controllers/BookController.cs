using Library_Management.Data;
using Library_Management.Filters;
using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Management.Controllers
{
    public class BookController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public BookController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _contextAccessor = httpContextAccessor;
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
            value.UpdateDate = DateTime.Now;
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
        public IActionResult RequestBook(int bookId)
        {
            int? id = _contextAccessor.HttpContext.Session.GetInt32("userId");

            RequestBook reqBook = new RequestBook();
            reqBook.BookId = bookId;
            reqBook.UserId = (int)id;
            reqBook.RequestDate = DateTime.Now;
            _context.RequestBooks.Add(reqBook);

            _context.SaveChanges();
            return RedirectToAction("Index","Student");
        }
        public IActionResult RequestedBookList()
        {
            var reqBookList = _context.RequestBooks.Include(x => x.Books).Include(x => x.User).ToList();
            return View(reqBookList);
        }

        public IActionResult AcceptBook(int Id)
        {
            var reqBook = _context.RequestBooks.Where(x=>x.Id==Id).FirstOrDefault();
            var lentBook = new LentBook();
            lentBook.UserId = reqBook.UserId;
            lentBook.BookId = reqBook.BookId;
            _context.LentBooks.Add(lentBook);
            _context.RequestBooks.Remove(reqBook);
            _context.SaveChanges();

            return RedirectToAction("Index", "Book");
        }

        public IActionResult RejectBook(int Id)
        {
            return View();
        }
    }
}
