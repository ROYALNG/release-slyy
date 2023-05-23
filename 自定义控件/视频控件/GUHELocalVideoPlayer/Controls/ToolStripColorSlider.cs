using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GHIBMS.NHikPlayer
{
    [ToolboxBitmap(typeof(ToolStripColorSlider), "NHikPlayer.Controls.ColorSlider.bmp")]
    [DefaultProperty("Value")]
    [ToolStripItemDesignerAvailability(
        ToolStripItemDesignerAvailability.ContextMenuStrip |
        ToolStripItemDesignerAvailability.MenuStrip |
        ToolStripItemDesignerAvailability.StatusStrip |
        ToolStripItemDesignerAvailability.ToolStrip)]
    public class ToolStripColorSlider : ToolStripControlHost
    {
        #region Instance ColorSlider
        private ColorSlider colorSlider;
        #endregion

        #region Constructor
        /// <summary>
        /// 初始化承载指定控件的 System.Windows.Forms.ToolStripControlHost 类的新实例。
        /// 参数Control c: 为此 System.Windows.Forms.ToolStripControlHost 类承载的 System.Windows.Forms.Control。
        /// </summary>
        public ToolStripColorSlider()
            : base(new ColorSlider())
        {
            this.AutoSize = false;

            this.colorSlider = base.Control as ColorSlider;
            this.colorSlider.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.colorSlider.ColorSchema = NHikPlayer.ColorSlider.ColorSchemas.PerlNHikPlayer;
            this.colorSlider.DrawFocusRectangle = false;
            this.colorSlider.MouseEffects = false;
            this.colorSlider.Size = new System.Drawing.Size(90, 18);
            this.colorSlider.ThumbRoundRectSize = new System.Drawing.Size(10, 10);
            this.colorSlider.ThumbSize = 8;
        }
        #endregion

        #region Events
        private void Refresh()
        {
            if (this.Owner != null)
                this.Owner.Refresh();
        }

        public event ScrollEventHandler Scroll;

        private void OnScroll(object sender, ScrollEventArgs e)
        {
            if (Scroll != null)
            {
                Scroll(this, e);
            }
        }

        protected override void OnSubscribeControlEvents(Control c)
        {
            base.OnSubscribeControlEvents(c);

            ColorSlider colorSlider = (ColorSlider)c;
            colorSlider.Scroll += new ScrollEventHandler(OnScroll);
        }

        protected override void OnUnsubscribeControlEvents(Control c)
        {
            base.OnUnsubscribeControlEvents(c);

            ColorSlider colorSlider = (ColorSlider)c;
            colorSlider.Scroll += new ScrollEventHandler(OnScroll);
        }
        #endregion

        #region Properties
        [Category("重要属性")]
        [Description("滑动条上的刻度值")]
        [DefaultValue(50)]
        public int Value
        {
            get
            {
                return this.colorSlider.Value;
            }
            set
            {
                this.colorSlider.Value = value;
            }
        }

        [Category("重要属性")]
        [Description("ColorSlider控件")]
        public ColorSlider ColorSlider
        {
            get
            {
                return base.Control as ColorSlider;
            }
        }
        #endregion

        #region Methods
        #endregion
    }
}
