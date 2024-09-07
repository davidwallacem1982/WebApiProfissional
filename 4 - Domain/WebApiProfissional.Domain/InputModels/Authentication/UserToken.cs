namespace WebApiProfissional.Domain.InputModels.Authentication
{
    /// <summary>
    /// Representa um modelo que encapsula os tokens de acesso e de atualização, juntamente com uma indicação se o usuário é administrador.
    /// </summary>
    /// <remarks>
    /// Esta classe é utilizada para armazenar e transferir as informações relacionadas ao token de acesso (AccessToken), token de atualização (RefreshToken) 
    /// e se o usuário possui permissões administrativas.
    /// </remarks>
    /// <remarks>
    /// Inicializa uma nova instância da classe <see cref="UserToken"/> com o token de acesso, token de atualização e a flag de administrador.
    /// </remarks>
    /// <param name="accesToken">O token de acesso do usuário.</param>
    /// <param name="refreshToken">O token de atualização do usuário.</param>
    /// <param name="isAdmin">Indica se o usuário é administrador.</param>
    public class UserToken(string accesToken, string refreshToken, bool isAdmin)
    {
        /// <summary>
        /// Obtém ou define o token de acesso associado ao usuário.
        /// </summary>
        public string AccesToken { get; set; } = accesToken;

        /// <summary>
        /// Obtém ou define o token de atualização associado ao usuário.
        /// </summary>
        public string RefreshToken { get; set; } = refreshToken;

        /// <summary>
        /// Obtém ou define um valor indicando se o usuário é administrador.
        /// </summary>
        public bool IsAdmin { get; set; } = isAdmin;
    }

}
