using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("Autores")]
    public class Autores
    {
        [Key]
        public int AutorId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Autor { get; set; }

        [Required]
        public bool Activo { get; set; }

        [Required]
        public int UsuarioRegistra { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; }

        public int? UsuarioActualiza { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}