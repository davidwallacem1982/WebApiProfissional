namespace WebApiProfissional.Domain.Mensagens
{
    /// <summary>
    /// Classe utilitária para gerar mensagens de erro de validação.
    /// Fornece métodos para retornar mensagens formatadas que indicam erros comuns,
    /// como campos obrigatórios, tamanho de campos, e validade de email.
    /// </summary>
    public static class Mensagem
    {
        /// <summary>
        /// Gera uma mensagem de erro para um campo obrigatório ausente.
        /// </summary>
        /// <param name="dado">Nome do campo que é obrigatório.</param>
        /// <returns>Mensagem de erro indicando que o campo é obrigatório.</returns>
        public static string CampoObrigatorio(string dado)
        {
            // Substitui {0} na mensagem pelo nome do campo fornecido
            return string.Format(Resources.Erros.ERR0001_CampoObrigatorio, dado);
        }

        /// <summary>
        /// Gera uma mensagem de erro para um campo cujo tamanho está fora dos limites permitidos.
        /// </summary>
        /// <param name="dado">Nome do campo que está sendo validado.</param>
        /// <param name="minimo">Tamanho mínimo permitido para o campo.</param>
        /// <param name="maximo">Tamanho máximo permitido para o campo.</param>
        /// <returns>Mensagem de erro indicando o tamanho permitido para o campo.</returns>
        public static string TamanhoCampo(string dado, int minimo, int maximo)
        {
            // Substitui {0}, {1}, e {2} na mensagem com o nome do campo, tamanho mínimo e máximo, respectivamente
            return string.Format(Resources.Erros.ERR0002_TamanhoCampo, dado, minimo, maximo);
        }

        /// <summary>
        /// Gera uma mensagem de erro indicando que o campo tem menos caracteres do que o permitido.
        /// </summary>
        /// <param name="dado">Nome do campo que está sendo validado.</param>
        /// <param name="minimo">Número mínimo de caracteres permitido.</param>
        /// <returns>Mensagem de erro indicando o número mínimo de caracteres necessário para o campo.</returns>
        public static string MinimoCaracteres(string dado, int minimo)
        {
            // Substitui {0} e {1} na mensagem com o nome do campo e o número mínimo de caracteres, respectivamente
            return string.Format(Resources.Erros.ERR0003_MinimoCaracteres, dado, minimo);
        }

        /// <summary>
        /// Gera uma mensagem de erro indicando que o email fornecido é inválido.
        /// </summary>
        /// <returns>Mensagem de erro para email inválido.</returns>
        public static string EmailValido()
        {
            // Retorna uma mensagem de erro padrão para email inválido
            return string.Format(Resources.Erros.ERR0004_EmailValido);
        }
    }

}
