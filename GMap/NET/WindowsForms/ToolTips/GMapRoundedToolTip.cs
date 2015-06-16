namespace GMap.NET.WindowsForms.ToolTips
{
    using GMap.NET;
    //using GMap.NET.WindowsForms;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.Serialization;

    [Serializable]
    public class GMapRoundedToolTip : GMapToolTip, ISerializable
    {
        public float Radius;

        public GMapRoundedToolTip(GMapMarker marker) : base(marker)
        {
            this.Radius = 10f;
            base.TextPadding = new Size((int) this.Radius, (int) this.Radius);
        }

        protected GMapRoundedToolTip(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.Radius = 10f;
            this.Radius = Extensions.GetStruct<float>(info, "Radius", 10f);
        }

        public void DrawRoundRectangle(Graphics g, Pen pen, float h, float v, float width, float height, float radius)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddLine(h + radius, v, (h + width) - (radius * 2f), v);
                path.AddArc((h + width) - (radius * 2f), v, radius * 2f, radius * 2f, 270f, 90f);
                path.AddLine((float) (h + width), (float) (v + radius), (float) (h + width), (float) ((v + height) - (radius * 2f)));
                path.AddArc((float) ((h + width) - (radius * 2f)), (float) ((v + height) - (radius * 2f)), (float) (radius * 2f), (float) (radius * 2f), 0f, 90f);
                path.AddLine((float) ((h + width) - (radius * 2f)), (float) (v + height), (float) (h + radius), (float) (v + height));
                path.AddArc(h, (v + height) - (radius * 2f), radius * 2f, radius * 2f, 90f, 90f);
                path.AddLine(h, (v + height) - (radius * 2f), h, v + radius);
                path.AddArc(h, v, radius * 2f, radius * 2f, 180f, 90f);
                path.CloseFigure();
                g.FillPath(base.Fill, path);
                g.DrawPath(pen, path);
            }
        }

        public override void OnRender(Graphics g)
        {
            Size size = g.MeasureString(base.Marker.ToolTipText, base.Font).ToSize();
            Rectangle layoutRectangle = new Rectangle(base.Marker.ToolTipPosition.X, base.Marker.ToolTipPosition.Y - size.Height, size.Width + (this.TextPadding.Width * 2), size.Height + this.TextPadding.Height);
            layoutRectangle.Offset(this.Offset.X, this.Offset.Y);
            g.DrawLine(base.Stroke, (float) base.Marker.ToolTipPosition.X, (float) base.Marker.ToolTipPosition.Y, layoutRectangle.X + (this.Radius / 2f), (layoutRectangle.Y + layoutRectangle.Height) - (this.Radius / 2f));
            this.DrawRoundRectangle(g, base.Stroke, (float) layoutRectangle.X, (float) layoutRectangle.Y, (float) layoutRectangle.Width, (float) layoutRectangle.Height, this.Radius);
            if (base.Format.Alignment == StringAlignment.Near)
            {
                layoutRectangle.Offset(this.TextPadding.Width, 0);
            }
            g.DrawString(base.Marker.ToolTipText, base.Font, base.Foreground, layoutRectangle, base.Format);
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Radius", this.Radius);
            base.GetObjectData(info, context);
        }
    }
}

