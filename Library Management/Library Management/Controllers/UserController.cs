using Library_Management.Data;
using Library_Management.Filters;
using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
    [ServiceFilter(typeof(AdminAuthorizeFilter))]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            var Userlist = _context.Users.ToList();
            return View(Userlist);
        }

        //Get for ADD New Data
        [HttpGet]
        public IActionResult Create()
        {
            var user = new User();
            return View(user);
        }


        //Post for ADD New Data
        [HttpPost]
        public IActionResult Create(User users)
        {
            User value = new User();
            value.Name = users.Name;
            value.Email = users.Email;
            value.Phone = users.Phone;
            value.Password = users.Password;
            value.Faculty = users.Faculty;
            value.UserType = users.UserType;
            _context.Users.Add(value);
            _context.SaveChanges();
            return RedirectToAction("Index", "User");

        }

        //Post for Delete
        //Or use Post Method for using the Form Method
        [HttpGet]
        public IActionResult Delete(int id)
        {
            User user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index", "User");
        }

        //GET for Edit Data
        [HttpGet]
        public IActionResult Edit(int id)
        {
            User user = _context.Users.Where(x => x.Id == id).First();
            return View(user);
        }

        //Post for Edit Data
        [HttpPost]
        public IActionResult Edit(User users, int id)
        {
            User value = _context.Users.Find(id);
            value.Name = users.Name;
            value.Email = users.Email;
            value.Phone = users.Phone;
            value.Password = users.Password;
            value.Faculty = users.Faculty;
            value.UserType = users.UserType;
            _context.SaveChanges();
            return RedirectToAction("Index", "User");
        }
    }

}
