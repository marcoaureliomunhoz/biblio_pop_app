namespace BiblioPopApp.RepositorioNetFull.Tabelas
{
    public class TabAutor
    {
        public int AutorId { get; set; } = 0;

        public const int NomeTamanhoMaximo = Dominio.Entidades.Autor.NomeTamanhoMaximo;
        public string Nome { get; set; } = "";

        public const int SobrenomeTamanhoMaximo = Dominio.Entidades.Autor.SobrenomeTamanhoMaximo;
        public string Sobrenome { get; set; } = "";

        public const int EmailTamanhoMaximo = Dominio.Entidades.Autor.EmailTamanhoMaximo;
        public string Email { get; set; } = "";

        public TabAutor()
        {
        }

        public TabAutor(int id)
        {
            AutorId = id;
        }
    }
}
