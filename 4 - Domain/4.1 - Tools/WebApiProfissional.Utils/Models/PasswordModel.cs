namespace WebApiProfissional.Utils.Models
{
    /// <summary>
    /// Representa um modelo para armazenar informações relacionadas à senha, incluindo o hash e o salt.
    /// </summary>
    /// <remarks>
    /// Inicializa uma nova instância da classe <see cref="PasswordModel"/> com o hash e o salt especificados.
    /// </remarks>
    /// <param name="Hash">O hash da senha.</param>
    /// <param name="Salt">O salt da senha.</param>
    public class PasswordModel(byte[]? Hash, byte[]? Salt)
    {
        /// <summary>
        /// Obtém ou define o hash da senha, que é a representação criptografada da senha.
        /// </summary>
        public byte[]? Hash { get; set; } = Hash;

        /// <summary>
        /// Obtém ou define o salt da senha, que é um valor aleatório usado para tornar o hash mais seguro.
        /// </summary>
        public byte[]? Salt { get; set; } = Salt;
    }

}
