using System.Collections.Generic;
using SIGCOMT.Common.Enum;
using SIGCOMT.DataBase.Generator.Core;
using SIGCOMT.Domain;

namespace SIGCOMT.DataBase.Generator.Operaciones
{
    public class HomeOperation : OperationBase
    {
        public HomeOperation(List<Rol> rolesConPermiso)
        {
            RolesConPermiso = rolesConPermiso;
            ResourceKey = TipoOperacion.HomeOperation.ToString();
            Direccion = UrlOperationManager.OperationsUrl[TipoOperacion.HomeOperation];

            PermisosOperacion = new List<TipoPermiso>
            {
                TipoPermiso.Mostrar,
                TipoPermiso.Crear,
                TipoPermiso.Editar,
                TipoPermiso.Eliminar
            };
        } 
    }
}