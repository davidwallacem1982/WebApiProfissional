using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using WebApiProfissional.Domain.InputModels.Usuarios;
using WebApiProfissional.Domain.Interfaces.Logic;
using WebApiProfissional.Domain.Mensagens;

namespace WebApiProfissional.Domain.Validation.Usuarios
{
    public class NewUsuarioInputValidator : AbstractValidator<NewUsuarioInput>
    {
        private readonly IUsuarioLogic _usuario;
        public NewUsuarioInputValidator(IUsuarioLogic usuario)
        {
            _usuario = usuario;

            RuleFor(x => x.Login)
                .Must(_usuario.UserExistByLogin).WithMessage("Este Login já possui um cadastro").When(customer => customer.Login != string.Empty && customer.Login?.Length > 7).WithErrorCode("ERR001")
                .NotEmpty().WithMessage(Mensagem.CampoObrigatorio("Login")).When(customer => customer.Login != string.Empty).WithErrorCode("ERR002")
                .Length(8, 20).WithMessage(Mensagem.TamanhoCampo("Login", 8, 20)).WithErrorCode("ERR003");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(Mensagem.CampoObrigatorio("Password")).When(customer => customer.Password != string.Empty).WithErrorCode("ERR002")
                .Length(8, 15).WithMessage(Mensagem.TamanhoCampo("Password", 8, 15)).WithErrorCode("ERR003");
        }
    }
}
