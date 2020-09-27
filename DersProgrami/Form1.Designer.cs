namespace DersProgrami
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.liste = new System.Windows.Forms.ListView();
            this.tb_sinifSay = new System.Windows.Forms.TextBox();
            this.btn_uyg = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rb_guz = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.rb_bahar = new System.Windows.Forms.RadioButton();
            this.teo_uygLabel = new System.Windows.Forms.Label();
            this.cb_Secim = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // liste
            // 
            this.liste.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.liste.HideSelection = false;
            this.liste.Location = new System.Drawing.Point(23, 12);
            this.liste.Name = "liste";
            this.liste.Size = new System.Drawing.Size(925, 701);
            this.liste.TabIndex = 1;
            this.liste.UseCompatibleStateImageBehavior = false;
            this.liste.View = System.Windows.Forms.View.Details;
            // 
            // tb_sinifSay
            // 
            this.tb_sinifSay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tb_sinifSay.Location = new System.Drawing.Point(178, 719);
            this.tb_sinifSay.Multiline = true;
            this.tb_sinifSay.Name = "tb_sinifSay";
            this.tb_sinifSay.Size = new System.Drawing.Size(200, 28);
            this.tb_sinifSay.TabIndex = 2;
            this.tb_sinifSay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_sinifSay_KeyPress);
            // 
            // btn_uyg
            // 
            this.btn_uyg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_uyg.Location = new System.Drawing.Point(825, 719);
            this.btn_uyg.Name = "btn_uyg";
            this.btn_uyg.Size = new System.Drawing.Size(123, 28);
            this.btn_uyg.TabIndex = 3;
            this.btn_uyg.Text = "Uygula";
            this.btn_uyg.UseVisualStyleBackColor = true;
            this.btn_uyg.Click += new System.EventHandler(this.btn_uyg_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(19, 723);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Sınıf Sayısını Giriniz: ";
            // 
            // rb_guz
            // 
            this.rb_guz.AutoSize = true;
            this.rb_guz.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rb_guz.Location = new System.Drawing.Point(149, 757);
            this.rb_guz.Name = "rb_guz";
            this.rb_guz.Size = new System.Drawing.Size(105, 24);
            this.rb_guz.TabIndex = 5;
            this.rb_guz.TabStop = true;
            this.rb_guz.Text = "Güz Yarılıyı";
            this.rb_guz.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(19, 761);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Dönem Seçiniz: ";
            // 
            // rb_bahar
            // 
            this.rb_bahar.AutoSize = true;
            this.rb_bahar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rb_bahar.Location = new System.Drawing.Point(260, 757);
            this.rb_bahar.Name = "rb_bahar";
            this.rb_bahar.Size = new System.Drawing.Size(118, 24);
            this.rb_bahar.TabIndex = 7;
            this.rb_bahar.TabStop = true;
            this.rb_bahar.Text = "Bahar Yarıyılı";
            this.rb_bahar.UseVisualStyleBackColor = true;
            // 
            // teo_uygLabel
            // 
            this.teo_uygLabel.AutoSize = true;
            this.teo_uygLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.teo_uygLabel.Location = new System.Drawing.Point(390, 723);
            this.teo_uygLabel.Name = "teo_uygLabel";
            this.teo_uygLabel.Size = new System.Drawing.Size(192, 20);
            this.teo_uygLabel.TabIndex = 8;
            this.teo_uygLabel.Text = "Teorik/Uygulamalı Seçimi: ";
            // 
            // cb_Secim
            // 
            this.cb_Secim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Secim.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cb_Secim.FormattingEnabled = true;
            this.cb_Secim.Items.AddRange(new object[] {
            "Teorik+Uygulama",
            "Teorik",
            "Uygulama"});
            this.cb_Secim.Location = new System.Drawing.Point(588, 719);
            this.cb_Secim.Name = "cb_Secim";
            this.cb_Secim.Size = new System.Drawing.Size(231, 28);
            this.cb_Secim.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 793);
            this.Controls.Add(this.cb_Secim);
            this.Controls.Add(this.teo_uygLabel);
            this.Controls.Add(this.rb_bahar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rb_guz);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_uyg);
            this.Controls.Add(this.tb_sinifSay);
            this.Controls.Add(this.liste);
            this.Name = "Form1";
            this.Text = "Ders Programı Uygulaması";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView liste;
        private System.Windows.Forms.TextBox tb_sinifSay;
        private System.Windows.Forms.Button btn_uyg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rb_guz;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rb_bahar;
        private System.Windows.Forms.Label teo_uygLabel;
        private System.Windows.Forms.ComboBox cb_Secim;
    }
}

