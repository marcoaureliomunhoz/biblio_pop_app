using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPopApp.Aplicacao._DTO;
using BiblioPopApp.Aplicacao.RegistrarLivro;
using BiblioPopApp.Aplicacao.RegistrarLivro.Operacao;
using BiblioPopApp.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiblioPopAppApiNetCore.Controllers
{
    [Produces("application/json")]
    [Route("api/RegistrarLivro")]
    public class RegistrarLivroController : Controller
    {
        RegistrarLivro registrarLivro;

        public RegistrarLivroController(RegistrarLivro registrarLivro)
        {
            this.registrarLivro = registrarLivro;
        }

        // GET: api/RegistrarLivro/LocalizaLivro/5
        [HttpGet("LocalizaLivro/{id}")]
        public RetornoBase<LivroDTO> LocalizaLivro(int id)
        {
            return registrarLivro.Realizar(new LocalizaLivro { LivroId = id });
        }

        // GET: api/RegistrarLivro/ListaAutoresDisponiveis/5
        [HttpGet("ListaAutoresDisponiveis/{id}")]
        public RetornoBase<List<AutorDTO>> ListaAutoresDisponiveis(int id)
        {
            return registrarLivro.Realizar(new ListaAutoresDisponiveis { LivroId = id });
        }

        // POST: api/RegistrarLivro/NovoLivro
        [HttpPost("NovoLivro")]
        public RetornoBase<int> NovoLivro([FromBody]LivroDTO livro)
        {
            var novoLivro = new NovoLivro
            {
                Titulo = livro.Titulo,
                Estante = livro.Estante,
                AnoPublicacao = livro.AnoPublicacao,
                EditoraId = livro.Editora != null ? livro.Editora.EditoraId : 0,
                Editora = livro.Editora,
                Autores = livro.Autores
            };
            return registrarLivro.Realizar(novoLivro);
        }

        // PUT: api/RegistrarLivro/AjusteLivro/5
        [HttpPut("AjusteLivro/{id}")]
        public RetornoBase<bool> AjusteLivro(int id, [FromBody]LivroDTO livro)
        {
            var ajusteLivro = new AjusteLivro
            {
                LivroId = livro.LivroId,
                Titulo = livro.Titulo,
                Estante = livro.Estante,
                AnoPublicacao = livro.AnoPublicacao,
                EditoraId = livro.Editora != null ? livro.Editora.EditoraId : 0,
                Editora = livro.Editora,
                Autores = livro.Autores
            };
            return registrarLivro.Realizar(ajusteLivro);
        }
    }
}