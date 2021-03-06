﻿using System;
using System.Linq;
using System.Linq.Expressions;
using SIGCOMT.Aspects;
using SIGCOMT.BusinessLogic.Interfaces;
using SIGCOMT.Common;
using SIGCOMT.Common.Enum;
using SIGCOMT.Domain;
using SIGCOMT.Persistence.Aspects;
using SIGCOMT.Repository;

namespace SIGCOMT.BusinessLogic
{
    public class UsuarioBL : IUsuarioBL
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioBL(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [TryCatch(ExceptionTypeExpected = typeof (Exception), RethrowException = true)]
        public Usuario Get(Expression<Func<Usuario, bool>> where)
        {
            return _usuarioRepository.FindOne(where);
        }

        [TryCatch(ExceptionTypeExpected = typeof (Exception), RethrowException = true)]
        [CommitsOperation]
        public void Add(Usuario entity)
        {
            _usuarioRepository.Add(entity);
        }

        [TryCatch(ExceptionTypeExpected = typeof (Exception), RethrowException = true)]
        [CommitsOperation]
        public void Update(Usuario entity)
        {
            _usuarioRepository.Update(entity);
        }

        [TryCatch(ExceptionTypeExpected = typeof (Exception), RethrowException = true)]
        public Usuario GetById(int id)
        {
            return _usuarioRepository.FindOne(p => p.Id == id);
        }

        [TryCatch(ExceptionTypeExpected = typeof (Exception), RethrowException = true)]
        public int Count(Expression<Func<Usuario, bool>> where)
        {
            return _usuarioRepository.Count(where);
        }

        [TryCatch(ExceptionTypeExpected = typeof (Exception), RethrowException = true)]
        public IQueryable<Usuario> GetAll(FilterParameters<Usuario> parameters)
        {
            return _usuarioRepository.FindAllPaging(parameters);
        }

        [TryCatch(ExceptionTypeExpected = typeof (Exception), RethrowException = true)]
        public IQueryable<Usuario> FindAll(Expression<Func<Usuario, bool>> where)
        {
            return _usuarioRepository.FindAll(where);
        }

        [TryCatch(ExceptionTypeExpected = typeof (Exception), RethrowException = true)]
        public Usuario ValidateUser(string username, string password)
        {
            Usuario user = Get(p => p.UserName == username && p.Estado == (int) TipoEstado.Activo);

            if (user?.UserName == null)
            {
                return null;
            }

            string enc = Encriptador.Encriptar(password);

            return enc == user.Password ? user : null;
        }
    }
}