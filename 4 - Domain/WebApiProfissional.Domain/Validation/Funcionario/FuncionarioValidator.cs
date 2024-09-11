using FluentValidation;
using System;
using WebApiProfissional.Domain.InputModels.Funcionarios;
using WebApiProfissional.Domain.Interfaces.Logic;
using WebApiProfissional.Domain.Mensagens;
using WebApiProfissional.Utils;

namespace WebApiProfissional.Domain.Validation.Funcionario
{
    /// <summary>
    /// Classe de validação para a entrada de um novo funcionário (<see cref="NewFuncionarioInput"/>).
    /// Esta classe valida as regras de negócio associadas ao registro de um novo funcionário, como a verificação de campos obrigatórios,
    /// tamanho de campos e a validação de CPF e data de nascimento.
    /// </summary>
    public class RegisterFuncionarioValidator : AbstractValidator<NewFuncionarioInput>
    {
        private readonly IFuncionariosLogic _funcionario;

        /// <summary>
        /// Construtor da classe <see cref="RegisterFuncionarioValidator"/>.
        /// Inicializa as regras de validação para os campos do modelo <see cref="NewFuncionarioInput"/>.
        /// </summary>
        /// <param name="funcionario">Instância de <see cref="IFuncionariosLogic"/> usada para verificar se um CPF já existe no sistema.</param>
        public RegisterFuncionarioValidator(IFuncionariosLogic funcionario)
        {
            _funcionario = funcionario;

            // Validação do campo "Nome"
            RuleFor(x => x.Nome)
                // Verifica se o campo "Nome" não está vazio
                .NotEmpty().WithMessage(Mensagem.CampoObrigatorio("Nome"))
                .When(customer => customer.Nome != string.Empty && customer.Sobrenome?.Length > 5)
                .WithErrorCode("ERR0001")

                // Verifica se o tamanho do nome está entre 6 e 50 caracteres
                .Length(6, 50)
                .WithMessage(Mensagem.TamanhoCampo("Nome", 6, 50))
                .WithErrorCode("ERR0002");

            // Validação do campo "Sobrenome"
            RuleFor(x => x.Sobrenome)
                // Verifica se o campo "Sobrenome" não está vazio
                .NotEmpty().WithMessage(Mensagem.CampoObrigatorio("Sobrenome"))
                .When(customer => customer.Sobrenome != string.Empty && customer.Sobrenome?.Length > 5)
                .WithErrorCode("ERR0001")

                // Verifica se o tamanho do sobrenome está entre 6 e 100 caracteres
                .Length(6, 100)
                .WithMessage(Mensagem.TamanhoCampo("Sobrenome", 6, 100))
                .WithErrorCode("ERR0002");

            // Validação do campo "Cpf"
            RuleFor(x => x.Cpf.ToString())
                // Verifica se o CPF já está cadastrado
                .Must(_funcionario.GetFuncionarioExistByCpf)
                .WithMessage("Este Cpf já possui um cadastro")
                .When(customer => customer.Cpf.ToString() != "0" && customer.Cpf.ToString()?.Length > 10)
                .WithErrorCode("ERR0001C")

                // Verifica se o CPF tem entre 11 e 14 caracteres
                .Length(11, 14)
                .WithMessage(Mensagem.TamanhoCampo("Cpf", 11, 14))
                .WithErrorCode("ERR0002");

            // Validação do campo "DtNascimento"
            RuleFor(x => x.DtNascimento.ToString())
                // Verifica se a data de nascimento não está vazia ou não é inválida
                .NotEmpty().WithMessage(Mensagem.CampoObrigatorio("Data de Nascimento"))
                .WithErrorCode("ERR001")

                // Verifica se a data de nascimento é válida
                .Must(Util.ValidaDate)
                .WithMessage(Mensagem.CampoObrigatorio("Data de Nascimento"))
                .When(customer => customer.DtNascimento.ToString() != string.Empty)
                .WithErrorCode("ERR0002C");

            // Validação do campo "IdAutarquiaFederal"
            RuleFor(x => x.IdAutarquiaFederal.ToString())
                // Verifica se o campo "Autarquia Federal" não está vazio
                .NotEmpty().WithMessage(Mensagem.CampoObrigatorio("Autarquia Federal"))
                .When(customer => customer.DtNascimento.ToString() != "0")
                .WithErrorCode("ERR001");

            // Validação do campo "IdEnderecos"
            RuleFor(x => x.IdEnderecos.ToString())
                // Verifica se o campo "Endereço" não está vazio
                .NotEmpty().WithMessage(Mensagem.CampoObrigatorio("Endereço"))
                .When(customer => customer.DtNascimento.ToString() != "0")
                .WithErrorCode("ERR001");
        }
    }
}
