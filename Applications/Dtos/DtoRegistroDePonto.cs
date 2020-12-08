using Entities.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace Applications.Dtos
{
    [Serializable]
    public class DtoRegistroDePonto
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime DataDoRegistro { get; set; }
        public EnumTipoDeRegistro TipoDeRegistro { get; set; }
    }
}
