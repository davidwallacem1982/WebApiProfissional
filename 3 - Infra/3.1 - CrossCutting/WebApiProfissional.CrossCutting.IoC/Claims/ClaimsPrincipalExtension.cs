using System.Security.Claims;

namespace WebApiProfissional.CrossCutting.IoC.Claims
{
    public static class ClaimsPrincipalExtension
    {
        /// <summary>
        /// Obtém o identificador do usuário (ID) a partir do token de segurança (ClaimsPrincipal).
        /// Este método busca o valor da claim "id" e o converte para um inteiro.
        /// </summary>
        /// <param name="user">O objeto ClaimsPrincipal que contém as informações do usuário autenticado.</param>
        /// <returns>O ID do usuário como um valor inteiro.</returns>
        /// <exception cref="FormatException">Lançado se a claim "id" não puder ser convertida para inteiro.</exception>
        public static int GetId(this ClaimsPrincipal user)
        {
            // Busca a claim "id" no token e converte seu valor para inteiro.
            return int.Parse(user.FindFirst("id").Value);
        }

        /// <summary>
        /// Obtém o login do usuário a partir do token de segurança (ClaimsPrincipal).
        /// Este método busca o valor da claim "login".
        /// </summary>
        /// <param name="user">O objeto ClaimsPrincipal que contém as informações do usuário autenticado.</param>
        /// <returns>O login do usuário como uma string.</returns>
        /// <exception cref="NullReferenceException">Lançado se a claim "login" não for encontrada ou se o valor for nulo.</exception>
        public static string GetLogin(this ClaimsPrincipal user)
        {
            // Busca a claim "login" no token e retorna seu valor.
            return user.FindFirst("login").Value;
        }
    }
}
