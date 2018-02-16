using BiblioPopApp.Aplicacao._DTO;
using BiblioPopApp.Aplicacao.RegistrarEditora;
using BiblioPopApp.Aplicacao.RegistrarEditora.Operacao;
using BiblioPopApp.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BiblioPopAppApiNetFull.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EditorasController : ApiController
    {
        RegistrarEditora registrarEditora;

        public EditorasController(RegistrarEditora registrarEditora)
        {
            this.registrarEditora = registrarEditora;
        }

        // GET: api/Editoras
        public RetornoBase<List<EditoraDTO>> Get()
        {
            return registrarEditora.Realizar(new ListaEditoras());
        }

        // GET: api/Editoras/5
        public RetornoBase<EditoraDTO> Get(int id)
        {
            return registrarEditora.Realizar(new LocalizaEditora { EditoraId = id });
        }

        // POST: api/Editoras
        public RetornoBase<int> Post([FromBody]EditoraDTO editora)
        {
            var novaEditora = new NovaEditora
            {
                Nome = editora.Nome,
                Site = editora.Site
            };
            return registrarEditora.Realizar(novaEditora);
        }

        // PUT: api/Editoras/5
        public RetornoBase<bool> Put(int id, [FromBody]EditoraDTO editora)
        {
            var ajusteEditora = new AjusteEditora
            {
                EditoraId = editora.EditoraId,
                Nome = editora.Nome,
                Site = editora.Site
            };
            return registrarEditora.Realizar(ajusteEditora);
        }
    }
}
