namespace WebApiProfissional.Domain.InputModels.Usuarios
{
    /// <summary>
    /// Representa os dados necessários para atualizar um usuário, incluindo o identificador e a nova senha.
    /// </summary>
    /// <remarks>
    /// Inicializa uma nova instância da classe <see cref="UpdateUsuarioInput"/> com os parâmetros fornecidos.
    /// </remarks>
    /// <param name="Id">O identificador do usuário.</param>
    /// <param name="Password">A nova senha do usuário.</param>
    public class UpdateUsuarioInput(int Id, string Password)
    {
        /// <summary>
        /// Obtém ou define o identificador do usuário a ser atualizado.
        /// </summary>
        public int Id { get; set; } = Id;

        /// <summary>
        /// Obtém ou define a nova senha do usuário.
        /// </summary>
        public string Password { get; set; } = Password;
    }

}
