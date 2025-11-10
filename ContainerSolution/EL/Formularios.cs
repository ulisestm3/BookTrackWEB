using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
	[Table("Formularios")]
	public class Formularios
	{
		[Key]
		public byte IdFormulario { get; set; }
		[MaxLength(50)]
		[Required]
		public string Formulario { get; set; }
		[MaxLength(200)]
		[Required]
		public string Observaciones { get; set; }
	}
}
