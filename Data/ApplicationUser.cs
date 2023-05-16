using Microsoft.AspNetCore.Identity;

namespace Conference_Management_System.Data
{
    public class ApplicationUser :IdentityUser
    {
        public string FullName { get; set; }
        public string Expertises { get; set; }
    }
}
