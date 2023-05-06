using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace KursovaWork.Services
{

    public class IDRetriever
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IDRetriever(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

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
