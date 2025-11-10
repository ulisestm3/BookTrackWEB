using EL;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
    public interface IUsuarioReportesController
    {
        #region CRUD_INTERFAZ
        Task<UsuarioReportes> Insert(UsuarioReportes entidad);
        Task<bool> Update(UsuarioReportes entidad);
        Task<bool> Anular(UsuarioReportes entidad);
        Task<bool> Existe(byte id);
        Task<UsuarioReportes> Registro(byte id);
        Task<List<UsuarioReportes>> Lista(bool Activo = true);
        #endregion

    }
    public class UsuarioReportesController : IUsuarioReportesController
    {
        readonly IDbContextFactory<dbContext> db;
        public UsuarioReportesController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
        #region CRUD_CLASE
        public async Task<UsuarioReportes> Insert(UsuarioReportes entidad)
        {
            using var dbContext = db.CreateDbContext();
            entidad.Activo = true;
            entidad.FechaRegistro = DateTime.Now;
            await dbContext.UsuarioReportes.AddAsync(entidad);
            await dbContext.SaveChangesAsync();
            return entidad;
        }
        public async Task<bool> Update(UsuarioReportes entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.UsuarioReportes.FindAsync(entidad.IdUsuarioReporte);
            if (registro != null)
            {
                registro.IdUsuario = entidad.IdUsuario;
                registro.IdReporte = entidad.IdReporte;
                registro.UsuarioActualiza = entidad.UsuarioActualiza;
                registro.FechaActualizacion = entidad.FechaActualizacion;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Anular(UsuarioReportes entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.UsuarioReportes.FindAsync(entidad.IdUsuarioReporte);
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
        public async Task<bool> Existe(byte id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.UsuarioReportes.AnyAsync(a => a.IdUsuarioReporte == id);
        }
        public async Task<UsuarioReportes> Registro(byte id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.UsuarioReportes.FirstOrDefaultAsync(a => a.IdUsuarioReporte == id) ?? new UsuarioReportes();
        }
        public async Task<List<UsuarioReportes>> Lista(bool Activo = true)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.UsuarioReportes.Where(a => a.Activo == Activo).ToListAsync();
        }
        #endregion

    }
}
