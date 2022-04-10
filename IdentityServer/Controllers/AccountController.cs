using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nami.DXP.Common;
using Nami.DXP.Domain;
using Nami.DXP.Persistence;
using System.Threading.Tasks;

namespace Nami.DXP.IdentityServer
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AccountController> _logger;
        private readonly INamiUserRepository _namiUserRepository;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 RoleManager<IdentityRole> roleManager,
                                 ILogger<AccountController> logger, INamiUserRepository namiUserRepository,
                                 IOptions<IdentityServerOptions> options)
                                 : base(logger, options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _namiUserRepository = namiUserRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl=null)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.EmployeeId);
                if (user != null && !user.IsActive)
                    return RedirectToAction("AccessDenied", "Error");

                if (user == null)
                {
                    // Call Nisshinbo Database and get user from UserTable
                    var namiUser = await _namiUserRepository.GetUserAsync("ID" + model.EmployeeId);

                    if (namiUser == null)
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Login Attempt.");
                        return View(model);
                    }

                    //Create new ApplicationUser from user of Nisshinbo Database
                    var u = new ApplicationUser
                    {
                        FullName = namiUser.NameFirst + " " + namiUser.NameLast,
                        UserName = model.EmployeeId,
                        IsActive = true,
                    };

                    await _userManager.CreateAsync(u, model.EmployeeId);
                }


                var result = await _signInManager.PasswordSignInAsync(model.EmployeeId, model.EmployeeId,
                                            model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return Redirect(_config.WebNavigatorIndexUrl);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
