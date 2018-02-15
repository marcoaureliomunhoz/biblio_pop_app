using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPopApp.Aplicacao._DTO;
using BiblioPopApp.Aplicacao.RegistrarAutor;
using BiblioPopApp.Aplicacao.RegistrarAutor.Operacao;
using BiblioPopApp.Aplicacao.RegistrarEditora;
using BiblioPopApp.Aplicacao.RegistrarEditora.Operacao;
using BiblioPopApp.Aplicacao.RegistrarLivro;
using BiblioPopApp.Aplicacao.RegistrarLivro.Operacao;
using Microsoft.AspNetCore.Mvc;

namespace BiblioPopAppWebNetCore.Controllers
{
    public class RegistrarLivroController : Controller
    {
        RegistrarLivro registrarLivro;
        RegistrarEditora registrarEditora;
        RegistrarAutor registrarAutor;

        public RegistrarLivroController(RegistrarLivro registrarLivro, RegistrarEditora registrarEditora, RegistrarAutor registrarAutor)
        {
            this.registrarLivro = registrarLivro;
            this.registrarEditora = registrarEditora;
            this.registrarAutor = registrarAutor;
        }

        // GET: RegistrarLivro
        public ActionResult Cadastro(int id, string origem)
        {
            ViewBag.Mensagem = "";
            ViewBag.Problemas = null;
            ViewBag.Origem = origem;

            var model = new LivroDTO();
            if (id > 0)
            {
                var retornoAoRealizarLocalizaLivro = registrarLivro.Realizar(new LocalizaLivro { LivroId = id });
                ViewBag.Mensagem = retornoAoRealizarLocalizaLivro.Mensagem;
                ViewBag.Problemas = retornoAoRealizarLocalizaLivro.Problemas;
                model = retornoAoRealizarLocalizaLivro.Valor;
            }

            PrepareCadastro(id);

            return View(model);
        }

        private void PrepareCadastro(int livroId)
        {
            var retornoAoRealizarListaEditoras = registrarEditora.Realizar(new ListaEditoras());
            ViewBag.ListaEditoras = retornoAoRealizarListaEditoras.Valor;

            var retornoAoRealizarListaAutoresDisponiveis = registrarLivro.Realizar(new ListaAutoresDisponiveis() { LivroId = livroId });
            ViewBag.ListaAutoresDisponiveis = retornoAoRealizarListaAutoresDisponiveis.Valor;
        }

        [HttpPost]
        public ActionResult Cadastro(LivroDTO livro, string origem, int EditoraId, string Autoria)
        {
            ViewBag.Mensagem = "";
            ViewBag.Problemas = null;
            ViewBag.Origem = origem;

            livro.EditoraId = EditoraId;

            if (EditoraId > 0)
            {
                var retornoAoRealizarLocalizaEditora = registrarEditora.Realizar(new LocalizaEditora() { EditoraId = EditoraId });
                livro.Editora = retornoAoRealizarLocalizaEditora.Valor;
            }

            if (!string.IsNullOrEmpty(Autoria))
            {
                var aux = "";
                var ids = Autoria.Split(',');
                var retornoAoRealizarListaAutores = registrarAutor.Realizar(new ListaAutores());
                foreach (var autor in retornoAoRealizarListaAutores.Autores)
                {
                    aux = autor.AutorId.ToString();
                    if (!string.IsNullOrEmpty(ids.FirstOrDefault(x => x == aux)))
                    {
                        livro.Autores.Add(autor);
                    }
                }
            }

            if (livro.LivroId > 0)
            {
                var ajusteLivro = new AjusteLivro();
                ajusteLivro.LivroId = livro.LivroId;
                ajusteLivro.Titulo = livro.Titulo;
                ajusteLivro.Estante = livro.Estante;
                ajusteLivro.AnoPublicacao = livro.AnoPublicacao;
                ajusteLivro.Editora = livro.Editora;
                ajusteLivro.Autores = livro.Autores;
                var retornoAoRealizarAjusteLivro = registrarLivro.Realizar(ajusteLivro);
                if (retornoAoRealizarAjusteLivro.Problemas.Count > 0)
                {
                    ViewBag.Mensagem = retornoAoRealizarAjusteLivro.Mensagem;
                    ViewBag.Problemas = retornoAoRealizarAjusteLivro.Problemas;
                }
                else
                {
                    ViewBag.Mensagem = "Ajuste realizado com sucesso.";
                }
            }
            else
            {
                var novoLivro = new NovoLivro();
                novoLivro.Titulo = livro.Titulo;
                novoLivro.Estante = livro.Estante;
                novoLivro.AnoPublicacao = livro.AnoPublicacao;
                novoLivro.Editora = livro.Editora;
                novoLivro.Autores = livro.Autores;
                var retornoAoRealizarNovoLivro = registrarLivro.Realizar(novoLivro);
                if (retornoAoRealizarNovoLivro.Problemas.Count > 0)
                {
                    ViewBag.Mensagem = retornoAoRealizarNovoLivro.Mensagem;
                    ViewBag.Problemas = retornoAoRealizarNovoLivro.Problemas;
                }
                else if (retornoAoRealizarNovoLivro.Valor > 0)
                {
                    livro.LivroId = retornoAoRealizarNovoLivro.Valor;
                    ViewBag.Mensagem = "Livro registrado com sucesso.";
                }
            }

            PrepareCadastro(livro.LivroId);

            return View(livro);
        }

        [ResponseCache(Duration = 0, NoStore = true)]
        public JsonResult ListaEditoras()
        {
            var retornoAoRealizarListaEditoras = registrarEditora.Realizar(new ListaEditoras());
            return Json(retornoAoRealizarListaEditoras.Valor);
        }
    }
}