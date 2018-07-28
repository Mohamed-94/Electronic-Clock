using System;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
using Mohamed.Programming;
using Mohamed.Programming.CSharp.Clocks;
 
class Timer_1 : Form
{
    DateTime dt;
    ClockControl clkcon1;

    //public static void Main()
   // {
   //     Application.Run(new Timer_1());
   // }

    Timer_1()
    {
        Text = "Timer..";
        ForeColor = SystemColors.WindowText;  
        BackColor = Color.White;
        ResizeRedraw = true;
        MinimumSize = SystemInformation.MinimizedWindowSize + new Size(0, 1);

        clkcon1 = new ClockControl();
        clkcon1.Parent = this;
        clkcon1.Time = DateTime.Now;
        clkcon1.Dock = DockStyle.None;
        clkcon1.BackColor = Color.PaleVioletRed;
        clkcon1.ForeColor = Color.Blue;

        dt = DateTime.Now;

        Timer timer = new Timer();
        timer.Interval = 1000;
        timer.Tick += new EventHandler(TimerAnalog);
        timer.Tick += new EventHandler(TimerOnTick);
        timer.Enabled = true;

    }

    private void TimerOnTick(object sender, EventArgs ea)
    {
        DateTime dtNow = DateTime.Now;
        dtNow = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, dtNow.Hour, dtNow.Minute, dtNow.Second);
        if (dtNow != dt)
        {
            dt = dtNow;
            Invalidate();
        }
    }

    private void TimerAnalog(object sender, EventArgs e)
    {
        clkcon1.Time = DateTime.Now;
        
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        SegmentDisply ssd = new SegmentDisply(e.Graphics);

        string strtime = dt.ToString("T", DateTimeFormatInfo.InvariantInfo);
        SizeF sizef = ssd.MesureString(strtime, Font);
        float fscal = Math.Min(ClientSize.Width / sizef.Width, ClientSize.Height / sizef.Height);
        Font font = new Font(Font.FontFamily, fscal * Font.SizeInPoints);

        sizef = ssd.MesureString(strtime, font);

        ssd.DrawString(strtime, font, Brushes.Black, (ClientSize.Width - sizef.Width) / 2, (ClientSize.Height - sizef.Height) / 2);


        //Graphics graf = e.Graphics;
        //String strT = DateTime.Now.ToString("T");
        //SizeF sif = graf.MeasureString(strT, Font);
        //float fscal = Math.Min(ClientSize.Width / sif.Width, ClientSize.Height / sif.Height);
        //Font font = new Font(Font.FontFamily, fscal * Font.SizeInPoints);
        //sif = graf.MeasureString(strT, font);
        //graf.DrawString(strT, font, new SolidBrush(ForeColor), (ClientSize.Width - sif.Width) / 2,( ClientSize.Height - sif.Height) / 2);

    }

    void TimerEventH(object obj, EventArgs ea)
    {
        Random ran = new Random();
        int x1 = ran.Next(ClientSize.Width);
        int x2 = ran.Next(ClientSize.Width);
        int y1 = ran.Next(ClientSize.Height);
        int y2 = ran.Next(ClientSize.Height);

        Color color = Color.FromArgb(ran.Next(256),
                                     ran.Next(256),
                                     ran.Next(256));

        Graphics graf = CreateGraphics();
        graf.FillRectangle(new SolidBrush(color),
                             Math.Min(x1, x2), Math.Min(y1, y2),
                             Math.Abs(x1- x2), Math.Abs(y1- y2));

        graf.Dispose();

    }
}

