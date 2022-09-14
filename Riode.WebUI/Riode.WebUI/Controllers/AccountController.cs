using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Riode.WebUI.AppCode.Extensions;
using Riode.WebUI.Models.Entities.Membership;
using Riode.WebUI.Models.FormModels;

namespace Riode.WebUI.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<RiodeUser> _signInManager;
        private readonly UserManager<RiodeUser> _userManager;
        private readonly IConfiguration _configuration;
        public AccountController(SignInManager<RiodeUser> signInManager,UserManager<RiodeUser> userManager, IConfiguration configuration )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration= configuration;
        }
        [Route("/signin.html")]
        [AllowAnonymous]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [Route("/signin.html")]
        [AllowAnonymous]   
        public async Task<IActionResult> SignIn(LoginFormModel user)
        {
          if (ModelState.IsValid)
            {
                RiodeUser foundedUser = null;
                if (user.UserName.isEmail())
                {
                    foundedUser = await _userManager.FindByEmailAsync(user.UserName);
                }
                else
                {
                    foundedUser = await _userManager.FindByNameAsync(user.UserName);
                }
                if (foundedUser == null)
                {
                    ViewBag.Message = "Your UserName or Email is wrong";
                    goto end;
                }
                var result = await _signInManager.PasswordSignInAsync(foundedUser, user.Password, true, true);
                if (!result.Succeeded)
                {
                    ViewBag.Message = "Your UserName or Email is wrong";
                    goto end;
                }
                var callbackUrl = Request.Query["ReturnUrl"];
                if (!string.IsNullOrWhiteSpace(callbackUrl))
                {
                    return Redirect(callbackUrl);
                }

                return RedirectToAction("Index", "Shop");
            }
        end:
            return View(user);

        }

        [HttpGet]
        [Route("/registration-confirm.html")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterConfirm(string email, string token)
        {
            var foundedUser = await _userManager.FindByEmailAsync(email);
            if (foundedUser == null)
            {
                ViewBag.Message = "Invalid Token!";
                goto end;
            }
            token = token.Replace(" ", "+");
            var result= await _userManager.ConfirmEmailAsync(foundedUser, token);
            if (!result.Succeeded)
            {
                ViewBag.Message = "Invalid Token!";
                goto end;
            }

            ViewBag.Message = "Your accunt confirmed!";

        end:
            return RedirectToAction(nameof(SignIn));

        }
        [Route("/register.html")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new RiodeUser();
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.UserName = model.Email;
                user.Email = model.Email;
                //user.EmailConfirmed = true;
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    string path = $"{Request.Scheme}://{Request.Host}/registration-confirm.html?email={user.Email}&token={token}";
                    var emailResponse = _configuration.SendEmail(user.Email, "Riode User Registration", $"Please complete registration with this <a href ={path}>link</a>");  
                    if (emailResponse)
                    {
                        ViewBag.Message = "Cangrulation, Registation Completed!";
                    }
                    else
                    {
                        ViewBag.Message = "Something was wrong please try again";
                    }

                    ViewBag.Message = "Conguraltions. Registered finalized!";
                    return RedirectToAction(nameof(SignIn));
                }
                foreach( var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code,error.Description);
                }
            }
            
            return View(model);
        }
        [Route("/profile.html")]
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult WishList()
        {
            return View();
        }
        [Route("/logout.html")]
        public async Task<IActionResult> Logout()
        {
          await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
