using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SIGCOMT.BusinessLogic.Interfaces;
using SIGCOMT.Domain;
using SIGCOMT.DTO;
using SIGCOMT.DTO.AutoMapper;
using SIGCOMT.Web.Core;
using SIGCOMT.Web.Helpers;

namespace SIGCOMT.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        #region Propiedades
        
        private readonly IUsuarioBL _usuarioBl;

        #endregion

        #region Constructor

        public AccountController(IUsuarioBL usuarioBl)
        {
            _usuarioBl = usuarioBl;
        }

        #endregion

        #region Métodos Públicos

        [AllowAnonymous]
        public ActionResult Login()
        {
            var login = new LogInDto
            {
                UserName = "",
                Password = ""
            };

            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogInDto model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);

                Usuario usuario = _usuarioBl.ValidateUser(model.UserName, model.Password);

                if (usuario == null)
                {
                    ViewBag.MessageError = Resources.Usuario.UsuarioNoRegistrado;
                }
                else
                {
                    var usuarioDto = MapperHelper.Map<Usuario, UsuarioDto>(usuario);
                    GenerarTickectAutenticacion(usuarioDto);

                    return RedirectToAction("Index", "Home");
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                LogError(ex);
                return MensajeError();
            }
        }

        public ActionResult LogOut()
        {
            LimpiarAutenticacion();
            return RedirectToAction("Login");
        }

        #endregion

        #region Métodos Privados

        private void GenerarTickectAutenticacion(UsuarioDto usuario)
        {
            AuthenticationHelper.CreateAuthenticationTicket(usuario.UserName);
            WebSession.Usuario = usuario;
            //WebSession.Formularios = _formularioAppService.GetByUsuario(usuario.Id);
        }

        private void LimpiarAutenticacion()
        {
            AuthenticationHelper.SignOut();

            WebSession.Usuario = null;
            WebSession.Formularios = new List<FormularioDto>();
        }

        #endregion
    }
}