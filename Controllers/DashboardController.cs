using ChurchWeApp.Models;
using ChurchWeApp.RepositoryInterfaces;
using ChurchWeApp.Service;
using ChurchWeApp.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChurchWeApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProfilePhotoService _profilePhotoService;

        public DashboardController(IUserService userService, IProfilePhotoService profilePhotoService)
        {
            _userService = userService;
            _profilePhotoService = profilePhotoService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _userService.GetMyDetails();
            if (model == null)
            {
                return NotFound();
            }

            if (model.ProfilePhoto != null)
            {
                ViewBag.ProfilePhotoBase64 = Convert.ToBase64String(model.ProfilePhoto);
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

        public IActionResult UploadProfilePhotoModal()
        {
            return PartialView("_UploadProfilePhotoPartial");
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
            else
                updateUserDto.FirstName = currentUser.FirstName;

            if (currentUser.MiddleName != model.MiddleName)
            {
                updateUserDto.MiddleName = model.MiddleName;
                changesDetected = true;
            }
            else
                updateUserDto.MiddleName = currentUser.MiddleName;

            if (currentUser.LastName != model.LastName)
            {
                updateUserDto.LastName = model.LastName;
                changesDetected = true;
            }
            else
                updateUserDto.LastName = currentUser.LastName;

            if (currentUser.DOB != model.DOB)
            {
                updateUserDto.DOB = model.DOB;
                changesDetected = true;
            }
            else
                updateUserDto.DOB = currentUser.DOB;

            if (currentUser.Email != model.Email)
            {
                updateUserDto.Email = model.Email;
                changesDetected = true;
            }
            else
                updateUserDto.Email = currentUser.Email;
            // Do similar comparisons for other fields...

            updateUserDto.Role = currentUser.Role;

            if (!changesDetected)
            {
                return Json(new { success = true, message = "No changes detected. Profile not updated." });
            }

            var updatedUser = await _userService.UpdateUserProfile(updateUserDto);
            if (updatedUser == null)
            {
                TempData["AlertMessage"] = "Profile update failed. Please try again.";
                TempData["AlertType"] = "danger"; // For a red Bootstrap alert
                return RedirectToAction("Index", "Dashboard");
            }

            TempData["AlertMessage"] = "Profile update Successful!.";
            TempData["AlertType"] = "success"; // For a red Bootstrap alert
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfilePhoto(IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
            {
                TempData["Message"] = "Invalid photo file.";
                return RedirectToAction("Index");
            }

            using var memoryStream = new MemoryStream();
            await photo.CopyToAsync(memoryStream);

            var photoBytes = memoryStream.ToArray();

            var profilePhoto = await _profilePhotoService.UploadProfilePhoto(photoBytes);

            if (profilePhoto == null)
            {
                TempData["Message"] = "Failed to update profile photo. Please try again.";
                return RedirectToAction("Index");
            }

            TempData["Message"] = "Successfully updated profile photo.";
            var model = await _userService.GetMyDetails();
            return RedirectToAction("Index", model);
        }

    }
}
