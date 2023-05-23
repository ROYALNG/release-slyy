using GHIBMS.Common;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace GHIBMS.Server
{
    public partial class FormHistroyList : Form
    {
        //[DllImport("user32.dll")]
        //static extern bool LockWindowUpdate(IntPtr hWndLock);  

        private static HistoryValueChangedEvent historyvalueChangedController;

        public SourceGrid.Grid hisGrid
        {
            get { return grid1; }
        }
        public FormHistroyList()
        {
            InitializeComponent();
            historyvalueChangedController = new HistoryValueChangedEvent(this);

        }



        private void FormHistroyList_Shown(object sender, EventArgs e)
        {
            //显示历史记录列表

            grid1.BorderStyle = BorderStyle.FixedSingle;

            grid1.ColumnsCount = 5;
            grid1.FixedRows = 1;
            grid1.Rows.Insert(0);

            //ColumnHeader view
            SourceGrid.Cells.Views.ColumnHeader boldHeader = new SourceGrid.Cells.Views.ColumnHeader();
            DevAge.Drawing.VisualElements.ColumnHeader backHeader = new DevAge.Drawing.VisualElements.ColumnHeader();
            backHeader.BackColor = Color.Gray;
            backHeader.Border = DevAge.Drawing.RectangleBorder.NoBorder;
            boldHeader.Background = backHeader;
            boldHeader.ForeColor = Color.White;
            boldHeader.Font = new Font("宋体", 10, FontStyle.Bold);
            boldHeader.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

            grid1[0, 0] = new SourceGrid.Cells.ColumnHeader("变量名称");
            grid1[0, 1] = new SourceGrid.Cells.ColumnHeader("控制器名称");
            grid1[0, 2] = new SourceGrid.Cells.ColumnHeader("定时记录");
            grid1[0, 3] = new SourceGrid.Cells.ColumnHeader("变化记录");
            grid1[0, 4] = new SourceGrid.Cells.ColumnHeader("事件记录");

            grid1[0, 0].View = boldHeader;
            grid1[0, 1].View = boldHeader;
            grid1[0, 2].View = boldHeader;
            grid1[0, 3].View = boldHeader;
            grid1[0, 4].View = boldHeader;



            SourceGrid.Cells.Editors.ComboBox comboStandard = new SourceGrid.Cells.Editors.ComboBox(typeof(HistoryTimerRecordEnum));
            //comboStandard = new SourceGrid.Cells.Editors.ComboBox(typeof(string), HistoryTimerRecordEnum, false);
            //comboStandard.KeyPress += pubFun.NoKey_KeyPress;
            // LockWindowUpdate(this.Handle);
            this.SuspendLayout();
            foreach (BaseChannel chan in Rtdb.ChanList)
            {
                foreach (BaseController con in chan.ConList)
                {
                    foreach (BaseVariable var in con.VarList)
                    {
                        int n = grid1.RowsCount;
                        grid1.Rows.Insert(n);
                        grid1[n, 0] = new SourceGrid.Cells.Cell(var.Name);
                        grid1[n, 1] = new SourceGrid.Cells.Cell(var.ControllerObject.Name);
                        grid1[n, 2] = new SourceGrid.Cells.Cell(var.HistoryRecorder, comboStandard);
                        grid1[n, 3] = new SourceGrid.Cells.Cell(var.DataChangedRecorderEnable, typeof(Boolean));
                        grid1[n, 4] = new SourceGrid.Cells.Cell(var.AlarmRecorderEnable, typeof(Boolean));
                        grid1[n, 2].AddController(historyvalueChangedController);
                        grid1[n, 3].AddController(historyvalueChangedController);
                        grid1[n, 4].AddController(historyvalueChangedController);

                    }
                }
            }

            grid1.AutoStretchColumnsToFitWidth = true;
            grid1.AutoSizeCells();
            grid1.Columns.StretchToFit();
            this.ResumeLayout();
            // LockWindowUpdate(IntPtr.Zero);

        }
    }
    public class HistoryValueChangedEvent : SourceGrid.Cells.Controllers.ControllerBase
    {
        private FormHistroyList mfrm;
        public HistoryValueChangedEvent(FormHistroyList frm)
        {
            mfrm = frm;
        }

        public override void OnValueChanged(SourceGrid.CellContext sender, EventArgs e)
        {
            base.OnValueChanged(sender, e);

            // string val = "变量名称 {0} is '{1}'";
            // MessageBox.Show(sender.Grid, string.Format(val, sender.Position.Row, sender.Value));
            // MessageBox.Show(mfrm.hisGrid[sender.Position.Row,0].DisplayText);
            string varName = mfrm.hisGrid[sender.Position.Row, 0].DisplayText;

            foreach (BaseChannel chan in Rtdb.ChanList)
            {
                foreach (BaseController con in chan.ConList)
                {
                    foreach (BaseVariable var in con.VarList)
                    {
                        if (var.Name == varName)
                        {
                            switch (sender.Position.Column)
                            {
                                case 2:  //定时
                                    var.HistoryRecorder = (HistoryTimerRecordEnum)(sender.Value);
                                    break;
                                case 3:  //变化
                                    var.DataChangedRecorderEnable = (Boolean)(sender.Value);
                                    break;
                                case 4:  //事件
                                    var.AlarmRecorderEnable = (Boolean)(sender.Value);
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }

}
