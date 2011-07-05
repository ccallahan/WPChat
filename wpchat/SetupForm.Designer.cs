namespace csharpirc
{
    partial class SetupForm
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
            this.textBoxNick = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSetupOk = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxNick
            // 
            this.textBoxNick.Location = new System.Drawing.Point(119, 24);
            this.textBoxNick.Name = "textBoxNick";
            this.textBoxNick.Size = new System.Drawing.Size(100, 20);
            this.textBoxNick.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nick";
            // 
            // buttonSetupOk
            // 
            this.buttonSetupOk.Location = new System.Drawing.Point(30, 152);
            this.buttonSetupOk.Name = "buttonSetupOk";
            this.buttonSetupOk.Size = new System.Drawing.Size(75, 23);
            this.buttonSetupOk.TabIndex = 8;
            this.buttonSetupOk.Text = "Apply";
            this.buttonSetupOk.UseVisualStyleBackColor = true;
            this.buttonSetupOk.Click += new System.EventHandler(this.buttonSetupOk_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(144, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 187);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonSetupOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxNick);
            this.Name = "SetupForm";
            this.Text = "Setup";
            this.Load += new System.EventHandler(this.SetupForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxNick;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSetupOk;
        private System.Windows.Forms.Button button1;
    }
}