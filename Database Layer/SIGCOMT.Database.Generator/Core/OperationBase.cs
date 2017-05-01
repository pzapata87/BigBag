using System.Collections.Generic;
using SIGCOMT.Common.Enum;
using SIGCOMT.Domain;

namespace SIGCOMT.DataBase.Generator.Core
{
    public class OperationBase : IOperation
    {
        private const int Activo = (int)TipoEstado.Activo;
        protected List<Rol> RolesConPermiso { get; set; }
        protected List<TipoPermiso> PermisosOperacion { get; set; }

        protected string ResourceKey { get; set; }
        protected string Direccion { get; set; }

        public void Registrar(Formulario parent)
        {
            parent.FormulariosHijosList.Add(new Formulario
            {
                ResourceKey = ResourceKey,
                Direccion = Direccion,
                Orden = 1,
                Estado = Activo
            });
        }
    }
}