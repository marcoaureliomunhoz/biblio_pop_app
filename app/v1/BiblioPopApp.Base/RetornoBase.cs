using System.Collections.Generic;

namespace BiblioPopApp.Base
{
    public class RetornoBase<T>
    {
        public string Mensagem { get; set; } = "";
        public List<string> Problemas { get; set; }
        public T Valor { get; set; }

        public RetornoBase()
        {
            Problemas = new List<string>();
        }
    }
}
