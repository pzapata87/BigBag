using System.Data.Entity.ModelConfiguration;
using SIGCOMT.Domain;

namespace SIGCOMT.Persistence.DatabaseMappings
{
    public class OrdenPedidoSapConfiguration : EntityTypeConfiguration<OrdenPedidoSap>
    {
        public OrdenPedidoSapConfiguration()
        {
            HasKey(p => p.DocNum);

            Property(p => p.CardCode).HasMaxLength(15);
            Ignore(p => p.Estado);
        }
    }
}