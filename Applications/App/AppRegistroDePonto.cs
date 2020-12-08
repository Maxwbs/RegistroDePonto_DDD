using Applications.Dtos;
using Applications.Interfaces;
using Domain.Interfaces.IRegistroDePonto;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Entidades;
using Applications.Conversores;
using System.Linq;
using Entities.Enumeradores;

namespace Applications.App
{
    public class AppRegistroDePonto : InterfaceAppRegistroDePonto
    {
        private readonly InterfaceRegistroDePonto _interfaceRegistroDePonto;
        private ConversorRegistroDePonto _conversorRegistroDePonto;

        public AppRegistroDePonto(InterfaceRegistroDePonto registroDePontoServico)
        {
            _interfaceRegistroDePonto = registroDePontoServico;
        }
        public DtoInconsistencias Salve(DtoRegistroDePonto dtoRegistroDePonto)
        {
            var registroDePonto = ConverdorRegistroDePonto().Converta(dtoRegistroDePonto);
            if (registroDePonto != null)
            {
                var inconsistencias = ValideObjeto(registroDePonto);
                if (!inconsistencias.Notificacoes.Any())
                {
                    _interfaceRegistroDePonto.Salve(registroDePonto);
                }

                return inconsistencias;
            }

            return new DtoInconsistencias();
        }

        public DtoInconsistencias Atualize(DtoRegistroDePonto dtoRegistroDePonto)
        {
            var registroDePonto = ConverdorRegistroDePonto().Converta(dtoRegistroDePonto);

            if (registroDePonto != null)
            {
                var inconsistencias = ValideObjeto(registroDePonto);
                if (!inconsistencias.Notificacoes.Any())
                {
                    _interfaceRegistroDePonto.Atualize(registroDePonto);
                }

                return inconsistencias;
            }

            return new DtoInconsistencias();
        }

        public DtoInconsistencias Remove(DtoRegistroDePonto dtoRegistroDePonto)
        {
            var registroDePonto = _interfaceRegistroDePonto.ObtenhaPorId(dtoRegistroDePonto.Id);
            if (registroDePonto != null)
            {
                var inconsistencias = ValideObjeto(registroDePonto);
                if (!inconsistencias.Notificacoes.Any())
                {
                    _interfaceRegistroDePonto.Deleta(registroDePonto);
                }

                return inconsistencias;
            }

            return new DtoInconsistencias();
        }

        public IList<DtoRegistroDePonto> ObtenhaLista()
        {
            var listaDeRegistroDePonto = _interfaceRegistroDePonto.ObtenhaLista();
            var listaDtosRegistroDoPonto = ConverdorRegistroDePonto().ConvertaLista(listaDeRegistroDePonto);
            return listaDtosRegistroDoPonto;
        }

        public DtoRegistroDePonto ObtenhaPorCodigo(int id)
        {
            var registroDePonto = _interfaceRegistroDePonto.ObtenhaPorId(id);
            var dtoRegistroDoPonto = ConverdorRegistroDePonto().Converta(registroDePonto);
            return dtoRegistroDoPonto;
        }

        private DtoInconsistencias ValideObjeto(RegistroDePonto registroDePonto)
        {
            var dtoInconsistencias = new List<DtoInconsistencias>();

            ValideInformacoesNome(registroDePonto);
            
            ValiDeInformacoesDataDoRegistro(registroDePonto);

            ValideInformacoesDeCpf(registroDePonto);

            ValideInformacoesTipoDeRegistro(registroDePonto);

            registroDePonto.notificacoes.ForEach(x => dtoInconsistencias.Add(new DtoInconsistencias { Mensagem = x.mensagem, NomePropriedade = x.NomePropriedade }));

            return new DtoInconsistencias { Notificacoes = dtoInconsistencias };
        }

        private ConversorRegistroDePonto ConverdorRegistroDePonto()
        {
            return _conversorRegistroDePonto ?? (new ConversorRegistroDePonto());
        }

        private void ValideInformacoesTipoDeRegistro(RegistroDePonto registroDePonto) 
        {
            registroDePonto.ValidaPropriedadeString(registroDePonto.TipoDeRegistro.ToString(), "TipoDeRegistro");
            registroDePonto.ValideValorPropriedadeEnum(typeof(EnumTipoDeRegistro), registroDePonto.TipoDeRegistro.ToString(), "TipoDeRegistro");
        }

        private void ValiDeInformacoesDataDoRegistro(RegistroDePonto registroDePonto) 
        {
            registroDePonto.ValidaPropriedadeString(registroDePonto.DataDoRegistro.ToString(), "DataDoRegistro");
            registroDePonto.ValidaPropriedadeDateTime(registroDePonto.DataDoRegistro, "DataDoRegistro");
        }

        private void ValideInformacoesDeCpf(RegistroDePonto registroDePonto) 
        {
            registroDePonto.ValidaPropriedadeString(registroDePonto.Cpf, "Cpf");

            registroDePonto.ValidaPropriedadeCpf(registroDePonto.Cpf, "Cpf");
        }

        private void ValideInformacoesNome(RegistroDePonto registroDePonto)
        {
            var tamanhoMaximo = 150;
            var tamanhoMinimo = 10;
            registroDePonto.ValideTamanhoPropriedade(registroDePonto.Nome, "Nome", tamanhoMaximo, tamanhoMinimo);
            registroDePonto.ValidaPropriedadeString(registroDePonto.Nome, "Nome");

        }
    }
}
