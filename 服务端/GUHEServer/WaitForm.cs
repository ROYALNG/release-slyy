using System;
using System.Windows.Forms;


namespace GHIBMS.Server
{
    public partial class WaitForm : Form
    {
        //Create a Bitmpap Object.
        //Bitmap animatedImage = new Bitmap("Progressbar.gif");
        //bool currentlyAnimating = false;

        public WaitForm()
        {
            InitializeComponent();
        }
        public bool IsShowing = false;
        public delegate void SetTextDelegate(string text);
        public void SetText(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetTextDelegate(SetText), new object[] { text });
            }
            else
            {
                if (text != "")
                {
                    lblMsg.Text = text;
                    this.Text = text;
                }
                this.Invalidate();
            }
        }

        private void WaitForm_Shown(object sender, EventArgs e)
        {
            IsShowing = true;
            //this.WindowState = FormWindowState.Normal;
            //this.Left = Screen.AllScreens[0].Bounds.X + Screen.AllScreens[0].Bounds.Width - this.Width / 2;
            //this.Top = Screen.AllScreens[0].Bounds.Y + Screen.AllScreens[0].Bounds.Height - this.Height / 2;

            //this.WindowState = FormWindowState.Normal;

        }

        private void WaitForm_Load(object sender, EventArgs e)
        {

        }


        ////This method begins the animation.
        //public void AnimateImage()
        //{
        //    if (!currentlyAnimating)
        //    {

        //        //Begin the animation only once.
        //        ImageAnimator.Animate(animatedImage, new EventHandler(this.OnFrameChanged));
        //        currentlyAnimating = true;
        //    }
        //}

        //private void OnFrameChanged(object o, EventArgs e)
        //{

        //    //Force a call to the Paint event handler.
        //    this.Invalidate();
        //}

        //protected override void OnPaint(PaintEventArgs e)
        //{

        //    //Begin the animation.
        //    AnimateImage();

        //    //Get the next frame ready for rendering.
        //    ImageAnimator.UpdateFrames();

        //    //Draw the next frame in the animation.
        //    e.Graphics.DrawImage(this.animatedImage, new Point(0, 0));
        //}
    }
}
