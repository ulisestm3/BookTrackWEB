using EL;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
    public interface IPublicacionLibrosController
    {

        #region CRUD_INTERFAZ
        Task<PublicacionLibros> Insert(PublicacionLibros entidad);
        Task<bool> Update(PublicacionLibros entidad);
        Task<bool> Anular(PublicacionLibros entidad);
        Task<bool> Existe(int id);
        Task<PublicacionLibros> Registro(int PublicacionLibroId);
        Task<List<PublicacionLibros>> Lista(bool Activo = true);
        Task<List<vPublicacionLibros>> vPublicacionLibrosLista();
        Task<vPublicacionLibros?> vPublicacionLibrosPorId(int PublicacionLibroId);

        #endregion
        Task<bool> ExisteISBN(int PublicacionLibroId, string ISBN);
    }
    public class PublicacionLibrosController : IPublicacionLibrosController
    {
        readonly IDbContextFactory<dbContext> db;
        public PublicacionLibrosController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
        #region CRUD_CLASE
        public async Task<PublicacionLibros> Insert(PublicacionLibros entidad)
        {
            using var dbContext = db.CreateDbContext();
            entidad.Activo = true;
            entidad.FechaRegistro = DateTime.Now;
            await dbContext.PublicacionLibros.AddAsync(entidad);
            await dbContext.SaveChangesAsync();
            return entidad;
        }
        public async Task<bool> Update(PublicacionLibros entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.PublicacionLibros.FindAsync(entidad.PublicacionLibroId);
            if (registro != null)
            {
                registro.LibroId = entidad.LibroId;
                registro.ISBN = entidad.ISBN;
                registro.AnioPublicacion = entidad.AnioPublicacion;
                registro.Activo = entidad.Activo;
                registro.UsuarioActualiza = entidad.UsuarioActualiza;
                registro.FechaActualizacion = entidad.FechaActualizacion;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Anular(PublicacionLibros entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.PublicacionLibros.FindAsync(entidad.PublicacionLibroId);
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
            return await dbContext.PublicacionLibros.AnyAsync(a => a.PublicacionLibroId == id);
        }
        public async Task<PublicacionLibros> Registro(int PublicacionLibroId)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.PublicacionLibros.FirstOrDefaultAsync(a => a.PublicacionLibroId == PublicacionLibroId) ?? new PublicacionLibros();
        }
        public async Task<List<PublicacionLibros>> Lista(bool Activo = true)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.PublicacionLibros.Where(a => a.Activo == Activo).ToListAsync();
        }

        public async Task<List<vPublicacionLibros>> vPublicacionLibrosLista()
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.vPublicacionLibros.ToListAsync();
        }

        public async Task<vPublicacionLibros?> vPublicacionLibrosPorId(int PublicacionLibroId)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.vPublicacionLibros.FirstOrDefaultAsync(l => l.PublicacionLibroId == PublicacionLibroId);
        }

        #endregion
        public async Task<bool> ExisteISBN(int PublicacionLibroId, string ISBN)
        {
            if (string.IsNullOrWhiteSpace(ISBN)) return false;

            using var dbContext = db.CreateDbContext();
            return await dbContext.PublicacionLibros.AnyAsync(a =>
                a.PublicacionLibroId != PublicacionLibroId &&
                a.ISBN == ISBN &&
                a.Activo);
        }

    }
}
