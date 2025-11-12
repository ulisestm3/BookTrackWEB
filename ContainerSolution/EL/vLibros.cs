using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("vLibros")]
    public class vLibros
    {
        [Key]
        public int LibroId { get; set; }

        [Required]
        [MaxLength(200)]
        public string TituloLibro { get; set; }
        [Required]
        public int AutorId { get; set; }
        [MaxLength(150)]
        public string? Autor { get; set; }
        [Required]
        public int CategoriaId { get; set; }
        [MaxLength(100)]
        public string? Categoria { get; set; }
        [MaxLength(200)]
        public string? ISBN { get; set; }

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