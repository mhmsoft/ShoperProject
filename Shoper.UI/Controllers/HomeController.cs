using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Shoper.BusinessLogic.Interface;
using Shoper.BusinessLogic.Utility;
using Shoper.Data;
using Shoper.Entities;
using Shoper.UI.Models;
using Shoper.UI.Models.ViewModels;
using System.Data;
using System.Diagnostics;

namespace Shoper.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerService _customerService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMailSender _mailSender;

        public HomeController(ILogger<HomeController> logger,
                                     ICustomerService customerService,
                                    UserManager<AppUser> userManager,
                                    SignInManager<AppUser> signInManager,
                                    IMailSender mailSender)
        {
            _logger = logger;
            _customerService = customerService;
            _userManager = userManager;
            _signInManager = signInManager;
            _mailSender = mailSender;

        }

       
        public IActionResult Index()
        {
            return View();
        }             

       
        
        #region Auth

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(registerViewModel model, string ReturnUrl = null)
        {

            ReturnUrl = ReturnUrl ?? Url.Content("~/Home/Index");
            var user = new AppUser()
            {
                fullName = model.Name + " " + model.LastName,
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.Phone
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            var roleResult = await _userManager.AddToRoleAsync(user, "customer"); // role ataması

            if (result.Succeeded) // user oluşturulmuşsa
            {
                var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("ConfirmEmail", "Home", new
                {
                    userId = user.Id,
                    token = confirmationToken
                }, HttpContext.Request.Scheme);

                ViewBag.EmailSuccess = false;

                var emailResult = await _mailSender.MailSend(new Email()
                {
                    Subject = "Confirm e-mail",
                    Body = $"Please <a href='{confirmationLink}'>click</a> to confirm your e-mail address.",
                    To = InternetAddress.Parse(user.Email)
                });

                if (!emailResult)
                {
                    ViewBag.EmailSuccess = false;
                    ModelState.AddModelError("Email", "Mail sunucu hatası");
                    return View(model);

                }
                else // Mail gönderilmişse
                {
                    ViewBag.EmailSuccess = true;
                    ViewBag.Info = "Tarafınıza üyeliği tamamlamnız için mail gönderilmiştir. Lütfen onaylayın.";
                    return View(model);
                }
                             

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect(ReturnUrl);
                

            }
            result.Errors.ToList().ForEach(f => ModelState.AddModelError(string.Empty, f.Description));


            return View(model);
        }
        public IActionResult Login(string ReturnUrl = null)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(loginViewModel model, string ReturnUrl = null)
        {
            ReturnUrl = ReturnUrl ?? Url.Content("~/Home/Index");
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                await _signInManager.SignOutAsync();

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    await _userManager.ResetAccessFailedCountAsync(user);
                    await _userManager.SetLockoutEndDateAsync(user, null);

                    return LocalRedirect(ReturnUrl);


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

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    if (_customerService.GetExp(x => x.Email == user.Email).Count() == 0)
                    {
                        Customer customer = new Customer()
                        {
                            Email = user.Email,
                            FirstName = user.fullName,
                            UserId = user.Id
                        };
                        _customerService.Add(customer);
                    }
                    

                    return RedirectToAction("Login");
                }
                ModelState.AddModelError(string.Empty, "Kullanıcı doğrulaması yapılamadı.");
                return View();
            }
            ModelState.AddModelError(string.Empty, "Kullanıcı bulunamadı.");
            return View();


        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            //return RedirectToAction("Login");
            return Redirect("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied(string ReturnUrl = null)
        {

            return RedirectToAction("Login", new { ReturnUrl = ReturnUrl });

        }


        public IActionResult ForgotPassword()
        {
            return View();
        }
        #endregion


    }
}