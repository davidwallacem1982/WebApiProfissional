using System;

namespace WebApiProfissional.Domain.ViewModels
{
    /// <summary>
    /// Representa um modelo de visão para exibição dos detalhes de um funcionário.
    /// </summary>
    /// <remarks>
    /// Esta classe é utilizada para encapsular as informações de um funcionário, incluindo o identificador, nome completo, CPF e data de nascimento.
    /// Ela é ideal para exibir os dados de um funcionário em interfaces de usuário ou para transferir dados entre camadas da aplicação.
    /// </remarks>
    /// <remarks>
    /// Inicializa uma nova instância da classe <see cref="FuncionariosViewModel"/> com o identificador, nome completo, CPF e data de nascimento fornecidos.
    /// </remarks>
    /// <param name="id">O identificador do funcionário.</param>
    /// <param name="nomeCompleto">O nome completo do funcionário.</param>
    /// <param name="cpf">O CPF do funcionário.</param>
    /// <param name="dtNascimento">A data de nascimento do funcionário.</param>
    public class FuncionariosViewModel(int id, string nomeCompleto, long cpf, DateOnly dtNascimento)
    {
        /// <summary>
        /// Obtém ou define o identificador do funcionário.
        /// </summary>
        public int Id { get; set; } = id;

        /// <summary>
        /// Obtém ou define o nome completo do funcionário.
        /// </summary>
        public string NomeCompleto { get; set; } = nomeCompleto;

        /// <summary>
        /// Obtém ou define o CPF do funcionário.
        /// </summary>
        public long Cpf { get; set; } = cpf;

        /// <summary>
        /// Obtém ou define a data de nascimento do funcionário.
        /// </summary>
        public DateOnly DtNascimento { get; set; } = dtNascimento;
    }

}
