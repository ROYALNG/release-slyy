using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using GHIBMS.Common;

namespace GHIBMS.Server
{
    public partial class FormDeviceType : DevComponents.DotNetBar.Office2007Form
    {
        private string selectDevText = "";
        private int selectDevID = 0;
 

        public string SelectDevText
        {
            get { return selectDevText; }
        }
        public int SelectDevID
        {
            get { return selectDevID; }
        }
       
        public FormDeviceType()
        {
          
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if ((trvDevType.SelectedNode != null) && (trvDevType.SelectedNode.Parent != null))
            {
                //if (Convert.ToInt32(trvDevType.SelectedNode.Tag) == (int)ProtocolCodeEnum.OPC_DA20)
                //{
                    selectDevText = trvDevType.SelectedNode.Text;
                    selectDevID = Convert.ToInt32(trvDevType.SelectedNode.Tag);
                    this.DialogResult = DialogResult.OK;
               // }
                //else
                //{
                //    MessageBox.Show("请正确选择设备类型！");
                //}
            }
            else
            {
               // btnOK.DialogResult = DialogResult.None;
                MessageBox.Show("请正确选择设备类型！");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void FormDeviceType_Load(object sender, EventArgs e)
        {
            //trvDevType.ExpandAll();
        }

        private void trvDevType_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            btnOK_Click(null,null);
        }

     
        #region TreeView 2 XML

        private void btnSave_Click(object sender, EventArgs e)
        {
            //将TreeView保存到XML文件中
            SaveFileDialog dlgSave = new SaveFileDialog();
            dlgSave.Filter = "数据文件(*.xml)|*.xml|All files (*.*)|*.*";
            dlgSave.FilterIndex = 0;
            dlgSave.RestoreDirectory = true;
            dlgSave.Title = "保存文件";
            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<DeviceProtocol></DeviceProtocol>");
                XmlNode root = doc.DocumentElement;
                doc.InsertBefore(doc.CreateXmlDeclaration("1.0", "utf-8", "yes"), root);
                TreeNode2Xml(this.trvDevType.Nodes, root);
                doc.Save(dlgSave.FileName);
            }
        }

        private void TreeNode2Xml(TreeNodeCollection treeNodes, XmlNode xmlNode)
        {
            XmlDocument doc = xmlNode.OwnerDocument;
            foreach (TreeNode treeNode in treeNodes)
            {
                XmlNode element = doc.CreateNode("element", "Item", "");
                XmlAttribute attr = doc.CreateAttribute("Title");
                attr.Value = treeNode.Text;
                element.Attributes.Append(attr);
                //element.AppendChild(doc.CreateCDataSection(treeNode.Tag.ToString()));
                xmlNode.AppendChild(element);

                if (treeNode.Nodes.Count > 0)
                {
                    TreeNode2Xml(treeNode.Nodes, element);
                }
            }
        }
        #endregion
        #region XML 2 TreeView

        private void XmlNode2TreeNode(XmlNodeList xmlNode, TreeNodeCollection treeNode)
        {
            foreach (XmlNode var in xmlNode)
            {
                if (var.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                TreeNode newTreeNode = new TreeNode();
                newTreeNode.Text = var.Attributes["Title"].Value;

                if (var.HasChildNodes)
                {
                    if (var.ChildNodes[0].NodeType == XmlNodeType.CDATA)
                    {
                        newTreeNode.Tag = var.ChildNodes[0].Value;
                    }

                    XmlNode2TreeNode(var.ChildNodes, newTreeNode.Nodes);
                }
                treeNode.Add(newTreeNode);
            }
        }
        #endregion

        private void btnLoad_Click_1(object sender, EventArgs e)
        {
            //从XML中读取数据到TreeView
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "数据文件 (*.xml)|*.xml|All files (*.*)|*.*";
            dlgOpen.FilterIndex = 0;
            dlgOpen.RestoreDirectory = true;
            dlgOpen.FileName = "";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(dlgOpen.FileName);

                XmlNodeList xmlNodes = xmlDoc.DocumentElement.ChildNodes;

                this.trvDevType.BeginUpdate();
                this.trvDevType.Nodes.Clear();
                XmlNode2TreeNode(xmlNodes, this.trvDevType.Nodes);
                this.trvDevType.EndUpdate();
            }

        }
      

    }
}
