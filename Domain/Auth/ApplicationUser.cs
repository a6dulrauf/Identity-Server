using Microsoft.AspNetCore.Identity;

namespace Nami.DXP.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool IsActive { get; set; }
    }
}
