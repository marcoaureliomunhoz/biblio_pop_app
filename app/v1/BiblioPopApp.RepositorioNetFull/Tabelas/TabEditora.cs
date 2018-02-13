using System.ComponentModel.DataAnnotations;

namespace BiblioPopApp.RepositorioNetFull.Tabelas
{
    public class TabEditora
    {
        [Key]
        public int EditoraId { get; set; } = 0;

        [MaxLength(Dominio.Entidades.Editora.NomeTamanhoMaximo)]
        public string Nome { get; set; } = "";

        [MaxLength(Dominio.Entidades.Editora.SiteTamanhoMaximo)]
        public string Site { get; set; } = "";

        public TabEditora()
        {
        }

        public TabEditora(int id)
        {
            EditoraId = id;
        }
    }
}
