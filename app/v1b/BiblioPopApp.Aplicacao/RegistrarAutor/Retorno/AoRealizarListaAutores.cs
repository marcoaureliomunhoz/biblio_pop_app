using BiblioPopApp.Aplicacao._DTO;
using BiblioPopApp.Base;
using System.Collections.Generic;

namespace BiblioPopApp.Aplicacao.RegistrarAutor.Retorno
{
    public class AoRealizarListaAutores : DetalheRetorno
    {
        public List<AutorDTO> Autores { get; set; }
    }
}
