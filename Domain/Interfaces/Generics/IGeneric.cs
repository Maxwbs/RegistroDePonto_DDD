using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Generics
{
    public interface IGeneric<T> where T: class
    {
        void Salve(T Objecto);
        void Atualize(T Objeto);
        void Deleta(T Objeto);
        T ObtenhaPorId(int id);
        IList<T> ObtenhaLista();
    }
}
