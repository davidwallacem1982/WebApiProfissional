using Microsoft.IdentityModel.Tokens;
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
        /// Cria e retorna as credenciais de assinatura para JWT.
        /// Este método configura uma chave secreta HMAC para a assinatura de tokens JWT. 
        /// Verifica se a chave é suficientemente longa (pelo menos 256 bits) e cria um objeto 
        /// <see cref="SigningCredentials"/> usando a chave e o algoritmo HMAC SHA-256.
        /// </summary>
        /// <returns>Um objeto <see cref="SigningCredentials"/> configurado para assinatura de tokens JWT.</returns>
        /// <exception cref="InvalidOperationException">Se a chave HMAC for menor que 256 bits.</exception>
        SigningCredentials Credentials();

        /// <summary>
        /// Gera um token JWT (Access Token) para um usuário autenticado.
        /// Cria uma lista de Claims com base no ID e no login do usuário, define as credenciais de assinatura com uma chave secreta
        /// e especifica o emissor, público e tempo de expiração do token. Retorna o token JWT gerado.
        /// </summary>
        /// <param name="id">id so usuário que está autenticado</param>
        /// <param name="login">login do usuário</param>
        /// <returns></returns>
        Task<string> GenerateAccesToken(int id, string login);

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

        /// <summary>
        /// Armazena um RefreshToken na base de dados associado ao usuário.
        /// Obtém o usuário pelo login, cria um novo registro de Refresh Token contendo o token, JTI e data de expiração,
        /// e insere-o na tabela de RefreshTokens.
        /// </summary>
        /// <param name="login">Login do usuário</param>
        /// <param name="refreshToken">a Refresh Token a ser armazenado.</param>
        /// <param name="jti">identificador único do token</param>
        /// <param name="expiresAt">data de expiração</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exception"></exception>
        Task StoreRefreshToken(int id, string refreshToken, string jti, DateTime expiresAt);

        /// <summary>
        /// Revoga um Refresh Token correspondente ao identificador único (JTI) para um usuário específico.
        /// Obtém o usuário pelo login, busca o token associado ao JTI fornecido e marca o token como revogado, 
        /// atualizando a data de revogação na base de dados.
        /// </summary>
        /// <param name="login">Login do usuário</param>
        /// <param name="jti">identificador único do token</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task WithoutRevokeRefreshToken(string Login, string jti);

        /// <summary>
        /// Revoga um Refresh Token correspondente ao identificador único (JTI) para um usuário específico.
        /// Obtém o usuário pelo login, busca o token associado ao JTI fornecido e grava o token revogado na tabela RevokedTokens,
        /// registrando a data de revogação na base de dados.
        /// </summary>
        /// <param name="Login">Login do usuário</param>
        /// <param name="jti">identificador único do token</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task WithRevokeRefreshToken(string Login, string jti);

        /// <summary>
        /// Verifica se um token específico foi revogado.
        /// Consulta a tabela RefreshTokens para determinar se o token fornecido está marcado como revogado.
        /// Retorna verdadeiro se o token estiver revogado, caso contrário, retorna falso.
        /// </summary>
        /// <param name="token">O Token a ser verificado.</param>
        /// <returns></returns>
        Task<bool> IsTokenRevoked(string token);

        /// <summary>
        /// Revoga o Refresh Token fornecido e o marca como revogado na tabela de Refresh Tokens.
        /// Também adiciona um registro na tabela RevokedTokens para manter o histórico de revogações.
        /// </summary>
        /// <param name="userId">O ID do usuário associado ao Refresh Token.</param>
        /// <param name="refreshToken">O Refresh Token a ser revogado.</param>
        /// <exception cref="InvalidOperationException">Se o Refresh Token não for encontrado.</exception>
        Task RevokeRefreshToken(int userId, string refreshToken);
    }
}
