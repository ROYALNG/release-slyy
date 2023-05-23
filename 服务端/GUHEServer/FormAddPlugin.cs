
using GHIBMS.Common;
using GHIBMS.Interface;
using GHIBMS.Pub;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace GHIBMS.Server
{
    public partial class FormAddPlugin : Form
    {
        public const string PLUGIN_ACTIVE = "已激活";
        public const string PLUGIN_UNACTIVE = "未激活";
        public FormAddPlugin()
        {
            InitializeComponent();
            iniListViewHead();
            LoadPlugin();
        }
        private void iniListViewHead()
        {
            try
            {

                listView1.Clear();
                listView1.GridLines = true;     //显示各个记录的分隔线 
                listView1.FullRowSelect = true; //要选择就是一行 
                listView1.MultiSelect = true;
                listView1.View = View.Details;  //定义列表显示的方式 
                listView1.Scrollable = true;    //需要时候显示滚动条 
                listView1.MultiSelect = true;  // 不可以多行选择 
                listView1.HeaderStyle = ColumnHeaderStyle.Clickable;


                listView1.Columns.Add("序号", 50, HorizontalAlignment.Center);
                listView1.Columns.Add("插件名称", 250, HorizontalAlignment.Center);
                listView1.Columns.Add("文件名称", 200, HorizontalAlignment.Center);
                listView1.Columns.Add("激活状态", 200, HorizontalAlignment.Center);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void LoadPlugin()
        {
            List<PluginConfig> ps = ServerConfig.PluginList;
            foreach (PluginConfig p in ps)
            {
                ListViewItem lsvItem = new ListViewItem((listView1.Items.Count + 1).ToString());
                lsvItem.Tag = p;
                //验证一次是否激活
                RSAHelper rsa = new RSAHelper();
                p.ActiveState = rsa.CheckPluginRegCodeByInput(StrConst.SOFTNAME, p.PlugID, p.ActiveCode);

                lsvItem.SubItems.AddRange(new string[] {
                    p.PlugName,
                    p.FileName,
                    (p.ActiveState==true)?"已激活":"未激活",
                    });
                listView1.Items.Add(lsvItem);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fi = new OpenFileDialog();
                fi.Multiselect = true;
                fi.Filter = "通讯驱动库文件(Communication.*.dll)|Communication*.dll";
                fi.InitialDirectory = Application.StartupPath;
                fi.RestoreDirectory = true;
                if (fi.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string[] files = fi.SafeFileNames;
                    for (int i = 0; i < files.Length; i++)
                    {
                        try
                        {
                            string file = files[i];
                            Assembly lib = Assembly.LoadFrom(fi.FileNames[i]);

                            foreach (Type t in lib.GetExportedTypes())
                            {
                                if (t.GetInterface(typeof(ICommunicationPlug).FullName) != null)
                                {
                                    ICommunicationPlug plug = (ICommunicationPlug)Activator.CreateInstance(t);
                                    PluginConfig p = new PluginConfig(plug.Name, file, "");
                                    p.PlugID = plug.PlugID;

                                    bool bExist = false;
                                    foreach (ListViewItem item in listView1.Items)
                                    {
                                        PluginConfig pc = item.Tag as PluginConfig;
                                        if (pc != null)
                                        {
                                            if (pc.PlugID == p.PlugID)
                                            {
                                                //MessageBox.Show("该通讯插件已经存在了");
                                                bExist = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (!bExist)
                                    {
                                        ListViewItem lsvItem = new ListViewItem((listView1.Items.Count + 1).ToString());
                                        lsvItem.Tag = p;
                                        lsvItem.SubItems.AddRange(new string[] {
                                    p.PlugName,
                                    p.FileName,
                                    PLUGIN_UNACTIVE,
                                    });
                                        listView1.Items.Add(lsvItem);
                                        listView1.Invalidate();
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.GetInstance().LogError(ex.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }

        private void FormAddPlugin_FormClosing(object sender, FormClosingEventArgs e)
        {
            SavePlugin();
            PluginMng.PlugLoad();
        }
        private void SavePlugin()
        {
            List<PluginConfig> list = new List<PluginConfig>();
            foreach (ListViewItem item in listView1.Items)
            {

                PluginConfig p = item.Tag as PluginConfig;
                if (p != null) list.Add(p);

            }
            ServerConfig.PluginList.Clear();
            ServerConfig.PluginList = list;
            ServerConfig.saveToFile();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem var in listView1.Items)
            {
                if (var.Selected)
                {
                    var.Remove();
                }
            }
        }

        private void buttonActive_Click(object sender, EventArgs e)
        {
            ActivePlug();
        }
        private void ActivePlug()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                PluginConfig cfg = listView1.SelectedItems[0].Tag as PluginConfig;
                FormDriverReg reg = new FormDriverReg(cfg);
                reg.ShowDialog();

                if (cfg.ActiveState)
                {
                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (((PluginConfig)item.Tag).PlugID == cfg.PlugID)
                        {
                            item.SubItems[3].Text = "已激活";
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("请在驱动列表选中一项后再操作");
                return;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //验证一次是否激活
            RSAHelper rsa = new RSAHelper();
            string code = rsa.CreateGomputerbit(StrConst.SOFTNAME);
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string filename = dir + "\\驱动注册信息.txt";
            Stream stream = File.Open(filename, FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("软件序列号：" + code);
            foreach (PluginConfig cfg in ServerConfig.PluginList)
            {
                writer.WriteLine("-------------------------------------------------------------");
                writer.WriteLine("驱动名称：" + cfg.PlugName);
                writer.WriteLine("驱动   ID：" + cfg.PlugID);
                writer.WriteLine("驱动文件：" + cfg.FileName);
                writer.WriteLine("是否激活：" + cfg.ActiveState.ToString());
                writer.WriteLine("激  活  码：" + cfg.ActiveCode);
            }
            writer.WriteLine("--------------------------结束-------------------------------");
            writer.Close();
            stream.Close();
            MessageBox.Show("导出成功，文件名：" + filename);
        }
    }
}
