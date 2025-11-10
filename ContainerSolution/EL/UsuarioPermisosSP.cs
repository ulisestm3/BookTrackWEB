using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL
{
    public class UsuarioPermisosSP
    {
        [Key]
        [Required]
        public short IdPermisoFormulario { get; set; }
        [Required]
        public byte IdPermiso { get; set; }
        [MaxLength(50)]
        [Required]
        public string Permiso { get; set; }
        [Required]
        public byte IdFormulario { get; set; }
        [MaxLength(50)]
        [Required]
        public string Formulario { get; set; }
        [Required]
        public short IdUsuario { get; set; }
        [MaxLength(20)]
        [Required]
        public string Login { get; set; }
        [Required]
        public int IdUsuarioPermiso { get; set; }
        public bool TienePermiso { get; set; }
    }
}
