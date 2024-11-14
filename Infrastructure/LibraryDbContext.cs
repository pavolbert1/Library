using LibraryAPI.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Infrastructure
{
    public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Loan> Loans { get; set; }
    }
}