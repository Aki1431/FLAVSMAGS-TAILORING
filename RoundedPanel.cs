using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FLAVSMAGS_TAILORING
{
    [ToolboxItem(true)]
    public class RoundedPanel : Panel
    {
        private int _radius = 20;

        [Category("Appearance")]
        [DefaultValue(20)]
        public int Radius
        {
            get { return _radius; }
            set
            {
                _radius = value;
                Invalidate();
            }
        }

        public RoundedPanel()
        {
            ResizeRedraw = true;
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, _radius, _radius, 180, 90);
            path.AddArc(Width - _radius, 0, _radius, _radius, 270, 90);
            path.AddArc(Width - _radius, Height - _radius, _radius, _radius, 0, 90);
            path.AddArc(0, Height - _radius, _radius, _radius, 90, 90);
            path.CloseFigure();
            Region = new Region(path);
        }
    }
}