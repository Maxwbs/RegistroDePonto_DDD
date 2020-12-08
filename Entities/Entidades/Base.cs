using Entities.Notificacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entidades
{
    public class Base : Notifica
    {
        [Display(Name = "Id do usuário")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Cpf")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Cpf { get; set; }
    }
}
