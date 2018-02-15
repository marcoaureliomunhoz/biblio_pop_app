using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPopApp.Aplicacao._DTO;
using BiblioPopApp.Aplicacao.RegistrarEditora;
using BiblioPopApp.Aplicacao.RegistrarEditora.Operacao;
using Microsoft.AspNetCore.Mvc;

namespace BiblioPopAppWebNetCore.Controllers
{
    public class RegistrarEditoraController : Controller
    {
        RegistrarEditora registrarEditora;

        public RegistrarEditoraController(RegistrarEditora registrarEditora)
        {
            this.registrarEditora = registrarEditora;
        }

        // GET: RegistrarEditora
        public ActionResult Index(string origem)
        {
            var retornoAoRealizarListaEditoras = registrarEditora.Realizar(new ListaEditoras());
            ViewBag.Mensagem = retornoAoRealizarListaEditoras.Mensagem;
            ViewBag.Problemas = retornoAoRealizarListaEditoras.Problemas;
            ViewBag.Origem = origem;
            return View(retornoAoRealizarListaEditoras.Valor);
        }

        public ActionResult Cadastro(int id, string origem)
        {
            ViewBag.Mensagem = "";
            ViewBag.Problemas = null;
            ViewBag.Origem = origem;

            var model = new EditoraDTO();
            if (id > 0)
            {
                var retornoAoRealizarLocalizaEditora = registrarEditora.Realizar(new LocalizaEditora { EditoraId = id });
                ViewBag.Mensagem = retornoAoRealizarLocalizaEditora.Mensagem;
                ViewBag.Problemas = retornoAoRealizarLocalizaEditora.Problemas;
                model = retornoAoRealizarLocalizaEditora.Valor;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Cadastro(EditoraDTO editora, string origem)
        {
            ViewBag.Mensagem = "";
            ViewBag.Problemas = null;
            ViewBag.Origem = origem;

            if (editora.EditoraId > 0)
            {
                var ajusteEditora = new AjusteEditora
                {
                    EditoraId = editora.EditoraId,
                    Nome = editora.Nome,
                    Site = editora.Site
                };
                var retornoAoRelizarAjusteEditora = registrarEditora.Realizar(ajusteEditora);
                if (retornoAoRelizarAjusteEditora.Valor || retornoAoRelizarAjusteEditora.Problemas.Count <= 0)
                {
                    ViewBag.Mensagem = "Editora ajustada com sucesso.";
                }
                else
                {
                    ViewBag.Mensagem = retornoAoRelizarAjusteEditora.Mensagem;
                    ViewBag.Problemas = retornoAoRelizarAjusteEditora.Problemas;
                }
            }
            else
            {
                var novaEditora = new NovaEditora
                {
                    Nome = editora.Nome,
                    Site = editora.Site
                };
                var retornoAoRelizarNovaEditora = registrarEditora.Realizar(novaEditora);
                if (retornoAoRelizarNovaEditora.Valor > 0)
                {
                    editora.EditoraId = retornoAoRelizarNovaEditora.Valor;
                    if (string.IsNullOrEmpty(origem))
                        return RedirectToAction(nameof(Index));
                }
                ViewBag.Mensagem = retornoAoRelizarNovaEditora.Mensagem;
                ViewBag.Problemas = retornoAoRelizarNovaEditora.Problemas;
            }

            return View(editora);
        }
    }
}