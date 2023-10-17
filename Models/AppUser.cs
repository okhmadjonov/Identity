using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class AppUser : IdentityUser
    {


        public string Role { get; set; } = "Admin";
    }
}
