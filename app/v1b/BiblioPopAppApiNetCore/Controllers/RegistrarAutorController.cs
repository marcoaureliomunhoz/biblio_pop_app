using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPopApp.Aplicacao._DTO;
using BiblioPopApp.Aplicacao.RegistrarAutor;
using BiblioPopApp.Aplicacao.RegistrarAutor.Operacao;
using BiblioPopApp.Aplicacao.RegistrarAutor.Retorno;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiblioPopAppApiNetCore.Controllers
{
    [Produces("application/json")]
    [Route("api/RegistrarAutor")]
    public class RegistrarAutorController : Controller
    {
        RegistrarAutor registrarAutor;

        public RegistrarAutorController(RegistrarAutor registrarEditora)
        {
            this.registrarAutor = registrarEditora;
        }

        // GET: api/RegistrarAutor/ListaAutores
        [HttpGet("ListaAutores")]
        public AoRealizarListaAutores ListaAutores()
        {
            return registrarAutor.Realizar(new ListaAutores());
        }

        // GET: api/RegistrarAutor/LocalizaAutor/5
        [HttpGet("LocalizaAutor/{id}")]
        public AoRealizarLocalizaAutor LocalizaAutor(int id)
        {
            return registrarAutor.Realizar(new LocalizaAutor { AutorId = id });
        }

        // POST: api/RegistrarAutor/NovoAutor
        [HttpPost("NovoAutor")]
        public AoRealizarNovoAutor NovoAutor([FromBody]AutorDTO autor)
        {
            var novoAutor = new NovoAutor
            {
                Nome = autor.Nome,
                Sobrenome = autor.Sobrenome,
                Email = autor.Email
            };
            return registrarAutor.Realizar(novoAutor);
        }

        // PUT: api/RegistrarAutor/AjusteAutor/5
        [HttpPut("AjusteAutor/{id}")]
        public AoRealizarAjusteAutor AjusteAutor(int id, [FromBody]AutorDTO autor)
        {
            var ajusteAutor = new AjusteAutor
            {
                AutorId = autor.AutorId, 
                Nome = autor.Nome,
                Sobrenome = autor.Sobrenome,
                Email = autor.Email
            };
            return registrarAutor.Realizar(ajusteAutor);
        }
    }
}