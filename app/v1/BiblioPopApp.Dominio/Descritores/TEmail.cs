using BiblioPopApp.Base;

namespace BiblioPopApp.Dominio.Descritores
{
    public class TEmail : Descritor
    {
        public const int EnderecoTamanhoMaximo = 300;
        public string Endereco { get; } = "";

        public TEmail(string endereco) 
        {
            this.ValidarTamanhoMaximo(endereco, EnderecoTamanhoMaximo, $"O número máximo de caracteres permitidos para o e-mail é de {EnderecoTamanhoMaximo}.");
            if (!string.IsNullOrEmpty(endereco))
                this.ValidarExpressao(endereco, @"[\w+]@[\w+].[\w+]", "O e-mail informado não é válido.");

            if (this.EstaEmEstadoIntegro())
            {
                Endereco = endereco;
            }
        }
    }
}
