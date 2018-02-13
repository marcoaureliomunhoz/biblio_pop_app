using BiblioPopApp.Base;

namespace BiblioPopApp.Dominio.Descritores
{
    public class TNomePessoa : Descritor
    {
        public const int NomeTamanhoMinimo = 3;
        public const int NomeTamanhoMaximo = 50;
        public string Nome { get; } = "";

        public const int SobrenomeTamanhoMinimo = 3;
        public const int SobrenomeTamanhoMaximo = 100;
        public string Sobrenome { get; } = "";

        public TNomePessoa(string nome, string sobrenome)
        {
            this.ValidarTamanhoMinimo(nome, NomeTamanhoMinimo, $"O número mínimo de caracteres para o nome é de {NomeTamanhoMinimo}");
            this.ValidarTamanhoMaximo(nome, NomeTamanhoMaximo, $"O número máximo de caracteres para o nome é de {NomeTamanhoMaximo}");

            this.ValidarTamanhoMinimo(sobrenome, SobrenomeTamanhoMinimo, $"O número mínimo de caracteres para o sobrenome é de {SobrenomeTamanhoMinimo}");
            this.ValidarTamanhoMaximo(sobrenome, SobrenomeTamanhoMaximo, $"O número máximo de caracteres para o sobrenome é de {SobrenomeTamanhoMaximo}");

            if (this.EstaEmEstadoIntegro())
            {
                Nome = nome;
                Sobrenome = sobrenome;
            }
        }
    }
}
