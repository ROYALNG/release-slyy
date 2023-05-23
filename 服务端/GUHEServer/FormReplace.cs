
using GHIBMS.Common;
using GHIBMS.Interface;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GHIBMS.Server
{
    public partial class FormReplace : Form
    {
        IController curController;
        ListViewEx curListView;
        //初始化绑定默认关键词（此数据源可以从数据库取）

        List<string> listOnit = new List<string>();
        //输入key之后，返回的关键词
        List<string> listNew = new List<string>();
        FormMain formmain;
        public FormReplace(FormMain frm)
        {
            InitializeComponent();
            formmain = frm;

        }


        public void SetController(IController con, ListViewEx list)
        {
            curController = con;
            curListView = list;
            listOnit.Clear();
            foreach (IVariable var in con.VarList)
            {
                listOnit.Add(var.Name);
            }



        }



        private void FormSearch_Shown(object sender, EventArgs e)
        {

            //string[] tmp = listOnit.ToArray(); ;

        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            if (textBoxX1.Text == "")
            {
                MessageBox.Show("查找的字符串不能为空！");
                return;

            }
            foreach (IVariable var in curController.VarList)
            {
                string name2 = var.Name.Replace(textBoxX1.Text, textBoxX2.Text);
                if (!Rtdb.IsExistName(name2))
                {
                    // MessageBox.Show("替换失败！"+name2+"变量名称已经存在了！");
                    // return;
                    var.Name = name2;// var.Name.Replace(textBoxX1.Text, textBoxX2.Text);
                }

            }
            formmain.RefreshListViewDev();
        }
    }
}
