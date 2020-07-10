using System.Threading.Tasks;
using LoginDemoApplication.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoginDemoApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Name, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("EditUser", new { name = model.Name });
                }

                ModelState.AddModelError("", "Invalid Login Attempt");
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> EditUser(string name)
        {
            var user = await userManager.FindByNameAsync(name);

            var model = new EditUserViewModel()
            {
                Name = user.UserName,
                Email = user.Email
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Name);

                if (user.Email != model.Email)
                {
                    var emailtoken = await userManager.GenerateChangeEmailTokenAsync(user, model.Email);

                    var emailupdate = await userManager.ChangeEmailAsync(user, model.Email, emailtoken); //UpdateNormalizedEmailAsync
                }

                if (!string.IsNullOrEmpty(model.Password))
                {
                    var passwordtoken = await userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordupdate = await userManager.ResetPasswordAsync(user, passwordtoken, model.Password);
                }

            }

            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

    }
}
