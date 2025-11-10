using EL;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
	public interface IvUsuariosController
	{
#region CRUD_INTERFAZ
		Task<bool> Existe(short id);
		Task<List<vUsuarios>> Lista();
#endregion

	}
	public class vUsuariosController : IvUsuariosController
	{
		readonly IDbContextFactory<dbContext> db;
		public vUsuariosController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
		#region CRUD_CLASE
		public async Task<bool> Existe(short id)
		{
			using var dbContext = db.CreateDbContext();
			return await dbContext.vUsuarios.AnyAsync(a => a.IdUsuario == id);
		}
		public async Task<List<vUsuarios>> Lista()
		{
			using var dbContext = db.CreateDbContext();
			return await dbContext.vUsuarios.ToListAsync();
		}
		#endregion

	}
}
