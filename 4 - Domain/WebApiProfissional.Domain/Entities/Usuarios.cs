using WebApiProfissional.Domain.Entities.Token;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace WebApiProfissional.Domain.Entities
{
    [Table("usuarios")]
    [Index("IdFuncionario", Name = "Fk_Usuarios_Funcionarios")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public partial class Usuarios
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Login { get; set; }

        [Required]
        [Column("passwordHash")]
        public byte[] PasswordHash { get; set; }

        [Required]
        [Column("passwordSalt")]
        public byte[] PasswordSalt { get; set; }

        [Required]
        [Column("IsAdmin")]
        public bool IsAdmin { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime DtCadastro { get; set; }
        
        [Column(TypeName = "datetime")]
        public DateTime DtModificacao { get; set; }

        public int IdFuncionario { get; set; }

        [ForeignKey("IdFuncionario")]
        [InverseProperty("Usuarios")]
        public virtual Funcionarios IdFuncionarioNavigation { get; set; }

        public virtual ICollection<RefreshTokens> RefreshTokens { get; set; }

        public virtual ICollection<RevokedTokens> RevokedTokens { get; set; }
    }
}