using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProfissional.Domain.Entities.Token
{
    public class RefreshTokens
    {
        public int Id { get; set; } // Chave Primária
        public int IdUsuario { get; set; } // Chave Estrangeira que referencia o usuário
        public string Token { get; set; } // O Refresh Token
        public string JwtId { get; set; } // Identificador do JWT (Jti)
        public bool IsRevoked { get; set; } // Indica se o token foi revogado
        public DateTime ExpiresAt { get; set; } // Data de expiração do token
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Data de criação do token
        public DateTime? RevokedAt { get; set; } // Data de revogação, se aplicável

        // Relacionamento com o Usuario (navegação)
        public virtual Usuarios UsuarioNavigation { get; set; }
    }
}
