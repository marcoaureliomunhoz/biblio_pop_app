using BiblioPopApp.Base;
using BiblioPopApp.Dominio.Entidades;
using System.Collections.Generic;

namespace BiblioPopApp.Dominio.Repositorios.Retorno
{
    public class AoListarDeRepositorioAutor : DetalheRetorno
    {
        public ICollection<Autor> Autores { get; set; }
    }
}
