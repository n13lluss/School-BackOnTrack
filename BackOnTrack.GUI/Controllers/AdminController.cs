using BackOnTrack.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackOnTrack.GUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAccountService _accountService;
        public AdminController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
