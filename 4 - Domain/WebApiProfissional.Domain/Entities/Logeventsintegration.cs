using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProfissional.Domain.Entities
{
    [Table("logeventsintegration")]
    public partial class Logeventsintegration
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [StringLength(100)]
        public string Timestamp { get; set; }

        [StringLength(15)]
        public string Level { get; set; }

        [Column(TypeName = "text")]
        public string Template { get; set; }

        [Column(TypeName = "text")]
        public string Message { get; set; }

        [Column(TypeName = "text")]
        public string Exception { get; set; }

        [Column(TypeName = "text")]
        public string Properties { get; set; }

        [Column("_ts", TypeName = "timestamp")]
        public DateTime? Ts { get; set; }
    }
}