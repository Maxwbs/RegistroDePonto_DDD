using System;
using System.Collections.Generic;
using System.Text;

namespace Applications.Dtos
{
    [Serializable]
    public class DtoInconsistencias
    {
        public DtoInconsistencias()
        {
            Notificacoes = new List<DtoInconsistencias>();
        }

        public string NomePropriedade { get; set; }

        
        public string Mensagem { get; set; }

        
        public List<DtoInconsistencias> Notificacoes;
    }
}
