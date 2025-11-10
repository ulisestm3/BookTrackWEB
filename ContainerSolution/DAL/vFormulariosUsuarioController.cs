using EL;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
	public interface IvFormulariosUsuarioController
	{
#region CRUD_INTERFAZ
		Task<bool> Existe(int id);
		Task<List<vFormulariosUsuario>> Lista();
        #endregion
        Task<List<vFormulariosUsuario>> Lista(Usuarios User);

    }
    public class vFormulariosUsuarioController : IvFormulariosUsuarioController
	{
		readonly IDbContextFactory<dbContext> db;
		public vFormulariosUsuarioController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
		#region CRUD_CLASE
		public async Task<bool> Existe(int id)
		{
			using var dbContext = db.CreateDbContext();
			return await dbContext.vFormulariosUsuario.AnyAsync(a => a.IdUsuarioFormulario == id);
		}
		public async Task<List<vFormulariosUsuario>> Lista()
		{
			using var dbContext = db.CreateDbContext();
			return await dbContext.vFormulariosUsuario.ToListAsync();
		}
        #endregion

        public async Task<List<vFormulariosUsuario>> Lista(Usuarios User)
		{
			using var dbContext = db.CreateDbContext();
			if (User.IdUsuario > 0)
				return await dbContext.vFormulariosUsuario.Where(a => a.IdUsuario == User.IdUsuario).ToListAsync();
            return await dbContext.vFormulariosUsuario.ToListAsync();
        }
    }
}
