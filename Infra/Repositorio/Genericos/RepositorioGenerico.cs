using Domain.Interfaces.Generics;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Infra.Repositorio.Genericos
{
    public class RepositorioGenerico<T> : IGeneric<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<Contexto> _OptionsBuilder;

        public RepositorioGenerico()
        {
            _OptionsBuilder = new DbContextOptions<Contexto>();
        }

        public void Salve(T Obj)
        {
            using (var banco = new Contexto(_OptionsBuilder))
            {
                banco.Set<T>().Add(Obj);
                banco.SaveChanges();
            }
        }

        public void Atualize(T Obj)
        {
            using (var banco = new Contexto(_OptionsBuilder))
            {
                banco.Set<T>().Update(Obj);
                banco.SaveChangesAsync();
            }
        }

        public void Deleta(T Obj)
        {
            using (var banco = new Contexto(_OptionsBuilder))
            {
                banco.Set<T>().Remove(Obj);
                banco.SaveChangesAsync();
            }
        }

        public T ObtenhaPorId(int id)
        {
            using (var banco = new Contexto(_OptionsBuilder))
            {
                var objeto = banco.Set<T>().Find(id);

                banco.Database.CloseConnection();

                return objeto;
            }
        }
        public IList<T> ObtenhaLista()
        {
            using (var banco = new Contexto(_OptionsBuilder))
            {
                return banco.Set<T>().AsNoTracking().ToList();
            }
        }

        #region Dispose
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
        ~RepositorioGenerico()
        {
            Dispose(false);

        }
        #endregion
    }
}
