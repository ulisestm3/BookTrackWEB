using EL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
namespace DAL
{
    public interface IUsuarioPermisosController
    {
        #region CRUD_INTERFAZ
        Task<UsuarioPermisos> Insert(UsuarioPermisos entidad);
        Task<bool> Update(UsuarioPermisos entidad);
        Task<bool> Anular(UsuarioPermisos entidad);
        Task<bool> Existe(int id);
        Task<UsuarioPermisos> Registro(int id);
        Task<List<UsuarioPermisos>> Lista(bool Activo = true);
        #endregion
        Task<bool> InsertOrUpdateUsuarioPermisosAsync(List<UsuarioPermisos> lista);
    }
    public class UsuarioPermisosController : IUsuarioPermisosController
    {
        readonly IDbContextFactory<dbContext> db;
        public UsuarioPermisosController(IDbContextFactory<dbContext> dbContex) => db = dbContex;
        #region CRUD_CLASE
        public async Task<UsuarioPermisos> Insert(UsuarioPermisos entidad)
        {
            using var dbContext = db.CreateDbContext();
            entidad.Activo = true;
            entidad.FechaRegistro = DateTime.Now;
            await dbContext.UsuarioPermisos.AddAsync(entidad);
            await dbContext.SaveChangesAsync();
            return entidad;
        }
        public async Task<bool> Update(UsuarioPermisos entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.UsuarioPermisos.FindAsync(entidad.IdUsuarioPermiso);
            if (registro != null)
            {
                registro.IdUsuario = entidad.IdUsuario;
                registro.IdPermisoFormulario = entidad.IdPermisoFormulario;
                registro.UsuarioActualiza = entidad.UsuarioActualiza;
                registro.FechaActualizacion = entidad.FechaActualizacion;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Anular(UsuarioPermisos entidad)
        {
            using var dbContext = db.CreateDbContext();
            var registro = await dbContext.UsuarioPermisos.FindAsync(entidad.IdUsuarioPermiso);
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
            return await dbContext.UsuarioPermisos.AnyAsync(a => a.IdUsuarioPermiso == id);
        }
        public async Task<UsuarioPermisos> Registro(int id)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.UsuarioPermisos.FirstOrDefaultAsync(a => a.IdUsuarioPermiso == id) ?? new UsuarioPermisos();
        }
        public async Task<List<UsuarioPermisos>> Lista(bool Activo = true)
        {
            using var dbContext = db.CreateDbContext();
            return await dbContext.UsuarioPermisos.Where(a => a.Activo == Activo).ToListAsync();
        }
        #endregion
        public async Task<bool> InsertOrUpdateUsuarioPermisosAsync(List<UsuarioPermisos> lista)
        {
            using var dbContext = db.CreateDbContext();

            var table = new DataTable();
            table.Columns.Add("IdUsuario", typeof(short));
            table.Columns.Add("IdPermisoFormulario", typeof(short));
            table.Columns.Add("Activo", typeof(bool));
            table.Columns.Add("UsuarioRegistra", typeof(int));
            table.Columns.Add("FechaRegistro", typeof(DateTime));
            table.Columns.Add("UsuarioActualiza", typeof(int));
            table.Columns.Add("FechaActualizacion", typeof(DateTime));

            foreach (var item in lista)
            {
                table.Rows.Add(
                    item.IdUsuario,
                    item.IdPermisoFormulario,
                    item.Activo,
                    item.UsuarioRegistra,
                    item.FechaRegistro,
                    item.UsuarioActualiza,
                    item.FechaActualizacion
                );
            }

            var parametro = new SqlParameter("@UsuarioPermisosType", table)
            {
                SqlDbType = SqlDbType.Structured,
                TypeName = "dbo.UsuarioPermisosType"
            };

            await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.sp_UsuarioPermisosUpdate @UsuarioPermisosType", parametro);
            return true;
        }

    }
}
