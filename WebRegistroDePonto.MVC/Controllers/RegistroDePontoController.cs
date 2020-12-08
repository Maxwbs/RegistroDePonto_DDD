using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Applications.Dtos;
using Applications.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebRegistroDePonto.MVC.Controllers
{
    public class RegistroDePontoController : Controller
    {
        private readonly InterfaceAppRegistroDePonto _interfaceAppRegistroDePonto;

        public RegistroDePontoController(InterfaceAppRegistroDePonto interfaceAppRegistroDePonto)
        {
            _interfaceAppRegistroDePonto = interfaceAppRegistroDePonto;
        }

        public ActionResult Index()
        {
            return View(_interfaceAppRegistroDePonto.ObtenhaLista());
        }

        public ActionResult Details(int id)
        {
            var dtoRegistroDePonto = _interfaceAppRegistroDePonto.ObtenhaPorCodigo(id);
            return View(dtoRegistroDePonto);
        }

        public ActionResult Create()
        {
            CarregaTipoDeRegistro();
            var dataDefault = DateTime.Now;
            var dto = new DtoRegistroDePonto { DataDoRegistro = new DateTime(dataDefault.Year, dataDefault.Month, dataDefault.Day, dataDefault.Hour, dataDefault.Minute, 0) };
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DtoRegistroDePonto dtoRegistroDePonto)
        {
            try
            {
                CarregaTipoDeRegistro();

                var inconsistencias = _interfaceAppRegistroDePonto.Salve(dtoRegistroDePonto);
                
                TrateListaDeInconsistencias(inconsistencias);

                if (inconsistencias.Notificacoes.Any()) 
                {
                    return View(dtoRegistroDePonto);
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // GET: RegistroDePontoController/Edit/5
        public ActionResult Edit(int id)
        {
            CarregaTipoDeRegistro();
            return View(_interfaceAppRegistroDePonto.ObtenhaPorCodigo(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DtoRegistroDePonto dtoRegistroDePonto)
        {
            try
            {
                CarregaTipoDeRegistro();
                var inconsistencias = _interfaceAppRegistroDePonto.Atualize(dtoRegistroDePonto);
                TrateListaDeInconsistencias(inconsistencias);
                
                if (inconsistencias.Notificacoes.Any())
                {
                    return View(dtoRegistroDePonto);
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View(_interfaceAppRegistroDePonto.ObtenhaPorCodigo(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, DtoRegistroDePonto dtoRegistroDePonto)
        {
            try
            {
                var inconsistencias =_interfaceAppRegistroDePonto.Remove(dtoRegistroDePonto);
                
                TrateListaDeInconsistencias(inconsistencias);
                
                if (inconsistencias.Notificacoes.Any())
                {
                    return View(dtoRegistroDePonto);
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        private void TrateListaDeInconsistencias(DtoInconsistencias inconsistencias)
        {
            if (inconsistencias.Notificacoes.Any())
            {
                foreach (var item in inconsistencias.Notificacoes)
                {
                    ModelState.AddModelError(item.NomePropriedade, item.Mensagem);
                }
            }
        }

        private void CarregaTipoDeRegistro()
        {
            var listaTipoDespesa = new List<SelectListItem>();
            listaTipoDespesa.Add(new SelectListItem { Text = "Entrada", Value = "1" });
            listaTipoDespesa.Add(new SelectListItem { Text = "Saída", Value = "2" });
            ViewData["TipoDeRegistro"] = listaTipoDespesa;
        }
    }
}
