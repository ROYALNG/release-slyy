using GHIBMS.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GHIBMS.Server
{
    public partial class VarForm : Form
    {
        private static List<BaseChannel> DriverList;
        AccidenceAnalyzer express;
        public VarForm()
        {
            InitializeComponent();
            express = new AccidenceAnalyzer();
            express.OnAccidenceAnalysis += new AccidenceAnalyzer.AccidenceAnalysis(express_OnAccidenceAnalysis);
            express.OnCallBack += new AccidenceAnalyzer.CallBack(express_OnCallBack);
            button2.Enabled = false;
            button3.Enabled = false;
        }
        //������������ص�������
        static object express_OnCallBack(string funcName, object[] param, ref bool isOk)
        {
            isOk = true;
            string fun = funcName.ToUpper();
            if (fun.Equals("SIN"))
            {
                return Math.Sin(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("COS"))
            {
                return Math.Cos(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("LOG10"))
            {
                return Math.Log10(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("EXP"))
            {
                return Math.Exp(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("ASIN"))
            {
                return Math.Asin(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("ACOS"))
            {
                return Math.Acos(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("ABS"))
            {
                return Math.Abs(Convert.ToDecimal(param[0]));
            }
            else if (fun.Equals("ATAN"))
            {
                return Math.Atan(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("CEILING"))
            {
                return Math.Ceiling(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("COSH"))
            {
                return Math.Cosh(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("FLOOR"))
            {
                return Math.Floor(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("SINH"))
            {
                return Math.Sinh(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("SQRT"))
            {
                return Math.Sqrt(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("TAN"))
            {
                return Math.Tan(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("TANH"))
            {
                return Math.Tanh(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("TRUNCATE"))
            {
                return Math.Truncate(Convert.ToDouble(param[0]));
            }
            isOk = false;
            return 0;
        }
        //������������ص�������
        static bool express_OnAccidenceAnalysis(ref Operand opd)
        {
            //��������valueֵ

            foreach (BaseChannel chan in DriverList)
            {
                foreach (BaseController con in chan.ConList)
                {
                    foreach (BaseVariable var in con.VarList)
                    {
                        if (var.Name.Equals(opd.Value.ToString()))
                        {
                            opd.Type = OperandType.NUMBER;
                            opd.Value = var.Value;
                            return true;
                        }

                    }
                }
            }

            opd.Type = OperandType.NUMBER;
            opd.Value = 0;
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool bok = express.Parse(textBox1.Text);
            if (bok)
            {
                bool ok = false;
                object d = express.Evaluate(ref ok);
                if (ok)
                {
                    errorProvider1.SetError((Control)textBox1, "");
                    //MessageBox.Show(this, "OK��", "��Ϣ��ʾ");
                    button2.Enabled = false;
                }
                else
                {
                    errorProvider1.SetIconAlignment((Control)textBox1, ErrorIconAlignment.MiddleRight);
                    errorProvider1.SetError((Control)textBox1, "�д�������������ʽ�������!");
                }
            }
            else
            {
                errorProvider1.SetIconAlignment((Control)textBox1, ErrorIconAlignment.MiddleRight);
                errorProvider1.SetError((Control)textBox1, "�д�������������ʽ�������!");
            }
        }
        public string VarExpress
        {
            get
            {
                return this.textBox1.Text;
            }
            set
            {
                this.textBox1.Text = value;
            }
        }
        public void actionList(List<BaseChannel> List)
        {
            DriverList = List;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private void addfun()
        {
            int pos = 0;
            listViewFunction.Items.Clear();
            ListViewItem item = new ListViewItem();
            item = listViewFunction.Items.Add("Sin");
            listViewFunction.Items[pos++].ImageIndex = 0;
            item.SubItems.Add("����ָ���Ƕȵ�����ֵ");

            item = new ListViewItem();
            item = listViewFunction.Items.Add("Cos");
            listViewFunction.Items[pos++].ImageIndex = 0;
            item.SubItems.Add("����ָ���Ƕȵ�����ֵ");

            item = new ListViewItem();
            item = listViewFunction.Items.Add("Log10");
            listViewFunction.Items[pos++].ImageIndex = 0;
            item.SubItems.Add("����ָ��������10Ϊ�׵Ķ���");

            item = new ListViewItem();
            item = listViewFunction.Items.Add("Exp");
            listViewFunction.Items[pos++].ImageIndex = 0;
            item.SubItems.Add("����e��ָ������");

            item = new ListViewItem();
            item = listViewFunction.Items.Add("Asin");
            listViewFunction.Items[pos++].ImageIndex = 0;
            item.SubItems.Add("��������ֵΪָ�����ֵĽǶ�");

            item = new ListViewItem();
            item = listViewFunction.Items.Add("Acos");
            listViewFunction.Items[pos++].ImageIndex = 0;
            item.SubItems.Add("��������ֵΪָ�����ֵĽǶ�");

            item = new ListViewItem();
            item = listViewFunction.Items.Add("Abs");
            listViewFunction.Items[pos++].ImageIndex = 0;
            item.SubItems.Add("����ָ�����ֵľ���ֵ");

            item = new ListViewItem();
            item = listViewFunction.Items.Add("Atan");
            listViewFunction.Items[pos++].ImageIndex = 0;
            item.SubItems.Add("��������ֵΪָ�����ֵĽǶ�");

            item = new ListViewItem();
            item = listViewFunction.Items.Add("Ceiling");
            listViewFunction.Items[pos++].ImageIndex = 0;
            item.SubItems.Add("���ش��ڻ���ڸ�ָ��˫���ȸ���������С����");

            item = new ListViewItem();
            item = listViewFunction.Items.Add("Cosh");
            listViewFunction.Items[pos++].ImageIndex = 0;
            item.SubItems.Add("����ָ���Ƕȵ�˫������ֵ");

            item = new ListViewItem();
            item = listViewFunction.Items.Add("Floor");
            listViewFunction.Items[pos++].ImageIndex = 0;
            item.SubItems.Add("����С�ڻ���ڸ�ָ��˫���ȸ��������������");

            item = new ListViewItem();
            item = listViewFunction.Items.Add("Sinh");
            listViewFunction.Items[pos++].ImageIndex = 0;
            item.SubItems.Add("����ָ���Ƕȵ�˫������ֵ");

            item = new ListViewItem();
            item = listViewFunction.Items.Add("Sqrt");
            listViewFunction.Items[pos++].ImageIndex = 0;
            item.SubItems.Add("����ָ�����ֵ�ƽ����");

            item = new ListViewItem();
            item = listViewFunction.Items.Add("Tan");
            listViewFunction.Items[pos++].ImageIndex = 0;
            item.SubItems.Add("����ָ���Ƕȵ�����ֵ");

            item = new ListViewItem();
            item = listViewFunction.Items.Add("Tanh");
            listViewFunction.Items[pos++].ImageIndex = 0;
            item.SubItems.Add("����ָ���Ƕȵ�˫������ֵ");

            item = new ListViewItem();
            item = listViewFunction.Items.Add("Truncate");
            listViewFunction.Items[pos++].ImageIndex = 0;
            item.SubItems.Add("����ָ��˫���ȸ���������������");

            item = new ListViewItem();
            item = listViewFunction.Items.Add("+��-��*��/��%��()");
            listViewFunction.Items[pos++].ImageIndex = 0;
            item.SubItems.Add("�������㣺�ӡ������ˡ��������࣬��������");

            item = new ListViewItem();
            item = listViewFunction.Items.Add(">=,>,<,<=,=");
            listViewFunction.Items[pos++].ImageIndex = 0;
            item.SubItems.Add("�߼����㣺���ڵ��ڡ����ڡ�С�ڡ�С�ڵ��ڡ�����");

            item = new ListViewItem();
            item = listViewFunction.Items.Add("&��|��������");
            listViewFunction.Items[pos++].ImageIndex = 0;
            item.SubItems.Add("�߼����㣺�롢�򡢷ǡ���������");

            item = new ListViewItem();
            item = listViewFunction.Items.Add("ע�⣺");
            listViewFunction.Items[pos++].ImageIndex = 1;
            item.SubItems.Add("������ֻ���ǳ������߱����������Ǵ������ı��ʽ�������ο���ο�C#�����﷨");

            listViewFunction.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void VarForm_Load(object sender, EventArgs e)
        {
            addfun();
            if (DriverList != null)
            {
                listViewVar.Items.Clear();
                int pos = 0;

                foreach (BaseChannel chan in DriverList)
                {
                    foreach (BaseController con in chan.ConList)
                    {
                        foreach (BaseVariable var in con.VarList)
                        {
                            ListViewItem item = new ListViewItem();
                            item = listViewVar.Items.Add(var.Name);
                            listViewVar.Items[pos++].ImageIndex = 2;
                            item.SubItems.Add(var.ControllerObject.Name);
                            item.SubItems.Add(var.Description);

                        }
                    }
                }
                listViewVar.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        private void listViewVar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listViewVar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewVar.SelectedItems.Count == 1)
            {
                string name = listViewVar.SelectedItems[0].SubItems[0].Text;
                VarExpress = name;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button3.Enabled = true;
            button2.Enabled = true;
        }

        private void listViewFunction_DoubleClick(object sender, EventArgs e)
        {
            //if (listViewFunction.SelectedItems.Count > 0)
            //{
            //    textBox1.Text += listViewFunction.SelectedItems[0].Text;
            //}
        }
    }
}