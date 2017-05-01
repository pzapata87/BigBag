using System;
using System.Reflection;
using System.Web.Mvc;
using SIGCOMT.Common.Enum;

namespace SIGCOMT.Web.Core
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpGetAction : ActionMethodSelectorAttribute
    {
        private readonly TipoPermiso _permiso;

        public HttpGetAction(TipoPermiso permiso)
        {
            _permiso = permiso;
        }

        public TipoPermiso GetPermiso()
        {
            return _permiso;
        }

        private static readonly AcceptVerbsAttribute InnerAttribute = new AcceptVerbsAttribute(HttpVerbs.Get);

        /// <summary>
        /// Determina si la solicitud post del método de acción es válida para el contexto de controlador especificado.
        /// </summary>
        /// 
        /// <returns>
        /// true si la solicitud del método de acción es válida para el contexto de controlador especificado; de lo contrario, false.
        /// </returns>
        /// <param name="controllerContext">Contexto del controlador.</param><param name="methodInfo">Información acerca del método de la acción.</param>
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return InnerAttribute.IsValidForRequest(controllerContext, methodInfo);
        }
    }
}