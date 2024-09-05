using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiProfissional.Domain.Entities
{
    [Table("enderecos")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public partial class Enderecos
    {
        public Enderecos()
        {
            Funcionarios = new HashSet<Funcionarios>();
            Telefones = new HashSet<Telefones>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Complemento { get; set; }

        public int? Numero { get; set; }

        [Required]
        [StringLength(58)]
        public string Cidade { get; set; }

        [Required]
        [StringLength(2)]
        public string Uf { get; set; }

        public int Cep { get; set; }

        [InverseProperty("IdEnderecosNavigation")]
        public virtual ICollection<Funcionarios> Funcionarios { get; set; }
        [InverseProperty("IdEnderecoNavigation")]
        public virtual ICollection<Telefones> Telefones { get; set; }
    }
}