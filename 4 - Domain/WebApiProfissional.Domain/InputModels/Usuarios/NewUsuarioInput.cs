namespace WebApiProfissional.Domain.InputModels.Usuarios
{
    /// <summary>
    /// Representa os dados necessários para criar um novo usuário, incluindo o identificador, o login, a senha e o identificador do funcionário associado.
    /// </summary>
    /// <remarks>
    /// Inicializa uma nova instância da classe <see cref="NewUsuarioInput"/> com os parâmetros fornecidos.
    /// </remarks>
    /// <param name="Login">O login do usuário.</param>
    /// <param name="Password">A senha do usuário.</param>
    /// <param name="IdFuncionario">O identificador do funcionário associado ao usuário.</param>
    public class NewUsuarioInput(string Login, string Password, bool IsAdmin, int IdFuncionario)
    {
        /// <summary>
        /// Obtém ou define o login do usuário.
        /// </summary>
        public string Login { get; set; } = Login;

        /// <summary>
        /// Obtém ou define a senha do usuário.
        /// </summary>
        public string Password { get; set; } = Password;

        /// <summary>
        /// Obtém ou define se usuário o é administrador ou não.
        /// </summary>
        public bool IsAdmin { get; set; } = IsAdmin;

        /// <summary>
        /// Obtém ou define o identificador do funcionário associado ao usuário.
        /// </summary>
        public int IdFuncionario { get; set; } = IdFuncionario;
    }

}
