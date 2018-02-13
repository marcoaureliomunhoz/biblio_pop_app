using BiblioPopApp.Aplicacao._DTO;
using BiblioPopApp.Aplicacao.RegistrarEditora.Operacao;
using BiblioPopApp.Aplicacao.RegistrarLivro.Operacao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiblioPopApp.CasosDeUso.RegistrarLivro
{
    public partial class FormCadastroRegistrarLivro : Form
    {
        private bool carregando = true;
        private int LivroId = 0;
        private List<EditoraDTO> listaEditoras;
        private List<AutorDTO> listaAutoresLivro;
        private List<AutorDTO> listaAutoresDisponiveis;

        public FormCadastroRegistrarLivro(LivroDTO livro)
        {
            InitializeComponent();

            CarregarLivro(livro);
        }

        private void CarregarLivro(LivroDTO livro)
        {
            if (livro != null)
            {
                carregando = true;

                CarregarEditoras();

                LivroId = livro.LivroId;
                txtTitulo.Text = livro.Titulo;
                txtEstante.Text = livro.Estante;
                txtAnoPublicacao.Text = livro.AnoPublicacao.ToString();
                cmbEditora.SelectedItem = livro.Editora;
                listaAutoresLivro = new List<AutorDTO>();
                listaAutoresLivro.AddRange(livro.Autores);
                listboxAutores.DataSource = listaAutoresLivro;

                CarregarAutoresDisponiveis();

                btnSalvar.Visible = false;

                carregando = false;
            }
        }

        private void DesabilitarTela()
        {
            this.Enabled = false;
        }

        private void HabilitarTela()
        {
            this.Enabled = true;
        }

        private void CarregarEditoras()
        {
            var registrarEditora = ControleDependenciaNetFull.Resolve.InstanciaDe<Aplicacao.RegistrarEditora.RegistrarEditora>();
            var retorno = registrarEditora.Realizar(new ListaEditoras());
            listaEditoras = retorno.Valor;
            cmbEditora.DataSource = null;
            cmbEditora.DataSource = listaEditoras;
        }

        private void CarregarAutoresDisponiveis()
        {
            listaAutoresDisponiveis = new List<AutorDTO>();
            var registrarLivro = ControleDependenciaNetFull.Resolve.InstanciaDe<Aplicacao.RegistrarLivro.RegistrarLivro>();
            var retorno = registrarLivro.Realizar(new ListaAutoresDisponiveis() { LivroId = LivroId });
            listaAutoresDisponiveis.AddRange(retorno.Valor);
            if (listaAutoresLivro != null && listaAutoresLivro.Count > 0)
            {
                int autoresAjustados = 0;
                foreach (var autorLivro in listaAutoresLivro)
                {
                    var autorPreDisponivel = listaAutoresDisponiveis.FirstOrDefault(x => x.AutorId == autorLivro.AutorId);
                    if (autorPreDisponivel != null)
                    {
                        autorLivro.Nome = autorPreDisponivel.Nome;
                        autorLivro.Sobrenome = autorPreDisponivel.Sobrenome;
                        autoresAjustados++;
                        listaAutoresDisponiveis.Remove(autorPreDisponivel);
                    }
                }
                if (autoresAjustados > 0)
                {
                    listboxAutores.DataSource = null;
                    listboxAutores.DataSource = listaAutoresLivro;
                }
            }
            cmbAutoresDisponiveis.DataSource = null;
            cmbAutoresDisponiveis.DataSource = listaAutoresDisponiveis;
            btnIncluirAutorDisponivel.Enabled = listaAutoresDisponiveis != null && listaAutoresDisponiveis.Count > 0;
        }

        private void Salvar()
        {
            if (!carregando)
            {
                var podeSalvar = txtTitulo.Text.Length > 0 && cmbEditora.SelectedItem != null && listboxAutores.Items.Count > 0;

                if (LivroId > 0)
                {
                    if (podeSalvar)
                    {
                        SalvarAjuste();
                    }
                }
                else
                {
                    btnSalvar.Enabled = podeSalvar;
                }
            }
        }

        private void SalvarAjuste()
        {
            var registrarLivro = ControleDependenciaNetFull.Resolve.InstanciaDe<Aplicacao.RegistrarLivro.RegistrarLivro>();

            var ajusteLivro = new AjusteLivro();
            ajusteLivro.LivroId = LivroId;
            ajusteLivro.Titulo = txtTitulo.Text;
            ajusteLivro.Estante = txtEstante.Text;
            int anoPublicacaoAux = 0;
            if (int.TryParse(txtAnoPublicacao.Text, out anoPublicacaoAux))
                ajusteLivro.AnoPublicacao = anoPublicacaoAux;

            if (cmbEditora.SelectedItem != null)
            {
                ajusteLivro.Editora = cmbEditora.SelectedItem as EditoraDTO;
            }

            if (listboxAutores.Items.Count > 0)
            {
                foreach (var autor in listboxAutores.Items)
                {
                    ajusteLivro.Autores.Add(autor as AutorDTO);
                }
            }

            var retorno = registrarLivro.Realizar(ajusteLivro);

            BiblioPopAppUtil.ProcessarMensagensRetornoBase(retorno.Mensagem, retorno.Problemas, panelMensagem, lblMensagem, listBoxProblemas);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            DesabilitarTela();

            var registrarLivro = ControleDependenciaNetFull.Resolve.InstanciaDe<Aplicacao.RegistrarLivro.RegistrarLivro>();

            var novoLivro = new NovoLivro();
            novoLivro.Titulo = txtTitulo.Text;
            novoLivro.Estante = txtEstante.Text;
            int anoPublicacaoAux = 0;
            if (int.TryParse(txtAnoPublicacao.Text, out anoPublicacaoAux))
                novoLivro.AnoPublicacao = anoPublicacaoAux;

            if (cmbEditora.SelectedItem != null)
            {
                novoLivro.Editora = cmbEditora.SelectedItem as EditoraDTO;
            }

            if (listboxAutores.Items.Count > 0)
            {
                foreach (var autor in listboxAutores.Items)
                {
                    novoLivro.Autores.Add(autor as AutorDTO);
                }
            }

            var retorno = registrarLivro.Realizar(novoLivro);

            BiblioPopAppUtil.ProcessarMensagensRetornoBase(retorno.Mensagem, retorno.Problemas, panelMensagem, lblMensagem, listBoxProblemas);

            if (retorno.Valor > 0)
            {
                LivroId = retorno.Valor;
                btnSalvar.Visible = false;
            }

            HabilitarTela();
        }

        private void CarregamentoInicial()
        {
            carregando = true;
            LivroId = 0;
            txtTitulo.Clear();
            txtEstante.Clear();
            txtAnoPublicacao.Clear();
            listaAutoresLivro = new List<AutorDTO>();
            CarregarEditoras();
            CarregarAutoresDisponiveis();
            carregando = false;
        }

        private void FormCadastroRegistrarLivro_Load(object sender, EventArgs e)
        {
            if (LivroId<=0) CarregamentoInicial();
        }

        private void btnIncluirAutorDisponivel_Click(object sender, EventArgs e)
        {
            var autor = cmbAutoresDisponiveis.SelectedItem as AutorDTO;
            if (listaAutoresDisponiveis.Remove(autor))
            {
                listaAutoresLivro.Add(autor);
                listboxAutores.DataSource = null;
                listboxAutores.DataSource = listaAutoresLivro;

                cmbAutoresDisponiveis.DataSource = null;
                cmbAutoresDisponiveis.DataSource = listaAutoresDisponiveis;

                Salvar();
            }
            btnIncluirAutorDisponivel.Enabled = listaAutoresDisponiveis.Count > 0;
        }

        private void txtTitulo_KeyUp(object sender, KeyEventArgs e)
        {
            Salvar();
        }

        private void cmbEditora_SelectedIndexChanged(object sender, EventArgs e)
        {
            Salvar();
        }

        private void btnRegistrarEditora_Click(object sender, EventArgs e)
        {
            var editoraId = 0;
            if (cmbEditora.SelectedItem != null)
            {
                editoraId = (cmbEditora.SelectedItem as EditoraDTO).EditoraId;
            }
            var frmPrincipal = new CasosDeUso.RegistrarEditora.FormPrincipalRegistrarEditora();
            frmPrincipal.ShowDialog();
            CarregarEditoras();
            if (editoraId > 0)
            {
                cmbEditora.SelectedItem = listaEditoras.FirstOrDefault(x => x.EditoraId == editoraId);
            }
        }

        private void btnRegistrarAutor_Click(object sender, EventArgs e)
        {
            var frmPrincipal = new CasosDeUso.RegistrarAutor.FormPrincipalRegistrarAutor();
            frmPrincipal.ShowDialog();
            CarregarAutoresDisponiveis();
        }

        private void txtAnoPublicacao_KeyUp(object sender, KeyEventArgs e)
        {
            Salvar();
        }

        private void txtEstante_KeyUp(object sender, KeyEventArgs e)
        {
            Salvar();
        }
    }
}
