using System.Security.Claims;

namespace KursovaWork.Services
{
    /// <summary>
    /// Клас для отримання ідентифікатора користувача.
    /// </summary>
    public class IDRetriever
    {
        /// <summary>
        /// Об'єкт класу IHttpContextAccessor для получення даних про користувача з Cookie
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Ініціалізує новий екземпляр класу IDRetriever.
        /// </summary>
        /// <param name="httpContextAccessor">Об'єкт IHttpContextAccessor.</param>
        public IDRetriever(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Отримує ідентифікатор ввійшовшого користувача.
        /// </summary>
        /// <returns>Ідентифікатор користувача, або 0, якщо користувач не ввійшов.</returns>
        public int GetLoggedInUserId()
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext.User;
            var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }

            return 0;
        }
    }

}
