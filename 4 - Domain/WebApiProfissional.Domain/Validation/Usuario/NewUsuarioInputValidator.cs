using FluentValidation;
using System.Resources;
using WebApiProfissional.Domain.InputModels.Usuarios;
using WebApiProfissional.Domain.Mensagens;
using WebApiProfissional.Domain.Resources;

namespace WebApiProfissional.Domain.Validation.Usuario
{
    public class NewUsuarioInputValidator : AbstractValidator<NewUsuarioInput>
    {
        public NewUsuarioInputValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage(Mensagem.CampoObrigatorio("Login"));
            //    .Length(10, 20)
            //    .WithMessage(Mensagens.TamanhoCampo) // Mensagem do arquivo Resources com {0}, {1}, {2}
            //    .WithName("Login"); // Nome do campo que será inserido no {0}

            //RuleFor(x => x.Password)
            //    .NotEmpty()
            //    .WithMessage(Mensagens.CampoObrigatorio)
            //    .Length(8, 15)
            //    .WithMessage(Mensagens.TamanhoCampo)
            //    .WithName("Password");

            //RuleFor(x => x.IdFuncionario.ToString())
            //    .NotEmpty()
            //    .WithMessage(Mensagens.CampoObrigatorio)
            //    .WithName("Funcionario");

            //RuleFor(x => x.IsAdmin.ToString())
            //    .NotEmpty()
            //    .WithMessage(Mensagens.CampoObrigatorio)
            //    .WithName("Se é Administrador"); 
        }
    }
}
