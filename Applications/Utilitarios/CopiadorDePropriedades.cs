using Entities.Enumeradores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Applications.Utilitarios
{
    public static class CopiadorDePropriedades
    {

        #region CONSTANTES

        private const BindingFlags FLAGS = BindingFlags.Public
                                 | BindingFlags.Instance
                                 | BindingFlags.GetProperty
                                 | BindingFlags.SetProperty
                                 | BindingFlags.FlattenHierarchy;

        #endregion

        #region MÉTODOS PÚBLICOS

        /// <summary>
        /// Copia um objeto para um tipo especifico(somente propriedades comuns.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto de destino.</typeparam>
        /// <param name="origem">Objeto de origem.</param>
        /// <returns>O objeto de destino com os dados copiados.</returns>
        public static T Copie<T>(this object origem)
        {
            var destino = Activator.CreateInstance(typeof(T));
            CopiePropriedades(origem, destino);
            return (T)destino;
        }

        public static void Copie<T, K>(T origem, K destino)
        {
            CopiePropriedades(origem, destino);
        }

        #endregion

        #region MÉTODOS PRIVADOS

        private static void CopiePropriedadeComuns(object origem, object destino, PropertyInfo propGet, PropertyInfo propSet)
        {
            var valorOrigem = propGet.GetValue(origem, null);

            if (propSet.PropertyType.IsAssignableFrom(propGet.PropertyType))
            {
                propSet.SetValue(destino, valorOrigem, null);
            }
            else
            {
                if (valorOrigem != null)
                {
                    var conversor = TypeDescriptor.GetConverter(valorOrigem);

                    if (conversor != null && conversor.CanConvertTo(propSet.PropertyType))
                    {
                        var valorDestino = conversor.ConvertTo(valorOrigem, propSet.PropertyType);
                        propSet.SetValue(destino, valorDestino, null);
                    }
                }
            }
        }

        private static void CopiePropriedades(object origem, object destino)
        {
            ValideOrigemEDestino(origem, destino);

            var props = origem.GetType().GetProperties(FLAGS);

            foreach (var propGet in props)
            {

                var propSet = destino.GetType().GetProperty(propGet.Name);

                if (propSet != null && propSet.CanWrite)
                {
                    CopiePropriedadeComuns(origem, destino, propGet, propSet);
                }

            }
        }

        private static void ValideOrigemEDestino(object origem, object destino)
        {
            if (origem == null || destino == null)
            {
                throw new ArgumentException("Ambas as instâncias envolvidas na cópia devem ser informadas.");
            }
        }

        #endregion
    }

}
