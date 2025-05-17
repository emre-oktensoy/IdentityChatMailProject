using Microsoft.AspNetCore.Mvc;

namespace IdentityChatMailProject.ViewComponents
{
    public class _SideBarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
