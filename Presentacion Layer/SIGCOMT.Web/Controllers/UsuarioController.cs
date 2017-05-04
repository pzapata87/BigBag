using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SIGCOMT.BusinessLogic.Core;
using SIGCOMT.BusinessLogic.Interfaces;
using SIGCOMT.Common;
using SIGCOMT.Common.DataTable;
using SIGCOMT.Common.Enum;
using SIGCOMT.Domain;
using SIGCOMT.DTO;
using SIGCOMT.DTO.AutoMapper;
using SIGCOMT.Web.Core;
using SIGCOMT.Web.Core.Aspects;

namespace SIGCOMT.Web.Controllers
{
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioBL _usuarioBl;
        private readonly IRolBL _rolBl;

        public UsuarioController(IUsuarioBL usuarioBl, IRolBL rolBl)
        {
            _usuarioBl = usuarioBl;
            _rolBl = rolBl;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Listar(GridTable gridTable)
        {
            return ListarJqGrid(new ListParameter<Usuario, UsuarioDto>
            {
                BusinessLogicClass = _usuarioBl,
                Grid = gridTable,
                SelecctionFormat = p => new UsuarioDto
                {
                    Id = p.Id,
                    UserName = p.UserName,
                    Email = p.Email,
                    Nombre = p.Nombre,
                    Apellido = p.Apellido,
                    RolNombre = p.RolUsuarioList.First().Rol.Nombre,
                    Estado = p.Estado
                }
            });
        }

        [Controller(TipoVerbo = TipoAccionControlador.Post)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(UsuarioDto usuarioDto)
        {
            var response = new JsonResponse {Success = false};

            var entityTemp =
                        _usuarioBl.Get(
                            p => p.UserName == usuarioDto.UserName && p.Id != usuarioDto.Id && p.Estado == (int)TipoEstado.Activo);

            if (entityTemp == null)
            {
                var usuarioDomain = _usuarioBl.GetById(usuarioDto.Id);
                //UsuarioConverter.DtoToDomain(usuarioDomain, usuarioDto);

                _usuarioBl.Update(usuarioDomain);

                response.Message = "Se actualizó el usuario correctamente";
                response.Success = true;
            }
            else
            {
                response.Message = "Ya existe el nombre de usuario";
                response.Success = false;
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [Controller(TipoVerbo = TipoAccionControlador.Post)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Crear(UsuarioDto userDto)
        {
            var response = new JsonResponse { Success = false };

            var entityTemp = _usuarioBl.Get(p => p.UserName == userDto.UserName && p.Estado == (int)TipoEstado.Activo);

            if (entityTemp == null)
            {
                var userDomain = MapperHelper.Map<UsuarioDto, Usuario>(userDto);
                Guid guid = Guid.NewGuid();
                var password = guid.ToString().Split('-')[1];
                userDomain.Password = Encriptador.Encriptar(password);

                userDomain.RolUsuarioList = new List<RolUsuario>
                {
                    new RolUsuario
                    {
                        RolId = userDto.RolId,
                        Estado = TipoEstado.Activo.GetNumberValue()
                    }
                };

                _usuarioBl.Add(userDomain);

                response.Message = "Se registró el usuario correctamente";
                response.Success = true;
            }
            else
            {
                response.Message = "Ya existe el nombre de usuario";
                response.Success = false;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [Controller(TipoVerbo = TipoAccionControlador.Post)]
        [HttpPost]
        public JsonResult Eliminar(int id)
        {
            var usuarioDomain = _usuarioBl.GetById(id);
            usuarioDomain.Estado = (int)TipoEstado.Inactivo;

            _usuarioBl.Update(usuarioDomain);

            var jsonResponse = new JsonResponse { Success = true, Message = "Se eliminó satisfactoriamente el usuario" };
            return Json(jsonResponse, JsonRequestBehavior.AllowGet);
        }

        [Controller(TipoVerbo = TipoAccionControlador.Post)]
        [HttpPost]
        public JsonResult GetUsuario(int usuarioId)
        {
            var response = new JsonResponse { Success = false };
            var user = _usuarioBl.GetById(usuarioId);

            if (user != null)
            {
                response.Data = MapperHelper.Map<Usuario, UsuarioDto>(user);
                response.Success = true;
            }
            else
            {
                response.Message = Resources.Usuario.UsuarioNoRegistrado;
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [Controller(TipoVerbo = TipoAccionControlador.Post)]
        [HttpPost]
        public JsonResult GetRoles()
        {
            var roles = _rolBl.GetAll().ToList();

            var response = new JsonResponse
            {
                Success = true,
                Data = MapperHelper.Map<List<Rol>, List<RolDto>>(roles)
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}