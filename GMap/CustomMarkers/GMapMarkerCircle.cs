namespace GMap.CustomMarkers
{
    using GMap.NET;
    using GMap.NET.WindowsForms;
    using System;
    using System.Drawing;

    public class GMapMarkerCircle : GMapMarker
    {
        public Brush Fill;
        public bool IsFilled;
        public int Radius;
        public Pen Stroke;

        public GMapMarkerCircle(PointLatLng p, int _radius) : base(p)
        {
            this.Stroke = new Pen(Color.FromArgb(0x9b, Color.Red));
            this.Fill = new SolidBrush(Color.FromArgb(0x9b, Color.GhostWhite));
            this.Stroke.Width = 3f;
            this.Radius = _radius;
            base.IsHitTestVisible = false;
        }

        public override void Dispose()
        {
            if (this.Stroke != null)
            {
                this.Stroke.Dispose();
                this.Stroke = null;
            }
            if (this.Fill != null)
            {
                this.Fill.Dispose();
                this.Fill = null;
            }
            base.Dispose();
        }

        public override void OnRender(Graphics g)
        {
            int width = ((int) (((double) this.Radius) / base.Overlay.Control.MapProvider.Projection.GetGroundResolution((int) base.Overlay.Control.Zoom, base.Position.Lat))) * 2;
            if (this.IsFilled)
            {
                g.FillEllipse(this.Fill, new Rectangle(base.LocalPosition.X - (width / 2), base.LocalPosition.Y - (width / 2), width, width));
            }
            g.DrawEllipse(this.Stroke, new Rectangle(base.LocalPosition.X - (width / 2), base.LocalPosition.Y - (width / 2), width, width));
        }
    }
}

