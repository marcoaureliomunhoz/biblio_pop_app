using BiblioPopApp.Dominio.Entidades;

namespace BiblioPopApp.Aplicacao._DTO
{
    public class AutorDTO
    {
        public int AutorId { get; set; } = 0;
        public string Nome { get; set; } = "";
        public string Sobrenome { get; set; } = "";
        public string Email { get; set; } = "";

        public override string ToString()
        {
            return Nome + (!string.IsNullOrEmpty(Sobrenome) ? " " + Sobrenome : "");
        }

        public Autor Fabricar()
        {
            return new Autor(AutorId, Nome, Sobrenome, Email);
        }

        public static AutorDTO Fabricar(Autor autor)
        {
            if (autor == null) return new AutorDTO();
            return new AutorDTO
            {
                AutorId = autor.AutorId,
                Nome = autor.Nome,
                Sobrenome = autor.Sobrenome,
                Email = autor.Email
            };
        }
    }
}
