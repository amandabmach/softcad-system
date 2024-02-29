using System.Security.Claims;

namespace WebApiOperacaoCuriosidade.Infrastructure
{
    public static class ClaimsPrincipalExtension
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
