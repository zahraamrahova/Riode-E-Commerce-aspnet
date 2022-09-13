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
        public AccountController(SignInManager<RiodeUser> signInManager,UserManager<RiodeUser> userManager )
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [AllowAnonymous]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
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
                user.EmailConfirmed = true;
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //Accuallly we have to finalized with email confirmed
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

        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult WishList()
        {
            return View();
        }
       
    }
}
