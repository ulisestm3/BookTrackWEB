using EL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IAuthenticationController
    {
        Task<Usuarios?> Authenticate(Usuarios user);
        Task<Usuarios?> Existe(short Id);
        Task<List<vFormulariosUsuario>> GetFormsUser(short IdUsuario);
        Task<bool> Existe(string Login);
        Task<bool> Existe(string Login, byte[] Password);
        Task<Usuarios> Registro(string Login);
        Task<bool> BloquearCuenta(string UserName);
        Task<bool> DesbloquearCuenta(vUsuarios User);
        Task<bool> SumarIntento(string UserName);
        Task<Usuarios> BuscarUsuario(string Correo);
        Task<bool> UpdatePassword(byte[] Password, string Correo, string Login);
        Task<bool> AutorizedForm(byte IdFormulario, int IdUsuario);
        Task<List<vPermisosUsuario>> GetPermisosUsuario(short IdUsuario, byte IdFormulario);
        Task<List<vFormulariosUsuario>> GetVFormularios(short IdUsuario);
    }
    public class AuthenticationController: IAuthenticationController
    {
        readonly IDbContextFactory<dbContext> db;
        public AuthenticationController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
        public async Task<Usuarios?> Authenticate(Usuarios user)
        {
            using var conexion = db.CreateDbContext();
            return await conexion.Usuarios
                .Where(a => a.Login == user.Login && a.Activo)
                .SingleOrDefaultAsync();
        }
        public async Task<Usuarios?> Existe(short Id)
        {
            using var conexion = db.CreateDbContext();
            return await conexion.Usuarios
                .Where(a => a.IdUsuario == Id && a.Activo)
                .SingleOrDefaultAsync();
        }
        public async Task<List<vFormulariosUsuario>> GetFormsUser(short IdUsuario)
        {
            using var conexion = db.CreateDbContext();
            var result = await conexion.vFormulariosUsuario.Where(a => a.IdUsuario == IdUsuario).ToListAsync();
            return result;
        }
        public async Task<bool> Existe(string Login)
        {
            using var conexion = db.CreateDbContext();
            return await conexion.Usuarios.Where(a => a.Login == Login && a.Activo).AnyAsync();
        }
        public async Task<bool> Existe(string Login, byte[] Password)
        {
            using var conexion = db.CreateDbContext();
            return await conexion.Usuarios.Where(a => a.Login == Login && a.Password == Password && a.Activo).AnyAsync();
        }
        public async Task<Usuarios> Registro(string Login)
        {
            using var conexion = db.CreateDbContext();
            var Registro = await conexion.Usuarios.Where(a => a.Login == Login).SingleOrDefaultAsync();
            if (Registro != null) return Registro;
            return await Task.FromResult<Usuarios>(new());
        }
        public async Task<bool> BloquearCuenta(string UserName)
        {
            using var conexion = db.CreateDbContext();
            var Registro = await conexion.Usuarios.Where(a => a.Login.Equals(UserName)).SingleOrDefaultAsync();
            if (Registro != null)
            {
                Registro.Bloqueado = true;
                Registro.Intentos++;
                return await conexion.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(false);
        }
        public async Task<bool> DesbloquearCuenta(vUsuarios User)
        {
            using var conexion = db.CreateDbContext();
            var Registro = await conexion.Usuarios.Where(a => a.IdUsuario == User.IdUsuario).SingleOrDefaultAsync();
            if (Registro != null)
            {
                Registro.Bloqueado = false;
                Registro.Intentos = 0;
                return await conexion.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(false);
        }
        public async Task<bool> SumarIntento(string UserName)
        {
            using var conexion = db.CreateDbContext();
            var Registro = await conexion.Usuarios.Where(a => a.Login.Equals(UserName)).SingleOrDefaultAsync();
            if (Registro != null)
            {
                Registro.Intentos++;
                return await conexion.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(false);
        }
        public async Task<Usuarios> BuscarUsuario(string Correo)
        {
            using var conexion = db.CreateDbContext();
            var Registro = await conexion.Usuarios.Where(a => a.Correo.Equals(Correo)).SingleOrDefaultAsync();
            if (Registro != null)
            {
                return Registro;
            }
            return await Task.FromResult<Usuarios>(new());
        }
        public async Task<bool> UpdatePassword(byte[] Password, string Correo, string Login)
        {
            using var conexion = db.CreateDbContext();
            var Registro = await conexion.Usuarios.Where(a => a.Correo.Equals(Correo) && a.Login.Equals(Login)).SingleOrDefaultAsync();
            if (Registro != null)
            {
                Registro.Password = Password;
                Registro.Intentos = 0;
                Registro.Bloqueado = false;
                return await conexion.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(false);
        }
        public async Task<bool> AutorizedForm(byte IdFormulario, int IdUsuario)
        {
            using var conexion = db.CreateDbContext();
            return await conexion.vFormulariosUsuario.Where(a => a.IdFormulario == IdFormulario && a.IdUsuario == IdUsuario).CountAsync() > 0;
        }
        public async Task<List<vPermisosUsuario>> GetPermisosUsuario(short IdUsuario, byte IdFormulario)
        {
            using var conexion = db.CreateDbContext();
            return await conexion.vPermisosUsuario.Where(a => a.IdUsuario == IdUsuario && a.IdFormulario == IdFormulario).ToListAsync();
        }
        public async Task<List<vFormulariosUsuario>> GetVFormularios(short IdUsuario)
        {
            using var conexion = db.CreateDbContext();
            var result = await conexion.vFormulariosUsuario.Where(a => a.IdUsuario == IdUsuario).ToListAsync();
            return result;
        }
    }
}
