using Microsoft.AspNetCore.Mvc;

namespace IdentityChatMailProject.ViewComponents
{
    public class _FooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
