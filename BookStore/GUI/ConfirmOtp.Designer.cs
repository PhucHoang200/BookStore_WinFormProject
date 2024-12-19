namespace GUI
{
    partial class ConfirmOtp
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtOtp = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnXacnhan = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nhập mã otp:";
            // 
            // txtOtp
            // 
            this.txtOtp.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtOtp.DefaultText = "";
            this.txtOtp.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtOtp.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtOtp.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtOtp.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtOtp.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtOtp.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtOtp.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtOtp.Location = new System.Drawing.Point(118, 39);
            this.txtOtp.Name = "txtOtp";
            this.txtOtp.PasswordChar = '\0';
            this.txtOtp.PlaceholderText = "";
            this.txtOtp.SelectedText = "";
            this.txtOtp.Size = new System.Drawing.Size(135, 23);
            this.txtOtp.TabIndex = 1;
            // 
            // btnXacnhan
            // 
            this.btnXacnhan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXacnhan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXacnhan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXacnhan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXacnhan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXacnhan.ForeColor = System.Drawing.Color.White;
            this.btnXacnhan.Location = new System.Drawing.Point(60, 95);
            this.btnXacnhan.Name = "btnXacnhan";
            this.btnXacnhan.Size = new System.Drawing.Size(180, 45);
            this.btnXacnhan.TabIndex = 2;
            this.btnXacnhan.Text = "Xác nhận";
            this.btnXacnhan.Click += new System.EventHandler(this.btnXacnhan_Click);
            // 
            // ConfirmOtp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 187);
            this.Controls.Add(this.btnXacnhan);
            this.Controls.Add(this.txtOtp);
            this.Controls.Add(this.label1);
            this.Name = "ConfirmOtp";
            this.Text = "ConfirmOtp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox txtOtp;
        private Guna.UI2.WinForms.Guna2Button btnXacnhan;
    }
}