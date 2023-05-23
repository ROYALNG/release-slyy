using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace GH.IO.Monitor

{
    public partial class Monitor : Form
    {
        public const int USER = 0x0400;//用户自定义消息的开始数值
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern void PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        internal DataTable Dt = new DataTable("Activity");
        private Timer _pollTimer;
        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case USER + 1:
                    //string message = string.Format("收到自己消息的参数:{0},{1}", m.WParam, m.LParam);
                    //MessageBox.Show(message);
                    break;
                default:
                    base.DefWndProc(ref m);//一定要调用基类函数，以便系统处理其它消息。
                    break;
            }
        }
        public Monitor()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var dc = new DataColumn("Time", typeof(DateTime));
            var dc2 = new DataColumn("Event", typeof(String));
            var dc3 = new DataColumn("Data", typeof(String));

            Dt.Columns.Add(dc);
            Dt.Columns.Add(dc2);
            Dt.Columns.Add(dc3);
            Dt.AcceptChanges();

            var dr = Dt.NewRow();
            dr["Time"] = DateTime.Now;
            dr["Event"] = "STARTED";
            dr["Data"] = Program.ProgramName;

            Dt.Rows.Add(dr);

            dataGridView1.DataSource = Dt;
            dataGridView1.Invalidate();

            WindowState = FormWindowState.Minimized;
            Hide();

            _pollTimer = new Timer(5000);
            _pollTimer.Elapsed += tmrPoll_Tick;
            _pollTimer.AutoReset = true;
            _pollTimer.SynchronizingObject = this;
            _pollTimer.Start();

        }

        private void tmrPoll_Tick(object sender, EventArgs e)
        {
            _pollTimer.Stop();
            try
            {
                var w = Process.GetProcessesByName(Program.ProgramName);
                if (w.Length == 0)
                {
                    if (File.Exists(Application.StartupPath+ "\\exit.txt") &&
                        File.ReadAllText(Application.StartupPath + "\\exit.txt") == "OK")
                    {
                        _reallyclose = true;
                        Close();
                        return;
                    }

                    //app has crashed and terminated
                    var dr = Dt.NewRow();
                    dr["Time"] = DateTime.Now;
                    dr["Event"] = "RESTART";
                    dr["Data"] = "";
                    Dt.Rows.Add(dr);
                    dataGridView1.Invalidate();

                    var si = new ProcessStartInfo(Program.AppPath + Program.ProgramName+".exe", "");
                   // MessageBox.Show(Program.AppPath + Program.ProgramName + ".exe");
                    Process.Start(si);

                }
                else
                {
                    var p = w[0];
                    var b = p.Responding;
                    var c = 0;
                    while (!b && c < 180)
                    {
                        b = p.Responding;
                        Thread.Sleep(1000);
                        c++;
                    }

                    if (!b)
                    {
                        //app has hung (3 minutes non responsive)
                        p.Kill();
                        var dr = Dt.NewRow();
                        dr["Time"] = DateTime.Now;
                        dr["Event"] = "KILL (UNRESPONSIVE)";
                        dr["Data"] = "";
                        Dt.Rows.Add(dr);
                        dataGridView1.Invalidate();
                    }

                }
            } catch {}

            _pollTimer.Start();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void niMonitor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void niMonitor_DoubleClick(object sender, EventArgs e)
        {
            Activate();
            Visible = true;
            if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Normal;
            }
            TopMost = true;
            TopMost = false;//need to force a switch to move above other forms
            BringToFront();
            Focus();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _reallyclose = true;
            Close();
        }

        private bool _reallyclose;
        private void Monitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.WindowsShutDown)
            {
                if (!_reallyclose)
                {
                    e.Cancel = true;
                    WindowState = FormWindowState.Minimized;
                }
            }
        }
    

    }
}
