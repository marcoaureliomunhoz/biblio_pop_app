using BiblioPopApp.Aplicacao.Descritores;
using BiblioPopApp.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiblioPopApp.Aplicacao.ConsultarAcervo.ProtocolosDeRetorno
{
    public class RetornoDeRealizarListaAutoresEmConsultarAcervo : DetalheRetorno
    {
        public List<TAutor> Autores { get; set; }    
    }
}
