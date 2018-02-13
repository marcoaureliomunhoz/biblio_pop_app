namespace BiblioPopApp.CasosDeUso.ConsultarAcervo
{
    partial class FormPrincipalConsultarAcervo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMenu = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridLivros = new System.Windows.Forms.DataGridView();
            this.Titulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AnoPublicacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Editora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Autoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbEditora = new System.Windows.Forms.ComboBox();
            this.cmbAutor = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLivros)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.Gray;
            this.panelMenu.Controls.Add(this.cmbAutor);
            this.panelMenu.Controls.Add(this.label3);
            this.panelMenu.Controls.Add(this.cmbEditora);
            this.panelMenu.Controls.Add(this.label2);
            this.panelMenu.Controls.Add(this.txtTitulo);
            this.panelMenu.Controls.Add(this.label1);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(784, 130);
            this.panelMenu.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusBar
            // 
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(16, 17);
            this.statusBar.Text = "...";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gridLivros);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 130);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 409);
            this.panel1.TabIndex = 3;
            // 
            // gridLivros
            // 
            this.gridLivros.AllowUserToDeleteRows = false;
            this.gridLivros.BackgroundColor = System.Drawing.Color.White;
            this.gridLivros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLivros.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Titulo,
            this.Estante,
            this.AnoPublicacao,
            this.Editora,
            this.Autoria});
            this.gridLivros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLivros.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridLivros.Location = new System.Drawing.Point(0, 0);
            this.gridLivros.MultiSelect = false;
            this.gridLivros.Name = "gridLivros";
            this.gridLivros.ReadOnly = true;
            this.gridLivros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridLivros.Size = new System.Drawing.Size(784, 409);
            this.gridLivros.TabIndex = 0;
            this.gridLivros.DoubleClick += new System.EventHandler(this.gridLivros_DoubleClick);
            this.gridLivros.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridLivros_KeyUp);
            // 
            // Titulo
            // 
            this.Titulo.DataPropertyName = "Titulo";
            this.Titulo.HeaderText = "Título";
            this.Titulo.Name = "Titulo";
            this.Titulo.ReadOnly = true;
            this.Titulo.Width = 300;
            // 
            // Estante
            // 
            this.Estante.DataPropertyName = "Estante";
            this.Estante.HeaderText = "Estante";
            this.Estante.Name = "Estante";
            this.Estante.ReadOnly = true;
            // 
            // AnoPublicacao
            // 
            this.AnoPublicacao.DataPropertyName = "AnoPublicacao";
            this.AnoPublicacao.HeaderText = "Ano Publicação";
            this.AnoPublicacao.Name = "AnoPublicacao";
            this.AnoPublicacao.ReadOnly = true;
            // 
            // Editora
            // 
            this.Editora.DataPropertyName = "Editora";
            this.Editora.HeaderText = "Editora";
            this.Editora.Name = "Editora";
            this.Editora.ReadOnly = true;
            this.Editora.Width = 250;
            // 
            // Autoria
            // 
            this.Autoria.DataPropertyName = "Autoria";
            this.Autoria.HeaderText = "Autoria";
            this.Autoria.Name = "Autoria";
            this.Autoria.ReadOnly = true;
            this.Autoria.Width = 400;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Título";
            // 
            // txtTitulo
            // 
            this.txtTitulo.Location = new System.Drawing.Point(15, 30);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(618, 24);
            this.txtTitulo.TabIndex = 1;
            this.txtTitulo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTitulo_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Editora";
            // 
            // cmbEditora
            // 
            this.cmbEditora.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEditora.FormattingEnabled = true;
            this.cmbEditora.Location = new System.Drawing.Point(15, 84);
            this.cmbEditora.Name = "cmbEditora";
            this.cmbEditora.Size = new System.Drawing.Size(306, 26);
            this.cmbEditora.TabIndex = 3;
            this.cmbEditora.SelectedIndexChanged += new System.EventHandler(this.cmbEditora_SelectedIndexChanged);
            // 
            // cmbAutor
            // 
            this.cmbAutor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAutor.FormattingEnabled = true;
            this.cmbAutor.Location = new System.Drawing.Point(327, 84);
            this.cmbAutor.Name = "cmbAutor";
            this.cmbAutor.Size = new System.Drawing.Size(306, 26);
            this.cmbAutor.TabIndex = 5;
            this.cmbAutor.SelectedIndexChanged += new System.EventHandler(this.cmbAutor_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(324, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Autor";
            // 
            // FormPrincipalConsultarAcervo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelMenu);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormPrincipalConsultarAcervo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consultar Acervo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormPrincipalConsultarAcervo_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridLivros)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gridLivros;
        private System.Windows.Forms.DataGridViewTextBoxColumn Titulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estante;
        private System.Windows.Forms.DataGridViewTextBoxColumn AnoPublicacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Editora;
        private System.Windows.Forms.DataGridViewTextBoxColumn Autoria;
        private System.Windows.Forms.ComboBox cmbAutor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbEditora;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label label1;
    }
}