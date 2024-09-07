using System;
using WebApiProfissional.Domain.Entities;
using WebApiProfissional.Domain.InputModels.Funcionarios;

namespace WebApiProfissional.Services.PreencherModels
{
    /// <summary>
    /// Classe responsável por preencher uma instância de <see cref="Funcionarios"/> com base nos dados fornecidos
    /// no objeto <see cref="NewFuncionarioInput"/>. Este método cria um novo objeto <see cref="Funcionarios"/>
    /// utilizando as propriedades fornecidas, adicionando automaticamente a data de cadastro.
    /// </summary>
    public static class FuncionarioPreencher
    {
        /// <summary>
        /// Cria uma nova instância de <see cref="Funcionarios"/> a partir dos dados fornecidos em um objeto 
        /// <see cref="NewFuncionarioInput"/>. Este método é utilizado para mapear os dados de entrada do usuário
        /// para a entidade de funcionário, incluindo informações como nome, sobrenome, CPF e datas relevantes.
        /// </summary>
        /// <param name="model">Objeto de entrada do tipo <see cref="NewFuncionarioInput"/> contendo as informações 
        /// necessárias para criar um novo funcionário.</param>
        /// <returns>Retorna uma nova instância de <see cref="Funcionarios"/> preenchida com os dados fornecidos.</returns>
        public static Funcionarios NewFuncionarioInputWithUsuario(NewFuncionarioInput model)
        {
            // Cria uma nova instância da entidade Funcionarios com base nas informações fornecidas no model.
            var funcionario = new Funcionarios()
            {
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                Cpf = model.Cpf,
                DtNascimento = model.DtNascimento,
                DtCadastro = DateTime.Now,  // Define a data de cadastro como a data e hora atual.
                IdAutarquiaFederal = model.IdAutarquiaFederal,
                IdEnderecos = model.IdEnderecos
            };

            return funcionario; // Retorna o objeto Funcionarios preenchido.
        }
    }

}
