using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GHIBMS.NHikPlayer
{
    /// <summary>
    /// 直线类型
    /// </summary>
    public enum LineType
    {
        Etched,
        Single
    }

    /// <summary>
    /// HorizontalLine Control
    /// Author:Oliver Sturm
    /// URL:http://www.sturmnet.org/blog/archives/2005/10/06/horizontal-lines/
    /// </summary>
    [ToolboxBitmap(typeof(Line), "NHikPlayer.Controls.Line.bmp")]
    public class Line : Control
    {
        public Line()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.FixedHeight |
              ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            lineType = LineType.Etched;
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            if (proposedSize == Size.Empty)
                return new Size(150, 10);
            return proposedSize;
        }

        private LineType lineType;
        [DefaultValue(LineType.Etched)]
        public LineType LineType
        {
            get
            {
                return lineType;
            }
            set
            {
                if (lineType != value)
                {
                    lineType = value;
                    CreatePen();
                    Invalidate();
                }
            }
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                CreatePen();
            }
        }

        private Pen pen;
        void CreatePen()
        {
            if (pen != null)
            {
                pen.Dispose();
                pen = null;
            }

            if (lineType == LineType.Single)
                pen = new Pen(ForeColor);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            switch (lineType)
            {
                case LineType.Etched:
                    ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle,
                      Border3DStyle.Etched, Border3DSide.Top);
                    break;
                case LineType.Single:
                    e.Graphics.DrawLine(pen, 0, 0, Width - 1, 0);
                    break;
            }
        }
    }
}
