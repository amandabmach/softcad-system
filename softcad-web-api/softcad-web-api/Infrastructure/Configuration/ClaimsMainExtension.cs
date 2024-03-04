using System.Security.Claims;

namespace softcad_web_api.Infrastructure.Configuration
{
    public static class ClaimsMainExtension
    {
        public static int GetId(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst("id").Value);
        }
        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.FindFirst("email").Value.ToString();
        }
    }
}
