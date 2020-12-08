using Entities.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Configuracao
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
            
        }

        public DbSet<RegistroDePonto> RegistroDePonto { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Se não estiver configurado no projeto IU pega deginição de chame do json configurado
            if (!optionsBuilder.IsConfigured) 
            {
                optionsBuilder.UseSqlServer(GetStringConectionConfig());
            }            

            base.OnConfiguring(optionsBuilder);
        }

        private string GetStringConectionConfig()
        {
            string strCon = "Data Source=.\\SQLEXPRESS;Initial Catalog=CLI_REGISTRODEPONTO;Integrated Security=False;User ID=sa;Password=fpw;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;MultipleActiveResultSets=True;";

            return strCon;
        }

    }
}
