using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
	[Table("vUsuarios")]
	public class vUsuarios
	{
		[Key]
		[Required]
		public short IdUsuario { get; set; }
		[MaxLength(200)]
		[Required]
		public string NombreCompleto { get; set; }
		[MaxLength(100)]
		[Required]
		public string Correo { get; set; }
		[MaxLength(15)]
		[Required]
		public string Celular { get; set; }
		[MaxLength(20)]
		[Required]
		public string Login { get; set; }
		[Required]
		public bool Bloqueado { get; set; }
		[MaxLength(2)]
		public string Bloq { get; set; }
		[Required]
		public byte Intentos { get; set; }
	}
}
