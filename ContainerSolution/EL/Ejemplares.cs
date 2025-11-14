using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("Ejemplares")]
    public class Ejemplares
    {
        [Key]
        public int EjemplarId { get; set; }

        [MaxLength(50)]
        public string? CodigoBarra { get; set; }
        [Required]
        public int EstadoLibroId { get; set; }
        [Required]
        public int PublicacionLibroId { get; set; }
        public DateTime FechaAdquisicion { get; set; }

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