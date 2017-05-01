using SIGCOMT.Domain.Core;

namespace SIGCOMT.Domain
{
    public class PermisoRol : EntityBase
    {
        public int FormularioId { get; set; }
        public int RolId { get; set; }

        public virtual Formulario Formulario { get; set; }
        public virtual Rol Rol { get; set; }
    }
}