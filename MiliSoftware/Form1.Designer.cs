namespace MiliSoftware
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btSalir = new System.Windows.Forms.PictureBox();
            this.btMinizar = new System.Windows.Forms.PictureBox();
            this.textBoxM2 = new MiliControls.Controls.TextBoxM();
            this.textBoxM1 = new MiliControls.Controls.TextBoxM();
            this.buttomM1 = new Controles.Controls.ButtomM();
            this.btRegistrar = new Controles.Controls.ButtomM();
            ((System.ComponentModel.ISupportInitialize)(this.btSalir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btMinizar)).BeginInit();
            this.SuspendLayout();
            // 
            // btSalir
            // 
            this.btSalir.BackColor = System.Drawing.Color.Transparent;
            this.btSalir.Image = ((System.Drawing.Image)(resources.GetObject("btSalir.Image")));
            this.btSalir.Location = new System.Drawing.Point(6, 1);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(13, 13);
            this.btSalir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btSalir.TabIndex = 2;
            this.btSalir.TabStop = false;
            this.btSalir.Click += new System.EventHandler(this.btSalir_Click);
            // 
            // btMinizar
            // 
            this.btMinizar.BackColor = System.Drawing.Color.Transparent;
            this.btMinizar.Image = ((System.Drawing.Image)(resources.GetObject("btMinizar.Image")));
            this.btMinizar.InitialImage = ((System.Drawing.Image)(resources.GetObject("btMinizar.InitialImage")));
            this.btMinizar.Location = new System.Drawing.Point(22, 1);
            this.btMinizar.Name = "btMinizar";
            this.btMinizar.Size = new System.Drawing.Size(13, 13);
            this.btMinizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btMinizar.TabIndex = 3;
            this.btMinizar.TabStop = false;
            this.btMinizar.Click += new System.EventHandler(this.btMinizar_Click);
            // 
            // textBoxM2
            // 
            this.textBoxM2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(149)))), ((int)(((byte)(185)))));
            this.textBoxM2.BorderRadius = 15;
            this.textBoxM2.BorderSize = 2;
            this.textBoxM2.Location = new System.Drawing.Point(556, 372);
            this.textBoxM2.Name = "textBoxM2";
            this.textBoxM2.Padding = new System.Windows.Forms.Padding(7);
            this.textBoxM2.PasswordChar = true;
            this.textBoxM2.Size = new System.Drawing.Size(250, 36);
            this.textBoxM2.TabIndex = 6;
            this.textBoxM2.UnderLineStyle = false;
            // 
            // textBoxM1
            // 
            this.textBoxM1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(149)))), ((int)(((byte)(185)))));
            this.textBoxM1.BorderRadius = 15;
            this.textBoxM1.BorderSize = 2;
            this.textBoxM1.Location = new System.Drawing.Point(556, 310);
            this.textBoxM1.Name = "textBoxM1";
            this.textBoxM1.Padding = new System.Windows.Forms.Padding(7);
            this.textBoxM1.PasswordChar = false;
            this.textBoxM1.Size = new System.Drawing.Size(250, 36);
            this.textBoxM1.TabIndex = 5;
            this.textBoxM1.UnderLineStyle = false;
            // 
            // buttomM1
            // 
            this.buttomM1.BackColor = System.Drawing.Color.GhostWhite;
            this.buttomM1.BackgroundColor = System.Drawing.Color.GhostWhite;
            this.buttomM1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(149)))), ((int)(((byte)(185)))));
            this.buttomM1.BorderRadius = 16;
            this.buttomM1.BorderSize = 2;
            this.buttomM1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttomM1.FlatAppearance.BorderSize = 0;
            this.buttomM1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.buttomM1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.buttomM1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttomM1.Font = new System.Drawing.Font("Courier New", 8.76F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttomM1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(149)))), ((int)(((byte)(185)))));
            this.buttomM1.Location = new System.Drawing.Point(586, 432);
            this.buttomM1.Name = "buttomM1";
            this.buttomM1.Size = new System.Drawing.Size(91, 31);
            this.buttomM1.TabIndex = 0;
            this.buttomM1.Text = "INGRESAR";
            this.buttomM1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(149)))), ((int)(((byte)(185)))));
            this.buttomM1.UseVisualStyleBackColor = false;
            // 
            // btRegistrar
            // 
            this.btRegistrar.BackColor = System.Drawing.Color.GhostWhite;
            this.btRegistrar.BackgroundColor = System.Drawing.Color.GhostWhite;
            this.btRegistrar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(149)))), ((int)(((byte)(185)))));
            this.btRegistrar.BorderRadius = 16;
            this.btRegistrar.BorderSize = 2;
            this.btRegistrar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btRegistrar.FlatAppearance.BorderSize = 0;
            this.btRegistrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btRegistrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btRegistrar.Font = new System.Drawing.Font("Courier New", 8.76F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btRegistrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(149)))), ((int)(((byte)(185)))));
            this.btRegistrar.Location = new System.Drawing.Point(683, 432);
            this.btRegistrar.Name = "btRegistrar";
            this.btRegistrar.Size = new System.Drawing.Size(91, 31);
            this.btRegistrar.TabIndex = 1;
            this.btRegistrar.Text = "REGISTRAR";
            this.btRegistrar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(149)))), ((int)(((byte)(185)))));
            this.btRegistrar.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(934, 666);
            this.Controls.Add(this.textBoxM2);
            this.Controls.Add(this.textBoxM1);
            this.Controls.Add(this.buttomM1);
            this.Controls.Add(this.btMinizar);
            this.Controls.Add(this.btSalir);
            this.Controls.Add(this.btRegistrar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btSalir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btMinizar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Controles.Controls.ButtomM btRegistrar;
        private System.Windows.Forms.PictureBox btSalir;
        private System.Windows.Forms.PictureBox btMinizar;
        private Controles.Controls.ButtomM buttomM1;
        private MiliControls.Controls.TextBoxM textBoxM1;
        private MiliControls.Controls.TextBoxM textBoxM2;
    }
}

