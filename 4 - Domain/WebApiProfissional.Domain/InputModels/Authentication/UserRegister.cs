using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApiProfissional.Domain.InputModels.Authentication
{
    public class UserRegister(int Id, string Email, string Password, string ConfirmPassword, int IdFuncionario)
    {
        public int Id { get; set; } = Id;

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O {0} está em um formato incorreto")]
        public string Email { get; set; } = Email;

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [StringLength(15, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public string Password { get; set; } = Password;

        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "The passwords doesn't match.")]
        public string ConfirmPassword { get; set; } = ConfirmPassword;

        public int IdFuncionario { get; set; } = IdFuncionario;
    }
}
