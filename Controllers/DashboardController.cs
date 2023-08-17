using ChurchWeApp.Models;
using ChurchWeApp.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
                return NotFound();
            }
            return View(model);
        }

        public async Task<IActionResult> UpdateUserProfileModal()
        {
            var model = await _userService.GetMyDetails();
            if (model == null)
            {
                return NotFound();
            }
            return PartialView("_UpdateUserProfile", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserProfile(UpdateUserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_UpdateUserProfile", model);
            }

            var currentUser = await _userService.GetMyDetails();
            if (currentUser == null)
            {
                return Json(new { success = false, message = "Failed to retrieve current user details." });
            }

            bool changesDetected = false;
            var updateUserDto = new UpdateUserDTO();

            if (currentUser.FirstName != model.FirstName)
            {
                updateUserDto.FirstName = model.FirstName;
                changesDetected = true;
            }
            // Do similar comparisons for other fields...

            if (!changesDetected)
            {
                return Json(new { success = true, message = "No changes detected. Profile not updated." });
            }

            var updatedUser = await _userService.UpdateUserProfile(updateUserDto);
            if (updatedUser == null)
            {
                return Json(new { success = false, message = "Profile update failed. Please try again." });
            }

            return Json(new { success = true, message = "Successfully updated profile." });
        }

    }
}
