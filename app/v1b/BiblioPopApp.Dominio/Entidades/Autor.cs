using BiblioPopApp.Base;

namespace BiblioPopApp.Dominio.Entidades
{
    public class Autor : Entidade
    {
        public int AutorId { get; }

        public const int NomeTamanhoMinimo = 3;
        public const int NomeTamanhoMaximo = 50;
        public string Nome { get; }

        public const int SobrenomeTamanhoMinimo = 3;
        public const int SobrenomeTamanhoMaximo = 100;
        public string Sobrenome { get; }

        public const int EmailTamanhoMaximo = 300;
        public string Email { get; }

        protected Autor() { }

        public Autor(int id, string nome, string sobrenome, string email)
        {
            this.ValidarTamanhoMinimo(nome, NomeTamanhoMinimo, $"O número mínimo de caracteres para o nome é de {NomeTamanhoMinimo}");
            this.ValidarTamanhoMaximo(nome, NomeTamanhoMaximo, $"O número máximo de caracteres para o nome é de {NomeTamanhoMaximo}");

            this.ValidarTamanhoMinimo(sobrenome, SobrenomeTamanhoMinimo, $"O número mínimo de caracteres para o sobrenome é de {SobrenomeTamanhoMinimo}");
            this.ValidarTamanhoMaximo(sobrenome, SobrenomeTamanhoMaximo, $"O número máximo de caracteres para o sobrenome é de {SobrenomeTamanhoMaximo}");

            this.ValidarTamanhoMaximo(email, EmailTamanhoMaximo, $"O número máximo de caracteres permitidos para o e-mail é de {EmailTamanhoMaximo}.");
            this.ValidarEmail(email, "O e-mail informado não é válido.");

            if (this.EstaEmEstadoIntegro())
            {
                AutorId = id;
                Nome = nome;
                Sobrenome = sobrenome;
                Email = email;                
            }
        }        
    }
}
