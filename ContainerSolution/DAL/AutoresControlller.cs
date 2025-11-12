using EL;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
    public interface IAutoresController
    {

        #region CRUD_INTERFAZ
        Task<Autores> Insert(Autores entidad);
        Task<bool> Update(Autores entidad);
        Task<bool> Anular(Autores entidad);
        Task<bool> Existe(int id);
        Task<Autores> Registro(int id);
        Task<List<Autores>> Lista(bool Activo = true);
        #endregion
        Task<bool> ValidateAutor(int AutorId, string Autor);
    }
    public class AutoresController : IAutoresController
    {
        readonly IDbContextFactory<dbContext> db;
        public AutoresController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
        #region CRUD_CLASE
        public async Task<Autores> Insert(Autores entidad)
        {
            using var dbContext = db.CreateDbContext();
            entidad.Activo = true;
            entidad.FechaRegistro = DateTime.Now;
            await dbContext.Autores.AddAsync(entidad);
            await dbContext.SaveChangesAsync();
            return entidad;
        }
        public async Task<bool> Update(Autores entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Autores.FindAsync(entidad.AutorId);
            if (registro != null)
            {
                registro.Autor = entidad.Autor;
                registro.Activo = entidad.Activo;
                registro.UsuarioActualiza = entidad.UsuarioActualiza;
                registro.FechaActualizacion = entidad.FechaActualizacion;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Anular(Autores entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Autores.FindAsync(entidad.AutorId);
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
            return await dbContext.Autores.AnyAsync(a => a.AutorId == id);
        }
        public async Task<Autores> Registro(int id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Autores.FirstOrDefaultAsync(a => a.AutorId == id) ?? new Autores();
        }
        public async Task<List<Autores>> Lista(bool Activo = true)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Autores.Where(a => a.Activo == Activo).ToListAsync();
        }
        #endregion
        public async Task<bool> ValidateAutor(int AutorId, string Autor)
        {
            using var DataBase = db.CreateDbContext();
            return await DataBase.Autores.AnyAsync(a => a.AutorId != AutorId && a.Autor.Equals(Autor) && a.Activo.Equals(true));
        }
    }
}
