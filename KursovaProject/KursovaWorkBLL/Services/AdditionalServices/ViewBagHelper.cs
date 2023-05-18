using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace KursovaWorkBLL.Services.AdditionalServices
{
    /// <summary>
    /// Клас-допоміжник для роботи з ViewBag.
    /// </summary>
    public static class ViewBagHelper
    {
        /// <summary>
        /// Об'єкт класу ILogger для логування подій 
        /// </summary>
        private static readonly ILogger _logger = LoggerFactory.Create(builder => builder.AddConsole())
            .CreateLogger(typeof(ViewBagHelper));

        /// <summary>
        /// Встановлює значення IsLoggedIn в ViewBag на основі інформації про аутентифікацію користувача.
        /// </summary>
        /// <param name="viewContext">Контекст перегляду ViewContext.</param>
        public static void SetIsLoggedInInViewBag(this ViewContext viewContext)
        {
            bool isLoggedIn = viewContext.HttpContext.User.Identity.IsAuthenticated;
            viewContext.ViewBag.IsLoggedIn = isLoggedIn;
            _logger.LogInformation("Успішно перевірено чи користувач є залогіненим");
        }
    }
}
