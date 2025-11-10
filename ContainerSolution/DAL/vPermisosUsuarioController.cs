using EL;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
	public interface IvPermisosUsuarioController
	{
#region CRUD_INTERFAZ
		Task<bool> Existe(short id);
		Task<List<vPermisosUsuario>> Lista();
#endregion

	}
	public class vPermisosUsuarioController : IvPermisosUsuarioController
	{
		readonly IDbContextFactory<dbContext> db;
		public vPermisosUsuarioController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
		#region CRUD_CLASE
		public async Task<bool> Existe(short id)
		{
			using var dbContext = db.CreateDbContext();
			return await dbContext.vPermisosUsuario.AnyAsync(a => a.IdPermisoFormulario == id);
		}
		public async Task<List<vPermisosUsuario>> Lista()
		{
			using var dbContext = db.CreateDbContext();
			return await dbContext.vPermisosUsuario.ToListAsync();
		}
		#endregion

	}
}
