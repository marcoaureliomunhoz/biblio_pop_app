using BiblioPopApp.Aplicacao._DTO;
using BiblioPopApp.Aplicacao.RegistrarAutor;
using BiblioPopApp.Aplicacao.RegistrarAutor.Operacao;
using BiblioPopApp.Aplicacao.RegistrarAutor.Retorno;
using BiblioPopApp.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BiblioPopAppApiNetFull.Controllers
{
    public class AutoresController : ApiController
    {
        RegistrarAutor registrarAutor;

        public AutoresController(RegistrarAutor registrarAutor)
        {
            this.registrarAutor = registrarAutor;
        }

        public AoRealizarListaAutores Get()
        {
            return registrarAutor.Realizar(new ListaAutores());
        }

        public AoRealizarLocalizaAutor Get(int id)
        {
            return registrarAutor.Realizar(new LocalizaAutor { AutorId = id });
        }

        public AoRealizarNovoAutor Post([FromBody]AutorDTO autor)
        {
            var novoAutor = new NovoAutor
            {
                Nome = autor.Nome,
                Sobrenome = autor.Sobrenome,
                Email = autor.Email
            };
            return registrarAutor.Realizar(novoAutor);
        }

        public AoRealizarAjusteAutor Put(int id, [FromBody]AutorDTO autor)
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
