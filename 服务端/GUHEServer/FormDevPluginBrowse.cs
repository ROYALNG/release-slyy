using GHIBMS.Common;
using GHIBMS.Interface;
using System;
using System.Windows.Forms;


namespace GHIBMS.Server
{
    public partial class FormDevPluginBrowse : Form
    {
        public FormDevPluginBrowse()
        {
            InitializeComponent();
        }
        public Protocol selectedProtocol;
        private void FormDevPluginBrowse_Shown(object sender, EventArgs e)
        {
            trvPlugin.Nodes.Clear();
            //系统Lable
            foreach (ICommunicationPlug plug in PluginMng.CommPlugs)
            {
                bool bExist = false;
                foreach (TreeNode node in trvPlugin.Nodes)
                {
                    if (node.Text == plug.SystemLabel)
                    {
                        bExist = true;
                        break;
                    }
                }
                if (!bExist)
                {
                    TreeNode sysNode = new TreeNode(plug.SystemLabel);
                    sysNode.ImageIndex = 2;
                    sysNode.SelectedImageIndex = 2;
                    trvPlugin.Nodes.Add(sysNode);
                }
            }

            foreach (ICommunicationPlug plug in PluginMng.CommPlugs)
            {

                TreeNode parentNode = GetPlugParent(plug.SystemLabel);
                if (parentNode != null)
                {
                    TreeNode newPlugNode = new TreeNode(plug.Name);
                    newPlugNode.ImageIndex = 0;
                    newPlugNode.SelectedImageIndex = 0;
                    parentNode.Nodes.Add(newPlugNode);
                    foreach (Protocol pro in plug.ProtocolList)
                    {
                        TreeNode proNode = new TreeNode(pro.ProtocolName);
                        proNode.ImageIndex = 1;
                        proNode.SelectedImageIndex = 1;
                        proNode.Tag = pro;
                        newPlugNode.Nodes.Add(proNode);
                    }
                }
            }
            trvPlugin.CollapseAll();

        }
        private TreeNode GetPlugParent(string text)
        {
            foreach (TreeNode node in trvPlugin.Nodes)
            {
                if (node.Text == text)
                {
                    return node;
                }
            }
            return null;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (trvPlugin.SelectedNode != null && trvPlugin.SelectedNode.Tag != null && trvPlugin.SelectedNode.Tag is Protocol)
            {

                selectedProtocol = trvPlugin.SelectedNode.Tag as Protocol;
                this.DialogResult = DialogResult.OK;

            }
            else
            {
                MessageBox.Show("请正确选择设备通讯插件后，再重试！");
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void trvPlugin_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            btnOK.PerformClick();
        }
    }
}
