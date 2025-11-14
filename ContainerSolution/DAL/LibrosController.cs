using EL;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
    public interface ILibrosController
    {

        #region CRUD_INTERFAZ
        Task<Libros> Insert(Libros entidad);
        Task<bool> Update(Libros entidad);
        Task<bool> Anular(Libros entidad);
        Task<bool> Existe(int id);
        Task<Libros> Registro(int LibroId);
        Task<List<Libros>> Lista(bool Activo = true);
        Task<List<vLibros>> vLibrosLista();
        Task<vLibros?> vLibrosPorId(int LibroId);

        #endregion
        Task<bool> ExisteTitulo(int LibroId, string TituloLibro, int AutorId);
    }
    public class LibrosController : ILibrosController
    {
        readonly IDbContextFactory<dbContext> db;
        public LibrosController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
        #region CRUD_CLASE
        public async Task<Libros> Insert(Libros entidad)
        {
            using var dbContext = db.CreateDbContext();
            entidad.Activo = true;
            entidad.FechaRegistro = DateTime.Now;
            await dbContext.Libros.AddAsync(entidad);
            await dbContext.SaveChangesAsync();
            return entidad;
        }
        public async Task<bool> Update(Libros entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Libros.FindAsync(entidad.LibroId);
            if (registro != null)
            {
                registro.TituloLibro = entidad.TituloLibro;
                registro.AutorId = entidad.AutorId;
                registro.CategoriaId = entidad.CategoriaId;
                registro.Activo = entidad.Activo;
                registro.UsuarioActualiza = entidad.UsuarioActualiza;
                registro.FechaActualizacion = entidad.FechaActualizacion;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Anular(Libros entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Libros.FindAsync(entidad.LibroId);
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
            return await dbContext.Libros.AnyAsync(a => a.LibroId == id);
        }
        public async Task<Libros> Registro(int LibroId)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Libros.FirstOrDefaultAsync(a => a.LibroId == LibroId) ?? new Libros();
        }
        public async Task<List<Libros>> Lista(bool Activo = true)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Libros.Where(a => a.Activo == Activo).ToListAsync();
        }

        public async Task<List<vLibros>> vLibrosLista()
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.vLibros.ToListAsync();
        }

        public async Task<vLibros?> vLibrosPorId(int LibroId)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.vLibros.FirstOrDefaultAsync(l => l.LibroId == LibroId);
        }

        #endregion
        public async Task<bool> ExisteTitulo(int LibroId, string TituloLibro, int AutorId)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Libros.AnyAsync(a =>
                a.LibroId != LibroId &&
                a.TituloLibro == TituloLibro &&
                a.AutorId == AutorId &&
                a.Activo);
        }

    }
}
