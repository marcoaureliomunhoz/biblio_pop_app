using BiblioPopApp.Aplicacao.ConsultarAcervo.Operacoes;
using BiblioPopApp.Aplicacao.Descritores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiblioPopApp.CasosDeUso.ConsultarAcervo
{
    public partial class FormPrincipalConsultarAcervo : Form
    {
        private bool carregando = true;

        public FormPrincipalConsultarAcervo()
        {
            InitializeComponent();
            gridLivros.AutoGenerateColumns = false;
        }

        private void gridLivros_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                var formCadastro = new CasosDeUso.RegistrarLivro.FormCadastroRegistrarLivro(null);
                formCadastro.ShowDialog();
                CarregarLivros();
            }
        }

        private void AlterarLivro()
        {
            if (gridLivros.Rows.Count > 0 && gridLivros.CurrentRow != null)
            {
                var tlivro = gridLivros.CurrentRow.DataBoundItem as Aplicacao.Descritores.TLivro;
                var formCadastro = new CasosDeUso.RegistrarLivro.FormCadastroRegistrarLivro(tlivro);
                formCadastro.ShowDialog();
                CarregarLivros();
            }
        }

        private void CarregarLivros()
        {
            var consultarAcerto = ControleDependenciaNetFull.Resolve.InstanciaDe<Aplicacao.ConsultarAcervo.ConsultarAcervo>();

            var retorno = consultarAcerto.Realizar(new ListaAcervo()
            {
                Titulo = txtTitulo.Text, 
                EditoraId = cmbEditora.SelectedItem != null ? (cmbEditora.SelectedItem as TEditora).EditoraId : 0,
                AutorId = cmbAutor.SelectedItem != null ? (cmbAutor.SelectedItem as TAutor).AutorId : 0
            });

            BiblioPopAppUtil.ProcessarMensagensRetornoBase(retorno.Mensagem, retorno.Problemas, null, null, null);

            gridLivros.DataSource = retorno.Valor;

            statusBar.Text = "Nenhum livro no acervo";

            if (retorno.Valor != null)
            {
                var aux = retorno.Valor.Count > 1 ? retorno.Valor.Count.ToString() + " livros encontrados no acervo" : "Um livro encontrado no acervo";
                statusBar.Text = aux;
            }
        }

        private void CarregarEditoras()
        {
            var consultarAcervo = ControleDependenciaNetFull.Resolve.InstanciaDe<Aplicacao.ConsultarAcervo.ConsultarAcervo>();
            var retorno = consultarAcervo.Realizar(new ListaEditoras());
            var listaEditoras = new List<TEditora>();
            listaEditoras.Add(new TEditora()
            {
                EditoraId = 0,
                Nome = "Todas"
            });
            listaEditoras.AddRange(retorno.Valor);
            cmbEditora.DataSource = null;
            cmbEditora.DataSource = listaEditoras;
        }

        private void CarregarAutores()
        {
            var consultarAcervo = ControleDependenciaNetFull.Resolve.InstanciaDe<Aplicacao.ConsultarAcervo.ConsultarAcervo>();
            var retorno = consultarAcervo.Realizar(new ListaAutores());
            var listaAutores = new List<TAutor>();
            listaAutores.Add(new TAutor()
            {
                AutorId = 0,
                Nome = "Todos"
            });
            listaAutores.AddRange(retorno.Valor);
            cmbAutor.DataSource = null;
            cmbAutor.DataSource = listaAutores;
        }

        private void FormPrincipalConsultarAcervo_Load(object sender, EventArgs e)
        {
            carregando = true;
            CarregarLivros();
            CarregarEditoras();
            CarregarAutores();
            carregando = false;
        }

        private void gridLivros_DoubleClick(object sender, EventArgs e)
        {
            AlterarLivro();
        }

        private void txtTitulo_KeyUp(object sender, KeyEventArgs e)
        {
            if (!carregando) CarregarLivros();
        }

        private void cmbEditora_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!carregando) CarregarLivros();
        }

        private void cmbAutor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!carregando) CarregarLivros();
        }
    }
}
