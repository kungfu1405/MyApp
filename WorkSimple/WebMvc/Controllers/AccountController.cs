using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebMvc.Data;
using WebMvc.Models.User;

namespace WebMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<WebUser> _userManager;
        private readonly SignInManager<WebUser> _signInManager;
        public AccountController(UserManager<WebUser> userManager , SignInManager<WebUser> signInManager )
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet , AllowAnonymous]
        public IActionResult Register()
        {
            UserRegistrationInput model = new UserRegistrationInput();
            return View(model);
        }
        [HttpPost , AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationInput input)
        {
        if(ModelState.IsValid)
            {
                var userCheck = await _userManager.FindByEmailAsync(input.Email);
                if(userCheck ==null)
                {
                    var user = new WebUser
                    {
                        UserName = input.Email,
                        NormalizedUserName = input.Email,
                        PhoneNumber = input.PhoneNumber,
                        PhoneNumberConfirmed = true,
                        EmailConfirmed = true

                    };
                    var result = await _userManager.CreateAsync(user);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        if(result.Errors.Count() > 0)
                        {
                            foreach( var error in result.Errors)
                            {
                                ModelState.AddModelError("message", error.Description);

                            }
                        }
                        return View(input);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Email already exists.");                   
                }
            }
        else
            {
                ModelState.AddModelError("", "data is not valid !");
            }
            return View(input);
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Login()
        {
            UserLoginInput model = new UserLoginInput();
            return View(model);
        }
        [HttpPost ,AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginInput input)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    var user = await _userManager.FindByEmailAsync(input.Email);
                    if (user != null && !user.EmailConfirmed)
                    {
                        ModelState.AddModelError("message", "Email not confirmed yet");
                        return View(input);

                    }
                    if (await _userManager.CheckPasswordAsync(user, input.Password) == false)
                    {
                        ModelState.AddModelError("message", "Invalid credentials");
                        return View(input);

                    }

                    var result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, input.RememberMe, true);

                    if (result.Succeeded)
                    {
                        await _userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                        return RedirectToAction("Dashboard");
                    }
                    else if (result.IsLockedOut)
                    {
                        return View("AccountLocked");
                    }
                    else
                    {
                        ModelState.AddModelError("message", "Invalid login attempt");
                        return View(input);
                    }
                }
                catch(Exception ex)
                { }
            }
            return View(input);
        }
    }
}
