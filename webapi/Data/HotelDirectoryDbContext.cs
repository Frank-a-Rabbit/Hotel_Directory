using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HotelDirectory.Models;

namespace HotelDirectory.Data
{
    public class HotelDirectoryDbContext : IdentityDbContext<User>
    {
        public HotelDirectoryDbContext(DbContextOptions<HotelDirectoryDbContext> options) : base(options)
        {

        }

    }
}