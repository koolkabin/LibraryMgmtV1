using Microsoft.EntityFrameworkCore;
using Library_Management.Models;

namespace Library_Management.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Books> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<LentBook> LentBooks { get; set; }
        public DbSet<ReturnBook> ReturnBook { get; set; }
        public DbSet<RequestBook> RequestBooks { get; set; }
        public DbSet<RequestCancelledLog> RequestCancelledLog   { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
