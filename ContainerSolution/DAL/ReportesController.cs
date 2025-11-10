using EL;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
    public interface IReportesController
    {
        #region CRUD_INTERFAZ
        Task<Reportes> Insert(Reportes entidad);
        Task<bool> Update(Reportes entidad);
        Task<bool> Anular(Reportes entidad);
        Task<bool> Existe(byte id);
        Task<Reportes> Registro(byte id);
        Task<List<Reportes>> Lista();
        #endregion

    }
    public class ReportesController : IReportesController
    {
        readonly IDbContextFactory<dbContext> db;
        public ReportesController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
        #region CRUD_CLASE
        public async Task<Reportes> Insert(Reportes entidad)
        {
            using var dbContext = db.CreateDbContext();
            await dbContext.Reportes.AddAsync(entidad);
            await dbContext.SaveChangesAsync();
            return entidad;
        }
        public async Task<bool> Update(Reportes entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Reportes.FindAsync(entidad.IdReporte);
            if (registro != null)
            {
                registro.Reporte = entidad.Reporte;
                registro.Observaciones = entidad.Observaciones;
                registro.ReportName = entidad.ReportName;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Anular(Reportes entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Reportes.FindAsync(entidad.IdReporte);
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
            return await dbContext.Reportes.AnyAsync(a => a.IdReporte == id);
        }
        public async Task<Reportes> Registro(byte id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Reportes.FirstOrDefaultAsync(a => a.IdReporte == id) ?? new Reportes();
        }
        public async Task<List<Reportes>> Lista()
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Reportes.ToListAsync();
        }
        #endregion

    }
}
