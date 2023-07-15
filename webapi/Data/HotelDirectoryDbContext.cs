using Microsoft.EntityFrameworkCore;
using HotelDirectory.Models;

namespace HotelDirectory.Data
{
    public class HotelDirectoryDbContext : DbContext
    {
        public HotelDirectoryDbContext(DbContextOptions<HotelDirectoryDbContext> options) : base(options)
        {

        }

        public DbSet<User>? Users { get; set; }
    }
}