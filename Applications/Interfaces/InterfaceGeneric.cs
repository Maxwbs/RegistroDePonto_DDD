using System;
using System.Collections.Generic;
using System.Text;

namespace Applications.Interfaces
{
    public interface InterfaceGeneric<TDto, TDtoInconsistencias> 
        where TDto : class
        where TDtoInconsistencias : class                                        
    {
        TDtoInconsistencias Salve(TDto dto);

        TDtoInconsistencias Atualize(TDto dto);

        TDtoInconsistencias Remove(TDto dto);

        TDto ObtenhaPorCodigo(int id);

        IList<TDto> ObtenhaLista();
    }
}
