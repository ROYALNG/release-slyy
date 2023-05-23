using GHIBMS.Common;
using GHIBMS.Interface;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace GHIBMS.Server
{
    public partial class FormVarList : Form
    {
        public FormVarList()
        {
            InitializeComponent();
        }

        private string varID = "";
        public string VarID
        {
            set { varID = value; }
            get { return varID; }
        }

        private void FormVarList_Shown(object sender, EventArgs e)
        {
            this.cmbCha.Items.Clear();
            this.cmbCon.Items.Clear();
            this.cmbVar.Items.Clear();
            List<ItemObject> s = new List<ItemObject>();
            foreach (BaseChannel chan in Rtdb.ChanList)
            {
                if (chan is ICamChannel) continue;
                s.Add(new ItemObject(chan.Name, chan.ID, chan));
                //foreach(BaseController con in chan.ConList)
                //    foreach (BaseVariable var in con.VarList)
                //    {
                //        s.Add(new ItemObject(var.Name,var.ID));
                //    }
            }
            this.cmbCha.Items.AddRange(s.ToArray());

        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            varID = ((ItemObject)cmbCha.SelectedItem).Value + ":" + ((ItemObject)cmbCon.SelectedItem).Value + ":" + ((ItemObject)cmbVar.SelectedItem).Value;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        public class ItemObject
        {
            public string Text = "", Value = "";//可以多个
            public object obj;
            public ItemObject(string _text, string _value, object _obj)
            {
                Text = _text;
                Value = _value;
                obj = _obj;
            }
            public override string ToString()
            {
                return Text;
            }

        }

        private void cmbCha_SelectedIndexChanged(object sender, EventArgs e)
        {
            IChannel chan = ((ItemObject)cmbCha.SelectedItem).obj as IChannel;
            if (chan != null)
            {
                cmbCon.Items.Clear();
                List<ItemObject> s = new List<ItemObject>();
                foreach (BaseController con in chan.ConList)
                    s.Add(new ItemObject(con.Name, con.ID, con));
                this.cmbCon.Items.AddRange(s.ToArray());
            }
        }

        private void cmbCon_SelectedIndexChanged(object sender, EventArgs e)
        {
            IController con = ((ItemObject)cmbCon.SelectedItem).obj as IController;
            if (con != null)
            {
                cmbVar.Items.Clear();
                List<ItemObject> s = new List<ItemObject>();
                foreach (IVariable v in con.VarList)
                    s.Add(new ItemObject(v.Name, v.ID, v));
                this.cmbVar.Items.AddRange(s.ToArray());
            }
        }
    }
}
