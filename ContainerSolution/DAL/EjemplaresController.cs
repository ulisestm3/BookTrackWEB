using EL;
using Microsoft.EntityFrameworkCore;
using System;
namespace DAL
{
    public interface IEjemplaresController
    {

        #region CRUD_INTERFAZ
        Task<Ejemplares> Insert(Ejemplares entidad);
        Task<bool> Update(Ejemplares entidad);
        Task<bool> Anular(Ejemplares entidad);
        Task<bool> Existe(int id);
        Task<Ejemplares> Registro(int id);
        Task<List<Ejemplares>> Lista(bool Activo = true);
        Task<List<vEjemplares>> vEjemplaresLista(bool Activo = true);
        Task<vEjemplares?> vEjemplaresPorId(int EjemplarId);

        #endregion
        Task<bool> ValidateCodigoBarra(int EjemplarId, string CodigoBarra);
    }
    public class EjemplaresController : IEjemplaresController
    {
        readonly IDbContextFactory<dbContext> db;
        public EjemplaresController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
        #region CRUD_CLASE
        public async Task<Ejemplares> Insert(Ejemplares entidad)
        {
            using var dbContext = db.CreateDbContext();
            entidad.Activo = true;
            entidad.FechaRegistro = DateTime.Now;
            await dbContext.Ejemplares.AddAsync(entidad);
            await dbContext.SaveChangesAsync();
            return entidad;
        }
        public async Task<bool> Update(Ejemplares entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Ejemplares.FindAsync(entidad.EjemplarId);
            if (registro != null)
            {
                registro.CodigoBarra = entidad.CodigoBarra;
                registro.EstadoLibroId = entidad.EstadoLibroId;
                registro.PublicacionLibroId = entidad.PublicacionLibroId;
                registro.FechaAdquisicion = entidad.FechaAdquisicion;
                registro.Activo = entidad.Activo;
                registro.UsuarioActualiza = entidad.UsuarioActualiza;
                registro.FechaActualizacion = entidad.FechaActualizacion;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Anular(Ejemplares entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Ejemplares.FindAsync(entidad.EjemplarId);
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
            return await dbContext.Ejemplares.AnyAsync(a => a.EjemplarId == id);
        }
        public async Task<Ejemplares> Registro(int id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Ejemplares.FirstOrDefaultAsync(a => a.EjemplarId == id) ?? new Ejemplares();
        }
        public async Task<List<Ejemplares>> Lista(bool Activo = true)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Ejemplares.Where(a => a.Activo == Activo).ToListAsync();
        }

        public async Task<List<vEjemplares>> vEjemplaresLista(bool Activo = true)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.vEjemplares.Where(a => a.Activo == Activo).ToListAsync();
        }

        public async Task<vEjemplares?> vEjemplaresPorId(int EjemplarId)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.vEjemplares.FirstOrDefaultAsync(l => l.EjemplarId == EjemplarId);
        }
        #endregion
        public async Task<bool> ValidateCodigoBarra(int EjemplarId, string CodigoBarra)
        {
            if (string.IsNullOrWhiteSpace(CodigoBarra))
                return false;

            using var DataBase = db.CreateDbContext();

            return await DataBase.Ejemplares
                .AnyAsync(a =>
                    a.EjemplarId != EjemplarId &&
                    a.CodigoBarra == CodigoBarra &&
                    a.Activo == true);
        }
    }
}
