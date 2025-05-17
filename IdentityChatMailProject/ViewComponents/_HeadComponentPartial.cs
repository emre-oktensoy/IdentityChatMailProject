using Microsoft.AspNetCore.Mvc;

namespace IdentityChatMailProject.ViewComponents
{
    public class _HeadComponentPartial: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
