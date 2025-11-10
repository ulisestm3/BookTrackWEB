using EL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUsuarioFormulariosSPController
    {
        Task<List<UsuarioFormulariosSP>> Lista(short? IdUser);
    }
    public class UsuarioFormulariosSPController: IUsuarioFormulariosSPController
    {
        readonly IDbContextFactory<dbContext> db;
        public UsuarioFormulariosSPController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
        public async Task<List<UsuarioFormulariosSP>> Lista(short? IdUser)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.UsuarioFormulariosSP
                .FromSqlInterpolated($"EXEC sp_UsuarioFormularios @IdUsuario = {IdUser}")
                .ToListAsync();
        }
    }
}
