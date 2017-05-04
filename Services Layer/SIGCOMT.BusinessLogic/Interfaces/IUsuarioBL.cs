using System;
using System.Linq;
using System.Linq.Expressions;
using SIGCOMT.BusinessLogic.Core;
using SIGCOMT.Domain;

namespace SIGCOMT.BusinessLogic.Interfaces
{
    public interface IUsuarioBL : IPaging<Usuario>
    {
        Usuario Get(Expression<Func<Usuario, bool>> where);
        void Add(Usuario entity);
        void Update(Usuario entity);
        Usuario GetById(int id);
        IQueryable<Usuario> FindAll(Expression<Func<Usuario, bool>> where);
        Usuario ValidateUser(string username, string password);
    }
}