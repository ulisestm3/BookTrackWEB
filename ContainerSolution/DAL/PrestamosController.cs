using EL;
using Microsoft.EntityFrameworkCore;
using System;
namespace DAL
{
    public interface IPrestamosController
    {

        #region CRUD_INTERFAZ
        Task<Prestamos> Insert(Prestamos entidad);
        Task<bool> Update(Prestamos entidad);
        Task<bool> Anular(Prestamos entidad);
        Task<bool> Existe(int id);
        Task<Prestamos> Registro(int id);
        Task<List<Prestamos>> Lista(bool Activo = true);

        #endregion

        Task<List<vPrestamos>> vPrestamosLista();
        Task<List<vPrestamos>> vPrestamosActivosLista();
        Task<List<vPrestamos>> vHistorialPrestamosLista();
        Task<vPrestamos?> vPrestamosPorId(int PrestamoId);
    }
    public class PrestamosController : IPrestamosController
    {
        readonly IDbContextFactory<dbContext> db;
        public PrestamosController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
        #region CRUD_CLASE
        public async Task<Prestamos> Insert(Prestamos entidad)
        {
            using var dbContext = db.CreateDbContext();
            entidad.Activo = true;
            entidad.FechaRegistro = DateTime.Now;
            await dbContext.Prestamos.AddAsync(entidad);
            await dbContext.SaveChangesAsync();
            return entidad;
        }
        public async Task<bool> Update(Prestamos entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Prestamos.FindAsync(entidad.PrestamoId);
            if (registro != null)
            {
                registro.SocioId = entidad.SocioId;
                registro.EjemplarId = entidad.EjemplarId;
                registro.FechaSalida = entidad.FechaSalida;
                registro.FechaVencimiento = entidad.FechaVencimiento;
                registro.FechaDevolucion = entidad.FechaDevolucion;
                registro.Activo = entidad.Activo;
                registro.UsuarioActualiza = entidad.UsuarioActualiza;
                registro.FechaActualizacion = entidad.FechaActualizacion;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Anular(Prestamos entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Prestamos.FindAsync(entidad.PrestamoId);
            if (registro != null)
            {
                registro.Activo = false;
                registro.UsuarioActualiza = entidad.UsuarioActualiza;
                registro.FechaActualizacion = entidad.FechaActualizacion;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Existe(int id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Prestamos.AnyAsync(a => a.PrestamoId == id);
        }
        public async Task<Prestamos> Registro(int id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Prestamos.FirstOrDefaultAsync(a => a.PrestamoId == id) ?? new Prestamos();
        }
        public async Task<List<Prestamos>> Lista(bool Activo = true)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Prestamos.Where(a => a.Activo == Activo).ToListAsync();
        }

        public async Task<List<vPrestamos>> vPrestamosLista()
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.vPrestamos.ToListAsync();
        }

        public async Task<List<vPrestamos>> vPrestamosActivosLista()
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.vPrestamos.Where(a => a.FechaDevolucion == null).ToListAsync();
        }

        public async Task<List<vPrestamos>> vHistorialPrestamosLista()
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.vPrestamos.Where(a => a.FechaDevolucion != null).ToListAsync();
        }

        public async Task<vPrestamos?> vPrestamosPorId(int PrestamoId)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.vPrestamos.FirstOrDefaultAsync(l => l.PrestamoId == PrestamoId);
        }
        #endregion
    }
}
