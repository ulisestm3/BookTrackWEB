using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Keyless]
    [Table("vPublicacionLibros")]
    public class vPublicacionLibros
    {
        public int PublicacionLibroId { get; set; }
        public int LibroId { get; set; }
        [MaxLength(200)]
        public string? TituloLibro { get; set; }
        public int AutorId { get; set; }

        [MaxLength(150)]
        public string? Autor { get; set; }
        public int CategoriaId { get; set; }

        [MaxLength(100)]
        public string? Categoria { get; set; }
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