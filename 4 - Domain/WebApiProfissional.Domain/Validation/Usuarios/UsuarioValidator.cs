using FluentValidation;
using WebApiProfissional.Domain.InputModels.Usuarios;
using WebApiProfissional.Domain.Interfaces.Logic;
using WebApiProfissional.Domain.Mensagens;

namespace WebApiProfissional.Domain.Validation.Usuarios
{
    /// <summary>
    /// Classe de validação para a entrada de um novo usuário (<see cref="NewUsuarioInput"/>).
    /// Esta classe valida as regras de negócio associadas ao registro de um novo usuário, como a verificação de login já existente,
    /// e o cumprimento de requisitos de tamanho e preenchimento dos campos "Login" e "Password".
    /// </summary>
    public class RegisterUsuarioValidator : AbstractValidator<NewUsuarioInput>
    {
        private readonly IUsuarioLogic _usuario;

        /// <summary>
        /// Construtor da classe <see cref="RegisterUsuarioValidator"/>.
        /// Inicializa as regras de validação para os campos do modelo <see cref="NewUsuarioInput"/>.
        /// </summary>
        /// <param name="usuario">Instância de <see cref="IUsuarioLogic"/> usada para verificar se um login já existe no sistema.</param>
        public RegisterUsuarioValidator(IUsuarioLogic usuario)
        {
            _usuario = usuario;

            // Validação do campo "Login"
            RuleFor(x => x.Login)
                // Verifica se o login já está cadastrado, aplicando a mensagem e código de erro caso positivo
                .Must(_usuario.UserExistByLogin)
                .WithMessage("Este Login já possui um cadastro")
                .When(customer => customer.Login != string.Empty && customer.Login?.Length > 7)
                .WithErrorCode("ERR001")

                // Valida se o campo "Login" não está vazio
                .NotEmpty()
                .WithMessage(Mensagem.CampoObrigatorio("Login"))
                .When(customer => customer.Login != string.Empty)
                .WithErrorCode("ERR002")

                // Verifica se o tamanho do login está entre 8 e 20 caracteres
                .Length(8, 20)
                .WithMessage(Mensagem.TamanhoCampo("Login", 8, 20))
                .WithErrorCode("ERR003");

            // Validação do campo "Password"
            RuleFor(x => x.Password)
                // Valida se o campo "Password" não está vazio
                .NotEmpty()
                .WithMessage(Mensagem.CampoObrigatorio("Password"))
                .When(customer => customer.Password != string.Empty)
                .WithErrorCode("ERR002")

                // Verifica se o tamanho da senha está entre 8 e 15 caracteres
                .Length(8, 15)
                .WithMessage(Mensagem.TamanhoCampo("Password", 8, 15))
                .WithErrorCode("ERR003");
        }
    }

}
