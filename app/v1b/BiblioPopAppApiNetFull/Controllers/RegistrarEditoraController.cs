using BiblioPopApp.Aplicacao._DTO;
using BiblioPopApp.Aplicacao.RegistrarEditora;
using BiblioPopApp.Aplicacao.RegistrarEditora.Operacao;
using BiblioPopApp.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BiblioPopAppApiNetFull.Controllers
{
    public class RegistrarEditoraController : ApiController
    {
        RegistrarEditora registrarEditora;

        public RegistrarEditoraController(RegistrarEditora registrarEditora)
        {
            this.registrarEditora = registrarEditora;
        }

        [AcceptVerbs("GET")]
        public RetornoBase<List<EditoraDTO>> ListaEditoras()
        {
            return registrarEditora.Realizar(new ListaEditoras());
        }

        [HttpGet]
        public RetornoBase<EditoraDTO> LocalizaEditora(int editoraId)
        {
            return registrarEditora.Realizar(new LocalizaEditora { EditoraId = editoraId });
        }

        /*
        [OutputCache(NoStore = true, Duration = 0)]
        [System.Web.Http.HttpPost]
        public JsonResult NovaEditora(EditoraDTO editora)
        {
            var novaEditora = new NovaEditora
            {
                Nome = editora.Nome,
                Site = editora.Site
            };
            return Json(registrarEditora.Realizar(novaEditora), JsonRequestBehavior.AllowGet);
        }

        [OutputCache(NoStore = true, Duration = 0)]
        [System.Web.Http.HttpPost]
        public JsonResult AjusteEditora(EditoraDTO editora)
        {
            var ajusteEditora = new AjusteEditora
            {
                EditoraId = editora.EditoraId,
                Nome = editora.Nome,
                Site = editora.Site
            };
            return Json(registrarEditora.Realizar(ajusteEditora), JsonRequestBehavior.AllowGet);
        }
        */
    }
}