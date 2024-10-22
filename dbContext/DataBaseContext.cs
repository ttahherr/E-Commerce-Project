using E_Commerce_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Project.DBContext
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options) { }

        public DbSet<Actor>Actors { get; set; }
        public DbSet<Movie> Movie { get; set; }

        

    }
}
