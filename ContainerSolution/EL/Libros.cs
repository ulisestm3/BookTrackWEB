using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("Libros")]
    public class Libros
    {
        [Key]
        public int LibroId { get; set; }

        [Required]
        [MaxLength(200)]
        public string TituloLibro { get; set; }
        [Required]
        public int AutorId { get; set; }
        [Required]
        public int CategoriaId { get; set; }

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