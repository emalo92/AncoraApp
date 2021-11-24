namespace Contabilita
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.movimenti = new System.Windows.Forms.Button();
            this.ricercaFornitori = new System.Windows.Forms.Button();
            this.labelHome = new System.Windows.Forms.Label();
            this.nuovaFattura = new System.Windows.Forms.Button();
            this.nuovoPagamento = new System.Windows.Forms.Button();
            this.nuovaNota = new System.Windows.Forms.Button();
            this.modificaFattura = new System.Windows.Forms.Button();
            this.modificaPagamento = new System.Windows.Forms.Button();
            this.modificaNota = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.modificaNota);
            this.panel1.Controls.Add(this.modificaPagamento);
            this.panel1.Controls.Add(this.modificaFattura);
            this.panel1.Controls.Add(this.nuovaNota);
            this.panel1.Controls.Add(this.nuovoPagamento);
            this.panel1.Controls.Add(this.nuovaFattura);
            this.panel1.Controls.Add(this.labelHome);
            this.panel1.Controls.Add(this.ricercaFornitori);
            this.panel1.Controls.Add(this.movimenti);
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 425);
            this.panel1.TabIndex = 1;
            // 
            // movimenti
            // 
            this.movimenti.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.movimenti.Location = new System.Drawing.Point(105, 42);
            this.movimenti.Name = "movimenti";
            this.movimenti.Size = new System.Drawing.Size(197, 67);
            this.movimenti.TabIndex = 0;
            this.movimenti.Text = "Movimenti";
            this.movimenti.UseVisualStyleBackColor = true;
            // 
            // ricercaFornitori
            // 
            this.ricercaFornitori.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ricercaFornitori.Location = new System.Drawing.Point(470, 42);
            this.ricercaFornitori.Name = "ricercaFornitori";
            this.ricercaFornitori.Size = new System.Drawing.Size(189, 67);
            this.ricercaFornitori.TabIndex = 1;
            this.ricercaFornitori.Text = "Ricerca Fornitori";
            this.ricercaFornitori.UseVisualStyleBackColor = true;
            // 
            // labelHome
            // 
            this.labelHome.AutoSize = true;
            this.labelHome.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.labelHome.Location = new System.Drawing.Point(12, 145);
            this.labelHome.Name = "labelHome";
            this.labelHome.Size = new System.Drawing.Size(119, 21);
            this.labelHome.TabIndex = 2;
            this.labelHome.Text = "Azioni Rapide :";
            // 
            // nuovaFattura
            // 
            this.nuovaFattura.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.nuovaFattura.Location = new System.Drawing.Point(53, 192);
            this.nuovaFattura.Name = "nuovaFattura";
            this.nuovaFattura.Size = new System.Drawing.Size(167, 54);
            this.nuovaFattura.TabIndex = 3;
            this.nuovaFattura.Text = "Nuova fattura";
            this.nuovaFattura.UseVisualStyleBackColor = true;
            // 
            // nuovoPagamento
            // 
            this.nuovoPagamento.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.nuovoPagamento.Location = new System.Drawing.Point(320, 192);
            this.nuovoPagamento.Name = "nuovoPagamento";
            this.nuovoPagamento.Size = new System.Drawing.Size(167, 54);
            this.nuovoPagamento.TabIndex = 4;
            this.nuovoPagamento.Text = "Nuovo Pagamento";
            this.nuovoPagamento.UseVisualStyleBackColor = true;
            // 
            // nuovaNota
            // 
            this.nuovaNota.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.nuovaNota.Location = new System.Drawing.Point(577, 192);
            this.nuovaNota.Name = "nuovaNota";
            this.nuovaNota.Size = new System.Drawing.Size(167, 54);
            this.nuovaNota.TabIndex = 5;
            this.nuovaNota.Text = "Nuova Nota";
            this.nuovaNota.UseVisualStyleBackColor = true;
            // 
            // modificaFattura
            // 
            this.modificaFattura.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.modificaFattura.Location = new System.Drawing.Point(53, 298);
            this.modificaFattura.Name = "modificaFattura";
            this.modificaFattura.Size = new System.Drawing.Size(167, 54);
            this.modificaFattura.TabIndex = 6;
            this.modificaFattura.Text = "Modifica Fattura";
            this.modificaFattura.UseVisualStyleBackColor = true;
            // 
            // modificaPagamento
            // 
            this.modificaPagamento.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.modificaPagamento.Location = new System.Drawing.Point(320, 298);
            this.modificaPagamento.Name = "modificaPagamento";
            this.modificaPagamento.Size = new System.Drawing.Size(167, 54);
            this.modificaPagamento.TabIndex = 7;
            this.modificaPagamento.Text = "Modifica Pagamento";
            this.modificaPagamento.UseVisualStyleBackColor = true;
            // 
            // modificaNota
            // 
            this.modificaNota.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.modificaNota.Location = new System.Drawing.Point(577, 298);
            this.modificaNota.Name = "modificaNota";
            this.modificaNota.Size = new System.Drawing.Size(167, 54);
            this.modificaNota.TabIndex = 8;
            this.modificaNota.Text = "Modifica Nota";
            this.modificaNota.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Home";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Button modificaNota;
        private Button modificaPagamento;
        private Button modificaFattura;
        private Button nuovaNota;
        private Button nuovoPagamento;
        private Button nuovaFattura;
        private Label labelHome;
        private Button ricercaFornitori;
        private Button movimenti;
    }
}