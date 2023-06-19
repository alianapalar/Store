using Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Models;

namespace StoreApp.Controllers
{
    public class AccountController:Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login([FromQuery] string ReturnUrl="/")
        {
            return View(new LoginModel()
            {
                ReturnUrl=ReturnUrl
            });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityUser user=await _userManager.FindByNameAsync(model.Username);
               if(user is not null)
                {
                    await _signInManager.SignOutAsync();
                    if((await _signInManager.PasswordSignInAsync(user, model.Password,false,false)).Succeeded)
                    {
                        return Redirect(model?.ReturnUrl ?? "/");
                    }
                }
                ModelState.AddModelError("Error","Invalid username or password");
                
            }
            return View();
            
        }
        public async Task<IActionResult> Logout([FromQuery] string ReturnUrl="/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(ReturnUrl);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            var user=new IdentityUser
            {
               UserName=registerDto.Username,
               Email=registerDto.Email,
               
            };
            var result= await _userManager.CreateAsync(user,registerDto.Password);
            if(result.Succeeded)
            {
                var roleResult= await _userManager.AddToRoleAsync(user,"User");
                if(roleResult.Succeeded)
                    return RedirectToAction("Login",new{ReturnUrl="/"});
            }
            else
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("",err.Description);
                }
            }
            return View();
        }
        public IActionResult AccessDenied([FromQuery(Name ="ReturnUrl")] string returUrl)
        {
            return View();
        }
    }
}