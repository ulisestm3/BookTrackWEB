using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL
{
    public class UsuarioFormulariosSP
    {
        [Key]
        public long? Id { get; set; }
        [Required]
        public int IdUsuarioFormulario { get; set; }
        [Required]
        public byte IdFormulario { get; set; }
        [MaxLength(50)]
        [Required]
        public string Formulario { get; set; }
        [Required]
        public short IdUsuario { get; set; }
        public bool? TienePermiso { get; set; }
    }
}
