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
        Task<Libros> Registro(int Libroid);
        Task<List<Libros>> Lista(bool Activo = true);
        Task<List<vLibros>> vLibrosLista();
        Task<vLibros?> vLibrosPorId(int libroId);

        #endregion
        Task<bool> ExisteTitulo(int LibroId, string TituloLibro, int AutorId);
        Task<bool> ExisteISBN(int LibroId, string ISBN);
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
                registro.ISBN = entidad.ISBN;
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
        public async Task<Libros> Registro(int Libroid)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Libros.FirstOrDefaultAsync(a => a.LibroId == Libroid) ?? new Libros();
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

        public async Task<vLibros?> vLibrosPorId(int libroId)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.vLibros.FirstOrDefaultAsync(l => l.LibroId == libroId);
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

        public async Task<bool> ExisteISBN(int LibroId, string ISBN)
        {
            if (string.IsNullOrWhiteSpace(ISBN)) return false;

            using var dbContext = db.CreateDbContext();
            return await dbContext.Libros.AnyAsync(a =>
                a.LibroId != LibroId &&
                a.ISBN == ISBN &&
                a.Activo);
        }

    }
}
