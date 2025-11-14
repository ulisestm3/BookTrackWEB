using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("PublicacionLibros")]
    public class PublicacionLibros
    {
        [Key]
        public int PublicacionLibroId { get; set; }
        public int LibroId { get; set; }
        [Required]
        [MaxLength(20)]
        public string? ISBN { get; set; }
        public int? AnioPublicacion { get; set; }

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