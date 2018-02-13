namespace BiblioPopApp
{
    partial class FormApp
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuApp = new System.Windows.Forms.MenuStrip();
            this.menuApp_Registrar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuApp_Registrar_Autor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuApp_Registrar_Editora = new System.Windows.Forms.ToolStripMenuItem();
            this.menuApp_Registrar_Livro = new System.Windows.Forms.ToolStripMenuItem();
            this.menuApp_Consultar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuApp_Consultar_Acervo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuApp.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuApp
            // 
            this.menuApp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuApp_Registrar,
            this.menuApp_Consultar});
            this.menuApp.Location = new System.Drawing.Point(0, 0);
            this.menuApp.Name = "menuApp";
            this.menuApp.Size = new System.Drawing.Size(650, 24);
            this.menuApp.TabIndex = 0;
            this.menuApp.Text = "menuPrincipal";
            // 
            // menuApp_Registrar
            // 
            this.menuApp_Registrar.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuApp_Registrar_Autor,
            this.menuApp_Registrar_Editora,
            this.menuApp_Registrar_Livro});
            this.menuApp_Registrar.Name = "menuApp_Registrar";
            this.menuApp_Registrar.Size = new System.Drawing.Size(65, 20);
            this.menuApp_Registrar.Text = "Registrar";
            // 
            // menuApp_Registrar_Autor
            // 
            this.menuApp_Registrar_Autor.Name = "menuApp_Registrar_Autor";
            this.menuApp_Registrar_Autor.Size = new System.Drawing.Size(152, 22);
            this.menuApp_Registrar_Autor.Text = "Autor";
            this.menuApp_Registrar_Autor.Click += new System.EventHandler(this.menuApp_Registrar_Autor_Click);
            // 
            // menuApp_Registrar_Editora
            // 
            this.menuApp_Registrar_Editora.Name = "menuApp_Registrar_Editora";
            this.menuApp_Registrar_Editora.Size = new System.Drawing.Size(152, 22);
            this.menuApp_Registrar_Editora.Text = "Editora";
            this.menuApp_Registrar_Editora.Click += new System.EventHandler(this.menuApp_Registrar_Editora_Click);
            // 
            // menuApp_Registrar_Livro
            // 
            this.menuApp_Registrar_Livro.Name = "menuApp_Registrar_Livro";
            this.menuApp_Registrar_Livro.Size = new System.Drawing.Size(152, 22);
            this.menuApp_Registrar_Livro.Text = "Livro";
            this.menuApp_Registrar_Livro.Click += new System.EventHandler(this.menuApp_Registrar_Livro_Click);
            // 
            // menuApp_Consultar
            // 
            this.menuApp_Consultar.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuApp_Consultar_Acervo});
            this.menuApp_Consultar.Name = "menuApp_Consultar";
            this.menuApp_Consultar.Size = new System.Drawing.Size(70, 20);
            this.menuApp_Consultar.Text = "Consultar";
            // 
            // menuApp_Consultar_Acervo
            // 
            this.menuApp_Consultar_Acervo.Name = "menuApp_Consultar_Acervo";
            this.menuApp_Consultar_Acervo.Size = new System.Drawing.Size(152, 22);
            this.menuApp_Consultar_Acervo.Text = "Acervo";
            this.menuApp_Consultar_Acervo.Click += new System.EventHandler(this.menuApp_Consultar_Acervo_Click);
            // 
            // FormApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 474);
            this.Controls.Add(this.menuApp);
            this.MainMenuStrip = this.menuApp;
            this.Name = "FormApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BiblioPopApp";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuApp.ResumeLayout(false);
            this.menuApp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuApp;
        private System.Windows.Forms.ToolStripMenuItem menuApp_Registrar;
        private System.Windows.Forms.ToolStripMenuItem menuApp_Registrar_Autor;
        private System.Windows.Forms.ToolStripMenuItem menuApp_Registrar_Editora;
        private System.Windows.Forms.ToolStripMenuItem menuApp_Registrar_Livro;
        private System.Windows.Forms.ToolStripMenuItem menuApp_Consultar;
        private System.Windows.Forms.ToolStripMenuItem menuApp_Consultar_Acervo;
    }
}

