using System.ComponentModel.DataAnnotations;

namespace WebApiProfissional.Domain.InputModels.Usuarios
{
    /// <summary>
    /// Representa os dados necessários para autenticação de um usuário, incluindo o login e a senha.
    /// </summary>
    /// <remarks>
    /// Inicializa uma nova instância da classe <see cref="LoginUsuarioInput"/> com os parâmetros fornecidos.
    /// </remarks>
    /// <param name="Login">O login do usuário.</param>
    /// <param name="Password">A senha do usuário.</param>
    public class LoginUsuarioInput(string Login, string Password)
    {
        /// <summary>
        /// Obtém ou define o login do usuário.
        /// </summary>
        [Required(ErrorMessage = "O {0} é obrigatório")]
        //[EmailAddress(ErrorMessage = "O {0} está em um formato incorreto")]
        public string Login { get; set; } = Login;

        /// <summary>
        /// Obtém ou define a senha do usuário.
        /// </summary>
        [Required(ErrorMessage = "O {0} é obrigatório")]
        //[StringLength(15, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public string Password { get; set; } = Password;
    }

}
