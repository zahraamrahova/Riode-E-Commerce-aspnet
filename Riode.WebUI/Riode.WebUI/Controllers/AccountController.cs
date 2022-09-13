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
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginFormModel user)
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
            if(foundedUser == null)
            {
                ViewBag.Message = "Your UserName or Email is wrong";
                goto end;
            }
            var result = await _signInManager.PasswordSignInAsync(foundedUser, user.Password, true, true);
            if (!result.Succeeded){
                ViewBag.Message = "Your UserName or Email is wrong";
                goto end;
            }
            var callbackUrl = Request.Query["ReturnUrl"];
            if(!string.IsNullOrWhiteSpace(callbackUrl))
            {
                return Redirect(callbackUrl);
            }

            return RedirectToAction("Index", "Shop");
        end:
            return View(user);

            return View();
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
