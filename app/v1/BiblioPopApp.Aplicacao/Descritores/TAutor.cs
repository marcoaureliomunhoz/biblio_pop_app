namespace BiblioPopApp.Aplicacao.Descritores
{
    public class TAutor
    {
        public int AutorId { get; set; } = 0;
        public string Nome { get; set; } = "";
        public string Sobrenome { get; set; } = "";
        public string Email { get; set; } = "";

        public override string ToString()
        {
            return Nome + (!string.IsNullOrEmpty(Sobrenome) ? " " + Sobrenome : "");
        }
    }
}
