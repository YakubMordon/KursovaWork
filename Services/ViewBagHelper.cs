using Microsoft.AspNetCore.Mvc.Rendering;

namespace KursovaWork.Services
{
    public static class ViewBagHelper
    {
        public static void SetIsLoggedInInViewBag(this ViewContext viewContext)
        {
            bool isLoggedIn = viewContext.HttpContext.User.Identity.IsAuthenticated;
            viewContext.ViewBag.IsLoggedIn = isLoggedIn;
        }
    }
}
