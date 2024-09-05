using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace WebApiProfissional.Domain.Entities
{
    [Table("telefones")]
    [Index("IdEndereco", Name = "Fk_Telefones_Enderecos")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public partial class Telefones
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(11)]
        public string Numero { get; set; }

        public int Tipo { get; set; }

        public int IdEndereco { get; set; }

        [ForeignKey("IdEndereco")]
        [InverseProperty("Telefones")]
        public virtual Enderecos IdEnderecoNavigation { get; set; }
    }
}