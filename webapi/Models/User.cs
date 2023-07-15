using Microsoft.AspNetCore.Identity;

namespace HotelDirectory.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public User()
        {
            Name = string.Empty;
        }
    }
}