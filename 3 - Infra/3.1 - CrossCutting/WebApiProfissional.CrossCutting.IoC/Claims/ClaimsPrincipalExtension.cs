using System.Security.Claims;

namespace WebApiProfissional.CrossCutting.IoC.Claims
{
    public static class ClaimsPrincipalExtension
    {
        public static int GetId(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst("id").Value);
        }

        public static string GetLogin(this ClaimsPrincipal user)
        {
            return user.FindFirst("login").Value;
        }
    }
}
