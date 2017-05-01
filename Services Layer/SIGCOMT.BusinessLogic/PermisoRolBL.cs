using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SIGCOMT.Aspects;
using SIGCOMT.BusinessLogic.Interfaces;
using SIGCOMT.Common;
using SIGCOMT.Domain;
using SIGCOMT.Repository;

namespace SIGCOMT.BusinessLogic
{
    [TryCatch(ExceptionTypeExpected = typeof (Exception), RethrowException = true)]
    public class PermisoRolBL : IPermisoRolBL
    {
        private readonly IPermisoRolRepository _permisoRolRepository;

        public PermisoRolBL(IPermisoRolRepository permisoRolRepository)
        {
            _permisoRolRepository = permisoRolRepository;
        }

        public PermisoRol GetById(long id)
        {
            return _permisoRolRepository.FindOne(p => p.Id == id);
        }

        public PermisoRol Get(Expression<Func<PermisoRol, bool>> where)
        {
            return _permisoRolRepository.FindOne(where);
        }

        public IEnumerable<PermisoRol> GetAll()
        {
            return _permisoRolRepository.FindAll();
        }

        public IEnumerable<PermisoRol> GetAll(Expression<Func<PermisoRol, bool>> where)
        {
            return _permisoRolRepository.FindAll(where);
        }

        public void Delete(PermisoRol entity)
        {
            _permisoRolRepository.Delete(entity);
        }

        public int Count(Expression<Func<PermisoRol, bool>> where)
        {
            return _permisoRolRepository.Count(where);
        }

        public IQueryable<PermisoRol> GetAll(FilterParameters<PermisoRol> parameters)
        {
            return _permisoRolRepository.FindAllPaging(parameters);
        }
    }
}