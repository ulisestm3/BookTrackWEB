using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
	[Table("Permisos")]
	public class Permisos
	{
		[Key]
		public byte IdPermiso { get; set; }
		[MaxLength(50)]
		[Required]
		public string Permiso { get; set; }
	}
}
