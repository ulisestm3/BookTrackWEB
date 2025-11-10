using System;
using Microsoft.EntityFrameworkCore;
using EL;

namespace DAL
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options) { }

        #region DBSETS_TABLAS
		public DbSet<Usuarios> Usuarios { get; set; }
		public DbSet<Reportes> Reportes { get; set; }
		public DbSet<UsuarioReportes> UsuarioReportes { get; set; }
		public DbSet<Permisos> Permisos { get; set; }
		public DbSet<Parametros> Parametros { get; set; }
		public DbSet<Formularios> Formularios { get; set; }
		public DbSet<UsuarioFormularios> UsuarioFormularios { get; set; }
		public DbSet<PermisoFormulario> PermisoFormulario { get; set; }
		public DbSet<UsuarioPermisos> UsuarioPermisos { get; set; }
        #endregion DBSETS_TABLAS
        


        #region DBSETS_VISTAS
        public DbSet<vUsuarios> vUsuarios { get; set; }
		public DbSet<vFormulariosUsuario> vFormulariosUsuario { get; set; }
		public DbSet<vPermisosUsuario> vPermisosUsuario { get; set; }
		public DbSet<vUsuarioFormularios> vUsuarioFormularios { get; set; }
        #endregion DBSETS_VISTAS



        #region Protegido
        //Cualquier otro codigo que no deba ser modificado
        public DbSet<UsuarioFormulariosSP> UsuarioFormulariosSP { get; set; }
        public DbSet<UsuarioPermisosSP> UsuarioPermisosSP { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
