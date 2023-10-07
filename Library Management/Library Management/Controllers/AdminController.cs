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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RequestedBookList()
        {
            var reqBookList = _context.RequestBooks
                .Include(x => x.Books)
                .Include(x => x.User)
                .Where(reqBook => reqBook.RequestStatus == EnumRequestStatus.Pending)
                .ToList();
            return View(reqBookList);
        }

        public IActionResult AcceptBook(int Id)
        {
            var reqBook = _context.RequestBooks.Where(x => x.Id == Id).FirstOrDefault();
            var book = _context.Books.Where(x => x.Id == reqBook.BookId).First();
            reqBook.RequestStatus = EnumRequestStatus.Approved;
            var lentBook = new LentBook();
            book.Count = book.Count - 1;
            lentBook.RequestBookId = reqBook.Id;
            lentBook.lentDate = DateTime.Now;
            _context.LentBooks.Add(lentBook);
            _context.SaveChanges();
            return RedirectToAction("RequestedBookList", "Admin");
        }

        public IActionResult RejectBook(int Id)
        {
            var reqBook = _context.RequestBooks.Where(x => x.Id == Id).FirstOrDefault();
            reqBook.RequestStatus = EnumRequestStatus.Rejected;
            RequestCancelledLog reqCancelLog = new RequestCancelledLog();
            reqCancelLog.Remarks = "Rejected By Admin.";
            reqCancelLog.CancelledDate = DateTime.Now;
            reqCancelLog.RequestBookID = reqBook.Id;
            _context.SaveChanges();
            return RedirectToAction("RequestedBookList", "Admin");
        }

        [HttpGet]
        public IActionResult ReturnBook()
        {
            var myLentBookList = _context.LentBooks
                .Where(lentBook => lentBook.RequestBook.RequestStatus == EnumRequestStatus.Approved)
                .Include(lentBook => lentBook.RequestBook)
                .Include(lentBook => lentBook.RequestBook.Books)
                .Include(lentBook => lentBook.RequestBook.User)
                .ToList();
            return View(myLentBookList);

        }

        [HttpPost]
        public IActionResult ReturnBook(int Id)
        {
            var reqbook = _context.RequestBooks.Where(x => x.RequestStatus == EnumRequestStatus.Approved).FirstOrDefault();
            var book = _context.Books.Where(x => x.Id == reqbook.BookId).First();
            reqbook.RequestStatus = EnumRequestStatus.Returned;
            book.Count = book.Count + 1;
            ReturnBook returnBook = new ReturnBook();
            returnBook.RequestBookId = reqbook.Id;
            returnBook.returnedDate = DateTime.Now;
            returnBook.Remarks = "Book Returned";
            _context.SaveChanges();
            return RedirectToAction("ReturnBook", "Admin");
        }
    }


}
