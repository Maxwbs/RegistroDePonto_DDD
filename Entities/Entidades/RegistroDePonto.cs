using Entities.Enumeradores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace Entities.Entidades
{
    [Table("REGISTRODEPONTO")]
    public class RegistroDePonto : Base
    {
        [Display(Name = "Data do Registro")]
        public DateTime DataDoRegistro { get; set; }

        [Display(Name = "Tipo de Registro")]
        public EnumTipoDeRegistro TipoDeRegistro { get; set; }
    }
}
