﻿using System.Collections.Generic;
using SIGCOMT.Common.Enum;

namespace SIGCOMT.DataBase.Generator
{
    public static class UrlOperationManager
    {
        static UrlOperationManager()
        {
            OperationsUrl = new Dictionary<TipoOperacion, string>
            {
                 {TipoOperacion.HomeOperation, "~/Home/Index"},
                {TipoOperacion.UsuarioOperation, "~/Usuario/Index"}
            };
        }

        public static Dictionary<TipoOperacion, string> OperationsUrl { get; set; }
    }
}