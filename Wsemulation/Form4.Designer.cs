namespace Wsemulation
{
    partial class Form4
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
            this.btnbrow = new System.Windows.Forms.Button();
            this.btnsend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtserver = new System.Windows.Forms.TextBox();
            this.txtbrow = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnbrow
            // 
            this.btnbrow.Location = new System.Drawing.Point(12, 38);
            this.btnbrow.Name = "btnbrow";
            this.btnbrow.Size = new System.Drawing.Size(75, 23);
            this.btnbrow.TabIndex = 0;
            this.btnbrow.Text = "browser";
            this.btnbrow.UseVisualStyleBackColor = true;
            this.btnbrow.Click += new System.EventHandler(this.btnbrow_Click);
            // 
            // btnsend
            // 
            this.btnsend.Location = new System.Drawing.Point(12, 281);
            this.btnsend.Name = "btnsend";
            this.btnsend.Size = new System.Drawing.Size(397, 43);
            this.btnsend.TabIndex = 1;
            this.btnsend.Text = "send";
            this.btnsend.UseVisualStyleBackColor = true;
            this.btnsend.Click += new System.EventHandler(this.btnsend_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "server";
            // 
            // txtserver
            // 
            this.txtserver.Location = new System.Drawing.Point(90, 15);
            this.txtserver.Name = "txtserver";
            this.txtserver.Size = new System.Drawing.Size(319, 20);
            this.txtserver.TabIndex = 3;
            
            // 
            // txtbrow
            // 
            this.txtbrow.Location = new System.Drawing.Point(90, 41);
            this.txtbrow.Name = "txtbrow";
            this.txtbrow.Size = new System.Drawing.Size(319, 20);
            this.txtbrow.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 67);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(397, 208);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 336);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtbrow);
            this.Controls.Add(this.txtserver);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnsend);
            this.Controls.Add(this.btnbrow);
            this.Name = "Form4";
            this.Text = "Sending Image";
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnbrow;
        private System.Windows.Forms.Button btnsend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtserver;
        private System.Windows.Forms.TextBox txtbrow;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}