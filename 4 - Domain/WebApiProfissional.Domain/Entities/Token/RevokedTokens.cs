using System;

namespace WebApiProfissional.Domain.Entities.Token
{
    public class RevokedTokens
    {
        public int Id { get; set; } // Chave Primária
        public string Token { get; set; } // O Token Revogado
        public int IdUsuario { get; set; } // Chave Estrangeira que referencia o usuário
        public DateTime RevokedAt { get; set; } = DateTime.UtcNow; // Data de revogação do token

        // Relacionamento com o usuário (navegação)
        public virtual Usuarios UsuarioNavigation { get; set; }
    }
}
