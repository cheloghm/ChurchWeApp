using ChurchWeApp.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChurchWeApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return PartialView("_LoginPartial");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return PartialView("_RegisterPartial");
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticateLogin(string email, string password)
        {
            var token = await _authService.Login(email, password);

            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "Invalid credentials.";  // Using TempData to pass the error message to the view.
                return RedirectToAction("Index", "Home"); // Assuming your Login action is named "Login" and is in "Auth" controller.
            }

            // Save token wherever required. If using server-side sessions, you can save it there.

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticateRegister(string firstName, string middleName, string lastName, string email, string role, DateTime dob, string password)
        {
            var registrationSuccess = await _authService.Register(firstName, middleName, lastName, email, role = " ", dob, password);

            if (!registrationSuccess)
            {
                return Json(new { success = false, message = "Registration failed. Perhaps the email is already in use." });
            }

            return Json(new { success = true, message = "Successfully registered. You can now log in." });
        }
    }
}
