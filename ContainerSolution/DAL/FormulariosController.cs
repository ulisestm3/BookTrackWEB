using EL;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
    public interface IFormulariosController
    {
        #region CRUD_INTERFAZ
        Task<Formularios> Insert(Formularios entidad);
        Task<bool> Update(Formularios entidad);
        Task<bool> Anular(Formularios entidad);
        Task<bool> Existe(byte id);
        Task<Formularios> Registro(byte id);
        Task<List<Formularios>> Lista();
        #endregion

    }
    public class FormulariosController : IFormulariosController
    {
        readonly IDbContextFactory<dbContext> db;
        public FormulariosController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
        #region CRUD_CLASE
        public async Task<Formularios> Insert(Formularios entidad)
        {
            using var dbContext = db.CreateDbContext();
            await dbContext.Formularios.AddAsync(entidad);
            await dbContext.SaveChangesAsync();
            return entidad;
        }
        public async Task<bool> Update(Formularios entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Formularios.FindAsync(entidad.IdFormulario);
            if (registro != null)
            {
                registro.Formulario = entidad.Formulario;
                registro.Observaciones = entidad.Observaciones;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Anular(Formularios entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Formularios.FindAsync(entidad.IdFormulario);
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
            return await dbContext.Formularios.AnyAsync(a => a.IdFormulario == id);
        }
        public async Task<Formularios> Registro(byte id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Formularios.FirstOrDefaultAsync(a => a.IdFormulario == id) ?? new Formularios();
        }
        public async Task<List<Formularios>> Lista()
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Formularios.ToListAsync();
        }
        #endregion

    }
}
