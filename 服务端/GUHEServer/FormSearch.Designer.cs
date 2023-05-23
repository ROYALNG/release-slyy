namespace GHIBMS.Server
{
    partial class FormSearch
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
            this.btnSearchALL = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnAutoComplete = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            // 
            // 
            // 
            this.Label1.Location = new System.Drawing.Point(114, 80);
            this.Label1.Margin = new System.Windows.Forms.Padding(6);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(208, 33);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "查找的变量名称：";
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(700, 377);
            this.btnOK.Margin = new System.Windows.Forms.Padding(6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(150, 46);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "选中当前";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnSearchALL
            // 
            this.btnSearchALL.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearchALL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchALL.Location = new System.Drawing.Point(360, 377);
            this.btnSearchALL.Margin = new System.Windows.Forms.Padding(6);
            this.btnSearchALL.Name = "btnSearchALL";
            this.btnSearchALL.Size = new System.Drawing.Size(150, 46);
            this.btnSearchALL.TabIndex = 4;
            this.btnSearchALL.Text = "全文查找";
            this.btnSearchALL.Click += new System.EventHandler(this.btnSearchALL_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(114, 156);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(728, 32);
            this.comboBox1.TabIndex = 5;
            // 
            // btnAutoComplete
            // 
            this.btnAutoComplete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAutoComplete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAutoComplete.Location = new System.Drawing.Point(176, 377);
            this.btnAutoComplete.Margin = new System.Windows.Forms.Padding(6);
            this.btnAutoComplete.Name = "btnAutoComplete";
            this.btnAutoComplete.Size = new System.Drawing.Size(150, 46);
            this.btnAutoComplete.TabIndex = 6;
            this.btnAutoComplete.Text = "智能提示";
            this.btnAutoComplete.Visible = false;
            // 
            // Button1
            // 
            this.Button1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.Location = new System.Drawing.Point(532, 377);
            this.Button1.Margin = new System.Windows.Forms.Padding(6);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(150, 46);
            this.Button1.TabIndex = 7;
            this.Button1.Text = "选中所有";
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // FormSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 508);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.btnAutoComplete);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnSearchALL);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.Label1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MinimizeBox = false;
            this.Name = "FormSearch";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "变量查找";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSearch_FormClosed);
            this.Shown += new System.EventHandler(this.FormSearch_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnSearchALL;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnAutoComplete;
        private System.Windows.Forms.Button Button1;
    }
}