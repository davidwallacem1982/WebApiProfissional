using System;

namespace WebApiProfissional.Domain.InputModels.Funcionarios
{
    public class NewFuncionarioInput(string Nome, string Sobrenome, long Cpf, DateOnly DtNascimento, int IdAutarquiaFederal, int IdEnderecos)
    {
        public string Nome { get; set; } = Nome;

        public string Sobrenome { get; set; } = Sobrenome;

        public long Cpf { get; set; } = Cpf;

        public DateOnly DtNascimento { get; set; } = DtNascimento;

        public int IdAutarquiaFederal { get; set; } = IdAutarquiaFederal;

        public int IdEnderecos { get; set; } = IdEnderecos;
    }
}
