using BiblioPopApp.Dominio.Entidades;

namespace BiblioPopApp.Aplicacao._DTO
{
    public class EditoraDTO
    {
        public int EditoraId { get; set; } = 0;
        public string Nome { get; set; } = "";
        public string Site { get; set; } = "";

        public override string ToString()
        {
            return Nome;
        }

        internal Editora Fabricar()
        {
            return new Editora(EditoraId, Nome, Site);
        }

        internal static EditoraDTO Fabricar(Editora editora)
        {
            if (editora==null) return new EditoraDTO();
            return new EditoraDTO
            {
                EditoraId = editora.EditoraId,
                Nome = editora.Nome,
                Site = editora.Site
            };
        }
    }
}
