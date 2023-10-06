using Library_Management.Data;
using Library_Management.Filters;
using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Management.Controllers
{
    [ServiceFilter(typeof(AdminAuthorizeFilter))]
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public AdminController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
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

        public IActionResult RequestedBookList()
        {
            var reqBookList = _context.RequestBooks.Include(x => x.Books).Include(x => x.User).ToList();
            return View(reqBookList);
        }

        public IActionResult AcceptBook(int Id)
        {
            var reqBook = _context.RequestBooks.Where(x => x.Id == Id).FirstOrDefault();
            var book = _context.Books.Where(x => x.Id == reqBook.BookId).First();
            var lentBook = new LentBook();
            book.Count = book.Count - 1;
            lentBook.UserId = reqBook.UserId;
            lentBook.BookId = reqBook.BookId;
            _context.LentBooks.Add(lentBook);
            _context.RequestBooks.Remove(reqBook);
            _context.SaveChanges();
            return RedirectToAction("RequestedBookList", "Admin");
        }

        public IActionResult RejectBook(int Id)
        {
            var reqBook = _context.RequestBooks.Where(x => x.Id == Id).FirstOrDefault();
            _context.RequestBooks.Remove(reqBook);
            _context.SaveChanges();
            return RedirectToAction("RequestedBookList", "Admin");
        }

        [HttpGet]
        public IActionResult ReturnBook()
        {
            var LentBookList = _context.LentBooks
                .Include(x => x.Books)
                .Include(x => x.User)
                .Where(lentBook => lentBook.returnDate == null)
                .ToList();
            return View(LentBookList);

        }

        [HttpPost]
        public IActionResult ReturnBook(int Id)
        {
            var returnBook = _context.LentBooks.Where(x => x.Id == Id).FirstOrDefault();
            var book = _context.Books.Where(x => x.Id == returnBook.BookId).First();
            book.Count = book.Count + 1;
            returnBook.returnDate = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("ReturnBook", "Admin");
        }
    }


}
