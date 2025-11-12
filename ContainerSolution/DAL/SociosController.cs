using EL;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
    public interface ISociosController
    {

        #region CRUD_INTERFAZ
        Task<Socios> Insert(Socios entidad);
        Task<bool> Update(Socios entidad);
        Task<bool> Anular(Socios entidad);
        Task<bool> Existe(int id);
        Task<Socios> Registro(int id);
        Task<List<Socios>> Lista(bool Activo = true);
        #endregion
        Task<bool> ValidateSocio(int SocioId, string Socio);
    }
    public class SociosController : ISociosController
    {
        readonly IDbContextFactory<dbContext> db;
        public SociosController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
        #region CRUD_CLASE
        public async Task<Socios> Insert(Socios entidad)
        {
            using var dbContext = db.CreateDbContext();
            entidad.Activo = true;
            entidad.FechaRegistro = DateTime.Now;
            await dbContext.Socios.AddAsync(entidad);
            await dbContext.SaveChangesAsync();
            return entidad;
        }
        public async Task<bool> Update(Socios entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Socios.FindAsync(entidad.SocioId);
            if (registro != null)
            {
                registro.Socio = entidad.Socio;
                registro.Email = entidad.Email;
                registro.Telefono = entidad.Telefono;
                registro.Activo = entidad.Activo;
                registro.UsuarioActualiza = entidad.UsuarioActualiza;
                registro.FechaActualizacion = entidad.FechaActualizacion;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Anular(Socios entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Socios.FindAsync(entidad.SocioId);
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
            return await dbContext.Socios.AnyAsync(a => a.SocioId == id);
        }
        public async Task<Socios> Registro(int id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Socios.FirstOrDefaultAsync(a => a.SocioId == id) ?? new Socios();
        }
        public async Task<List<Socios>> Lista(bool Activo = true)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Socios.Where(a => a.Activo == Activo).ToListAsync();
        }
        #endregion
        public async Task<bool> ValidateSocio(int SocioId, string Socio)
        {
            using var DataBase = db.CreateDbContext();
            return await DataBase.Socios.AnyAsync(a => a.SocioId != SocioId && a.Socio.Equals(Socio) && a.Activo.Equals(true));
        }
    }
}
