using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GHIBMS.Matrix
{
    public class CRectControl : System.Windows.Forms.UserControl
    {
        //移动后控件相对于窗体的rect
        Rectangle baseRect;
        //控件本身的Rect，用于鼠标击键测试
        Rectangle ControlRect;
        //8个允许调整控件大小的小正方形
        Rectangle[] SmallRect = new Rectangle[8];
        //CRectControl边框
        Rectangle[] BoundRect = new Rectangle[4];
        //小正方形的大小
        Size Square = new Size(6, 6);
        Graphics g;
        Control currentControl;
        //保存鼠标单击的位置，以备释放鼠标时计算距离
        Point prevLeftClick;
        bool isFirst = true;

        enum HitDownSquare
        {
            HDS_NONE = 0,
            HDS_TOP = 1,
            HDS_RIGHT = 2,
            HDS_BOTTOM = 3,
            HDS_LEFT = 4,
            HDS_TOPLEFT = 5,
            HDS_TOPRIGHT = 6,
            HDS_BOTTOMLEFT = 7,
            HDS_BOTTOMRIGHT = 8
        }

        private HitDownSquare CurrHitPlace;

        private void InitializeComponent()
        {
            this.BackColor = System.Drawing.Color.Wheat;
            this.Name = "TestMoveAndResizeControl";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.RectTracker_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RectTracker_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RectTracker_MouseUp);
        }

        public void Mouse_Move(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //控件最小为 8x8
            if (currentControl.Height < 8)
            {
                currentControl.Height = 8;
                return;
            }
            else if (currentControl.Width < 8)
            {
                currentControl.Width = 8;
                return;
            }
            switch (this.CurrHitPlace)
            {
                case HitDownSquare.HDS_TOP:
                    currentControl.Height = currentControl.Height - e.Y + prevLeftClick.Y;
                    if (currentControl.Height > 8)
                        currentControl.Top = currentControl.Top + e.Y - prevLeftClick.Y;
                    break;
                case HitDownSquare.HDS_TOPLEFT:
                    currentControl.Height = currentControl.Height - e.Y + prevLeftClick.Y;
                    if (currentControl.Height > 8)
                        currentControl.Top = currentControl.Top + e.Y - prevLeftClick.Y;
                    currentControl.Width = currentControl.Width - e.X + prevLeftClick.X;
                    if (currentControl.Width > 8)
                        currentControl.Left = currentControl.Left + e.X - prevLeftClick.X;
                    break;
                case HitDownSquare.HDS_TOPRIGHT:
                    currentControl.Height = currentControl.Height - e.Y + prevLeftClick.Y;
                    if (currentControl.Height > 8)
                        currentControl.Top = currentControl.Top + e.Y - prevLeftClick.Y;
                    currentControl.Width = currentControl.Width + e.X - prevLeftClick.X;
                    break;
                case HitDownSquare.HDS_RIGHT:
                    currentControl.Width = currentControl.Width + e.X - prevLeftClick.X;
                    break;
                case HitDownSquare.HDS_BOTTOM:
                    currentControl.Height = currentControl.Height + e.Y - prevLeftClick.Y;
                    break;
                case HitDownSquare.HDS_BOTTOMLEFT:
                    currentControl.Height = currentControl.Height + e.Y - prevLeftClick.Y;
                    currentControl.Width = currentControl.Width - e.X + prevLeftClick.X;
                    if (currentControl.Width > 8)
                        currentControl.Left = currentControl.Left + e.X - prevLeftClick.X;
                    break;
                case HitDownSquare.HDS_BOTTOMRIGHT:
                    currentControl.Height = currentControl.Height + e.Y - prevLeftClick.Y;
                    currentControl.Width = currentControl.Width + e.X - prevLeftClick.X;
                    break;
                case HitDownSquare.HDS_LEFT:
                    currentControl.Width = currentControl.Width - e.X + prevLeftClick.X;
                    if (currentControl.Width > 8)
                        currentControl.Left = currentControl.Left + e.X - prevLeftClick.X;
                    break;
                case HitDownSquare.HDS_NONE:
                    currentControl.Location = new Point(currentControl.Location.X + e.X - prevLeftClick.X, currentControl.Location.Y + e.Y - prevLeftClick.Y);
                    break;
            }
        }

        private void RectTracker_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isFirst == true)
                {
                    prevLeftClick = new Point(e.X, e.Y);
                    isFirst = false;
                }
                else
                {
                    this.Visible = false;
                    Mouse_Move(this, e); //调整位置或大小
                    prevLeftClick = new Point(e.X, e.Y);
                }
            }
            else
            {
                isFirst = true;
                this.Visible = true;
                Hit_Test(e.X, e.Y); //更新鼠标指针样式
            }
        }

        private void RectTracker_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Create();
            this.Visible = true;
        }

        private void RectTracker_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            //画边框
            Draw();
        }

        public Rectangle Rect
        {
            get { return baseRect; }
            set
            {
                int X = Square.Width;
                int Y = Square.Height;
                int Height = value.Height;
                int Width = value.Width;
                baseRect = new Rectangle(X, Y, Width, Height);
                SetRectangles();
            }
        }

        void SetRectangles()
        {
            //定义8个小正方形的范围
            //左上
            SmallRect[0] = new Rectangle(new Point(baseRect.X - Square.Width, baseRect.Y - Square.Height), Square);
            //上中间
            SmallRect[4] = new Rectangle(new Point(baseRect.X + (baseRect.Width / 2) - (Square.Width / 2), baseRect.Y - Square.Height), Square);
            //右上
            SmallRect[1] = new Rectangle(new Point(baseRect.X + baseRect.Width, baseRect.Y - Square.Height), Square);
            //左下
            SmallRect[2] = new Rectangle(new Point(baseRect.X - Square.Width, baseRect.Y + baseRect.Height), Square);
            //下中间
            SmallRect[5] = new Rectangle(new Point(baseRect.X + (baseRect.Width / 2) - (Square.Width / 2), baseRect.Y + baseRect.Height), Square);
            //右下
            SmallRect[3] = new Rectangle(new Point(baseRect.X + baseRect.Width, baseRect.Y + baseRect.Height), Square);
            //左中间
            SmallRect[6] = new Rectangle(new Point(baseRect.X - Square.Width, baseRect.Y + (baseRect.Height / 2) - (Square.Height / 2)), Square);
            //右中间
            SmallRect[7] = new Rectangle(new Point(baseRect.X + baseRect.Width, baseRect.Y + (baseRect.Height / 2) - (Square.Height / 2)), Square);

            //整个包括周围边框的范围
            ControlRect = new Rectangle(new Point(0, 0), this.Bounds.Size);
        }

        public CRectControl(Control theControl)
        {
            InitializeComponent();
            currentControl = theControl;
            Create();
        }

        private void Create()
        {
            //创建边界
            int X = currentControl.Bounds.X - Square.Width;
            int Y = currentControl.Bounds.Y - Square.Height;
            int Height = currentControl.Bounds.Height + (Square.Height * 2);
            int Width = currentControl.Bounds.Width + (Square.Width * 2);

            this.Bounds = new Rectangle(X, Y, Width + 1, Height + 1);

            this.BringToFront();

            Rect = currentControl.Bounds;
            //设置可视区域
            this.Region = new Region(BuildFrame());
            g = this.CreateGraphics();

        }

        private GraphicsPath BuildFrame()
        {
            GraphicsPath path = new GraphicsPath();
            //
            BoundRect[0] = new Rectangle(0, 0, currentControl.Width + (Square.Width * 2) + 1, Square.Height + 1);
            BoundRect[1] = new Rectangle(0, Square.Height + 1, Square.Width + 1, currentControl.Bounds.Height + Square.Height + 1);
            BoundRect[2] = new Rectangle(Square.Width + 1, currentControl.Bounds.Height + Square.Height - 1, currentControl.Width + Square.Width + 2, Square.Height + 2);
            BoundRect[3] = new Rectangle(currentControl.Width + Square.Width - 1, Square.Height + 1, Square.Width + 2, currentControl.Height - 1);
            path.AddRectangle(BoundRect[0]);
            path.AddRectangle(BoundRect[1]);
            path.AddRectangle(BoundRect[2]);
            path.AddRectangle(BoundRect[3]);

            return path;
        }

        public void Draw()
        {
            try
            {
                g.FillRectangles(Brushes.Teal, BoundRect); //填充用于调整的边框的内部
                g.FillRectangles(Brushes.White, SmallRect); //填充8个锚点的内部
                g.DrawRectangles(Pens.Black, SmallRect);  //绘制8个锚点的黑色边线
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool Hit_Test(int x, int y)
        {
            Point point = new Point(x, y);
            if (!ControlRect.Contains(point))
            {
                Cursor.Current = Cursors.Arrow;
                return false;
            }
            else if (SmallRect[0].Contains(point))
            {
                Cursor.Current = Cursors.SizeNWSE;
                CurrHitPlace = HitDownSquare.HDS_TOPLEFT;
            }
            else if (SmallRect[3].Contains(point))
            {
                Cursor.Current = Cursors.SizeNWSE;
                CurrHitPlace = HitDownSquare.HDS_BOTTOMRIGHT;
            }
            else if (SmallRect[1].Contains(point))
            {
                Cursor.Current = Cursors.SizeNESW;
                CurrHitPlace = HitDownSquare.HDS_TOPRIGHT;
            }
            else if (SmallRect[2].Contains(point))
            {
                Cursor.Current = Cursors.SizeNESW;
                CurrHitPlace = HitDownSquare.HDS_BOTTOMLEFT;
            }
            else if (SmallRect[4].Contains(point))
            {
                Cursor.Current = Cursors.SizeNS;
                CurrHitPlace = HitDownSquare.HDS_TOP;
            }
            else if (SmallRect[5].Contains(point))
            {
                Cursor.Current = Cursors.SizeNS;
                CurrHitPlace = HitDownSquare.HDS_BOTTOM;
            }
            else if (SmallRect[6].Contains(point))
            {
                Cursor.Current = Cursors.SizeWE;
                CurrHitPlace = HitDownSquare.HDS_LEFT;
            }
            else if (SmallRect[7].Contains(point))
            {
                Cursor.Current = Cursors.SizeWE;
                CurrHitPlace = HitDownSquare.HDS_RIGHT;
            }
            else if (ControlRect.Contains(point))
            {
                Cursor.Current = Cursors.SizeAll;
                CurrHitPlace = HitDownSquare.HDS_NONE;
            }
            return true;
        }
    }
}
