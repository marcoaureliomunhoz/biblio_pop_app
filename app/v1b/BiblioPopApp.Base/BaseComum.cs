using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BiblioPopApp.Base
{
    public abstract class BaseComum
    {
        private int _validacoes = 0;
        private List<string> _problemas;
        public IReadOnlyCollection<string> Problemas { get => _problemas; }

        public BaseComum()
        {
            _problemas = new List<string>();
        }

        public bool EstaEmEstadoIntegro()
        {
            if (_validacoes == 0) return false;
            return _problemas.Count == 0;
        }

        private void AdicionarProblema(string mensagem)
        {
            _problemas.Add(mensagem);
        }

        protected void ImportarEstadoDeIntegridade(params BaseComum[] itens)
        {
            foreach (var item in itens)
            {
                _validacoes += item._validacoes;
                _problemas.AddRange(item._problemas);
            }
        }

        protected void ValidarTamanhoMinimo(string valor, int tamanhoMinimo, string mensagem)
        {
            _validacoes++;
            if (string.IsNullOrEmpty(valor) || valor.Length < tamanhoMinimo) AdicionarProblema(mensagem);
        }

        protected void ValidarTamanhoMaximo(string valor, int tamanhoMaximo, string mensagem)
        {
            _validacoes++;
            if (!string.IsNullOrEmpty(valor) && valor.Length > tamanhoMaximo) AdicionarProblema(mensagem);
        }

        protected void ValidarExpressao(string valor, string expressao, string mensagem)
        {
            _validacoes++;
            if (!Regex.IsMatch(valor, expressao, RegexOptions.IgnoreCase)) AdicionarProblema(mensagem);
        }

        protected void ValidarEmail(string valor, string mensagem)
        {
            if (!string.IsNullOrEmpty(valor)) ValidarExpressao(valor, @"[\w+]@[\w+].[\w+]", mensagem);
        }

        protected void ValidarValorMinimo(int valor, int valorMinimo, string mensagem)
        {
            _validacoes++;
            if (valor < valorMinimo) AdicionarProblema(mensagem);
        }

        protected void ValidarInstancia(object instancia, string mensagem)
        {
            _validacoes++;
            if (instancia == null) AdicionarProblema(mensagem);
        }
    }
}
