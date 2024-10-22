using System;
using System.Threading.Tasks;

namespace WebApiProfissional.Domain.Interfaces.Account
{
    public interface IAuthenticate
    {

        /// <summary>
        /// Autentica um usuário verificando se as credenciais fornecidas (login e senha) estão corretas.
        /// Obtém o usuário pelo login e valida a senha fornecida com base no hash e no salt armazenados.
        /// Retorna verdadeiro se a autenticação for bem-sucedida, caso contrário, lança uma exceção se o usuário não for encontrado.
        /// </summary>
        /// <param name="login">id so usuário será autenticado</param>
        /// <param name="senha">senha do usuário que será autenticado</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<bool> AuthenticateUserAsync(string login, string senha);

        /// <summary>
        /// Gera um token JWT (Access Token) para um usuário autenticado.
        /// Cria uma lista de Claims com base no ID e no login do usuário, define as credenciais de assinatura com uma chave secreta
        /// e especifica o emissor, público e tempo de expiração do token. Retorna o token JWT gerado.
        /// </summary>
        /// <param name="id">id so usuário que está autenticado</param>
        /// <param name="login">login do usuário</param>
        /// <returns></returns>
        //Task<string> GenerateAccesToken(int id, string login);

        Task<string> GenerateToken(int userId, string tokenType, string login = null);

        /// <summary>
        /// Gera um token JWT (RefreshToken) para um usuário autenticado.
        /// Cria um conjunto de Claims com base no email do usuário e em um identificador único (JTI).
        /// Assina o token com as credenciais de assinatura obtidas de um serviço JWT e especifica os parâmetros do token.
        /// O token gerado é armazenado na base de dados junto com o JTI e a data de expiração.
        /// Retorna o Refresh Token gerado como uma string codificada.
        /// </summary>
        /// <param name="login">login do usuário</param>
        /// <returns></returns>
        Task<string> GenerateRefreshToken(int id);
    }
}
