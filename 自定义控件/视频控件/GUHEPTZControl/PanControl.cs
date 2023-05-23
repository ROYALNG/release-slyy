using System.Windows.Forms;

namespace GHIBMS.PTZControl
{
    internal class PanControl : DevComponents.Instrumentation.KnobControl
    {
        public const int WM_LBUTTONDOWN = 513;   //   0x0201    
        public const int WM_LBUTTONUP = 514;     //   0x0202    
        public delegate void OnMouseDowndelegate(object sender, MouseEventArgs e);
        public new event OnMouseDowndelegate OnMouseDown;
        public delegate void OnMouseUpdelegate(object sender, MouseEventArgs e);
        public new event OnMouseUpdelegate OnMouseUp;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDOWN)
            {
                MouseEventArgs e = new MouseEventArgs(MouseButtons.Left, 1, m.WParam.ToInt32(), m.LParam.ToInt32(), 1);
                if (OnMouseDown != null)
                    this.OnMouseDown(this, e);
            }
            if (m.Msg == WM_LBUTTONUP)
            {
                MouseEventArgs e = new MouseEventArgs(MouseButtons.Left, 1, m.WParam.ToInt32(), m.LParam.ToInt32(), 1);
                if (OnMouseUp != null)
                    this.OnMouseUp(this, e);
            }
            base.WndProc(ref m);

        }
        public PanControl()
        {
            this.StartAngle = 0;
            this.SweepAngle = 360;
            this.MajorTickAmount = 90;
            this.MaxZonePercentage = 1;
            this.MinorTickAmount = 45;
            this.MinZonePercentage = 1;
            this.MinValue = 0;
            this.MaxValue = 360;
            this.ShowTickLabels = false;

        }
    }
}
