using System.Data.Entity.ModelConfiguration;
using SIGCOMT.Domain;

namespace SIGCOMT.Persistence.DatabaseMappings
{
    public class PermisoRolConfiguration : EntityTypeConfiguration<PermisoRol>
    {
        public PermisoRolConfiguration()
        {
            HasKey(p => new {p.RolId, p.FormularioId});
            HasRequired(p => p.Formulario).WithMany().HasForeignKey(p => p.FormularioId);
            HasRequired(p => p.Rol).WithMany(p => p.PermisoRolList).HasForeignKey(p => p.RolId);
        }
    }
}