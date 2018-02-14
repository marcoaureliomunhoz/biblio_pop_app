using BiblioPopApp.Aplicacao.ConsultarAcervo;
using BiblioPopApp.Aplicacao.ConsultarAcervo.Operacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BiblioPopAppWebNetFull.Controllers
{
    public class ConsultarAcervoController : Controller
    {
        ConsultarAcervo consultarAcervo;

        public ConsultarAcervoController(ConsultarAcervo consultarAcervo)
        {
            this.consultarAcervo = consultarAcervo;
        }

        // GET: ConsultarAcervo
        public ActionResult Index()
        {
            var retornoAoRealizarConsultaAcervo = consultarAcervo.Realizar(new ListaAcervo());
            ViewBag.Mensagem = retornoAoRealizarConsultaAcervo.Mensagem;
            ViewBag.Problemas = retornoAoRealizarConsultaAcervo.Problemas;
            ViewBag.Titulo = "";
            ViewBag.Editora = 0;
            ViewBag.ListaEditoras = consultarAcervo.Realizar(new ListaEditoras()).Valor;
            ViewBag.Autor = 0;
            ViewBag.ListaAutores = consultarAcervo.Realizar(new ListaAutores()).Autores;
            return View(retornoAoRealizarConsultaAcervo.Valor);
        }

        [HttpPost]
        public ActionResult Index(string titulo, int editora, int autor)
        {
            var listaAcervo = new ListaAcervo
            {
                Titulo = titulo,
                EditoraId = editora,
                AutorId = autor
            };
            var retornoAoRealizarConsultaAcervo = consultarAcervo.Realizar(listaAcervo);
            ViewBag.Mensagem = retornoAoRealizarConsultaAcervo.Mensagem;
            ViewBag.Problemas = retornoAoRealizarConsultaAcervo.Problemas;
            ViewBag.Titulo = titulo;
            ViewBag.Editora = editora;
            ViewBag.ListaEditoras = consultarAcervo.Realizar(new ListaEditoras()).Valor;
            ViewBag.Autor = autor;
            ViewBag.ListaAutores = consultarAcervo.Realizar(new ListaAutores()).Autores;
            return View(retornoAoRealizarConsultaAcervo.Valor);
        }
    }
}