
using GHIBMS.Common;
using GHIBMS.Interface;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GHIBMS.Server
{
    public partial class FormSearch : Form
    {
        IController curController;
        ListViewEx curListView;
        //初始化绑定默认关键词（此数据源可以从数据库取）

        List<string> listOnit = new List<string>();
        //输入key之后，返回的关键词
        List<string> listNew = new List<string>();

        public FormSearch()
        {
            InitializeComponent();


        }

        public string SelectVarialbe
        {
            get
            {
                return comboBox1.Text;
            }
        }
        public void SetController(IController con, ListViewEx list)
        {
            curController = con;
            curListView = list;
            listOnit.Clear();
            comboBox1.Items.Clear();
            foreach (IVariable var in con.VarList)
            {
                listOnit.Add(var.Name);
            }

            //this.comboBox1.Items.Clear();
            //string[] tmp = listOnit.ToArray(); 
            //comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //this.comboBox1.AutoCompleteCustomSource.Clear();
            //this.comboBox1.AutoCompleteCustomSource.AddRange(tmp);
        }



        private void FormSearch_Shown(object sender, EventArgs e)
        {
            //this.comboBox1.Items.Clear();
            //string[] tmp = listOnit.ToArray(); ;
            //comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //this.comboBox1.AutoCompleteCustomSource.Clear();
            //this.comboBox1.AutoCompleteCustomSource.AddRange(tmp);
        }

        private void btnSearchALL_Click(object sender, EventArgs e)
        {
            string s = comboBox1.Text;
            comboBox1.Text = "";

            listNew.Clear();
            foreach (string s1 in listOnit)
            {
                if (s1.ToLower().Contains(s.ToLower()))
                {
                    listNew.Add(s1);
                }
            }
            string[] tmp = listNew.ToArray();
            comboBox1.AutoCompleteMode = AutoCompleteMode.None;
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(tmp);
            this.comboBox1.DroppedDown = true;


        }

        private void btnAutoComplete_Click(object sender, EventArgs e)
        {
            string[] tmp = listOnit.ToArray();
            comboBox1.AutoCompleteMode = AutoCompleteMode.None;
            this.comboBox1.AutoCompleteCustomSource.Clear();
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.comboBox1.AutoCompleteCustomSource.AddRange(tmp);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // curListView.SelectedNo();
            if (comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("当前项为空，请选择从查找结果中选择当前项。");
                return;
            }

            foreach (ListViewItem item in curListView.CurrentCacheItemsSource)
            {
                item.EnsureVisible();
                item.Selected = false;
                if (item.SubItems[1].Text == comboBox1.Text)
                {
                    item.Selected = true;

                }
            }
            curListView.Invalidate();
            curListView.Focus();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //curListView.SelectedNo();

            try
            {
                foreach (ListViewItem item in curListView.CurrentCacheItemsSource)
                {
                    item.EnsureVisible();
                    item.Selected = false;
                    foreach (string s in listNew)
                    {
                        if (item.SubItems[1].Text == s)
                        {
                            item.Selected = true;
                        }
                    }

                }
                curListView.Select();
                curListView.Invalidate();
                curListView.Focus();
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());

            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();

        }

        private void FormSearch_FormClosed(object sender, FormClosedEventArgs e)
        {
            curListView.Focus();
        }
    }
}
