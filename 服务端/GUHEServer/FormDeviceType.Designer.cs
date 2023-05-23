namespace SOPC
{
    partial class FormDeviceType
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("矩阵[Honeywell]", 2, 2);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("矩阵[Pelco]", 2, 2);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("矩阵[AD]", 2, 2);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("监控设备", 0, 1, new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("报警主机[Bosch DS7400]", 2, 2);
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("报警主机[Honeywell VISTA]", 2, 2);
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("报警设备", 0, 1, new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("门禁设备", 0, 1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDeviceType));
            this.trvDevType = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // trvDevType
            // 
            this.trvDevType.ImageIndex = 0;
            this.trvDevType.ImageList = this.imageList1;
            this.trvDevType.Location = new System.Drawing.Point(32, 40);
            this.trvDevType.Name = "trvDevType";
            treeNode1.ImageIndex = 2;
            treeNode1.Name = "节点5";
            treeNode1.SelectedImageIndex = 2;
            treeNode1.Tag = "101";
            treeNode1.Text = "矩阵[Honeywell]";
            treeNode2.ImageIndex = 2;
            treeNode2.Name = "节点6";
            treeNode2.SelectedImageIndex = 2;
            treeNode2.Tag = "102";
            treeNode2.Text = "矩阵[Pelco]";
            treeNode3.ImageIndex = 2;
            treeNode3.Name = "节点7";
            treeNode3.SelectedImageIndex = 2;
            treeNode3.Tag = "103";
            treeNode3.Text = "矩阵[AD]";
            treeNode4.ImageIndex = 0;
            treeNode4.Name = "nodeCCTV";
            treeNode4.SelectedImageIndex = 1;
            treeNode4.Tag = "1";
            treeNode4.Text = "监控设备";
            treeNode5.ImageIndex = 2;
            treeNode5.Name = "节点8";
            treeNode5.SelectedImageIndex = 2;
            treeNode5.Tag = "201";
            treeNode5.Text = "报警主机[Bosch DS7400]";
            treeNode6.ImageIndex = 2;
            treeNode6.Name = "节点9";
            treeNode6.SelectedImageIndex = 2;
            treeNode6.Tag = "202";
            treeNode6.Text = "报警主机[Honeywell VISTA]";
            treeNode7.ImageIndex = 0;
            treeNode7.Name = "nodeALM";
            treeNode7.SelectedImageIndex = 1;
            treeNode7.Tag = "2";
            treeNode7.Text = "报警设备";
            treeNode8.ImageIndex = 0;
            treeNode8.Name = "nodeAccess";
            treeNode8.SelectedImageIndex = 1;
            treeNode8.Tag = "3";
            treeNode8.Text = "门禁设备";
            this.trvDevType.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode7,
            treeNode8});
            this.trvDevType.SelectedImageIndex = 1;
            this.trvDevType.Size = new System.Drawing.Size(312, 267);
            this.trvDevType.TabIndex = 0;
            this.trvDevType.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvDevType_NodeMouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "(02,29).png");
            this.imageList1.Images.SetKeyName(1, "(01,34).png");
            this.imageList1.Images.SetKeyName(2, "(03,00).png");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "请选择设备型号：";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(365, 284);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(62, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(365, 238);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormDeviceType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 333);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trvDevType);
            this.Name = "FormDeviceType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设备选型";
            this.Load += new System.EventHandler(this.FormDeviceType_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trvDevType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ImageList imageList1;
    }
}