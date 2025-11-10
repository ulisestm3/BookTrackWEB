using EL;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
    public interface IParametrosController
    {
        #region CRUD_INTERFAZ
        Task<Parametros> Insert(Parametros entidad);
        Task<bool> Update(Parametros entidad);
        Task<bool> Anular(Parametros entidad);
        Task<bool> Existe(byte id);
        Task<Parametros> Registro(byte id);
        Parametros Register(byte id);
        Task<List<Parametros>> Lista(bool Activo = true);
        #endregion

    }
    public class ParametrosController : IParametrosController
    {
        readonly IDbContextFactory<dbContext> db;
        public ParametrosController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
        public  Parametros Register(byte id)
        {
            using var dbContext = db.CreateDbContext();
            var result = dbContext.Parametros.SingleOrDefault(a => a.IdParametro == id) ?? new Parametros();
            return result;
        }
        #region CRUD_CLASE
        public async Task<Parametros> Insert(Parametros entidad)
        {
            using var dbContext = db.CreateDbContext();
            entidad.Activo = true;
            entidad.FechaRegistro = DateTime.Now;
            await dbContext.Parametros.AddAsync(entidad);
            await dbContext.SaveChangesAsync();
            return entidad;
        }
        public async Task<bool> Update(Parametros entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Parametros.FindAsync(entidad.IdParametro);
            if (registro != null)
            {
                registro.Descripcion = entidad.Descripcion;
                registro.Valor = entidad.Valor;
                registro.TipoDato = entidad.TipoDato;
                registro.UsuarioActualiza = entidad.UsuarioActualiza;
                registro.FechaActualizacion = entidad.FechaActualizacion;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Anular(Parametros entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Parametros.FindAsync(entidad.IdParametro);
            if (registro != null)
            {
                registro.Activo = entidad.Activo;
                registro.UsuarioActualiza = entidad.UsuarioActualiza;
                registro.FechaActualizacion = entidad.FechaActualizacion;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Existe(byte id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Parametros.AnyAsync(a => a.IdParametro == id);
        }
        public async Task<Parametros> Registro(byte id)
        {
            using var dbContext = db.CreateDbContext();
            var result = await dbContext.Parametros.FirstOrDefaultAsync(a => a.IdParametro == id) ?? new Parametros();
            return result;
        }
        public async Task<List<Parametros>> Lista(bool Activo = true)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Parametros.Where(a => a.Activo == Activo).ToListAsync();
        }
        #endregion

    }
}
