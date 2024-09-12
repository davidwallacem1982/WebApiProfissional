using WebApiProfissional.Utils.Models;
using System.Security.Cryptography;
using System.Text;

namespace WebApiProfissional.Utils
{
    public static class PasswordHelper
    {
        /// <summary>
        /// Método para Gerar um novo HashedPassword como a senha criada pelo usuário para cadastra-la na Base;
        /// </summary>
        /// <param name="password">senha criada pelo usuário</param>
        /// <returns></returns>
        public static PasswordModel GerarHashAndSalt(string password)
        {
            using var hmac = new HMACSHA512();
            var newPassword = new PasswordModel(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)), hmac.Key);
            return newPassword;
        }

        /// <summary>
        /// Método para validar a senha do usuário com o hash e salt armazenados.
        /// </summary>
        /// <param name="password">Senha fornecida pelo usuário</param>
        /// <param name="storedHash">Hash armazenado na base de dados</param>
        /// <param name="storedSalt">Salt armazenado na base de dados</param>
        /// <returns>true se a senha for válida, caso contrário false</returns>
        public static bool ValidarSenha(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != storedHash[i])
                {
                    throw new InvalidOperationException("Senha inválido");
                }
            }
            return true;
        }
    }
}
