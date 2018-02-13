using System;
using System.Collections.Generic;
using System.Text;

namespace BiblioPopApp.Base
{
    public abstract class DetalheRetorno
    {
        public string Mensagem { get; set; } = "";
        public List<string> Problemas { get; set; }

        public DetalheRetorno()
        {
            Problemas = new List<string>();
        }
    }
}
