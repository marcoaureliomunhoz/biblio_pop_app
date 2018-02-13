using BiblioPopApp.Aplicacao._DTO;
using BiblioPopApp.Base;
using System.Collections.Generic;

namespace BiblioPopApp.Aplicacao.ConsultarAcervo.Retorno
{
    public class ListaAutores : DetalheRetorno
    {
        public List<AutorDTO> Autores { get; set; }    
    }
}
