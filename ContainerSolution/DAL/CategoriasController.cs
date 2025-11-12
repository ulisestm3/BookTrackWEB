using EL;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
    public interface ICategoriasController
    {

        #region CRUD_INTERFAZ
        Task<Categorias> Insert(Categorias entidad);
        Task<bool> Update(Categorias entidad);
        Task<bool> Anular(Categorias entidad);
        Task<bool> Existe(int id);
        Task<Categorias> Registro(int id);
        Task<List<Categorias>> Lista(bool Activo = true);
        #endregion
        Task<bool> ValidateCategoria(int CategoriaId, string Categoria);
    }
    public class CategoriasController : ICategoriasController
    {
        readonly IDbContextFactory<dbContext> db;
        public CategoriasController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
        #region CRUD_CLASE
        public async Task<Categorias> Insert(Categorias entidad)
        {
            using var dbContext = db.CreateDbContext();
            entidad.Activo = true;
            entidad.FechaRegistro = DateTime.Now;
            await dbContext.Categorias.AddAsync(entidad);
            await dbContext.SaveChangesAsync();
            return entidad;
        }
        public async Task<bool> Update(Categorias entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Categorias.FindAsync(entidad.CategoriaId);
            if (registro != null)
            {
                registro.Categoria = entidad.Categoria;
                registro.Descripcion = entidad.Descripcion;
                registro.Activo = entidad.Activo;
                registro.UsuarioActualiza = entidad.UsuarioActualiza;
                registro.FechaActualizacion = entidad.FechaActualizacion;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Anular(Categorias entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Categorias.FindAsync(entidad.CategoriaId);
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
            return await dbContext.Categorias.AnyAsync(a => a.CategoriaId == id);
        }
        public async Task<Categorias> Registro(int id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Categorias.FirstOrDefaultAsync(a => a.CategoriaId == id) ?? new Categorias();
        }
        public async Task<List<Categorias>> Lista(bool Activo = true)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Categorias.Where(a => a.Activo == Activo).ToListAsync();
        }
        #endregion
        public async Task<bool> ValidateCategoria(int CategoriaId, string Categoria)
        {
            using var DataBase = db.CreateDbContext();
            return await DataBase.Categorias.AnyAsync(a => a.CategoriaId != CategoriaId && a.Categoria.Equals(Categoria) && a.Activo.Equals(true));
        }
    }
}
