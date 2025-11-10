using EL;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
    public interface IPermisosController
    {
        #region CRUD_INTERFAZ
        Task<Permisos> Insert(Permisos entidad);
        Task<bool> Update(Permisos entidad);
        Task<bool> Anular(Permisos entidad);
        Task<bool> Existe(byte id);
        Task<Permisos> Registro(byte id);
        Task<List<Permisos>> Lista();
        #endregion

    }
    public class PermisosController : IPermisosController
    {
        readonly IDbContextFactory<dbContext> db;
        public PermisosController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
        #region CRUD_CLASE
        public async Task<Permisos> Insert(Permisos entidad)
        {
            using var dbContext = db.CreateDbContext();
            await dbContext.Permisos.AddAsync(entidad);
            await dbContext.SaveChangesAsync();
            return entidad;
        }
        public async Task<bool> Update(Permisos entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Permisos.FindAsync(entidad.IdPermiso);
            if (registro != null)
            {
                registro.Permiso = entidad.Permiso;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Anular(Permisos entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Permisos.FindAsync(entidad.IdPermiso);
            if (registro != null)
            {
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Existe(byte id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Permisos.AnyAsync(a => a.IdPermiso == id);
        }
        public async Task<Permisos> Registro(byte id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Permisos.FirstOrDefaultAsync(a => a.IdPermiso == id) ?? new Permisos();
        }
        public async Task<List<Permisos>> Lista()
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Permisos.ToListAsync();
        }
        #endregion

    }
}
