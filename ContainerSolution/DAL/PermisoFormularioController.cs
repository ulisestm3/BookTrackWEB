using EL;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
    public interface IPermisoFormularioController
    {
        #region CRUD_INTERFAZ
        Task<PermisoFormulario> Insert(PermisoFormulario entidad);
        Task<bool> Update(PermisoFormulario entidad);
        Task<bool> Anular(PermisoFormulario entidad);
        Task<bool> Existe(short id);
        Task<PermisoFormulario> Registro(short id);
        Task<List<PermisoFormulario>> Lista(bool Activo = true);

        #endregion
        Task<List<PermisoFormulario>> Lista(byte? IdFormulario, bool Activo = true);

    }
    public class PermisoFormularioController : IPermisoFormularioController
    {
        readonly IDbContextFactory<dbContext> db;
        public PermisoFormularioController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
        #region CRUD_CLASE
        public async Task<PermisoFormulario> Insert(PermisoFormulario entidad)
        {
            using var dbContext = db.CreateDbContext();
            entidad.Activo = true;
            entidad.FechaRegistro = DateTime.Now;
            await dbContext.PermisoFormulario.AddAsync(entidad);
            await dbContext.SaveChangesAsync();
            return entidad;
        }
        public async Task<bool> Update(PermisoFormulario entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.PermisoFormulario.FindAsync(entidad.IdPermisoFormulario);
            if (registro != null)
            {
                registro.IdPermiso = entidad.IdPermiso;
                registro.IdFormulario = entidad.IdFormulario;
                registro.UsuarioActualiza = entidad.UsuarioActualiza;
                registro.FechaActualizacion = entidad.FechaActualizacion;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Anular(PermisoFormulario entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.PermisoFormulario.FindAsync(entidad.IdPermisoFormulario);
            if (registro != null)
            {
                registro.Activo = entidad.Activo;
                registro.UsuarioActualiza = entidad.UsuarioActualiza;
                registro.FechaActualizacion = entidad.FechaActualizacion;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Existe(short id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.PermisoFormulario.AnyAsync(a => a.IdPermisoFormulario == id);
        }
        public async Task<PermisoFormulario> Registro(short id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.PermisoFormulario.FirstOrDefaultAsync(a => a.IdPermisoFormulario == id) ?? new PermisoFormulario();
        }
        public async Task<List<PermisoFormulario>> Lista(bool Activo = true)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.PermisoFormulario.Where(a => a.Activo == Activo).ToListAsync();
        }
        #endregion
        public async Task<List<PermisoFormulario>> Lista(byte? IdFormulario, bool Activo = true)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.PermisoFormulario.Where(a => a.IdFormulario == IdFormulario && a.Activo == Activo).ToListAsync();
        }

    }
}
