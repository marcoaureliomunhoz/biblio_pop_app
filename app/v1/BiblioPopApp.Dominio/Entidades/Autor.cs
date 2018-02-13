using BiblioPopApp.Base;
using BiblioPopApp.Dominio.Descritores;

namespace BiblioPopApp.Dominio.Entidades
{
    public class Autor : Entidade
    {
        public int AutorId { get; }

        public const int NomeTamanhoMinimo = TNomePessoa.NomeTamanhoMinimo;
        public const int NomeTamanhoMaximo = TNomePessoa.NomeTamanhoMaximo;
        public const int SobrenomeTamanhoMinimo = TNomePessoa.SobrenomeTamanhoMinimo;
        public const int SobrenomeTamanhoMaximo = TNomePessoa.SobrenomeTamanhoMaximo;
        public TNomePessoa Nome { get; }

        public const int EmailTamanhoMaximo = TEmail.EnderecoTamanhoMaximo;
        public TEmail Email { get; }

        protected Autor() { }

        public Autor(int id, TNomePessoa nome, TEmail email)
        {
            this.ImportarEstadoDeIntegridade(nome, email);

            if (this.EstaEmEstadoIntegro())
            {
                AutorId = id;
                Nome = nome;
                Email = email;                
            }
        }        
    }
}
