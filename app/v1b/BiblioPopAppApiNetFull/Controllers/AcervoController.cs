using BiblioPopApp.Aplicacao._DTO;
using BiblioPopApp.Aplicacao.ConsultarAcervo;
using BiblioPopApp.Aplicacao.ConsultarAcervo.Operacao;
using BiblioPopApp.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BiblioPopAppApiNetFull.Controllers
{
    public class AcervoController : ApiController
    {
        ConsultarAcervo consultarAcervo;

        public AcervoController(ConsultarAcervo consultarAcervo)
        {
            this.consultarAcervo = consultarAcervo;
        }

        [Route("api/acervo/{editoraId:int}/{autorId:int}/{titulo}")]
        public RetornoBase<List<LivroAcervoDTO>> Get(int editoraId, int autorId, string titulo)
        {
            return consultarAcervo.Realizar(new ListaAcervo() { Titulo = titulo, EditoraId = editoraId, AutorId = autorId });
        }

        [Route("api/acervo/{editoraId:int}/{autorId:int}")]
        public RetornoBase<List<LivroAcervoDTO>> Get(int editoraId, int autorId)
        {
            return consultarAcervo.Realizar(new ListaAcervo() { Titulo = "", EditoraId = editoraId, AutorId = autorId });
        }

    }
}
