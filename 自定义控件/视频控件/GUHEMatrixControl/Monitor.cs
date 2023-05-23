using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GHIBMS.Matrix
{
    public partial class Monitor : UserControl
    {
        static int id = 0;
        public Monitor(int split)
        {
            id++;

            InitializeComponent();
            // Set the value of the double-buffering style bits to true.
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.UserPaint |
               ControlStyles.AllPaintingInWmPaint,
               true);
            this.UpdateStyles();
            monID = id.ToString();
            splitNubs = split;
            CreateSubVideoControl(split);

        }
        private string monID;
        public string MonID
        {
            get { return monID; }
            set { monID = value; }

        }

        private string monName = "";
        public string MonName
        {
            get { return monName; }
            set
            {
                monName = value;
                lblMon.Text = monName;
            }
        }

        private int monChildCH = 1;
        public int MonChildCH
        {
            get { return monChildCH; }
            set { monChildCH = value; }
        }
        private int splitNubs = 1;
        public int SplitNubs
        {
            get { return splitNubs; }
            set { splitNubs = value; }
        }
        public void SelectChildCH(int id)
        {
            foreach (Control con in panelExTop.Controls)
            {
                if (con.Tag.ToString() == id.ToString())
                {
                    ((PanelEx)con).Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                    ((PanelEx)con).Style.BackColor2.Color = System.Drawing.Color.Teal;
                }
                else
                {
                    ((PanelEx)con).Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
                    ((PanelEx)con).Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
                }
            }

        }
        public delegate void OnSelectedWnddelegate(object sender, EventArgs e);
        public event OnSelectedWnddelegate OnSelectedWnd;
        public void CreateSubVideoControl(int MaxVideoNubs)
        {

            splitNubs = MaxVideoNubs;
            // if (panelExTop.Controls.Count == MaxVideoNubs) return;
            foreach (Control con in panelExTop.Controls)
            {
                con.Dispose();
            }

            panelExTop.Controls.Clear();
            for (int i = 1; i <= MaxVideoNubs; i++)
            {
                PanelEx pnl = new PanelEx();
                pnl.CanvasColor = System.Drawing.SystemColors.Control;
                pnl.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
                //pnl.Dock = System.Windows.Forms.DockStyle.Fill;
                pnl.Location = new System.Drawing.Point(0, 0);
                pnl.Size = new System.Drawing.Size(10, 10);
                pnl.Style.Alignment = System.Drawing.StringAlignment.Center;
                pnl.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
                pnl.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
                pnl.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
                pnl.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
                pnl.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
                pnl.StyleMouseDown.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                pnl.StyleMouseDown.BackColor2.Color = System.Drawing.Color.Teal;
                pnl.Tag = i;
                pnl.MouseClick += new MouseEventHandler(pnl_MouseClick);
                pnl.MouseDown += new MouseEventHandler(panelExTop_MouseDown);
                pnl.Style.GradientAngle = 90;
                panelExTop.Controls.Add(pnl);
            }
            MakeSubVidoPanelShow(MaxVideoNubs);
        }
        private void pnl_MouseClick(object sender, MouseEventArgs e)
        {
            PanelEx pnl = sender as PanelEx;
            this.monChildCH = int.Parse(pnl.Tag.ToString());
            if (OnSelectedWnd != null)
                OnSelectedWnd(this, e);

        }
        public void MakeSubVidoPanelShow()
        {
            MakeSubVidoPanelShow(splitNubs);
        }
        private void MakeSubVidoPanelShow(int SelNubs)
        {
            IList<Rectangle> rectList = CalcPanelRectangle(SelNubs, panelExTop.Size);
            int i = 0;
            foreach (Rectangle rect in rectList)
            {
                panelExTop.Controls[i].Location = new System.Drawing.Point(rect.X, rect.Y);
                panelExTop.Controls[i].Size = new System.Drawing.Size(rect.Width, rect.Height);
                i++;
            }
            this.Invalidate();
        }
        /// <summary>
        /// 计算视频面板位置和面积
        /// </summary>
        /// <param name="channelCount"></param>
        /// <param name="TotalSquare">总面积和坐标</param>
        /// <returns></returns>
        private IList<Rectangle> CalcPanelRectangle(int channelCount, Size TotalArea)
        {
            IList<Rectangle> result = new List<Rectangle>();

            //模数
            int moduloW;
            int moduloH;

            switch (channelCount)
            {
                case 1:
                    moduloW = 1;
                    moduloH = 1;
                    break;
                case 4:
                    moduloW = 2;
                    moduloH = 2;
                    break;
                case 6:
                    moduloW = 3;
                    moduloH = 2;
                    break;
                case 9:
                    moduloW = 3;
                    moduloH = 3;
                    break;
                case 12:
                    moduloW = 4;
                    moduloH = 3;
                    break;
                case 16:
                    moduloW = 4;
                    moduloH = 4;
                    break;
                case 20:
                    moduloW = 5;
                    moduloH = 4;
                    break;
                case 25:
                    moduloW = 5;
                    moduloH = 5;
                    break;
                case 30:
                    moduloW = 6;
                    moduloH = 5;
                    break;
                case 36:
                    moduloW = 6;
                    moduloH = 6;
                    break;
                case 42:
                    moduloW = 7;
                    moduloH = 6;
                    break;
                case 49:
                    moduloW = 7;
                    moduloH = 7;
                    break;
                case 56:
                    moduloW = 8;
                    moduloH = 7;
                    break;
                case 64:
                    moduloW = 8;
                    moduloH = 8;
                    break;
                default:
                    moduloW = 2;
                    moduloH = 2;
                    break;

            }

            int width, height;

            //单个画面大小
            width = (TotalArea.Width - moduloW * 2) / moduloW;
            height = (TotalArea.Height - moduloH * 2) / moduloH;

            for (int i = 0; i < channelCount; i++)
            {
                Rectangle rect = new Rectangle();
                rect.Width = width;
                rect.Height = height;

                if (i % moduloW == 0) //左边第1列
                {
                    rect.X = 1;
                    if (i == 0)
                        rect.Y = 1;
                    else
                        rect.Y = result[i - moduloW].Y + height + 2;
                }
                else
                {
                    rect.X = result[i - 1].X + width + 2;
                    rect.Y = result[i - 1].Y;
                }
                result.Add(rect);
            }
            return result;
        }

        private void btnSplit1_Click(object sender, EventArgs e)
        {
            CreateSubVideoControl(1);
        }

        private void btnSplit4_Click(object sender, EventArgs e)
        {
            CreateSubVideoControl(4);
        }

        private void btnSplit9_Click(object sender, EventArgs e)
        {
            CreateSubVideoControl(9);
        }

        private void btnSplit16_Click(object sender, EventArgs e)
        {
            CreateSubVideoControl(16);
        }

        private void panelExTop_MouseDown(object sender, MouseEventArgs e)
        {

            this.OnMouseDown(e);
        }

        private void panelExTop_SizeChanged(object sender, EventArgs e)
        {
            MakeSubVidoPanelShow(splitNubs);
        }
    }


}
