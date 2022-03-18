namespace Wifi_Unipampa
{
    partial class FormPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.pictureBoxLogoSTIC = new System.Windows.Forms.PictureBox();
            this.buttonConfigurar = new System.Windows.Forms.Button();
            this.buttonFechar = new System.Windows.Forms.Button();
            this.buttonAjuda = new System.Windows.Forms.Button();
            this.labelSituacao = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogoSTIC)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureBoxLogo);
            this.panel1.Controls.Add(this.pictureBoxLogoSTIC);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(170, 217);
            this.panel1.TabIndex = 0;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = global::Wifi_Unipampa.Properties.Resources.wifi_2561;
            this.pictureBoxLogo.Location = new System.Drawing.Point(22, 0);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(130, 130);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.TabIndex = 5;
            this.pictureBoxLogo.TabStop = false;
            // 
            // pictureBoxLogoSTIC
            // 
            this.pictureBoxLogoSTIC.Image = global::Wifi_Unipampa.Properties.Resources.logo_stic_2;
            this.pictureBoxLogoSTIC.Location = new System.Drawing.Point(10, 167);
            this.pictureBoxLogoSTIC.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxLogoSTIC.Name = "pictureBoxLogoSTIC";
            this.pictureBoxLogoSTIC.Size = new System.Drawing.Size(150, 41);
            this.pictureBoxLogoSTIC.TabIndex = 4;
            this.pictureBoxLogoSTIC.TabStop = false;
            this.pictureBoxLogoSTIC.Click += new System.EventHandler(this.pictureBoxLogoSTIC_Click);
            // 
            // buttonConfigurar
            // 
            this.buttonConfigurar.Location = new System.Drawing.Point(225, 47);
            this.buttonConfigurar.Name = "buttonConfigurar";
            this.buttonConfigurar.Size = new System.Drawing.Size(175, 23);
            this.buttonConfigurar.TabIndex = 3;
            this.buttonConfigurar.Text = "&Configurar rede sem fio";
            this.buttonConfigurar.UseVisualStyleBackColor = true;
            this.buttonConfigurar.Click += new System.EventHandler(this.buttonConfigurar_Click);
            // 
            // buttonFechar
            // 
            this.buttonFechar.Location = new System.Drawing.Point(225, 76);
            this.buttonFechar.Name = "buttonFechar";
            this.buttonFechar.Size = new System.Drawing.Size(175, 23);
            this.buttonFechar.TabIndex = 4;
            this.buttonFechar.Text = "&Fechar";
            this.buttonFechar.UseVisualStyleBackColor = true;
            this.buttonFechar.Click += new System.EventHandler(this.buttonFechar_Click);
            // 
            // buttonAjuda
            // 
            this.buttonAjuda.Location = new System.Drawing.Point(225, 129);
            this.buttonAjuda.Name = "buttonAjuda";
            this.buttonAjuda.Size = new System.Drawing.Size(175, 23);
            this.buttonAjuda.TabIndex = 5;
            this.buttonAjuda.Text = "&Preciso de ajuda";
            this.buttonAjuda.UseVisualStyleBackColor = true;
            this.buttonAjuda.Click += new System.EventHandler(this.buttonAjuda_Click);
            // 
            // labelSituacao
            // 
            this.labelSituacao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSituacao.AutoSize = true;
            this.labelSituacao.Location = new System.Drawing.Point(170, 195);
            this.labelSituacao.MaximumSize = new System.Drawing.Size(280, 13);
            this.labelSituacao.Name = "labelSituacao";
            this.labelSituacao.Size = new System.Drawing.Size(0, 13);
            this.labelSituacao.TabIndex = 9;
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(454, 211);
            this.Controls.Add(this.labelSituacao);
            this.Controls.Add(this.buttonAjuda);
            this.Controls.Add(this.buttonFechar);
            this.Controls.Add(this.buttonConfigurar);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurar rede wi-fi unipampa";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogoSTIC)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonConfigurar;
        private System.Windows.Forms.Button buttonFechar;
        private System.Windows.Forms.PictureBox pictureBoxLogoSTIC;
        private System.Windows.Forms.Button buttonAjuda;
        private System.Windows.Forms.Label labelSituacao;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
    }
}

