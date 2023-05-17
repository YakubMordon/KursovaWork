using Microsoft.AspNetCore.Mvc.Rendering;

namespace KursovaWork.Services
{
    /// <summary>
    /// Клас-допоміжник для роботи з ViewBag.
    /// </summary>
    public static class ViewBagHelper
    {
        /// <summary>
        /// Встановлює значення IsLoggedIn в ViewBag на основі інформації про аутентифікацію користувача.
        /// </summary>
        /// <param name="viewContext">Контекст перегляду ViewContext.</param>
        public static void SetIsLoggedInInViewBag(this ViewContext viewContext)
        {
            bool isLoggedIn = viewContext.HttpContext.User.Identity.IsAuthenticated;
            viewContext.ViewBag.IsLoggedIn = isLoggedIn;
        }
    }
}
