namespace GHIBMS.Server
{
    partial class FormTimeZone
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTimeZone));
            this.lsvZone = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxDetail = new System.Windows.Forms.GroupBox();
            this.btnRestTime = new System.Windows.Forms.Button();
            this.btnWorkTime = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.combCopy = new System.Windows.Forms.ComboBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.dt4End = new System.Windows.Forms.DateTimePicker();
            this.dt4Begin = new System.Windows.Forms.DateTimePicker();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.dt3End = new System.Windows.Forms.DateTimePicker();
            this.dt3Begin = new System.Windows.Forms.DateTimePicker();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.dt2End = new System.Windows.Forms.DateTimePicker();
            this.dt2Begin = new System.Windows.Forms.DateTimePicker();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.combWeek = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.dt1End = new System.Windows.Forms.DateTimePicker();
            this.dt1Begin = new System.Windows.Forms.DateTimePicker();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBoxDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsvZone
            // 
            this.lsvZone.FullRowSelect = true;
            this.lsvZone.GridLines = true;
            this.lsvZone.LargeImageList = this.imageList1;
            this.lsvZone.Location = new System.Drawing.Point(8, 32);
            this.lsvZone.Name = "lsvZone";
            this.lsvZone.Size = new System.Drawing.Size(305, 399);
            this.lsvZone.SmallImageList = this.imageList1;
            this.lsvZone.TabIndex = 0;
            this.lsvZone.UseCompatibleStateImageBehavior = false;
            this.lsvZone.SelectedIndexChanged += new System.EventHandler(this.lsvZone_SelectedIndexChanged);
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
            this.btnUp.Location = new System.Drawing.Point(7, 453);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(75, 23);
            this.btnUp.TabIndex = 2;
            this.btnUp.Text = "向上";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDown.Location = new System.Drawing.Point(82, 453);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(75, 23);
            this.btnDown.TabIndex = 3;
            this.btnDown.Text = "向下";
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnNew
            // 
            this.btnNew.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnNew.Location = new System.Drawing.Point(157, 453);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "新建";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnDel
            // 
            this.btnDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDel.Location = new System.Drawing.Point(232, 453);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 5;
            this.btnDel.Text = "删除";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lsvZone);
            this.groupBox1.Controls.Add(this.btnUp);
            this.groupBox1.Controls.Add(this.btnDel);
            this.groupBox1.Controls.Add(this.btnDown);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Location = new System.Drawing.Point(29, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(327, 489);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "时限列表";
            // 
            // groupBoxDetail
            // 
            this.groupBoxDetail.Controls.Add(this.btnRestTime);
            this.groupBoxDetail.Controls.Add(this.btnWorkTime);
            this.groupBoxDetail.Controls.Add(this.btnCopy);
            this.groupBoxDetail.Controls.Add(this.combCopy);
            this.groupBoxDetail.Controls.Add(this.Label11);
            this.groupBoxDetail.Controls.Add(this.dt4End);
            this.groupBoxDetail.Controls.Add(this.dt4Begin);
            this.groupBoxDetail.Controls.Add(this.btnOK);
            this.groupBoxDetail.Controls.Add(this.btnExit);
            this.groupBoxDetail.Controls.Add(this.Label9);
            this.groupBoxDetail.Controls.Add(this.Label10);
            this.groupBoxDetail.Controls.Add(this.dt3End);
            this.groupBoxDetail.Controls.Add(this.dt3Begin);
            this.groupBoxDetail.Controls.Add(this.Label7);
            this.groupBoxDetail.Controls.Add(this.Label8);
            this.groupBoxDetail.Controls.Add(this.dt2End);
            this.groupBoxDetail.Controls.Add(this.dt2Begin);
            this.groupBoxDetail.Controls.Add(this.Label4);
            this.groupBoxDetail.Controls.Add(this.Label5);
            this.groupBoxDetail.Controls.Add(this.combWeek);
            this.groupBoxDetail.Controls.Add(this.Label1);
            this.groupBoxDetail.Controls.Add(this.txtName);
            this.groupBoxDetail.Controls.Add(this.Label6);
            this.groupBoxDetail.Controls.Add(this.dt1End);
            this.groupBoxDetail.Controls.Add(this.dt1Begin);
            this.groupBoxDetail.Controls.Add(this.Label3);
            this.groupBoxDetail.Controls.Add(this.Label2);
            this.groupBoxDetail.Enabled = false;
            this.groupBoxDetail.Location = new System.Drawing.Point(362, 21);
            this.groupBoxDetail.Name = "groupBoxDetail";
            this.groupBoxDetail.Size = new System.Drawing.Size(326, 489);
            this.groupBoxDetail.TabIndex = 8;
            this.groupBoxDetail.TabStop = false;
            this.groupBoxDetail.Text = "时限详细";
            this.groupBoxDetail.Enter += new System.EventHandler(this.groupBoxDetail_Enter);
            // 
            // btnRestTime
            // 
            this.btnRestTime.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRestTime.Location = new System.Drawing.Point(167, 307);
            this.btnRestTime.Name = "btnRestTime";
            this.btnRestTime.Size = new System.Drawing.Size(102, 23);
            this.btnRestTime.TabIndex = 43;
            this.btnRestTime.Text = "休息时间模板";
            this.btnRestTime.Click += new System.EventHandler(this.btnRestTime_Click);
            // 
            // btnWorkTime
            // 
            this.btnWorkTime.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnWorkTime.Location = new System.Drawing.Point(37, 307);
            this.btnWorkTime.Name = "btnWorkTime";
            this.btnWorkTime.Size = new System.Drawing.Size(102, 23);
            this.btnWorkTime.TabIndex = 42;
            this.btnWorkTime.Text = "工作时间模板";
            this.btnWorkTime.Click += new System.EventHandler(this.btnWorkTime_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCopy.Location = new System.Drawing.Point(245, 247);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(60, 23);
            this.btnCopy.TabIndex = 41;
            this.btnCopy.Text = "复制";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // combCopy
            // 
            this.combCopy.FormattingEnabled = true;
            this.combCopy.Location = new System.Drawing.Point(105, 249);
            this.combCopy.Name = "combCopy";
            this.combCopy.Size = new System.Drawing.Size(121, 20);
            this.combCopy.TabIndex = 40;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            // 
            // 
            // 
            this.Label11.Location = new System.Drawing.Point(37, 251);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(50, 18);
            this.Label11.TabIndex = 39;
            this.Label11.Text = "复制到:";
            // 
            // dt4End
            // 
            this.dt4End.Location = new System.Drawing.Point(204, 202);
            this.dt4End.Name = "dt4End";
            this.dt4End.ShowUpDown = true;
            this.dt4End.Size = new System.Drawing.Size(65, 21);
            this.dt4End.TabIndex = 38;
            this.dt4End.Leave += new System.EventHandler(this.dt1Begin_Leave);
            // 
            // dt4Begin
            // 
            this.dt4Begin.Location = new System.Drawing.Point(105, 203);
            this.dt4Begin.Name = "dt4Begin";
            this.dt4Begin.ShowUpDown = true;
            this.dt4Begin.Size = new System.Drawing.Size(65, 21);
            this.dt4Begin.TabIndex = 37;
            this.dt4Begin.Leave += new System.EventHandler(this.dt1Begin_Leave);
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.Location = new System.Drawing.Point(128, 453);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "保存";
            this.btnOK.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Location = new System.Drawing.Point(230, 453);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "退出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            // 
            // 
            // 
            this.Label9.Location = new System.Drawing.Point(180, 205);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(13, 16);
            this.Label9.TabIndex = 36;
            this.Label9.Text = "-";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            // 
            // 
            // 
            this.Label10.Location = new System.Drawing.Point(37, 205);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(44, 18);
            this.Label10.TabIndex = 35;
            this.Label10.Text = "时段4:";
            // 
            // dt3End
            // 
            this.dt3End.Location = new System.Drawing.Point(204, 167);
            this.dt3End.Name = "dt3End";
            this.dt3End.ShowUpDown = true;
            this.dt3End.Size = new System.Drawing.Size(65, 21);
            this.dt3End.TabIndex = 34;
            this.dt3End.Leave += new System.EventHandler(this.dt1Begin_Leave);
            // 
            // dt3Begin
            // 
            this.dt3Begin.Location = new System.Drawing.Point(105, 168);
            this.dt3Begin.Name = "dt3Begin";
            this.dt3Begin.ShowUpDown = true;
            this.dt3Begin.Size = new System.Drawing.Size(65, 21);
            this.dt3Begin.TabIndex = 33;
            this.dt3Begin.Leave += new System.EventHandler(this.dt1Begin_Leave);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            // 
            // 
            // 
            this.Label7.Location = new System.Drawing.Point(180, 170);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(13, 16);
            this.Label7.TabIndex = 32;
            this.Label7.Text = "-";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            // 
            // 
            // 
            this.Label8.Location = new System.Drawing.Point(37, 170);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(44, 18);
            this.Label8.TabIndex = 31;
            this.Label8.Text = "时段3:";
            // 
            // dt2End
            // 
            this.dt2End.Location = new System.Drawing.Point(204, 134);
            this.dt2End.Name = "dt2End";
            this.dt2End.ShowUpDown = true;
            this.dt2End.Size = new System.Drawing.Size(65, 21);
            this.dt2End.TabIndex = 30;
            this.dt2End.Leave += new System.EventHandler(this.dt1Begin_Leave);
            // 
            // dt2Begin
            // 
            this.dt2Begin.Location = new System.Drawing.Point(105, 135);
            this.dt2Begin.Name = "dt2Begin";
            this.dt2Begin.ShowUpDown = true;
            this.dt2Begin.Size = new System.Drawing.Size(65, 21);
            this.dt2Begin.TabIndex = 29;
            this.dt2Begin.Leave += new System.EventHandler(this.dt1Begin_Leave);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            // 
            // 
            // 
            this.Label4.Location = new System.Drawing.Point(180, 137);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(13, 16);
            this.Label4.TabIndex = 28;
            this.Label4.Text = "-";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            // 
            // 
            // 
            this.Label5.Location = new System.Drawing.Point(37, 137);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(44, 18);
            this.Label5.TabIndex = 27;
            this.Label5.Text = "时段2:";
            // 
            // combWeek
            // 
            this.combWeek.FormattingEnabled = true;
            this.combWeek.Location = new System.Drawing.Point(106, 65);
            this.combWeek.Name = "combWeek";
            this.combWeek.Size = new System.Drawing.Size(121, 20);
            this.combWeek.TabIndex = 26;
            this.combWeek.SelectedIndexChanged += new System.EventHandler(this.combWeek_SelectedIndexChanged);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            // 
            // 
            // 
            this.Label1.Location = new System.Drawing.Point(37, 67);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(37, 18);
            this.Label1.TabIndex = 25;
            this.Label1.Text = "星期:";
            // 
            // txtName
            // 
            // 
            // 
            // 
            this.txtName.Location = new System.Drawing.Point(106, 29);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(163, 21);
            this.txtName.TabIndex = 24;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            // 
            // 
            // 
            this.Label6.Location = new System.Drawing.Point(37, 32);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(37, 18);
            this.Label6.TabIndex = 23;
            this.Label6.Text = "名称:";
            // 
            // dt1End
            // 
            this.dt1End.Location = new System.Drawing.Point(204, 99);
            this.dt1End.Name = "dt1End";
            this.dt1End.ShowUpDown = true;
            this.dt1End.Size = new System.Drawing.Size(65, 21);
            this.dt1End.TabIndex = 22;
            this.dt1End.Leave += new System.EventHandler(this.dt1Begin_Leave);
            // 
            // dt1Begin
            // 
            this.dt1Begin.Location = new System.Drawing.Point(105, 100);
            this.dt1Begin.Name = "dt1Begin";
            this.dt1Begin.ShowUpDown = true;
            this.dt1Begin.Size = new System.Drawing.Size(65, 21);
            this.dt1Begin.TabIndex = 21;
            this.dt1Begin.Leave += new System.EventHandler(this.dt1Begin_Leave);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            // 
            // 
            // 
            this.Label3.Location = new System.Drawing.Point(180, 102);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(13, 16);
            this.Label3.TabIndex = 6;
            this.Label3.Text = "-";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            // 
            // 
            // 
            this.Label2.Location = new System.Drawing.Point(37, 102);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(44, 18);
            this.Label2.TabIndex = 5;
            this.Label2.Text = "时段1:";
            // 
            // FormTimeZone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 522);
            this.Controls.Add(this.groupBoxDetail);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTimeZone";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "时间限制管理器";
            this.Shown += new System.EventHandler(this.FormTimeZone_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBoxDetail.ResumeLayout(false);
            this.groupBoxDetail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvZone;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxDetail;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.DateTimePicker dt1Begin;
        private System.Windows.Forms.DateTimePicker dt1End;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.DateTimePicker dt4End;
        private System.Windows.Forms.DateTimePicker dt4Begin;
        private System.Windows.Forms.Label Label9;
        private System.Windows.Forms.Label Label10;
        private System.Windows.Forms.DateTimePicker dt3End;
        private System.Windows.Forms.DateTimePicker dt3Begin;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.DateTimePicker dt2End;
        private System.Windows.Forms.DateTimePicker dt2Begin;
        private System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.ComboBox combWeek;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.ComboBox combCopy;
        private System.Windows.Forms.Label Label11;
        private System.Windows.Forms.Button btnRestTime;
        private System.Windows.Forms.Button btnWorkTime;
     
    }
}