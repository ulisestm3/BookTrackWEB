using EL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
namespace DAL
{
    public interface IUsuarioFormulariosController
    {
        #region CRUD_INTERFAZ
        Task<UsuarioFormularios> Insert(UsuarioFormularios entidad);
        Task<bool> Update(UsuarioFormularios entidad);
        Task<bool> Anular(UsuarioFormularios entidad);
        Task<bool> Existe(int id);
        Task<UsuarioFormularios> Registro(int id);
        Task<List<UsuarioFormularios>> Lista(bool Activo = true);
        #endregion
        Task<bool> InsertOrUpdateUsuarioFormulariosAsync(List<UsuarioFormularios> lista);
    }
    public class UsuarioFormulariosController : IUsuarioFormulariosController
    {
        readonly IDbContextFactory<dbContext> db;
        public UsuarioFormulariosController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
        #region CRUD_CLASE
        public async Task<UsuarioFormularios> Insert(UsuarioFormularios entidad)
        {
            using var dbContext = db.CreateDbContext();
            entidad.Activo = true;
            entidad.FechaRegistro = DateTime.Now;
            await dbContext.UsuarioFormularios.AddAsync(entidad);
            await dbContext.SaveChangesAsync();
            return entidad;
        }
        public async Task<bool> Update(UsuarioFormularios entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.UsuarioFormularios.FindAsync(entidad.IdUsuarioFormulario);
            if (registro != null)
            {
                registro.IdUsuario = entidad.IdUsuario;
                registro.IdFormulario = entidad.IdFormulario;
                registro.UsuarioActualiza = entidad.UsuarioActualiza;
                registro.FechaActualizacion = entidad.FechaActualizacion;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Anular(UsuarioFormularios entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.UsuarioFormularios.FindAsync(entidad.IdUsuarioFormulario);
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
        public async Task<bool> Existe(int id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.UsuarioFormularios.AnyAsync(a => a.IdUsuarioFormulario == id);
        }
        public async Task<UsuarioFormularios> Registro(int id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.UsuarioFormularios.FirstOrDefaultAsync(a => a.IdUsuarioFormulario == id) ?? new UsuarioFormularios();
        }
        public async Task<List<UsuarioFormularios>> Lista(bool Activo = true)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.UsuarioFormularios.Where(a => a.Activo == Activo).ToListAsync();
        }
        #endregion


        public async Task<bool> InsertOrUpdateUsuarioFormulariosAsync(List<UsuarioFormularios> lista)
        {
            using var dbContext = db.CreateDbContext();

            var table = new DataTable();
            table.Columns.Add("IdUsuario", typeof(short));
            table.Columns.Add("IdFormulario", typeof(byte));
            table.Columns.Add("Activo", typeof(bool));
            table.Columns.Add("UsuarioRegistra", typeof(int));
            table.Columns.Add("FechaRegistro", typeof(DateTime));
            table.Columns.Add("UsuarioActualiza", typeof(int));
            table.Columns.Add("FechaActualizacion", typeof(DateTime));

            foreach (var item in lista)
            {
                table.Rows.Add(
                    item.IdUsuario,
                    item.IdFormulario,
                    item.Activo,
                    item.UsuarioRegistra,
                    item.FechaRegistro,
                    item.UsuarioActualiza,
                    item.FechaActualizacion
                );
            }

            var parametro = new SqlParameter("@UsuarioFormulariosType", table)
            {
                SqlDbType = SqlDbType.Structured,
                TypeName = "dbo.UsuarioFormulariosType"
            };

            await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.sp_UsuarioFormularioUpdate @UsuarioFormulariosType", parametro);
            return true;
        }

    }
}
