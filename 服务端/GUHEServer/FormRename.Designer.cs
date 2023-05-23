namespace GHIBMS.Server
{
    partial class FormRename
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtOldName=new  System.Windows.Forms.TextBox();
            this.txtNewName=new  System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.Location = new System.Drawing.Point(206, 237);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确认";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Location = new System.Drawing.Point(320, 236);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            // 
            // 
            // 
            this.Label1.Location = new System.Drawing.Point(63, 87);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(56, 18);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "原名称：";
            // 
            // txtOldName
            // 
            // 
            // 
            // 
            this.txtOldName.Location = new System.Drawing.Point(135, 84);
            this.txtOldName.Name = "txtOldName";
            this.txtOldName.Size = new System.Drawing.Size(272, 21);
            this.txtOldName.TabIndex = 3;
            // 
            // txtNewName
            // 
            // 
            // 
            this.txtNewName.Location = new System.Drawing.Point(135, 128);
            this.txtNewName.Name = "txtNewName";
            this.txtNewName.Size = new System.Drawing.Size(272, 21);
            this.txtNewName.TabIndex = 5;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            // 
            // 
            // 
            this.Label2.Location = new System.Drawing.Point(63, 131);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(56, 18);
            this.Label2.TabIndex = 4;
            this.Label2.Text = "新名称：";
            // 
            // FormRename
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 318);
            this.Controls.Add(this.txtNewName);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtOldName);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRename";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "修改名称";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.TextBox txtOldName;
        private System.Windows.Forms.TextBox txtNewName;
        private System.Windows.Forms.Label Label2;
    }
}