using Library_Management.Data;
using Library_Management.Filters;
using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
    [ServiceFilter(typeof(AdminAuthorizeFilter))]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var Categories = _context.BookCategories.ToList();
            return View(Categories);
        }


        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(BookCategory category)
        {
            BookCategory value = new BookCategory();
            value.Name = category.Name;
            _context.BookCategories.Add(value);
            _context.SaveChanges();
            return RedirectToAction("Index","Categories");
        }


        [HttpGet]
        public IActionResult Delete(int id) 
        {
            BookCategory category = _context.BookCategories.Find(id);
            _context.BookCategories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index","Categories");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            BookCategory category = _context.BookCategories.Find(id);
            return View(category);
        }


        [HttpPost]
        public IActionResult Edit(BookCategory category, int id)
        {
            BookCategory value = _context.BookCategories.Find(id);
            value.Name=category.Name;
            _context.SaveChanges();

            return RedirectToAction("Index", "Categories");
        }


    }
}
