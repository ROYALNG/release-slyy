using ICSharpCode.TextEditor.Document;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace GHIBMS.Server
{
    public partial class FormEditVbs : Form
    {
        private XVBAEngine myVBAEngine = null;
        public FormEditVbs()
        {
            InitializeComponent();
            this.textEditor1.Encoding = Encoding.UTF8;
            this.textEditor1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("VBNET");
            AddScriptMethod();
        }
        private void AddScriptMethod()
        {
            // 将脚本方法名称添加到“运行脚本”的下拉菜单项目中
            List<string> names = new List<string>();
            names.Add("Server.GetValue");
            names.Add("Server.SetValue");
            //names.Add("Window.Prompt");
            //names.Add("Window.SetTimeout");
            //names.Add("Window.ClearTimeout");
            //names.Add("Window.SetInterval");
            //names.Add("Window.ClearInterval");
            //names.Add("Window.ClearInterval");
            //names.Add("Window.Alert");
            //names.Add("Window.Prompt");
            //names.Add("Window.Sleep");

            foreach (string name in names)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = name;
                item.Click += new EventHandler(ScriptItem_Click);
                tspScriptMethod.DropDownItems.Add(item);
            }
        }
        private void ScriptItem_Click(object sender, System.EventArgs args)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            int i = textEditor1.Document.PositionToOffset(textEditor1.ActiveTextAreaControl.Caret.Position);
            textEditor1.Document.Insert(i, item.Text);
        }

        public string VbsText
        {
            get { return textEditor1.Text; }
            set { textEditor1.Text = value; }
        }
        public XVBAEngine VBAEngine
        {
            get
            {
                return this.myVBAEngine;
            }
            set
            {
                this.myVBAEngine = value;
            }
        }

        private void FormEditVbs_FormClosing(object sender, FormClosingEventArgs e)
        {


        }

        private void tspCompile_Click(object sender, EventArgs e)
        {
            this.myVBAEngine.ScriptText = this.textEditor1.Text;
            if (!this.myVBAEngine.Compile())
            {
                MessageBox.Show(this, "编译错误:" + this.myVBAEngine.CompilerOutput, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show(this, "编译成功 ", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void tspOK_Click(object sender, EventArgs e)
        {
            this.myVBAEngine.ScriptText = this.textEditor1.Text;
            if (!this.myVBAEngine.Compile())
            {
                MessageBox.Show(this, "编译错误:" + this.myVBAEngine.CompilerOutput, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else
            {
                MessageBox.Show(this, "编译成功 ", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.DialogResult = DialogResult.OK;
            }

        }

        private void tspCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void tspSelVar_Click(object sender, EventArgs e)
        {
            FormVarList form = new FormVarList();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                int i = textEditor1.Document.PositionToOffset(textEditor1.ActiveTextAreaControl.Caret.Position);
                textEditor1.Document.Insert(i, $"\"{form.VarID.Split(':')[0]}\",\"{form.VarID.Split(':')[1]}\",\"{form.VarID.Split(':')[2]}\"");


            }
            form.Dispose();
        }
    }
}
