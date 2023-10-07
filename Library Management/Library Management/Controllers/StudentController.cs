using Library_Management.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library_Management.Models;
using Microsoft.AspNetCore.Http;
using Library_Management.Filters;

namespace Library_Management.Controllers
{
    [ServiceFilter(typeof(StudentAuthorizeFilter))]
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
            return View();
        }

        public IActionResult BookList()
        {
            //if (_contextAccessor.HttpContext.Session.GetInt32("userId") == null)
            //{
            //    return RedirectToAction("Index", "Account");
            //}
            //if (_contextAccessor.HttpContext.Session.GetString("userType") == "Admin")
            //{
            //    return RedirectToAction("ErrorPage", "Book");
            //}
            VMBook bookList = new VMBook();
            bookList.TotalLibraryBookList = _context.Books
                .Include(x => x.BookAuthor)
                .Include(x => x.BookCategory)
                .Include(x => x.Publication)
                .Include(x => x.BookLevel)
                .ToList();

            bookList.CurrentRequestBookList = _context.RequestBooks
                .Where(x => x.UserId == _contextAccessor.HttpContext.Session.GetInt32("userId")).ToList();
            return View(bookList);
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
            return RedirectToAction("BookList", "Student");
        }

        public IActionResult MyRequestBook()
        {
            var myReqBookList = _context.RequestBooks
                .Where(x => x.UserId == _contextAccessor.HttpContext.Session.GetInt32("userId"))
                .Where(reqBook => reqBook.RequestStatus == EnumRequestStatus.Pending)
                .Include(x => x.Books).ToList();
            return View(myReqBookList);
        }

        public IActionResult ReqCancel(int Id)
        {
            var reqBook = _context.RequestBooks.Where(x => x.Id == Id).FirstOrDefault();
            RequestCancelledLog reqCancelLog = new RequestCancelledLog();
            reqBook.RequestStatus = EnumRequestStatus.Cancelled;
            reqCancelLog.CancelledDate = DateTime.Now;
            reqCancelLog.RequestBookID = reqBook.Id;
            reqCancelLog.Remarks = "Rejected By User.";
            _context.RequestCancelledLogs.Add(reqCancelLog);
            _context.SaveChanges();
            return RedirectToAction("MyRequestBook", "Student");
        }

        public IActionResult MyIssuedBook()
        {
            var userId = _contextAccessor.HttpContext.Session.GetInt32("userId");
            var myLentBookList = _context.LentBooks
                .Where(lentBook => lentBook.RequestBook.UserId == userId && lentBook.RequestBook.RequestStatus == EnumRequestStatus.Approved)
                .Include(lentBook => lentBook.RequestBook)
                .Include(lentBook => lentBook.RequestBook.Books)
                .ToList();
            return View(myLentBookList);


        }



        [HttpGet]
       public IActionResult EditProfile()
        {
            int? id = _contextAccessor.HttpContext.Session.GetInt32("userId");
            User user = _context.Users.Where(x => x.Id == id).First();
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User users, int id)
        {
            User value = _context.Users.Find(id);
            value.Name = users.Name;
            value.Email = users.Email;
            value.Phone = users.Phone;
            value.Password = users.Password;
            value.Faculty = users.Faculty;
            _context.SaveChanges();
            return RedirectToAction("Index", "Student");
        }
    }
}
