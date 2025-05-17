using IdentityChatMailProject.Context;
using IdentityChatMailProject.Entities;
using IdentityChatMailProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityChatMailProject.ViewComponents
{
    public class _MessageSideBarComponentPartial : ViewComponent
    {
        private readonly IdentityMailContext _context;
        private readonly UserManager<AppUser> _userManager;

        public _MessageSideBarComponentPartial(IdentityMailContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var unreadCount = _context.Messages.Count(m => m.RecevierEmail == user.Email && m.IsRead == false && m.IsTrash == false);
            var sentCount = _context.Messages.Count(m => m.SenderEmail == user.Email && m.IsTrash == false);


            var model = new MessageSidebarCountsViewModel
            {
                UnreadCount = unreadCount,
                SentCount = sentCount               
            };

            return View(model);
        }
    }
}
