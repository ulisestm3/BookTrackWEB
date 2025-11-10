using EL;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
    public interface IvUsuarioFormulariosController
    {
        #region CRUD_INTERFAZ
        Task<bool> Existe(long id);
        Task<vUsuarioFormularios> Registro(long id);
        Task<List<vUsuarioFormularios>> Lista();
        #endregion
        Task<List<vUsuarioFormularios>> Lista(short? User);

    }
    public class vUsuarioFormulariosController : IvUsuarioFormulariosController
    {
        readonly IDbContextFactory<dbContext> db;
        public vUsuarioFormulariosController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
        #region CRUD_CLASE
        public async Task<bool> Existe(long id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.vUsuarioFormularios.AnyAsync(a => a.Id == id);
        }
        public async Task<vUsuarioFormularios> Registro(long id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.vUsuarioFormularios.FirstOrDefaultAsync(a => a.Id == id) ?? new vUsuarioFormularios();
        }
        public async Task<List<vUsuarioFormularios>> Lista()
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.vUsuarioFormularios.ToListAsync();
        }
        #endregion
        public async Task<List<vUsuarioFormularios>> Lista(short? User)
        {
            using var dbContext = db.CreateDbContext();
            if (User != null)
            {
                return await dbContext.vUsuarioFormularios.Where(a => a.IdUsuario == User ||a.IdUsuario ==0).DistinctBy(a=>a.IdFormulario).ToListAsync();
            }
            return [];
        }
    }
}
