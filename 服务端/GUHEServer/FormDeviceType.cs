using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOPC
{
    public partial class FormDeviceType : Form
    {
        private string selectDevText="";
        private int selectDevID=0;
        private int devClass=0;

        public string SelectDevText
        {
            get { return selectDevText; }
        }
        public int SelectDevID
        {
            get { return selectDevID; }
        }
        public int DevClass
        {
            set { devClass = value; }
        }
        public FormDeviceType(int deviceClass)
        {
            devClass = deviceClass;
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if ((trvDevType.SelectedNode != null) && (trvDevType.SelectedNode.Parent!=null) && (Convert.ToInt32(trvDevType.SelectedNode.Parent.Tag) == devClass))
            {

                selectDevText = trvDevType.SelectedNode.Text;
                selectDevID = Convert.ToInt32(trvDevType.SelectedNode.Tag);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("请正确选择设备类型！");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }

        private void FormDeviceType_Load(object sender, EventArgs e)
        {
            trvDevType.ExpandAll();
        }

        private void trvDevType_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            btnOK_Click(null,null);
        }
    }
}
