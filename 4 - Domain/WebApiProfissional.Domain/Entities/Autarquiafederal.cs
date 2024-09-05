using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiProfissional.Domain.Entities
{
    [Table("autarquiafederal")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public partial class Autarquiafederal
    {
        public Autarquiafederal()
        {
            Funcionarios = new HashSet<Funcionarios>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string CategoriaClasse { get; set; }

        [InverseProperty("IdAutarquiaFederalNavigation")]
        public virtual ICollection<Funcionarios> Funcionarios { get; set; }
    }
}