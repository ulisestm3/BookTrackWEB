using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
	[Table("Usuarios")]
	public class Usuarios
	{
		[Key]
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
		public byte[] Password { get; set; }
		[Required]
		public bool Bloqueado { get; set; }
		[Required]
		public byte Intentos { get; set; }
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
