using ChurchWeApp.Models;
using ChurchWeApp.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChurchWeApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUserService _userService;

        public DashboardController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _userService.GetMyDetails();
            if (model == null)
            {
                // Handle the error. For example, you could return a NotFound result.
                return NotFound();
            }
            return View(model);
        }

    }
}
