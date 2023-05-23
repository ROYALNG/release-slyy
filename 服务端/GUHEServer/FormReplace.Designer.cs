namespace GHIBMS.Server
{
    partial class FormReplace
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
            this.Label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.textBoxX1 = new System.Windows.Forms.TextBox();
            this.textBoxX2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            // 
            // 
            // 
            this.Label1.Location = new System.Drawing.Point(48, 132);
            this.Label1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(307, 33);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "查找的变量名称中的字符：";
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(646, 382);
            this.btnOK.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(150, 46);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "全部替换";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            // 
            // 
            // 
            this.Label2.Location = new System.Drawing.Point(48, 246);
            this.Label2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(307, 33);
            this.Label2.TabIndex = 7;
            this.Label2.Text = "替换的变量名称中的字符：";
            // 
            // textBoxX1
            // 
            this.textBoxX1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.textBoxX1.Location = new System.Drawing.Point(354, 126);
            this.textBoxX1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.Size = new System.Drawing.Size(442, 35);
            this.textBoxX1.TabIndex = 8;
            // 
            // textBoxX2
            // 
            this.textBoxX2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.textBoxX2.Location = new System.Drawing.Point(356, 240);
            this.textBoxX2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.textBoxX2.Name = "textBoxX2";
            this.textBoxX2.Size = new System.Drawing.Size(442, 35);
            this.textBoxX2.TabIndex = 9;
            // 
            // FormReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 508);
            this.Controls.Add(this.textBoxX2);
            this.Controls.Add(this.textBoxX1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.Label1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MinimizeBox = false;
            this.Name = "FormReplace";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "变量替换";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.FormSearch_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.TextBox textBoxX1;
        private System.Windows.Forms.TextBox textBoxX2;
    }
}