using System.ComponentModel.DataAnnotations;

namespace WebApiProfissional.Domain.InputModels.User
{
    /// <summary>
    /// Representa a entrada necessária para o processo de logout.
    /// </summary>
    /// <param name="RefreshToken">O Refresh Token que será revogado.</param>
    public class LogoutInput(string RefreshToken)
    {
        /// <summary>
        /// O Refresh Token que deve ser revogado durante o processo de logout.
        /// </summary>
        [Required(ErrorMessage = "O {0} é obrigatório.")]
        public string RefreshToken { get; set; } = RefreshToken;
    }
}
