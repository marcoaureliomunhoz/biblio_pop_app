using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPopApp.Aplicacao._DTO;
using BiblioPopApp.Aplicacao.RegistrarEditora;
using BiblioPopApp.Aplicacao.RegistrarEditora.Operacao;
using BiblioPopApp.Base;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiblioPopAppApiNetCore.Controllers
{
    [Produces("application/json")]
    [Route("api/RegistrarEditora")]
    public class RegistrarEditoraController : Controller
    {
        RegistrarEditora registrarEditora;

        public RegistrarEditoraController(RegistrarEditora registrarEditora)
        {
            this.registrarEditora = registrarEditora;
        }

        // GET: api/RegistrarEditora/ListaEditoras
        [HttpGet("ListaEditoras")]
        public RetornoBase<List<EditoraDTO>> ListaEditoras()
        {
            return registrarEditora.Realizar(new ListaEditoras());
        }

        // GET: api/RegistrarEditora/LocalizaEditora/5
        [HttpGet("LocalizaEditora/{id}")]        
        public RetornoBase<EditoraDTO> LocalizaEditora(int id)
        {
            return registrarEditora.Realizar(new LocalizaEditora { EditoraId = id });
        }

        // POST: api/RegistrarEditora/NovaEditora
        [HttpPost("NovaEditora")]
        public RetornoBase<int> NovaEditora([FromBody]EditoraDTO editora)
        {
            var novaEditora = new NovaEditora
            {
                Nome = editora.Nome,
                Site = editora.Site
            };
            return registrarEditora.Realizar(novaEditora);
        }

        // PUT: api/RegistrarEditora/AjusteEditora/5
        [HttpPut("AjusteEditora/{id}")]
        public RetornoBase<bool> AjusteEditora(int id, [FromBody]EditoraDTO editora)
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