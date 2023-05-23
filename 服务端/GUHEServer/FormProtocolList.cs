using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GHIBMS.Server
{
    public partial class FormProtocolList : DevComponents.DotNetBar.Office2007Form
    {
        public FormProtocolList()
        {
            InitializeComponent();
        }
        private string selectDevText = "";
        private int selectDevID = 0;
        private string selectDevSort = "";
   

        public string SelectDevText
        {
            get { return selectDevText; }
        }
        public int SelectDevID
        {
            get { return selectDevID; }
        }
        public string SelectDevSort
        {
            get { return selectDevSort; }
        }
        private void FormProtocolList_Load(object sender, EventArgs e)
        {
            listView1.GridLines = true;     //显示各个记录的分隔线 
            listView1.FullRowSelect = true; //要选择就是一行 
            listView1.View = View.Details;  //定义列表显示的方式 
            listView1.Scrollable = true;    //需要时候显示滚动条 
            listView1.MultiSelect = false;  // 不可以多行选择 
            listView1.HeaderStyle = ColumnHeaderStyle.Clickable;
         
            listView1.Columns.Add("协议名称", 200, HorizontalAlignment.Center);
            listView1.Columns.Add("协议编号", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("设备分类", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("协议说明", 200, HorizontalAlignment.Center);

            //矩阵
            CreateNewProtocol("HoneywellHVB","101","矩阵", "");
            CreateNewProtocol("Pelco9700","102",   "矩阵", "");
            CreateNewProtocol("AD",      "103",    "矩阵", "");
            CreateNewProtocol("Bosch",   "104",    "矩阵", "");
            CreateNewProtocol("虚拟矩阵", "105", "矩阵", "");
            //DVR
            CreateNewProtocol("海康DVR", "110", "DVR", "");
            CreateNewProtocol("大华DVR", "111", "DVR", "");
            //流媒体
            CreateNewProtocol("海康VOD", "120", "VOD", "");
            CreateNewProtocol("大华VOD", "121", "VOD", "");

            //报警
            CreateNewProtocol("DS7400",   "201", "报警主机", "");
            CreateNewProtocol("VistaIPM", "202", "报警主机", "");
            CreateNewProtocol("IP2000",   "203", "报警主机", "");
            //门禁
            CreateNewProtocol("海恩门禁", "301", "门禁", "");
            //OPC
            CreateNewProtocol("OPC服务",  "501", "OPC服务", "");
            //短信
            CreateNewProtocol("短信模块", "601", "短信猫", "");
           
        }
        private void CreateNewProtocol(string name,string code,string devName,string desc)
        {
            ListViewItem lsvItem = new ListViewItem(name);

            lsvItem.ImageIndex = 0;
            lsvItem.SubItems.AddRange(new string[] {
                        code,devName,desc
                    });
            listView1.Items.Add(lsvItem);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            //Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            //Close();

        }

        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                selectDevID = int.Parse(listView1.SelectedItems[0].SubItems[1].Text);
                selectDevText = listView1.SelectedItems[0].Text;
                selectDevSort = listView1.SelectedItems[0].SubItems[2].Text;
            }
        }
        
    }
}
