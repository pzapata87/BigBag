﻿using System;
using System.Configuration;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using log4net.Config;
using SIGCOMT.Common.Constantes;
using SIGCOMT.DTO.AutoMapper;
using SIGCOMT.IoC;
using SIGCOMT.Persistence;
using SIGCOMT.Persistence.EntityFramework;
using SIGCOMT.Resources;
using SIGCOMT.Resources.CustomModelMetadata;
using Usuario = SIGCOMT.Domain.Usuario;

namespace SIGCOMT.Web
{
    // Nota: para obtener instrucciones sobre cómo habilitar el modo clásico de IIS6 o IIS7, 
    // visite http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            Database.SetInitializer(new ContextInitializer());
            Database.SetInitializer<DbContextBase>(null);

            PersistenceConfigurator.Configure("DefaultConnection", typeof (Usuario).Assembly, typeof (ConnectionFactory).Assembly);
            StructuremapMvc.Start();


            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AutoMapperConfiguration.Configure();

            XmlConfigurator.Configure();

            ModelMetadataProviders.Current = new ConventionalModelMetadataProvider(true, typeof(Master));
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = int.Parse(ConfigurationManager.AppSettings[MasterConstantes.TimeOutSession]);
        }
    }
}