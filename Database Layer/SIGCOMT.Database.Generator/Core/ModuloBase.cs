﻿using System.Collections.Generic;
using System.Data.Entity;
using SIGCOMT.Common.Enum;
using SIGCOMT.Domain;

namespace SIGCOMT.DataBase.Generator.Core
{
    public class ModuloBase : IModulo
    {
        private Formulario _moduloFormulario;

        protected string IconoModulo { get; set; }
        protected List<Rol> RolesConPermiso { get; set; }
        protected List<IOperation> Operations;
        protected string ResourceKey { get; set; }

        private const int Activo = (int)TipoEstado.Activo;

        private void RegistrarModulo()
        {
            _moduloFormulario = new Formulario
            {
                FormulariosHijosList = new LinkedList<Formulario>(),
                ResourceKey = ResourceKey,
                Direccion = IconoModulo,
                Orden = 0,
                FormularioParentId = null,
                Estado = Activo
            };
        }

        private void RegistrarOperaciones()
        {
            foreach (var operacion in Operations)
            {
                operacion.Registrar(_moduloFormulario);
            }
        }

        public void Registrar(DbContext dbContext)
        {
            RegistrarModulo();
            RegistrarOperaciones();

            dbContext.Set<Formulario>().Add(_moduloFormulario);
        }
    }
}