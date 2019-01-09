using System;
using System.Windows.Forms;
using System.Drawing;
//using Un4seen.Bass;

namespace TotalTagger
{
    public class ModifiedProgressBarSeek : ProgressBar
    {
        public bool HasBorder = true;
        public float BorderThickness = 1f;
        public TimeSpan time1, time2;
        ToolTip seekTimeTooltip = new ToolTip();
        Point lastTipLocation;
        int trig = 0;

        public ModifiedProgressBarSeek()
        {
            SetStyle(ControlStyles.UserPaint, true);
            DoubleBuffered = true;
        }

        void ModifiedProgressBarSeek_MouseLeave(object sender, EventArgs e)
        {
            seekTimeTooltip.Hide(this);
        }

        void ModifiedProgressBarSeek_MouseMove(object sender, MouseEventArgs e)
        {
            if (lastTipLocation == e.Location) return;
            double time = (TotalTagger.MainWindow.GetLength() * (int)((float)e.X / (float)this.Width * this.Maximum)) / this.Maximum;
            seekTimeTooltip.Show(time.ToTime() + " ;; " + trig, this);
            trig++;
            lastTipLocation = e.Location;
        }

        public Bitmap CropImage(Bitmap source, Rectangle section)
        {
            // An empty bitmap which will hold the cropped image
            Bitmap bmp = new Bitmap(section.Width, section.Height);

            Graphics g = Graphics.FromImage(bmp);

            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if ((Parent as TotalTagger.MainWindow) == null) return;

            ////bar
            Rectangle rec = e.ClipRectangle;
            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
            //if (ProgressBarRenderer.IsSupported) ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            rec.Height = rec.Height - 4;
            //e.Graphics.FillRectangle(new SolidBrush((Parent as TotalTagger.MainWindow).BackColor), 0, 0, Width, Height);
            //e.Graphics.FillRectangle(new SolidBrush((Parent as TotalTagger.MainWindow).ForeColor), 2, 2, rec.Width, rec.Height);

            //if (HasBorder) e.Graphics.DrawRectangle(new Pen((Parent as TotalTagger.MainWindow).ForeColor, BorderThickness), 0, 0, Width - BorderThickness, Height - BorderThickness);

            //text

            if (!TotalTagger.MainWindow.streamLoaded)
                return;
            time1 = TimeSpan.FromSeconds(TotalTagger.BASS.BassLibrary.ChannelBytes2Seconds(TotalTagger.BASS.BassLibrary.GetPosition()));
            time2 = TimeSpan.FromSeconds(TotalTagger.BASS.BassLibrary.ChannelBytes2Seconds(TotalTagger.BASS.BassLibrary.GetLength())); 
            time2 -= time1;

            string toWrite = time1.TotalSeconds.ToTime();
            Font ft = new Font(TotalTagger.MainWindow.fontProgressBarColor.FontFamily, rec.Height - 2, TotalTagger.MainWindow.fontProgressBarColor.Style);
            SizeF sz = e.Graphics.MeasureString(toWrite, ft);
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(toWrite, ft, Brushes.DarkGreen, new Rectangle(2, 2, (int)sz.Width + 1, Height), sf);
            toWrite = time2.TotalSeconds.ToTime();
            sz = e.Graphics.MeasureString(toWrite, ft);
            e.Graphics.DrawString(toWrite, ft, Brushes.DarkOrange, new Rectangle((int)Width - (int)sz.Width - 2, 2, (int)sz.Width + 1, Height), sf);
        }
    }
}
