using GHDatabase.Influxdb;
using GHIBMS.Common;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace GHIBMS.Server
{
    public partial class FormConfig : Form
    {
        public FormConfig()
        {
            InitializeComponent();
            try
            {
                textBoxDBHost.Text = ServerConfig.DbHost;
                textBoxDBName.Text = ServerConfig.DbName;
                textBoxDBUser.Text = ServerConfig.DbUser;
                txtDbPort.Text = ServerConfig.DBPort.ToString();//new
                textBoxDBPw.Text = ServerConfig.DbPw;
                chkDB.Checked = ServerConfig.DataBaseEnable;

                txtInfluxIP.Text = ServerConfig.influxIP;
                txtInfluxUser.Text = ServerConfig.influxUsername;
                txtInfluxPw.Text = ServerConfig.influxPassword;
                chkInflux.Checked = ServerConfig.influxEnalble;
                txtInfluxPort.Text = ServerConfig.influxPort.ToString();//new
                txtInfluxName.Text = ServerConfig.influxName;//new

                txtModbsuIp.Text = ServerConfig.ModbusServerIP;
                txtModbusPort.Text = ServerConfig.ModbusServerPort.ToString();
                chkModbusEnable.Checked = ServerConfig.ModbusServerEnable;

                //txtMaxRecordHis.Text = ServerConfig.MaxHisCount.ToString();
                //chkEnableMqttRecord.Checked = ServerConfig.EnableMqttHisRecord;
            }
            catch { }
            //groupBox1.Enabled = chkInflux.Checked;
            //groupBox2.Enabled = chkDB.Checked;

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                ServerConfig.DbHost = textBoxDBHost.Text;
                ServerConfig.DbName = textBoxDBName.Text;
                ServerConfig.DbUser = textBoxDBUser.Text;
                ServerConfig.DbPw = textBoxDBPw.Text;
                ServerConfig.DataBaseEnable = chkDB.Checked;

                ServerConfig.DBPort = int.Parse(txtDbPort.Text);
                ServerConfig.influxIP = txtInfluxIP.Text;
                ServerConfig.influxName = txtInfluxName.Text;
                ServerConfig.influxPort = int.Parse(txtInfluxPort.Text);
                ServerConfig.influxEnalble = chkInflux.Checked;
                ServerConfig.influxUsername = txtInfluxUser.Text;
                ServerConfig.influxPassword = txtInfluxPw.Text;

                ServerConfig.ModbusServerIP = txtModbsuIp.Text;
                ServerConfig.ModbusServerPort = pubFun.Isushort(txtModbusPort.Text, 502);
                ServerConfig.ModbusServerEnable = chkModbusEnable.Checked;
               // ServerConfig.EnableMqttHisRecord = chkEnableMqttRecord.Checked;

            }
            catch { }
            ServerConfig.saveToFile();
            this.Close();
        }
        void Delay(int milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }

        private void buttonTestDB_Click(object sender, EventArgs e)
        {
            try
            {
                FormMain.g_bConnDbOK = TestIMSconn();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库操作出错！详细错误信息已经记录在Log文件中");
                Logger.GetInstance().LogError(ex.ToString());
            }
        }
        private bool TestLOGconn()
        {
            buttonOk.PerformClick();
            //bool ret = false;
            waitingmsg = "正在测试log数据库连接中，可能会等待1-2分钟";
            Thread th = new Thread(new ThreadStart(this.ShowProgress));
            th.IsBackground = true;
            th.Name = "waitingtestdb";
            th.Start();
            //确保frmWait显示
            while (frmWait == null)
            {
                Thread.Sleep(1);
            }
            while (!frmWait.IsShowing)
            {
                Thread.Sleep(1);
            }
            DBConnectTest dbTest = new DBConnectTest(ServerConfig.DbHost, ServerConfig.DbName, ServerConfig.DbUser, ServerConfig.DbPw, 5000);
            bool ConnDbOK = dbTest.Test();

            CloseWaitingForm();

            if (!ConnDbOK)
            {
                MessageBox.Show(this, "数据库" + StrConst.ERR_DB);
            }
            else
            {
                MessageBox.Show(this, "数据库连接成功！");
            }

            return ConnDbOK;

        }
        private bool TestIMSconn()
        {
            buttonOk.PerformClick();
            waitingmsg = "正在测试Mysql数据库连接中，请等待......";
            Thread th = new Thread(new ThreadStart(this.ShowProgress));
            th.IsBackground = true;
            th.Name = "waitingtestdb";
            th.Start();
            //确保frmWait显示
            while (frmWait == null)
            {
                Thread.Sleep(1);
            }
            while (!frmWait.IsShowing)
            {
                Thread.Sleep(1);
            }
            DBConnectTest dbTest = new DBConnectTest(ServerConfig.DbHost, ServerConfig.DbName, ServerConfig.DbUser, ServerConfig.DbPw, 5000);
            bool ConnDbOK = dbTest.Test();

            CloseWaitingForm();



            if (!ConnDbOK)
            {
                MessageBox.Show(this, "数据库" + StrConst.ERR_DB);
            }
            else
            {
                MessageBox.Show(this, "数据库连接成功！");
            }
            return ConnDbOK;

        }
        WaitForm frmWait = null;
        private string waitingmsg = "正在测试数据库连接中，请耐心等待约10秒……";
        private delegate void ShowProgressDelegete();
        private void ShowProgress()
        {
            try
            {
                frmWait = new WaitForm();
                frmWait.SetText(waitingmsg);
                frmWait.ShowDialog();

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());
            }
        }
        private delegate void CloseWaitingFormdelegate();
        private void CloseWaitingForm()
        {
            if (frmWait != null)
            {
                if (frmWait.InvokeRequired)
                {
                    frmWait.Invoke(new CloseWaitingFormdelegate(CloseWaitingForm));
                }
                else
                {

                    frmWait.Dispose();
                    frmWait = null;
                }
            }

        }

        //测试端口是否可用
        private void TestPort(int port)
        {
            try
            {
                IPAddress ipAddress = Dns.GetHostEntry("").AddressList[0];
                TcpListener listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                listener.Server.Close();
                listener.Stop();
                MessageBox.Show(this, StrConst.OK_PORT);
            }
            catch { MessageBox.Show(this, StrConst.ERR_PORT_DATA); }
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }


        private void btnTestLogDB_Click(object sender, EventArgs e)
        {
            string connstring = $"server={textBoxDBHost.Text};user={textBoxDBUser.Text};database={textBoxDBName.Text};port={txtDbPort.Text};password={textBoxDBPw.Text};";
            GhHistroyDB db = new GhHistroyDB(connstring);
            bool ConnDbOK = db.TestConn();
            if (!ConnDbOK)
            {
                MessageBox.Show(this, "数据库" + StrConst.ERR_DB);
            }
            else
            {
                MessageBox.Show(this, "数据库连接成功！");
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private  void btnTestInflux_Click(object sender, EventArgs e)
        {

            InfluxdbHelper help =new InfluxdbHelper(txtInfluxIP.Text + ":" + txtInfluxPort.Text, txtInfluxUser.Text, txtInfluxPw.Text);

            if (help.CheckDBsync(txtInfluxName.Text))
            {
                MessageBox.Show(this, "数据库" + StrConst.OK_DB);
            }
            else
                MessageBox.Show(this, "数据库" + StrConst.ERR_DB);


        }

        private void txtInfluxPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键  
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字  
                {
                    e.Handled = true;
                }
            }
        }

        private void chkDB_CheckedChanged(object sender, EventArgs e)
        {
            //groupBox2.Enabled =chkDB.Checked;
        }

        private void Label21_Click(object sender, EventArgs e)
        {

        }

        private void chkInflux_CheckedChanged(object sender, EventArgs e)
        {
            //groupBox1.Enabled = chkInflux.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            TestPort(pubFun.IsInt(txtModbusPort.Text, 0));
        }
    }
}
