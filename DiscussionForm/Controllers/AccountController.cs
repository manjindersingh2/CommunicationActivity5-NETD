/* Name - Manjinder Singh
 * Date - December 11, 2020
 * Course - NETD3202
 * Description - This is the account controller page which shows the form to user to register,
 *               create the user, confirm it, login and logout the user and do all the other 
 *               requirements for the communication activity.
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DiscussionForm.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiscussionForm.Controllers
{
    public class AccountController : Controller
    {
        //services
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<AccountController> logger;


        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger)
        {

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }
        //to show register form
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //to save user
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //to create user
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var comfirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token = token }, Request.Scheme);
                    logger.Log(LogLevel.Warning, comfirmationLink);
                    ViewBag.message = "Registeration Successfull, please confirm mail before login";
                    ViewBag.message = ViewBag.message+"<br/> "+comfirmationLink;
                    return View();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }

        //to confirm user
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, String token)
        {
            if(userId==null || token == null)
            {
                return RedirectToAction("index", "home");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.Message = "The user id" + userId + " is invalid";
                
            }
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded) {
                ViewBag.Message = "Email confirmed";
            }

            return View();
        }

        //to show login form
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //to login the user
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed) {
                    ModelState.AddModelError("", "Email not confirmed!");
                    return View(model);
                }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }


                ModelState.AddModelError("", "Invalid Credentials");

            }
            return View(model);
        }

        //to logout the user
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        //to show the forgot password form
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        //to generate the reset password link
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ResetPassword model)
        {
            if (ModelState.IsValid) {
                var user = await userManager.FindByEmailAsync(model.Email);
                if(user!=null && user.EmailConfirmed )
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                        new { email = model.Email, token = token }, Request.Scheme);
                    ViewBag.message = "Reset Link: " + passwordResetLink;
               
                    return View("ForgotPasswordConfirmation");
                }
                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }

        //To show forgot password confirmation form
        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }


        //to show reset password form
        [HttpGet]
        public IActionResult ResetPassword(string token, String email)
        {
            if (token == null || token == email)
            {
                ModelState.AddModelError("", "Invalid Password reset token");
                return View();
            }


            return View();
        }

        //to show post
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {

                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordSuccess");
                    }
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }

            return View(model);
        }

    }
}
