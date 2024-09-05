using System.Text.RegularExpressions;

namespace WebApiProfissional.Utils
{
    public static class Util
    {
        /// <summary>
        /// Remove todos os caracteres não numéricos de uma string.
        /// </summary>
        /// <param name="dado">A string da qual os caracteres não numéricos serão removidos.</param>
        /// <returns>A string resultante contendo apenas caracteres numéricos.</returns>
        public static string RemoverCaracteresNaoNumericos(string dado)
        {
            return Regex.Replace(dado, @"\D", "");
        }

        /// <summary>
        /// Verifica se a string fornecida é nula ou vazia.
        /// </summary>
        /// <param name="dado">A string a ser verificada.</param>
        /// <returns>True se a string for nula ou vazia, caso contrário, false.</returns>
        public static bool IsNullOrEmpty(string dado)
        {
            return string.IsNullOrEmpty(dado);
        }

        /// <summary>
        /// Valida se o CPF fornecido é um CPF válido, realizando verificações de formato e dígitos verificadores.
        /// </summary>
        /// <param name="cpf">O CPF a ser validado.</param>
        /// <returns>True se o CPF for válido, caso contrário, false.</returns>
        public static bool ValidarCPF(string cpf)
        {
            // Remover caracteres não numéricos do CPF
            var cpfNumerico = RemoverCaracteresNaoNumericos(cpf);

            // Verificar se o CPF possui 11 dígitos
            if (cpfNumerico.Length != 11)
                return false;

            // Verificar se todos os dígitos são iguais, o que tornaria o CPF inválido
            if (new string(cpfNumerico[0], 11) == cpfNumerico)
                return false;

            // Calcular o primeiro dígito verificador
            var soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (cpfNumerico[i] - '0') * (10 - i);

            var resto = soma % 11;
            var digitoVerificador1 = (resto < 2) ? 0 : 11 - resto;

            // Verificar o primeiro dígito verificador
            if (digitoVerificador1 != cpfNumerico[9] - '0')
                return false;

            // Calcular o segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (cpfNumerico[i] - '0') * (11 - i);

            resto = soma % 11;
            var digitoVerificador2 = (resto < 2) ? 0 : 11 - resto;

            // Verificar o segundo dígito verificador
            return digitoVerificador2 == cpfNumerico[10] - '0';
        }


        /// <summary>
        /// Valida se o CNPJ fornecido é um CNPJ válido, realizando verificações de formato e dígitos verificadores.
        /// </summary>
        /// <param name="cnpj">O CNPJ a ser validado.</param>
        /// <returns>True se o CNPJ for válido, caso contrário, false.</returns>
        public static bool ValidarCNPJ(string cnpj)
        {
            // Remover caracteres não numéricos do CNPJ
            string cnpjNumerico = RemoverCaracteresNaoNumericos(cnpj);

            // Verificar se o CNPJ possui 14 dígitos
            if (cnpjNumerico.Length != 14)
                return false;

            // Calcular o primeiro dígito verificador
            int soma = 0;
            int peso = 5;
            for (int i = 0; i < 12; i++)
            {
                soma += (cnpjNumerico[i] - '0') * peso;
                peso = (peso == 2) ? 9 : peso - 1;
            }

            int resto = soma % 11;
            int digitoVerificador1 = (resto < 2) ? 0 : 11 - resto;

            // Verificar o primeiro dígito verificador
            if (digitoVerificador1 != cnpjNumerico[12] - '0')
                return false;

            // Calcular o segundo dígito verificador
            soma = 0;
            peso = 6;
            for (int i = 0; i < 13; i++)
            {
                soma += (cnpjNumerico[i] - '0') * peso;
                peso = (peso == 2) ? 9 : peso - 1;
            }

            resto = soma % 11;
            int digitoVerificador2 = (resto < 2) ? 0 : 11 - resto;

            // Verificar o segundo dígito verificador
            return digitoVerificador2 == cnpjNumerico[13] - '0';
        }


        /// <summary>
        /// Formata um nome completo, capitalizando as palavras e tratando exceções para palavras específicas que não devem ser capitalizadas.
        /// </summary>
        /// <param name="nomeCompleto">O nome completo a ser formatado.</param>
        /// <returns>O nome completo formatado com a capitalização adequada.</returns>
        public static string FormatarNome(string nomeCompleto)
        {
            // Divide o nome em palavras
            string[] palavras = nomeCompleto.Split(' ');

            // Palavras que não devem ser capitalizadas
            string[] palavrasNaoCapitalizadas = { "da", "de", "do", "das", "dos" }; // Adicione outras palavras conforme necessário

            // Capitaliza o primeiro caractere de cada palavra, exceto as palavras não capitalizadas
            for (int i = 0; i < palavras.Length; i++)
            {
                if (!string.IsNullOrEmpty(palavras[i]))
                {
                    if (i == 0 || Array.IndexOf(palavrasNaoCapitalizadas, palavras[i].ToLower()) == -1)
                    {
                        char[] chars = palavras[i].ToCharArray();
                        chars[0] = char.ToUpper(chars[0]);
                        palavras[i] = new string(chars);
                    }
                }
            }

            // Junta as palavras de volta em um nome formatado
            var nomeFormatado = string.Join(" ", palavras);

            return nomeFormatado;
        }

        /// <summary>
        /// Formata uma data ou hora com base no tipo especificado.
        /// </summary>
        /// <param name="date">A data ou hora a ser formatada.</param>
        /// <param name="tipo">O tipo de formatação: 0 para data (dd/MM/yyyy), 1 para data (dd-MM-yyyy), 2 para hora (HH:mm), 3 para data e hora (dd/MM/yyyy HH:mm).</param>
        /// <returns>A string formatada de acordo com o tipo especificado.</returns>
        public static string FormatDateOrHour(DateTime date, int tipo)
        {
            switch (tipo)
            {
                case 0:
                    return date.ToString("dd/MM/yyyy");

                case 1:
                    return date.ToString("dd-MM-yyyy");

                case 2:
                    return date.ToString("HH:mm");

                case 3:
                    return date.ToString("dd/MM/yyyy HH:mm");

                default:
                    return date.ToString();
            }
        }

        /// <summary>
        /// Manipula a exceção fornecida e registra a mensagem de erro.
        /// </summary>
        /// <param name="errorMessage">A mensagem de erro a ser registrada.</param>
        /// <param name="ex">A exceção que foi lançada.</param>
        /// <returns>False, indicando que a exceção foi registrada, mas não há tratamento adicional.</returns>
        public static bool HandleException(string errorMessage, Exception ex)
        {
            return false;
        }

        /// <summary>
        /// Cria uma string de descrição para uma exceção de banco de dados.
        /// </summary>
        /// <param name="Message">A mensagem de erro da exceção.</param>
        /// <param name="StackTrace">O stack trace da exceção.</param>
        /// <returns>A string formatada contendo a mensagem de erro e o stack trace.</returns>
        public static string ExceptionService(string Message, string StackTrace)
        {
            return $"Erro durante o comando no banco de dados: {Message}\nStack Trace: {StackTrace}";
        }
    }
}
