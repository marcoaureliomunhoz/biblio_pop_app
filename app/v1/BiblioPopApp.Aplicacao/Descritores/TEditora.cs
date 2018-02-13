namespace BiblioPopApp.Aplicacao.Descritores
{
    public class TEditora
    {
        public int EditoraId { get; set; } = 0;
        public string Nome { get; set; } = "";
        public string Site { get; set; } = "";

        public override string ToString()
        {
            return Nome;
        }
    }
}
