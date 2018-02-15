using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPopApp.Aplicacao._DTO;
using BiblioPopApp.Aplicacao.RegistrarAutor;
using BiblioPopApp.Aplicacao.RegistrarAutor.Operacao;
using Microsoft.AspNetCore.Mvc;

namespace BiblioPopAppWebNetCore.Controllers
{
    public class RegistrarAutorController : Controller
    {
        RegistrarAutor registrarAutor;

        public RegistrarAutorController(RegistrarAutor registrarAutor)
        {
            this.registrarAutor = registrarAutor;
        }

        // GET: RegistrarAutor
        public ActionResult Index(string origem)
        {
            var retornoAoRealizarListaAutores = registrarAutor.Realizar(new ListaAutores());
            ViewBag.Mensagem = retornoAoRealizarListaAutores.Mensagem;
            ViewBag.Problemas = retornoAoRealizarListaAutores.Problemas;
            ViewBag.Origem = origem;
            return View(retornoAoRealizarListaAutores.Autores);
        }

        public ActionResult Cadastro(int id, string origem)
        {
            ViewBag.Mensagem = "";
            ViewBag.Problemas = null;
            ViewBag.Origem = origem;

            var model = new AutorDTO();
            if (id > 0)
            {
                var retornoAoRealizarLocalizaAutor = registrarAutor.Realizar(new LocalizaAutor { AutorId = id });
                ViewBag.Mensagem = retornoAoRealizarLocalizaAutor.Mensagem;
                ViewBag.Problemas = retornoAoRealizarLocalizaAutor.Problemas;
                model = retornoAoRealizarLocalizaAutor.Autor;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Cadastro(AutorDTO autor, string origem)
        {
            ViewBag.Mensagem = "";
            ViewBag.Problemas = null;
            ViewBag.Origem = origem;

            if (autor.AutorId > 0)
            {
                var ajusteAutor = new AjusteAutor
                {
                    AutorId = autor.AutorId,
                    Nome = autor.Nome,
                    Sobrenome = autor.Sobrenome,
                    Email = autor.Email
                };
                var retornoAoRelizarAjusteAutor = registrarAutor.Realizar(ajusteAutor);
                if (retornoAoRelizarAjusteAutor.AlterouComSucesso || retornoAoRelizarAjusteAutor.Problemas.Count <= 0)
                {
                    ViewBag.Mensagem = "Autor ajustado com sucesso.";
                }
                else
                {
                    ViewBag.Mensagem = retornoAoRelizarAjusteAutor.Mensagem;
                    ViewBag.Problemas = retornoAoRelizarAjusteAutor.Problemas;
                }
            }
            else
            {
                var novoAutor = new NovoAutor
                {
                    Nome = autor.Nome,
                    Sobrenome = autor.Sobrenome,
                    Email = autor.Email
                };
                var retornoAoRelizarNovoAutor = registrarAutor.Realizar(novoAutor);
                if (retornoAoRelizarNovoAutor.AutorId > 0)
                {
                    autor.AutorId = retornoAoRelizarNovoAutor.AutorId;
                    if (string.IsNullOrEmpty(origem))
                        return RedirectToAction(nameof(Index));
                }
                ViewBag.Mensagem = retornoAoRelizarNovoAutor.Mensagem;
                ViewBag.Problemas = retornoAoRelizarNovoAutor.Problemas;
            }

            return View(autor);
        }
    }
}