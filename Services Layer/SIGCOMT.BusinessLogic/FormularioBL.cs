using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SIGCOMT.Aspects;
using SIGCOMT.BusinessLogic.Interfaces;
using SIGCOMT.Common;
using SIGCOMT.Common.Enum;
using SIGCOMT.Domain;
using SIGCOMT.Repository;

namespace SIGCOMT.BusinessLogic
{
    [TryCatch(ExceptionTypeExpected = typeof (Exception), RethrowException = true)]
    public class FormularioBL : IFormularioBL
    {
        private readonly IFormularioRepository _formularioRepository;

        public FormularioBL(IFormularioRepository formularioRepository)
        {
            _formularioRepository = formularioRepository;
        }

        public Formulario GetById(int id)
        {
            return _formularioRepository.FindOne(id);
        }

        public IQueryable<Formulario> FindAll(Expression<Func<Formulario, bool>> where)
        {
            return _formularioRepository.FindAll(where);
        }

        public int Count(Expression<Func<Formulario, bool>> where)
        {
            return _formularioRepository.Count(where);
        }

        public IQueryable<Formulario> GetAll(FilterParameters<Formulario> parameters)
        {
            return _formularioRepository.FindAllPaging(parameters);
        }
    }
}