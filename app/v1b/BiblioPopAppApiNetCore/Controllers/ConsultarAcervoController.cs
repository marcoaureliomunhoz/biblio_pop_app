using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPopApp.Aplicacao._DTO;
using BiblioPopApp.Aplicacao.ConsultarAcervo;
using BiblioPopApp.Aplicacao.ConsultarAcervo.Operacao;
using BiblioPopApp.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiblioPopAppApiNetCore.Controllers
{
    [Produces("application/json")]
    [Route("api/ConsultarAcervo")]
    public class ConsultarAcervoController : Controller
    {
        ConsultarAcervo consultarAcervo;

        public ConsultarAcervoController(ConsultarAcervo consultarAcervo)
        {
            this.consultarAcervo = consultarAcervo;
        }

        // GET: api/ConsultarAcervo/ListaAutores
        [HttpGet("ListaAutores")]
        public BiblioPopApp.Aplicacao.ConsultarAcervo.Retorno.ListaAutores ListaAutores()
        {
            return consultarAcervo.Realizar(new ListaAutores());
        }

        // GET: api/ConsultarAcervo/ListaEditoras
        [HttpGet("ListaEditoras")]
        public RetornoBase<List<EditoraDTO>> ListaEditoras()
        {
            return consultarAcervo.Realizar(new ListaEditoras());
        }

        // GET: api/ConsultarAcervo/ListaAcervo/{editoraId:int}/{autorId:int}/{titulo}
        [HttpGet("ListaAcervo/{editoraId:int}/{autorId:int}/{titulo}")]
        public RetornoBase<List<LivroAcervoDTO>> ListaAcervo(int editoraId, int autorId, string titulo)
        {
            return consultarAcervo.Realizar(new ListaAcervo() { Titulo = titulo, EditoraId = editoraId, AutorId = autorId });
        }

        // GET: api/ConsultarAcervo/ListaAcervo/{editoraId:int}/{autorId:int}
        [HttpGet("ListaAcervo/{editoraId:int}/{autorId:int}")]
        public RetornoBase<List<LivroAcervoDTO>> ListaAcervo(int editoraId, int autorId)
        {
            return consultarAcervo.Realizar(new ListaAcervo() { Titulo = "", EditoraId = editoraId, AutorId = autorId });
        }
    }
}