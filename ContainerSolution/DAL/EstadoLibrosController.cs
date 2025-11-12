using EL;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
    public interface IEstadoLibrosController
    {

        #region CRUD_INTERFAZ
        Task<EstadoLibros> Insert(EstadoLibros entidad);
        Task<bool> Update(EstadoLibros entidad);
        Task<bool> Anular(EstadoLibros entidad);
        Task<bool> Existe(int id);
        Task<EstadoLibros> Registro(int id);
        Task<List<EstadoLibros>> Lista(bool Activo = true);
        #endregion
        Task<bool> ValidateEstadoLibro(int EstadoLibroId, string EstadoLibro);
    }
    public class EstadoLibrosController : IEstadoLibrosController
    {
        readonly IDbContextFactory<dbContext> db;
        public EstadoLibrosController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
        #region CRUD_CLASE
        public async Task<EstadoLibros> Insert(EstadoLibros entidad)
        {
            using var dbContext = db.CreateDbContext();
            entidad.Activo = true;
            entidad.FechaRegistro = DateTime.Now;
            await dbContext.EstadoLibros.AddAsync(entidad);
            await dbContext.SaveChangesAsync();
            return entidad;
        }
        public async Task<bool> Update(EstadoLibros entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.EstadoLibros.FindAsync(entidad.EstadoLibroId);
            if (registro != null)
            {
                registro.EstadoLibro = entidad.EstadoLibro;
                registro.Descripcion = entidad.Descripcion;
                registro.Activo = entidad.Activo;
                registro.UsuarioActualiza = entidad.UsuarioActualiza;
                registro.FechaActualizacion = entidad.FechaActualizacion;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Anular(EstadoLibros entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.EstadoLibros.FindAsync(entidad.EstadoLibroId);
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
            return await dbContext.EstadoLibros.AnyAsync(a => a.EstadoLibroId == id);
        }
        public async Task<EstadoLibros> Registro(int id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.EstadoLibros.FirstOrDefaultAsync(a => a.EstadoLibroId == id) ?? new EstadoLibros();
        }
        public async Task<List<EstadoLibros>> Lista(bool Activo = true)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.EstadoLibros.Where(a => a.Activo == Activo).ToListAsync();
        }
        #endregion
        public async Task<bool> ValidateEstadoLibro(int EstadoLibroId, string EstadoLibro)
        {
            using var DataBase = db.CreateDbContext();
            return await DataBase.EstadoLibros.AnyAsync(a => a.EstadoLibroId != EstadoLibroId && a.EstadoLibro.Equals(EstadoLibro) && a.Activo.Equals(true));
        }
    }
}
