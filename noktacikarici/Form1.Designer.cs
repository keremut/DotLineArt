namespace noktacikarici
{
    partial class Form1
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
            btnResimSec = new Button();
            btnNoktaOlustur = new Button();
            btnDisaAktar = new Button();
            txtDosyaYolu = new TextBox();
            label1 = new Label();
            btnCizimiGoster = new Button();
            txtNoktaSayisi = new TextBox();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            rbtnA4 = new RadioButton();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnResimSec
            // 
            btnResimSec.Location = new Point(49, 60);
            btnResimSec.Name = "btnResimSec";
            btnResimSec.Size = new Size(118, 33);
            btnResimSec.TabIndex = 1;
            btnResimSec.Text = "Resim Seç";
            btnResimSec.UseVisualStyleBackColor = true;
            btnResimSec.Click += btnResimSec_Click;
            // 
            // btnNoktaOlustur
            // 
            btnNoktaOlustur.Location = new Point(49, 160);
            btnNoktaOlustur.Name = "btnNoktaOlustur";
            btnNoktaOlustur.Size = new Size(118, 33);
            btnNoktaOlustur.TabIndex = 2;
            btnNoktaOlustur.Text = "Nokta Oluştur";
            btnNoktaOlustur.UseVisualStyleBackColor = true;
            btnNoktaOlustur.Click += btnNoktaOlustur_Click;
            // 
            // btnDisaAktar
            // 
            btnDisaAktar.Location = new Point(49, 310);
            btnDisaAktar.Name = "btnDisaAktar";
            btnDisaAktar.Size = new Size(118, 33);
            btnDisaAktar.TabIndex = 3;
            btnDisaAktar.Text = "Dışa Aktar";
            btnDisaAktar.UseVisualStyleBackColor = true;
            btnDisaAktar.Click += btnDisaAktar_Click;
            // 
            // txtDosyaYolu
            // 
            txtDosyaYolu.Location = new Point(486, 411);
            txtDosyaYolu.Name = "txtDosyaYolu";
            txtDosyaYolu.Size = new Size(302, 23);
            txtDosyaYolu.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(399, 415);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 6;
            label1.Text = "Dosya Yolu :";
            // 
            // btnCizimiGoster
            // 
            btnCizimiGoster.Location = new Point(49, 110);
            btnCizimiGoster.Name = "btnCizimiGoster";
            btnCizimiGoster.Size = new Size(118, 33);
            btnCizimiGoster.TabIndex = 7;
            btnCizimiGoster.Text = "Çizimi Göster";
            btnCizimiGoster.UseVisualStyleBackColor = true;
            // 
            // txtNoktaSayisi
            // 
            txtNoktaSayisi.Location = new Point(236, 206);
            txtNoktaSayisi.Name = "txtNoktaSayisi";
            txtNoktaSayisi.Size = new Size(118, 23);
            txtNoktaSayisi.TabIndex = 8;
            txtNoktaSayisi.KeyPress += txtNoktaSayisi_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(49, 214);
            label2.Name = "label2";
            label2.Size = new Size(161, 15);
            label2.TabIndex = 9;
            label2.Text = "Nokta Sayısı (varsayılan 500) :";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(399, 26);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(389, 366);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // rbtnA4
            // 
            rbtnA4.AutoSize = true;
            rbtnA4.Location = new Point(51, 263);
            rbtnA4.Name = "rbtnA4";
            rbtnA4.Size = new Size(39, 19);
            rbtnA4.TabIndex = 10;
            rbtnA4.TabStop = true;
            rbtnA4.Text = "A4";
            rbtnA4.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label3.Location = new Point(180, 263);
            label3.Name = "label3";
            label3.Size = new Size(171, 21);
            label3.TabIndex = 11;
            label3.Text = "Varsayılan Format A3";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(rbtnA4);
            Controls.Add(label2);
            Controls.Add(txtNoktaSayisi);
            Controls.Add(btnCizimiGoster);
            Controls.Add(label1);
            Controls.Add(txtDosyaYolu);
            Controls.Add(btnDisaAktar);
            Controls.Add(btnNoktaOlustur);
            Controls.Add(btnResimSec);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnResimSec;
        private Button btnNoktaOlustur;
        private Button btnDisaAktar;
        private TextBox txtDosyaYolu;
        private Label label1;
        private Button btnCizimiGoster;
        private TextBox txtNoktaSayisi;
        private Label label2;
        private PictureBox pictureBox1;
        private RadioButton rbtnA4;
        private Label label3;
    }
}
