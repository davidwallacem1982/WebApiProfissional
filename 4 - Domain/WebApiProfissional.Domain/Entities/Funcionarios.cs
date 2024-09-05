using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace WebApiProfissional.Domain.Entities
{

    [Table("funcionarios")]
    [Index("IdAutarquiaFederal", Name = "Fk_Funcionarios_AutarquiaFederal")]
    [Index("IdEnderecos", Name = "Fk_Funcionarios_Enderecos")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public partial class Funcionarios
    {
        public Funcionarios()
        {
            Usuarios = new HashSet<Usuarios>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string Sobrenome { get; set; }

        public long Cpf { get; set; }

        public DateOnly DtNascimento { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime DtCadastro { get; set; }

        public int IdAutarquiaFederal { get; set; }

        public int IdEnderecos { get; set; }

        // Propriedade calculada para o nome completo
        public string NomeCompleto
        {
            get { return $"{Nome} {Sobrenome}"; }
        }

        [ForeignKey("IdAutarquiaFederal")]
        [InverseProperty("Funcionarios")]
        public virtual Autarquiafederal IdAutarquiaFederalNavigation { get; set; }
        [ForeignKey("IdEnderecos")]
        [InverseProperty("Funcionarios")]
        public virtual Enderecos IdEnderecosNavigation { get; set; }
        [InverseProperty("IdFuncionarioNavigation")]
        public virtual ICollection<Usuarios> Usuarios { get; set; }
    }
}