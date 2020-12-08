using Applications.Utilitarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Applications.Conversores
{
    public abstract class ConversorPadrao<TObjeto, TDto>
        where TObjeto : class
        where TDto : class
    {
        public virtual TObjeto Converta(TDto dto)
        {
            return ConvertaObjeto(dto);
        }

        public virtual TDto Converta(TObjeto objeto)
        {
            return ConvertaDto(objeto);
        }

        public virtual IList<TDto> ConvertaLista(IList<TObjeto> listaDeObjeto)
        {
            return ConvertaListaDto(listaDeObjeto);
        }
        public virtual IList<TObjeto> ConvertaLista(IList<TDto> listaDeDto)
        {
            return ConvertaListaObjeto(listaDeDto);
        }

        private IList<TDto> ConvertaListaDto(IList<TObjeto> listaDeObjeto)
        {
            if (listaDeObjeto == null || listaDeObjeto.Count <= 0)
            {
                return new List<TDto>();
            }

            var listaDeRetorno = new List<TDto>();

            for (int i = 0; i < listaDeObjeto.Count; i++)
            {
                var objeto = listaDeObjeto[i];
                var dto = CopiadorDePropriedades.Copie<TDto>(objeto);
                listaDeRetorno.Add(dto);
            }

            return listaDeRetorno;
        }
        private IList<TObjeto> ConvertaListaObjeto(IList<TDto> listaDeDto)
        {
            if (listaDeDto == null || listaDeDto.Count <= 0)
            {
                return new List<TObjeto>();
            }

            var listaDeRetorno = new List<TObjeto>();

            for (int i = 0; i < listaDeDto.Count; i++)
            {
                var dto = listaDeDto[i];
                var objeto = CopiadorDePropriedades.Copie<TObjeto>(dto);
                listaDeRetorno.Add(objeto);
            }

            return listaDeRetorno;
        }


        private TDto ConvertaDto(TObjeto objeto)
        {
            if (objeto == null)
            {
                return default(TDto);
            }

            var dto = CopiadorDePropriedades.Copie<TDto>(objeto);

            return dto;
        }

        private TObjeto ConvertaObjeto(TDto dto)
        {
            if (dto == null)
            {
                return default(TObjeto);
            }

            var objeto = CopiadorDePropriedades.Copie<TObjeto>(dto);

            return objeto;
        }

    }
}
