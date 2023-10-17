using Identity.Data;
using Identity.Models;
using Identity.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        private AppDbContext _context;
        public AccountController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            AppDbContext appDbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = appDbContext;

        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();

            return View(response);

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewMdoel)
        {
            if (!ModelState.IsValid) return View(loginViewMdoel);
            var user = await _userManager.FindByEmailAsync(loginViewMdoel.Email);
            //Console.Write("AspNetUser email is: " + user.Email);
            if (user != null)
            {
                // User is found and check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewMdoel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewMdoel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                // Password is incorrect
                TempData["Error"] = "Wrong Credentials";
                return View(loginViewMdoel);
            };
            //User not found
            TempData["Error"] = "Wrong Credentials";
            return View(loginViewMdoel);

        }

        //Register user
        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }




        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.Email);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }

            var newUser = new AppUser()
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Name
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}