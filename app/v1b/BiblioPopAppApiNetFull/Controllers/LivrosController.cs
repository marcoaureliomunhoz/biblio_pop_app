using BiblioPopApp.Aplicacao._DTO;
using BiblioPopApp.Aplicacao.RegistrarLivro;
using BiblioPopApp.Aplicacao.RegistrarLivro.Operacao;
using BiblioPopApp.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BiblioPopAppApiNetFull.Controllers
{
    public class LivrosController : ApiController
    {
        RegistrarLivro registrarLivro;

        public LivrosController(RegistrarLivro registrarLivro)
        {
            this.registrarLivro = registrarLivro;
        }

        public RetornoBase<LivroDTO> Get(int id)
        {
            return registrarLivro.Realizar(new LocalizaLivro { LivroId = id });
        }

        [Route("api/autores-disponiveis/{id:int}")]
        public RetornoBase<List<AutorDTO>> GetAutoresDisponiveis(int id)
        {
            return registrarLivro.Realizar(new ListaAutoresDisponiveis { LivroId = id });
        }

        public RetornoBase<int> Post([FromBody]LivroDTO livro)
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

        public RetornoBase<bool> Put(int id, [FromBody]LivroDTO livro)
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
