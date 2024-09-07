using System.ComponentModel.DataAnnotations;

namespace WebApiProfissional.Domain.InputModels.Authentication
{
    /// <summary>
    /// Representa os dados necessários para o login do usuário.
    /// </summary>
    /// <remarks>
    /// Inicializa uma nova instância da classe <see cref="UserLogin"/> com os parâmetros fornecidos.
    /// </remarks>
    /// <param name="Email">O e-mail do usuário.</param>
    /// <param name="Password">A senha do usuário.</param>
    public class UserLogin(string Email, string Password)
    {
        /// <summary>
        /// Obtém ou define o e-mail do usuário.
        /// </summary>
        /// <remarks>
        /// Este campo é obrigatório e deve estar em um formato de e-mail válido.
        /// </remarks>
        [Required(ErrorMessage = "O {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O {0} está em um formato incorreto")]
        public string Email { get; set; } = Email;

        /// <summary>
        /// Obtém ou define a senha do usuário.
        /// </summary>
        /// <remarks>
        /// Este campo é obrigatório e deve ter entre 8 e 15 caracteres.
        /// </remarks>
        [Required(ErrorMessage = "O {0} é obrigatório")]
        [StringLength(15, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public string Password { get; set; } = Password;
    }

}
