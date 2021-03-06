﻿using System.Data.Entity.ModelConfiguration;
using SIGCOMT.Domain;

namespace SIGCOMT.Persistence.DatabaseMappings
{
    public class FormularioConfiguration : EntityTypeConfiguration<Formulario>
    {
        public FormularioConfiguration()
        {
            Property(p => p.Direccion).HasMaxLength(100);
            Property(p => p.ResourceKey).IsRequired().HasMaxLength(30);
            Property(p => p.Icono).HasMaxLength(20);

            HasOptional(p => p.FormularioParent).WithMany(p => p.FormulariosHijosList).HasForeignKey(p => p.FormularioParentId);
            Ignore(p => p.ListaFormularios);
        }
    }
}