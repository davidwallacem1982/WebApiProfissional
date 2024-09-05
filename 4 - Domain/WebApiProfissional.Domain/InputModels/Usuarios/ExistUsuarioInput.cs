namespace WebApiProfissional.Domain.InputModels.Usuarios
{
    /// <summary>
    /// Representa os dados necessários para verificar a existência de um usuário, incluindo o login e as informações de senha.
    /// </summary>
    /// <remarks>
    /// Inicializa uma nova instância da classe <see cref="ExistUsuarioInput"/> com os parâmetros fornecidos.
    /// </remarks>
    /// <param name="Login">O login do usuário.</param>
    /// <param name="PasswordHash">O hash da senha do usuário.</param>
    /// <param name="PasswordSalt">O salt da senha do usuário.</param>
    public class ExistUsuarioInput(string Login, string PasswordHash, string PasswordSalt)
    {
        /// <summary>
        /// Obtém ou define o login do usuário.
        /// </summary>
        public string Login { get; set; } = Login;

        /// <summary>
        /// Obtém ou define o hash da senha do usuário.
        /// </summary>
        public string PasswordHash { get; set; } = PasswordHash;

        /// <summary>
        /// Obtém ou define o salt da senha do usuário.
        /// </summary>
        public string PasswordSalt { get; set; } = PasswordSalt;
    }

}
