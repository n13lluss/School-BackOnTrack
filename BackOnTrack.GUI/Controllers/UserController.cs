using BackOnTrack.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackOnTrack.GUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IAccountService _accountService;
        public UserController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
