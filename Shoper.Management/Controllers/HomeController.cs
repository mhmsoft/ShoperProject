using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shoper.Data;
using Shoper.Management.Models;
using Shoper.Management.Models.ViewModels;
using System.Diagnostics;

namespace Shoper.Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, 
                                    UserManager<AppUser> userManager,
                                    SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(registerViewModel model, string ReturnUrl = null)
        {
            if (ModelState.IsValid)
            {
                ReturnUrl = ReturnUrl ?? Url.Content("~/");
                var user = new AppUser()
                {
                    Email = model.Email,
                    UserName = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                var roleResult = await _userManager.AddToRoleAsync(user, "Manager");

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    //var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var confirmationLink = Url.Action("ConfirmEmail", "Home", new
                    //{
                    //    userId = user.Id,
                    //    token = confirmationToken
                    //}, HttpContext.Request.Scheme);

                    //await _emailHelper.SendAsync(new()
                    //{
                    //    Subject = "Confirm e-mail",
                    //    Body = $"Please <a href='{confirmationLink}'>click</a> to confirm your e-mail address.",
                    //    To = user.Email
                    //});
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("index", "Home");
                }
                result.Errors.ToList().ForEach(f => ModelState.AddModelError(string.Empty, f.Description));

            }
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public  async Task<IActionResult> Login(loginViewModel model, string ReturnUrl = null)
        {
            ReturnUrl = ReturnUrl ?? Url.Content("~/");
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                await _signInManager.SignOutAsync();

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    await _userManager.ResetAccessFailedCountAsync(user);
                    await _userManager.SetLockoutEndDateAsync(user, null);

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                   
                }
                else if (result.IsLockedOut)
                {
                    var lockoutEndUtc = await _userManager.GetLockoutEndDateAsync(user);
                    var timeLeft = lockoutEndUtc.Value - DateTime.UtcNow;
                    // Account locked out, try again {timeLeft.Minutes} minutes later.
                    ModelState.AddModelError(string.Empty, $"This account has been locked out, please try again {timeLeft.Minutes} minutes later.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid e-mail or password.");
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid e-mail or password.");
            }
            return View(model);
        }
        public async Task<IActionResult> ComfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
            }

        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied(string ReturnUrl = null)
        {

            return RedirectToAction("Login", new { ReturnUrl = ReturnUrl });

        }


    }
}