using Library_Management.Data;
using Library_Management.Filters;
using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
    [ServiceFilter(typeof(AdminAuthorizeFilter))]
    public class PublicationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PublicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var BookPublications = _context.Publications.ToList();
            return View(BookPublications);
        }


        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Publication publication)
        {
            Publication value = new Publication();
            value.Name = publication.Name;
            _context.Publications.Add(value);
            _context.SaveChanges();
            return RedirectToAction("Index", "Publications");
        }


        [HttpGet]
        public IActionResult Delete(int id) 
        {
            Publication publication = _context.Publications.Find(id);
            _context.Publications.Remove(publication);
            _context.SaveChanges();
            return RedirectToAction("Index", "Publications");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Publication publication = _context.Publications.Find(id);
            return View(publication);
        }


        [HttpPost]
        public IActionResult Edit(Publication publication, int id)
        {
            Publication value = _context.Publications.Find(id);
            value.Name= publication.Name;
            _context.SaveChanges();

            return RedirectToAction("Index", "Publications");
        }


    }
}
