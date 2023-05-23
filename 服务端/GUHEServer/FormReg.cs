using GHIBMS.Common;
using GHIBMS.Pub;
using System;
using System.Windows.Forms;


namespace GHIBMS.Server
{
    public partial class FormReg : Form
    {
        //private string str1 = "";
        //private string str2 = "";
        //private string str3 = "";
        //private string str4 = "";
        //private string str5 = "";
        //private string str6 = "";
        public FormReg()
        {
            InitializeComponent();
        }

        private void FormReg_Load(object sender, EventArgs e)
        {
            RSAHelper rsa = new RSAHelper();
            textBox1.Text = rsa.CreateGomputerbit(StrConst.SOFTNAME);
            textBox2.Text = ServerConfig.Expire;
            //机器码||当前时间||授权类型||注册码||试运行时间||最大点数

            string code = ServerConfig.Expire;
            string code1 = rsa.RSADecrypt(code);
            string[] arr = code1.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length == 6)
            {
                if (arr[0] == textBox1.Text)
                {
                    if (rsa.CheckSoftRegCodeByInput(StrConst.SOFTNAME, arr[3]))
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "永久授权";
                    }
                    else
                    {
                        if (arr[2] == "0")
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "演示授权";
                        }
                        else if (arr[2] == "1")
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "试运行授权";
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "无授权";
                        }
                    }
                }
            }
            else
            {
                textBox2.Text = ServerConfig.Expire = "";
                lblMsg.Visible = true;
                lblMsg.Text = "无授权";
            }
        }



        private void btnReg_Click(object sender, EventArgs e)
        {
            RSAHelper rsa = new RSAHelper();
            textBox1.Text = rsa.CreateGomputerbit(StrConst.SOFTNAME);
            //机器码||当前时间||授权类型||注册码||试运行时间||最大点数

            string code = textBox2.Text;
            string code1 = rsa.RSADecrypt(code);
            string[] arr = code1.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length == 6)
            {
                if (arr[0] == textBox1.Text)
                {
                    if (rsa.CheckSoftRegCodeByInput(StrConst.SOFTNAME, arr[3]))
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "永久授权";
                        ServerConfig.Expire = textBox2.Text;
                        ServerConfig.saveToFile();

                    }
                    else
                    {
                        if (arr[2] == "0")
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "演示授权";
                            ServerConfig.Expire = textBox2.Text;
                            ServerConfig.saveToFile();

                        }
                        else if (arr[2] == "1")
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "试运行授权";
                            ServerConfig.Expire = textBox2.Text;
                            ServerConfig.saveToFile();

                        }
                        else
                        {
                            textBox2.Text = ServerConfig.Expire = "";
                            lblMsg.Visible = true;
                            lblMsg.Text = "无授权";
                        }
                    }
                }
                MessageBox.Show("软件授权完成，重启后生效！");
            }
            else
            {
                textBox2.Text = ServerConfig.Expire = "";
                lblMsg.Visible = true;
                lblMsg.Text = "无授权";
            }
        }
    }
}
