using System;
using System.Collections.Generic;
using System.Linq;
using Applications.Dtos;
using Applications.Interfaces;
using Entities.Enumeradores;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiRegistroDePonto.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class RegistroDePontoController : ControllerBase
    {
        private readonly InterfaceAppRegistroDePonto _interfaceAppRegistroDePonto;

        public RegistroDePontoController(InterfaceAppRegistroDePonto interfaceAppRegistroDePonto)
        {
            _interfaceAppRegistroDePonto = interfaceAppRegistroDePonto;
        }

        [HttpGet]
        public ActionResult<List<DtoRegistroDePonto>> Get()
        {
            var lista = _interfaceAppRegistroDePonto.ObtenhaLista();

            if (lista == null || lista.Count <= 0)
            {
                return NotFound();
            }

            return Ok(lista);
        }

        [HttpGet("{id}")]
        public ActionResult<DtoRegistroDePonto> Get(int id)
        {
            var dto = _interfaceAppRegistroDePonto.ObtenhaPorCodigo(id);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult Post([FromBody] DtoRegistroDePonto dtoRegistroDePonto)
        {
            try
            {
                if (dtoRegistroDePonto == null)
                {
                    return NotFound();
                }

                var dto = _interfaceAppRegistroDePonto.ObtenhaPorCodigo(dtoRegistroDePonto.Id);

                if (dto == null)
                {
                    var inconsistencia = _interfaceAppRegistroDePonto.Salve(dtoRegistroDePonto);
                    if (!inconsistencia.Notificacoes.Any())
                    {
                        return Ok("Registro de ponto cadastrado com sucesso!");
                    }
                    else
                    {
                        return Ok(inconsistencia.Notificacoes.ToList());
                    }
                }

                return Ok("Item já cadastrado!");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut]
        public ActionResult Put([FromBody] DtoRegistroDePonto dtoRegistroDePonto)
        {
            try
            {
                if (dtoRegistroDePonto == null)
                {
                    return NotFound();
                }

                var dto = _interfaceAppRegistroDePonto.ObtenhaPorCodigo(dtoRegistroDePonto.Id);

                if (dto != null)
                {
                    var inconsistencia = _interfaceAppRegistroDePonto.Atualize(dtoRegistroDePonto);

                    if (!inconsistencia.Notificacoes.Any())
                    {
                        return Ok("Registro de ponto atualizado com sucesso!");
                    }
                    else
                    {
                        return Ok(inconsistencia.Notificacoes.ToList());
                    }
                }

                return NotFound();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete()]
        public ActionResult Delete([FromBody] DtoRegistroDePonto dtoRegistroDePonto)
        {
            try
            {
                if (dtoRegistroDePonto == null)
                {
                    return NotFound();
                }

                var dto = _interfaceAppRegistroDePonto.ObtenhaPorCodigo(dtoRegistroDePonto.Id);

                if (dto != null)
                {

                    var inconsistencia = _interfaceAppRegistroDePonto.Remove(dtoRegistroDePonto);

                    if (!inconsistencia.Notificacoes.Any())
                    {
                        return Ok("Registro de ponto Removido com sucesso!");
                    }
                    else
                    {
                        return Ok(inconsistencia.Notificacoes.ToList());
                    }
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
