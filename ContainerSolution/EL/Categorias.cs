using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("Categorias")]
    public class Categorias
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Categoria { get; set; }
        [MaxLength(255)]
        public string? Descripcion { get; set; }

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