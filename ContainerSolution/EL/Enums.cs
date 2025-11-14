using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL
{
    public class Enums
    {
        public enum eIcon{
        success,
        warning,
        error,
        info
        }
        public enum eAlign
        {
            center,
            left,
            right,
            justify
        }
        public enum eParametros
        {
            None,
            BoqueoCuenta,
            TiempoEspera,
            ExtenderSesion
        }
        public enum eFormularios
        {
            None,
            Usuarios,
            UsuarioFormulario,
            UsuarioPermisos,
            CatEstadoLibros,
            CatAutores,
            CatSocios,
            CatCategorias,
            CatLibros,
            CatEjemplares,
            CatPublicacionLibros
        }
        public enum ePermiso
        {
            none,
            Agregar,
            Actualizar,
            Anular
        }
    }
}
