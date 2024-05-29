using Microsoft.AspNetCore.Identity;

namespace CarApp.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}
