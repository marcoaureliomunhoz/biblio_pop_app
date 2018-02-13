using BiblioPopApp.Base;
using BiblioPopApp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiblioPopApp.Dominio.Repositorios.ProtocoloDeRetorno
{
    public class AoListarDeRepositorioAutor : DetalheRetorno
    {
        public ICollection<Autor> Autores { get; set; }
    }
}
