namespace GHIBMS.Server
{
    partial class FormTimeTask
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTimeTask));
            this.lsvTask = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxDetail = new System.Windows.Forms.GroupBox();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.dtBegin = new System.Windows.Forms.DateTimePicker();
            this.Label5 = new System.Windows.Forms.Label();
            this.chkWeek7 = new System.Windows.Forms.CheckBox();
            this.chkWeek6 = new System.Windows.Forms.CheckBox();
            this.chkWeek5 = new System.Windows.Forms.CheckBox();
            this.chkWeek4 = new System.Windows.Forms.CheckBox();
            this.chkWeek3 = new System.Windows.Forms.CheckBox();
            this.chkWeek2 = new System.Windows.Forms.CheckBox();
            this.chkWeek1 = new System.Windows.Forms.CheckBox();
            this.btnAddVar = new System.Windows.Forms.Button();
            this.btnDelVar = new System.Windows.Forms.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.lstVariable = new System.Windows.Forms.ListView();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.chkKeep = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBoxDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsvTask
            // 
            this.lsvTask.FullRowSelect = true;
            this.lsvTask.GridLines = true;
            this.lsvTask.LargeImageList = this.imageList1;
            this.lsvTask.Location = new System.Drawing.Point(16, 64);
            this.lsvTask.Margin = new System.Windows.Forms.Padding(6);
            this.lsvTask.Name = "lsvTask";
            this.lsvTask.Size = new System.Drawing.Size(716, 794);
            this.lsvTask.SmallImageList = this.imageList1;
            this.lsvTask.TabIndex = 0;
            this.lsvTask.UseCompatibleStateImageBehavior = false;
            this.lsvTask.View = System.Windows.Forms.View.Details;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "time.png");
            this.imageList1.Images.SetKeyName(1, "tray_on.png");
            // 
            // btnUp
            // 
            this.btnUp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUp.Location = new System.Drawing.Point(80, 906);
            this.btnUp.Margin = new System.Windows.Forms.Padding(6);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(150, 46);
            this.btnUp.TabIndex = 2;
            this.btnUp.Text = "向上";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDown.Location = new System.Drawing.Point(230, 906);
            this.btnDown.Margin = new System.Windows.Forms.Padding(6);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(150, 46);
            this.btnDown.TabIndex = 3;
            this.btnDown.Text = "向下";
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnNew
            // 
            this.btnNew.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnNew.Location = new System.Drawing.Point(380, 906);
            this.btnNew.Margin = new System.Windows.Forms.Padding(6);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(150, 46);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "新建";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnDel
            // 
            this.btnDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDel.Location = new System.Drawing.Point(530, 906);
            this.btnDel.Margin = new System.Windows.Forms.Padding(6);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(150, 46);
            this.btnDel.TabIndex = 5;
            this.btnDel.Text = "删除";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lsvTask);
            this.groupBox1.Controls.Add(this.btnUp);
            this.groupBox1.Controls.Add(this.btnDel);
            this.groupBox1.Controls.Add(this.btnDown);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Location = new System.Drawing.Point(58, 42);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox1.Size = new System.Drawing.Size(762, 978);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "任务列表";
            // 
            // groupBoxDetail
            // 
            this.groupBoxDetail.Controls.Add(this.chkKeep);
            this.groupBoxDetail.Controls.Add(this.label8);
            this.groupBoxDetail.Controls.Add(this.textBoxValue);
            this.groupBoxDetail.Controls.Add(this.chkActive);
            this.groupBoxDetail.Controls.Add(this.Label7);
            this.groupBoxDetail.Controls.Add(this.txtName);
            this.groupBoxDetail.Controls.Add(this.Label6);
            this.groupBoxDetail.Controls.Add(this.dtEnd);
            this.groupBoxDetail.Controls.Add(this.dtBegin);
            this.groupBoxDetail.Controls.Add(this.Label5);
            this.groupBoxDetail.Controls.Add(this.chkWeek7);
            this.groupBoxDetail.Controls.Add(this.chkWeek6);
            this.groupBoxDetail.Controls.Add(this.chkWeek5);
            this.groupBoxDetail.Controls.Add(this.chkWeek4);
            this.groupBoxDetail.Controls.Add(this.chkWeek3);
            this.groupBoxDetail.Controls.Add(this.chkWeek2);
            this.groupBoxDetail.Controls.Add(this.chkWeek1);
            this.groupBoxDetail.Controls.Add(this.btnAddVar);
            this.groupBoxDetail.Controls.Add(this.btnDelVar);
            this.groupBoxDetail.Controls.Add(this.Label4);
            this.groupBoxDetail.Controls.Add(this.lblEnd);
            this.groupBoxDetail.Controls.Add(this.Label2);
            this.groupBoxDetail.Controls.Add(this.Label1);
            this.groupBoxDetail.Controls.Add(this.lstVariable);
            this.groupBoxDetail.Enabled = false;
            this.groupBoxDetail.Location = new System.Drawing.Point(876, 42);
            this.groupBoxDetail.Margin = new System.Windows.Forms.Padding(6);
            this.groupBoxDetail.Name = "groupBoxDetail";
            this.groupBoxDetail.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxDetail.Size = new System.Drawing.Size(668, 874);
            this.groupBoxDetail.TabIndex = 8;
            this.groupBoxDetail.TabStop = false;
            this.groupBoxDetail.Text = "任务详细";
            // 
            // textBoxValue
            // 
            this.textBoxValue.Location = new System.Drawing.Point(210, 263);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(100, 35);
            this.textBoxValue.TabIndex = 27;
            this.textBoxValue.Text = "0";
            this.textBoxValue.TextChanged += new System.EventHandler(this.textBoxValue_TextChanged);
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(212, 134);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(28, 27);
            this.chkActive.TabIndex = 26;
            this.chkActive.UseVisualStyleBackColor = true;
            this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(4, 460);
            this.Label7.Margin = new System.Windows.Forms.Padding(6);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(118, 24);
            this.Label7.TabIndex = 25;
            this.Label7.Text = "任务变量:";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.ForeColor = System.Drawing.Color.Black;
            this.txtName.Location = new System.Drawing.Point(212, 58);
            this.txtName.Margin = new System.Windows.Forms.Padding(6);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(412, 35);
            this.txtName.TabIndex = 24;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(74, 64);
            this.Label6.Margin = new System.Windows.Forms.Padding(6);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(118, 24);
            this.Label6.TabIndex = 23;
            this.Label6.Text = "任务名称:";
            // 
            // dtEnd
            // 
            this.dtEnd.Location = new System.Drawing.Point(496, 198);
            this.dtEnd.Margin = new System.Windows.Forms.Padding(6);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.ShowUpDown = true;
            this.dtEnd.Size = new System.Drawing.Size(126, 35);
            this.dtEnd.TabIndex = 22;
            this.dtEnd.ValueChanged += new System.EventHandler(this.dtEnd_ValueChanged);
            // 
            // dtBegin
            // 
            this.dtBegin.Location = new System.Drawing.Point(210, 200);
            this.dtBegin.Margin = new System.Windows.Forms.Padding(6);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.ShowUpDown = true;
            this.dtBegin.Size = new System.Drawing.Size(126, 35);
            this.dtBegin.TabIndex = 21;
            this.dtBegin.ValueChanged += new System.EventHandler(this.dtBegin_ValueChanged);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(74, 350);
            this.Label5.Margin = new System.Windows.Forms.Padding(6);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(118, 24);
            this.Label5.TabIndex = 20;
            this.Label5.Text = "每周重复:";
            // 
            // chkWeek7
            // 
            this.chkWeek7.AutoSize = true;
            this.chkWeek7.Location = new System.Drawing.Point(494, 402);
            this.chkWeek7.Margin = new System.Windows.Forms.Padding(6);
            this.chkWeek7.Name = "chkWeek7";
            this.chkWeek7.Size = new System.Drawing.Size(90, 28);
            this.chkWeek7.TabIndex = 19;
            this.chkWeek7.Text = "周日";
            this.chkWeek7.UseVisualStyleBackColor = true;
            this.chkWeek7.CheckedChanged += new System.EventHandler(this.chkWeek7_CheckedChanged);
            // 
            // chkWeek6
            // 
            this.chkWeek6.AutoSize = true;
            this.chkWeek6.Location = new System.Drawing.Point(370, 402);
            this.chkWeek6.Margin = new System.Windows.Forms.Padding(6);
            this.chkWeek6.Name = "chkWeek6";
            this.chkWeek6.Size = new System.Drawing.Size(90, 28);
            this.chkWeek6.TabIndex = 18;
            this.chkWeek6.Text = "周六";
            this.chkWeek6.UseVisualStyleBackColor = true;
            this.chkWeek6.CheckedChanged += new System.EventHandler(this.chkWeek6_CheckedChanged);
            // 
            // chkWeek5
            // 
            this.chkWeek5.AutoSize = true;
            this.chkWeek5.Location = new System.Drawing.Point(228, 402);
            this.chkWeek5.Margin = new System.Windows.Forms.Padding(6);
            this.chkWeek5.Name = "chkWeek5";
            this.chkWeek5.Size = new System.Drawing.Size(90, 28);
            this.chkWeek5.TabIndex = 17;
            this.chkWeek5.Text = "周五";
            this.chkWeek5.UseVisualStyleBackColor = true;
            this.chkWeek5.CheckedChanged += new System.EventHandler(this.chkWeek5_CheckedChanged);
            // 
            // chkWeek4
            // 
            this.chkWeek4.AutoSize = true;
            this.chkWeek4.Location = new System.Drawing.Point(94, 402);
            this.chkWeek4.Margin = new System.Windows.Forms.Padding(6);
            this.chkWeek4.Name = "chkWeek4";
            this.chkWeek4.Size = new System.Drawing.Size(90, 28);
            this.chkWeek4.TabIndex = 16;
            this.chkWeek4.Text = "周四";
            this.chkWeek4.UseVisualStyleBackColor = true;
            this.chkWeek4.CheckedChanged += new System.EventHandler(this.chkWeek4_CheckedChanged);
            // 
            // chkWeek3
            // 
            this.chkWeek3.AutoSize = true;
            this.chkWeek3.Location = new System.Drawing.Point(494, 354);
            this.chkWeek3.Margin = new System.Windows.Forms.Padding(6);
            this.chkWeek3.Name = "chkWeek3";
            this.chkWeek3.Size = new System.Drawing.Size(90, 28);
            this.chkWeek3.TabIndex = 15;
            this.chkWeek3.Text = "周三";
            this.chkWeek3.UseVisualStyleBackColor = true;
            this.chkWeek3.CheckedChanged += new System.EventHandler(this.chkWeek3_CheckedChanged);
            // 
            // chkWeek2
            // 
            this.chkWeek2.AutoSize = true;
            this.chkWeek2.Location = new System.Drawing.Point(370, 354);
            this.chkWeek2.Margin = new System.Windows.Forms.Padding(6);
            this.chkWeek2.Name = "chkWeek2";
            this.chkWeek2.Size = new System.Drawing.Size(90, 28);
            this.chkWeek2.TabIndex = 14;
            this.chkWeek2.Text = "周二";
            this.chkWeek2.UseVisualStyleBackColor = true;
            this.chkWeek2.CheckedChanged += new System.EventHandler(this.chkWeek2_CheckedChanged);
            // 
            // chkWeek1
            // 
            this.chkWeek1.AutoSize = true;
            this.chkWeek1.Location = new System.Drawing.Point(228, 354);
            this.chkWeek1.Margin = new System.Windows.Forms.Padding(6);
            this.chkWeek1.Name = "chkWeek1";
            this.chkWeek1.Size = new System.Drawing.Size(90, 28);
            this.chkWeek1.TabIndex = 13;
            this.chkWeek1.Text = "周一";
            this.chkWeek1.UseVisualStyleBackColor = true;
            this.chkWeek1.CheckedChanged += new System.EventHandler(this.chkWeek1_CheckedChanged);
            // 
            // btnAddVar
            // 
            this.btnAddVar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddVar.Image = ((System.Drawing.Image)(resources.GetObject("btnAddVar.Image")));
            this.btnAddVar.Location = new System.Drawing.Point(294, 500);
            this.btnAddVar.Margin = new System.Windows.Forms.Padding(6);
            this.btnAddVar.Name = "btnAddVar";
            this.btnAddVar.Size = new System.Drawing.Size(150, 46);
            this.btnAddVar.TabIndex = 9;
            this.btnAddVar.Text = "添加";
            this.btnAddVar.Click += new System.EventHandler(this.btnAddVar_Click);
            // 
            // btnDelVar
            // 
            this.btnDelVar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelVar.Location = new System.Drawing.Point(469, 500);
            this.btnDelVar.Margin = new System.Windows.Forms.Padding(6);
            this.btnDelVar.Name = "btnDelVar";
            this.btnDelVar.Size = new System.Drawing.Size(150, 46);
            this.btnDelVar.TabIndex = 10;
            this.btnDelVar.Text = "删除";
            this.btnDelVar.Click += new System.EventHandler(this.btnDelVar_Click);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(74, 274);
            this.Label4.Margin = new System.Windows.Forms.Padding(6);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(118, 24);
            this.Label4.TabIndex = 7;
            this.Label4.Text = "动作数值:";
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(370, 204);
            this.lblEnd.Margin = new System.Windows.Forms.Padding(6);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(118, 24);
            this.lblEnd.TabIndex = 6;
            this.lblEnd.Text = "结束时间:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(74, 204);
            this.Label2.Margin = new System.Windows.Forms.Padding(6);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(118, 24);
            this.Label2.TabIndex = 5;
            this.Label2.Text = "开始时间:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(74, 134);
            this.Label1.Margin = new System.Windows.Forms.Padding(6);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(118, 24);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "是否启用:";
            // 
            // lstVariable
            // 
            this.lstVariable.BackColor = System.Drawing.Color.White;
            this.lstVariable.ForeColor = System.Drawing.Color.Black;
            this.lstVariable.LargeImageList = this.imageList1;
            this.lstVariable.Location = new System.Drawing.Point(74, 558);
            this.lstVariable.Margin = new System.Windows.Forms.Padding(6);
            this.lstVariable.Name = "lstVariable";
            this.lstVariable.ShowItemToolTips = true;
            this.lstVariable.Size = new System.Drawing.Size(552, 304);
            this.lstVariable.SmallImageList = this.imageList1;
            this.lstVariable.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstVariable.TabIndex = 1;
            this.lstVariable.UseCompatibleStateImageBehavior = false;
            this.lstVariable.View = System.Windows.Forms.View.Tile;
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.Location = new System.Drawing.Point(1174, 948);
            this.btnOK.Margin = new System.Windows.Forms.Padding(6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(150, 46);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确认";
            this.btnOK.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Location = new System.Drawing.Point(1378, 948);
            this.btnExit.Margin = new System.Windows.Forms.Padding(6);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(150, 46);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "退出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(294, 134);
            this.label8.Margin = new System.Windows.Forms.Padding(6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(166, 24);
            this.label8.TabIndex = 28;
            this.label8.Text = "是否持续动作:";
            // 
            // chkKeep
            // 
            this.chkKeep.AutoSize = true;
            this.chkKeep.Location = new System.Drawing.Point(469, 134);
            this.chkKeep.Name = "chkKeep";
            this.chkKeep.Size = new System.Drawing.Size(28, 27);
            this.chkKeep.TabIndex = 29;
            this.chkKeep.UseVisualStyleBackColor = true;
            this.chkKeep.CheckedChanged += new System.EventHandler(this.chkKeep_CheckedChanged);
            // 
            // FormTimeTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1743, 1130);
            this.Controls.Add(this.groupBoxDetail);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnExit);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTimeTask";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "定时任务管理器";
            this.Shown += new System.EventHandler(this.FormTimeTask_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBoxDetail.ResumeLayout(false);
            this.groupBoxDetail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvTask;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxDetail;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.ListView lstVariable;
        //private System.Windows.Forms.SwitchButton swTaskValue;
        private System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label Label2;
        //private System.Windows.Forms.SwitchButton swEnable;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnAddVar;
        private System.Windows.Forms.Button btnDelVar;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox chkWeek4;
        private System.Windows.Forms.CheckBox chkWeek3;
        private System.Windows.Forms.CheckBox chkWeek2;
        private System.Windows.Forms.CheckBox chkWeek1;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.CheckBox chkWeek7;
        private System.Windows.Forms.CheckBox chkWeek6;
        private System.Windows.Forms.CheckBox chkWeek5;
        private System.Windows.Forms.DateTimePicker dtBegin;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.CheckBox chkKeep;
        private System.Windows.Forms.Label label8;
    }
}