using System.ComponentModel.DataAnnotations;

namespace WebApiProfissional.Domain.InputModels.Authentication
{
    /// <summary>
    /// Representa a entrada necessária para a renovação de um token.
    /// </summary>
    /// <param name="RefreshToken">O Refresh Token enviado pelo cliente.</param>
    public class RefreshTokenInput(string RefreshToken)
    {
        /// <summary>
        /// O Refresh Token enviado pelo cliente para renovação.
        /// </summary>
        [Required(ErrorMessage = "O {0} é obrigatório.")]
        public string RefreshToken { get; set; } = RefreshToken;
    }

}
