using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HierarchicalItemProcessingSystem.ViewModels;
using System.Security.Claims;

namespace HierarchicalItemProcessingSystem.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? "User";
            var displayName = Request.Cookies["UserDisplayName"];

            if (string.IsNullOrEmpty(displayName))
            {
                displayName = email.Split('@')[0]; // Default to email prefix
            }

            // Generate an avatar deterministically using the DisplayName
            // We use dicebear api to fetch a cool avatar!
            var avatarUrl = $"https://api.dicebear.com/7.x/adventurer/svg?seed={Uri.EscapeDataString(displayName)}";

            var model = new ProfileViewModel
            {
                Email = email,
                DisplayName = displayName,
                AvatarUrl = avatarUrl,
                AccountStatus = "Active Supervisor"
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateName(string newDisplayName)
        {
            if (!string.IsNullOrWhiteSpace(newDisplayName))
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddDays(30),
                    HttpOnly = true
                };
                Response.Cookies.Append("UserDisplayName", newDisplayName, cookieOptions);
            }
            return RedirectToAction("Index");
        }
    }
}
