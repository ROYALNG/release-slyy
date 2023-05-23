namespace GHIBMS.Server
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Pelco-D", 1, 1);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Pelco-P", 1, 1);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("摄像机", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("矩阵[Honeywell HVB]", 1, 1);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("矩阵[Pelco 9700]", 1, 1);
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("矩阵[AD]", 1, 1);
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("矩阵[Bosch]", 1, 1);
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("虚拟矩阵", 1, 1);
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("矩阵[OEM]", 1, 1);
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("视频矩阵", 0, 0, new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("海康DVR", 1, 1);
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("大华DVR", 1, 1);
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("视频编码", new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("海康VOD", 1, 1);
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("流媒体", new System.Windows.Forms.TreeNode[] {
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("报警主机[Bosch DS7400]", 1, 1);
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("报警主机[Honeywell IPM]", 1, 1);
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("报警主机[Honeywell 4100]", 1, 1);
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("报警设备", 0, 0, new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode17,
            treeNode18});
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("门禁[海恩]", 1, 1);
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("门禁设备", 0, 0, new System.Windows.Forms.TreeNode[] {
            treeNode20});
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("继电器[Se602]", 1, 1);
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("输出设备", 0, 0, new System.Windows.Forms.TreeNode[] {
            treeNode22});
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("DA2.0", 1, 1);
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("OPC设备", 0, 0, new System.Windows.Forms.TreeNode[] {
            treeNode24});
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("AT指令", 1, 1);
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("短信模块", 0, 0, new System.Windows.Forms.TreeNode[] {
            treeNode26});
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("巡更设备", 0, 0);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDeviceType));
            this.trvDevType = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // trvDevType
            // 
            this.trvDevType.ImageIndex = 0;
            this.trvDevType.ImageList = this.imageList1;
            this.trvDevType.Location = new System.Drawing.Point(34, 42);
            this.trvDevType.Name = "trvDevType";
            treeNode1.ImageIndex = 1;
            treeNode1.Name = "节点1";
            treeNode1.SelectedImageIndex = 1;
            treeNode1.Tag = "106";
            treeNode1.Text = "Pelco-D";
            treeNode2.ImageIndex = 1;
            treeNode2.Name = "节点2";
            treeNode2.SelectedImageIndex = 1;
            treeNode2.Tag = "107";
            treeNode2.Text = "Pelco-P";
            treeNode3.Name = "节点0";
            treeNode3.Tag = "CAM";
            treeNode3.Text = "摄像机";
            treeNode4.ImageIndex = 1;
            treeNode4.Name = "节点5";
            treeNode4.SelectedImageIndex = 1;
            treeNode4.Tag = "101";
            treeNode4.Text = "矩阵[Honeywell HVB]";
            treeNode5.ImageIndex = 1;
            treeNode5.Name = "节点6";
            treeNode5.SelectedImageIndex = 1;
            treeNode5.Tag = "102";
            treeNode5.Text = "矩阵[Pelco 9700]";
            treeNode6.ImageIndex = 1;
            treeNode6.Name = "节点7";
            treeNode6.SelectedImageIndex = 1;
            treeNode6.Tag = "103";
            treeNode6.Text = "矩阵[AD]";
            treeNode7.ImageIndex = 1;
            treeNode7.Name = "节点1";
            treeNode7.SelectedImageIndex = 1;
            treeNode7.Tag = "104";
            treeNode7.Text = "矩阵[Bosch]";
            treeNode8.ImageIndex = 1;
            treeNode8.Name = "节点0";
            treeNode8.SelectedImageIndex = 1;
            treeNode8.Tag = "105";
            treeNode8.Text = "虚拟矩阵";
            treeNode9.ImageIndex = 1;
            treeNode9.Name = "节点0";
            treeNode9.SelectedImageIndex = 1;
            treeNode9.Tag = "112";
            treeNode9.Text = "矩阵[OEM]";
            treeNode10.ImageIndex = 0;
            treeNode10.Name = "nodeCCTV";
            treeNode10.SelectedImageIndex = 0;
            treeNode10.Tag = "MAT";
            treeNode10.Text = "视频矩阵";
            treeNode11.ImageIndex = 1;
            treeNode11.Name = "hikdvr";
            treeNode11.SelectedImageIndex = 1;
            treeNode11.Tag = "110";
            treeNode11.Text = "海康DVR";
            treeNode11.ToolTipText = "111";
            treeNode12.ImageIndex = 1;
            treeNode12.Name = "节点4";
            treeNode12.SelectedImageIndex = 1;
            treeNode12.Tag = "111";
            treeNode12.Text = "大华DVR";
            treeNode12.ToolTipText = "112";
            treeNode13.Name = "DVR";
            treeNode13.Tag = "DVR";
            treeNode13.Text = "视频编码";
            treeNode14.ImageIndex = 1;
            treeNode14.Name = "节点5";
            treeNode14.SelectedImageIndex = 1;
            treeNode14.Tag = "120";
            treeNode14.Text = "海康VOD";
            treeNode15.Name = "vod";
            treeNode15.Tag = "VOD";
            treeNode15.Text = "流媒体";
            treeNode16.ImageIndex = 1;
            treeNode16.Name = "节点8";
            treeNode16.SelectedImageIndex = 1;
            treeNode16.Tag = "201";
            treeNode16.Text = "报警主机[Bosch DS7400]";
            treeNode17.ImageIndex = 1;
            treeNode17.Name = "节点9";
            treeNode17.SelectedImageIndex = 1;
            treeNode17.Tag = "202";
            treeNode17.Text = "报警主机[Honeywell IPM]";
            treeNode18.ImageIndex = 1;
            treeNode18.Name = "节点2";
            treeNode18.SelectedImageIndex = 1;
            treeNode18.Tag = "203";
            treeNode18.Text = "报警主机[Honeywell 4100]";
            treeNode19.ImageIndex = 0;
            treeNode19.Name = "nodeALM";
            treeNode19.SelectedImageIndex = 0;
            treeNode19.Tag = "ALM";
            treeNode19.Text = "报警设备";
            treeNode20.ImageIndex = 1;
            treeNode20.Name = "节点4";
            treeNode20.SelectedImageIndex = 1;
            treeNode20.Tag = "301";
            treeNode20.Text = "门禁[海恩]";
            treeNode21.ImageIndex = 0;
            treeNode21.Name = "nodeAccess";
            treeNode21.SelectedImageIndex = 0;
            treeNode21.Tag = "ACC";
            treeNode21.Text = "门禁设备";
            treeNode22.ImageIndex = 1;
            treeNode22.Name = "nodSe602";
            treeNode22.SelectedImageIndex = 1;
            treeNode22.Text = "继电器[Se602]";
            treeNode23.ImageIndex = 0;
            treeNode23.Name = "NodeRelay";
            treeNode23.SelectedImageIndex = 0;
            treeNode23.Tag = "OUT";
            treeNode23.Text = "输出设备";
            treeNode24.ImageIndex = 1;
            treeNode24.Name = "nodDA20";
            treeNode24.SelectedImageIndex = 1;
            treeNode24.Tag = "501";
            treeNode24.Text = "DA2.0";
            treeNode25.ImageIndex = 0;
            treeNode25.Name = "nodOPC";
            treeNode25.SelectedImageIndex = 0;
            treeNode25.Tag = "OPC";
            treeNode25.Text = "OPC设备";
            treeNode26.ImageIndex = 1;
            treeNode26.Name = "AT指令";
            treeNode26.SelectedImageIndex = 1;
            treeNode26.Tag = "601";
            treeNode26.Text = "AT指令";
            treeNode27.ImageIndex = 0;
            treeNode27.Name = "节点0";
            treeNode27.SelectedImageIndex = 0;
            treeNode27.Tag = "SMS";
            treeNode27.Text = "短信模块";
            treeNode28.ImageIndex = 0;
            treeNode28.Name = "节点1";
            treeNode28.SelectedImageIndex = 0;
            treeNode28.Tag = "TOU";
            treeNode28.Text = "巡更设备";
            this.trvDevType.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode10,
            treeNode13,
            treeNode15,
            treeNode19,
            treeNode21,
            treeNode23,
            treeNode25,
            treeNode27,
            treeNode28});
            this.trvDevType.SelectedImageIndex = 0;
            this.trvDevType.Size = new System.Drawing.Size(418, 410);
            this.trvDevType.TabIndex = 0;
            this.trvDevType.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvDevType_NodeMouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "device16.png");
            this.imageList1.Images.SetKeyName(1, "protocol.ico");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "请选择设备通讯协议：";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(476, 427);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 27);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(476, 386);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(476, 304);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 27);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保存列表";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(476, 345);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 27);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "导入列表";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click_1);
            // 
            // FormDeviceType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 474);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trvDevType);
            this.Name = "FormDeviceType";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "通讯协议";
            this.Load += new System.EventHandler(this.FormDeviceType_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView trvDevType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
    }
}