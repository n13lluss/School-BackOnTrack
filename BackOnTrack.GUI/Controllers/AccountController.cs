using Microsoft.AspNetCore.Mvc;
using BackOnTrack.GUI.Models;
using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;
using BackOnTrack.Core.Services;

namespace BackOnTrack.GUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserModel user = _accountService.CorrectLogin(model.UsernameOrEmail, model.Password);
                    
                if (user != null)
                {
                    // Authentication successful, redirect to the overview page in the user controller
                    return RedirectToAction("Overview", "User");
                }
                else
                {
                    // Invalid login, show a popup or error message
                    ModelState.AddModelError(string.Empty, "Invalid login credentials.");
                }
            }
            return RedirectToAction("Login", "Account", model);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            UserModel user = new UserModel
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            if (ModelState.IsValid)
            {
                if (true)
                {
                    bool registrationSuccess = _accountService.CreateUser(user);

                    if (registrationSuccess)
                    {
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "User registration failed.");
                    }
                }
            }
            return RedirectToAction("Register", "User");
        }
    }
}
