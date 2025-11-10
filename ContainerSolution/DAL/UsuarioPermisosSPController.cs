using EL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUsuarioPermisosSPController
    {
        Task<List<UsuarioPermisosSP>> Lista(short? IdUser, byte? IdForm);
    }
    public class UsuarioPermisosSPController : IUsuarioPermisosSPController
    {
        readonly IDbContextFactory<dbContext> db;
        public UsuarioPermisosSPController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
        public async Task<List<UsuarioPermisosSP>> Lista(short? IdUser, byte? IdFormulario)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.UsuarioPermisosSP
                .FromSqlInterpolated($"EXEC sp_UsuarioPermisos @IdUsuario = {IdUser ?? 0}, @IdFormulario = {IdFormulario ?? 0}")
                .ToListAsync();
        }
    }
}
