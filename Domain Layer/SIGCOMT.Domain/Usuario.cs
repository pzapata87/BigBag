using SIGCOMT.Domain.Core;

namespace SIGCOMT.Domain
{
    public class Usuario : Entity<int>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int RolId { get; set; }

        public virtual Rol Rol { get; set; }
    }
}