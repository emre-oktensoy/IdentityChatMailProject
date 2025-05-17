using IdentityChatMailProject.Context;
using IdentityChatMailProject.Entities;
using IdentityChatMailProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityChatMailProject.ViewComponents
{
    public class _NavBarComponentPartial : ViewComponent
    {
        private readonly IdentityMailContext _context;
        private readonly UserManager<AppUser> _userManager;

        public _NavBarComponentPartial(IdentityMailContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var unreadCount = _context.Messages.Count(m => m.RecevierEmail == user.Email && m.IsRead == false && m.IsTrash == false);

            var imageUrl = string.IsNullOrEmpty(user.ProfileImageUrl)
            ? "/quixlab-master/plugins/default-profile.png" // Varsayılan resim yolu
            : user.ProfileImageUrl;

            var model = new MessageSidebarCountsViewModel
            {
                UnreadCount = unreadCount,
                ImageUrl = imageUrl
            };

            return View(model);
        }
    
    }
}
