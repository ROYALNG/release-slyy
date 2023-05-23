
namespace GHIBMS.Server
{
    partial class FormPriority
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
            this.lblCurValue = new System.Windows.Forms.Label();
            this.lblCurPriority = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNewValue = new System.Windows.Forms.TextBox();
            this.cmbNewPriority = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRelingquish = new System.Windows.Forms.Button();
            this.btnSet = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.txtDefault = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前值：";
            // 
            // lblCurValue
            // 
            this.lblCurValue.AutoSize = true;
            this.lblCurValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurValue.Location = new System.Drawing.Point(108, 36);
            this.lblCurValue.MinimumSize = new System.Drawing.Size(120, 2);
            this.lblCurValue.Name = "lblCurValue";
            this.lblCurValue.Padding = new System.Windows.Forms.Padding(3);
            this.lblCurValue.Size = new System.Drawing.Size(120, 20);
            this.lblCurValue.TabIndex = 1;
            this.lblCurValue.Click += new System.EventHandler(this.label2_Click);
            // 
            // lblCurPriority
            // 
            this.lblCurPriority.AutoSize = true;
            this.lblCurPriority.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurPriority.Location = new System.Drawing.Point(345, 36);
            this.lblCurPriority.MinimumSize = new System.Drawing.Size(120, 2);
            this.lblCurPriority.Name = "lblCurPriority";
            this.lblCurPriority.Padding = new System.Windows.Forms.Padding(3);
            this.lblCurPriority.Size = new System.Drawing.Size(120, 20);
            this.lblCurPriority.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(253, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "当前优先级：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(253, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "设定优先级：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "设定值：";
            // 
            // txtNewValue
            // 
            this.txtNewValue.Location = new System.Drawing.Point(108, 76);
            this.txtNewValue.Name = "txtNewValue";
            this.txtNewValue.Size = new System.Drawing.Size(120, 21);
            this.txtNewValue.TabIndex = 6;
            // 
            // cmbNewPriority
            // 
            this.cmbNewPriority.FormattingEnabled = true;
            this.cmbNewPriority.Location = new System.Drawing.Point(345, 76);
            this.cmbNewPriority.Name = "cmbNewPriority";
            this.cmbNewPriority.Size = new System.Drawing.Size(121, 20);
            this.cmbNewPriority.TabIndex = 7;
            this.cmbNewPriority.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRelingquish);
            this.groupBox1.Controls.Add(this.btnSet);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbNewPriority);
            this.groupBox1.Controls.Add(this.lblCurValue);
            this.groupBox1.Controls.Add(this.txtNewValue);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblCurPriority);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(25, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(505, 186);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // btnRelingquish
            // 
            this.btnRelingquish.Location = new System.Drawing.Point(255, 134);
            this.btnRelingquish.Name = "btnRelingquish";
            this.btnRelingquish.Size = new System.Drawing.Size(82, 34);
            this.btnRelingquish.TabIndex = 9;
            this.btnRelingquish.Text = "释放";
            this.btnRelingquish.UseVisualStyleBackColor = true;
            this.btnRelingquish.Click += new System.EventHandler(this.btnRelingquish_Click);
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(134, 134);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(82, 34);
            this.btnSet.TabIndex = 8;
            this.btnSet.Text = "设置";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // listView1
            // 
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(25, 227);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(505, 196);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
            // 
            // txtDefault
            // 
            this.txtDefault.Location = new System.Drawing.Point(102, 444);
            this.txtDefault.Name = "txtDefault";
            this.txtDefault.Size = new System.Drawing.Size(65, 21);
            this.txtDefault.TabIndex = 11;
            this.txtDefault.Text = "0";
            this.txtDefault.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 449);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "释放缺省值：";
            this.label7.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(335, 488);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 34);
            this.button2.TabIndex = 13;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(443, 488);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(82, 34);
            this.button3.TabIndex = 14;
            this.button3.Text = "刷新";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // FormPriority
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 535);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtDefault);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(115, 55);
            this.Name = "FormPriority";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "通讯优先级-BACNET";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCurValue;
        private System.Windows.Forms.Label lblCurPriority;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNewValue;
        private System.Windows.Forms.ComboBox cmbNewPriority;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRelingquish;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox txtDefault;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}