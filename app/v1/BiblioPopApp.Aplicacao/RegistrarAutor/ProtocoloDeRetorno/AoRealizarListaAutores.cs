using BiblioPopApp.Aplicacao.Descritores;
using BiblioPopApp.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiblioPopApp.Aplicacao.RegistrarAutor.ProtocoloDeRetorno
{
    public class AoRealizarListaAutores : DetalheRetorno
    {
        public List<TAutor> Autores { get; set; }
    }
}
