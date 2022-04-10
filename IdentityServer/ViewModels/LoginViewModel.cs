using System.ComponentModel.DataAnnotations;

namespace Nami.DXP.IdentityServer
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "EMPLOYEE ID")]
        public string EmployeeId { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; } = false;

        public string ReturnUrl { get; set; }

        //public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
