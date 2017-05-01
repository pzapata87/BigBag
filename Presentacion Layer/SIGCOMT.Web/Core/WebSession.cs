using System.Collections.Generic;
using System.Web;
using SIGCOMT.DTO;

namespace SIGCOMT.Web.Core
{
    public static class WebSession
    {
        public static UsuarioDto Usuario
        {
            get { return HttpContext.Current.Session[Constantes.UsuarioSesion] as UsuarioDto; }
            set { HttpContext.Current.Session.Add(Constantes.UsuarioSesion, value); }
        }

        public static IEnumerable<FormularioDto> Formularios
        {
            get { return HttpContext.Current.Session[Constantes.FormulariosSesion] as IEnumerable<FormularioDto>; }
            set { HttpContext.Current.Session.Add(Constantes.FormulariosSesion, value); }
        }

        public static FormularioDto FormularioActual
        {
            get { return HttpContext.Current.Session[Constantes.FormularioActualSesion] as FormularioDto; }
            set { HttpContext.Current.Session.Add(Constantes.FormularioActualSesion, value); }
        }
    }
}