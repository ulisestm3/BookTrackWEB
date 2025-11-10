using EL;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
    public interface IUsuariosController
    {

        #region CRUD_INTERFAZ
        Task<Usuarios> Insert(Usuarios entidad);
        Task<bool> Update(Usuarios entidad);
        Task<bool> Anular(Usuarios entidad);
        Task<bool> Existe(short id);
        Task<Usuarios> Registro(short id);
        Task<List<Usuarios>> Lista(bool Activo = true);
        #endregion
        Task<bool> ValidateLogin(short IdRegistro, string Login);
        Task<bool> UpdatePassword(Usuarios Entidad);
    }
    public class UsuariosController : IUsuariosController
    {
        readonly IDbContextFactory<dbContext> db;
        public UsuariosController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
        #region CRUD_CLASE
        public async Task<Usuarios> Insert(Usuarios entidad)
        {
            using var dbContext = db.CreateDbContext();
            entidad.Activo = true;
            entidad.FechaRegistro = DateTime.Now;
            await dbContext.Usuarios.AddAsync(entidad);
            await dbContext.SaveChangesAsync();
            return entidad;
        }
        public async Task<bool> Update(Usuarios entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Usuarios.FindAsync(entidad.IdUsuario);
            if (registro != null)
            {
                registro.NombreCompleto = entidad.NombreCompleto;
                registro.Correo = entidad.Correo;
                registro.Celular = entidad.Celular;
                registro.Login = entidad.Login;
                registro.Bloqueado = entidad.Bloqueado;
                registro.Intentos = entidad.Intentos;
                registro.UsuarioActualiza = entidad.UsuarioActualiza;
                registro.FechaActualizacion = entidad.FechaActualizacion;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Anular(Usuarios entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.Usuarios.FindAsync(entidad.IdUsuario);
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
        public async Task<bool> Existe(short id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Usuarios.AnyAsync(a => a.IdUsuario == id);
        }
        public async Task<Usuarios> Registro(short id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Usuarios.FirstOrDefaultAsync(a => a.IdUsuario == id) ?? new Usuarios();
        }
        public async Task<List<Usuarios>> Lista(bool Activo = true)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.Usuarios.Where(a => a.Activo == Activo).ToListAsync();
        }
        #endregion
        public async Task<bool> ValidateLogin(short IdRegistro, string Login)
        {
            using var DataBase = db.CreateDbContext();
            return await DataBase.Usuarios.AnyAsync(a => a.IdUsuario != IdRegistro && a.Login.Equals(Login));
        }
        public async Task<bool> UpdatePassword(Usuarios Entidad)
        {
            using var DataBase = db.CreateDbContext();
            var Registro = await DataBase.Usuarios.FindAsync(Entidad.IdUsuario);
            if (Registro != null)
            {
                Registro.Password = Entidad.Password;
                Registro.UsuarioActualiza = Entidad.UsuarioActualiza;
                Registro.FechaActualizacion = Entidad.FechaActualizacion;
            }
            return await DataBase.SaveChangesAsync() > 0;
        }
    }
}
