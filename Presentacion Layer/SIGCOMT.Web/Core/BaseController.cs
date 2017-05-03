using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Web.Mvc;
using log4net;
using SIGCOMT.BusinessLogic.Core;
using SIGCOMT.Common;
using SIGCOMT.Common.DataTable;
using SIGCOMT.Common.Enum;
using SIGCOMT.Common.FiltersRules;
using SIGCOMT.Domain.Core;
using SIGCOMT.Web.Filters;
namespace SIGCOMT.Web.Core
{
    [Authorize]
    [HandleError]
    [WebSessionFilter]
    public class BaseController : Controller
    {
        #region Variables Privadas

        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Paginacion

        protected JsonResult ListarJqGrid<T, TResult>(ListParameter<T, TResult> configuracionListado)
            where T : EntityBase where TResult : class
        {
            try
            {
                GridTable grid = configuracionListado.Grid;

                Expression<Func<T, bool>> where =
                     LambdaFilterManager.ConvertToLambda<T>(grid.Columns, grid.Search, grid.Homologaciones)
                        .And(configuracionListado.FiltrosAdicionales ?? (q => true));

                var count = configuracionListado.CountMethod(where);
                OrderColumn ordenamiento = grid.Order.First();

                var currentPage = (grid.Start / grid.Length);
                var parametroFiltro = new FilterParameters<T>
                {
                    ColumnOrder = grid.Columns[ordenamiento.Column].Name,
                    CurrentPage = (currentPage >= 0 ? currentPage : 0) + 1,
                    OrderType =
                        ordenamiento.Dir != null
                            ? (TipoOrden)Enum.Parse(typeof(TipoOrden), ordenamiento.Dir, true)
                            : TipoOrden.Asc,
                    WhereFilter = where,
                    AmountRows = grid.Length > 0 ? grid.Length : count
                };

                int totalPages = 0;

                if (count > 0 && parametroFiltro.AmountRows > 0)
                {
                    if (count % parametroFiltro.AmountRows > 0)
                    {
                        totalPages = count / parametroFiltro.AmountRows + 1;
                    }
                    else
                    {
                        totalPages = count / parametroFiltro.AmountRows;
                    }

                    totalPages = totalPages == 0 ? 1 : totalPages;
                }

                parametroFiltro.CurrentPage = parametroFiltro.CurrentPage > totalPages
                    ? totalPages
                    : parametroFiltro.CurrentPage;
                parametroFiltro.Start = grid.Start;

                List<TResult> respuestaList =
                    configuracionListado.ListMethod(parametroFiltro)
                        .ToList()
                        .Select(configuracionListado.SelecctionFormat).ToList();

                var responseData = new DataTableResponse<TResult>
                {
                    data = respuestaList,
                    recordsFiltered = count,
                    recordsTotal = count
                };

                return Json(responseData);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return MensajeError();
            }
        }

        #endregion Paginacion

        #region Overrides

        protected override void OnException(ExceptionContext filterContext)
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var controllerName = filterContext.RouteData.Values["controller"];
            var actionName = filterContext.RouteData.Values["action"];

            Logger.Error(string.Format("Controlador:{0}  Action:{1}  Mensaje:{2}", controllerName, actionName, WebUtils.GetExceptionMessage(filterContext.Exception)));

            filterContext.Result = View("Error");
        }

        public new RedirectToRouteResult RedirectToAction(string action, string controller, object routeValues)
        {
            return base.RedirectToAction(action, controller, routeValues);
        }

        public new JsonResult Json(object data, JsonRequestBehavior behavior)
        {
            return base.Json(data, behavior);
        }

        #endregion

        #region Métodos

        protected new ViewResult View(object model)
        {
            var actionName = ControllerContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();

            return View(actionName, model);
        }

        protected void LogError(Exception exception)
        {
            Logger.Error(string.Format("Mensaje: {0} Trace: {1}", exception.Message, exception.StackTrace));
        }

        protected JsonResult MensajeError(string mensaje = "Ocurrio un error al cargar...")
        {
            Response.StatusCode = 404;
            return Json(new JsonResponse { Message = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}