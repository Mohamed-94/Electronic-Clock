using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mohamed.Programming
{
    class SegmentDisply
    {
        Graphics graf;

        static byte[,] bySegment = { 
        {1,1,1,0,1,1,1},
        {0,0,1,0,0,1,0}, 
        {1,0,1,1,1,0,1},
        {1,0,1,1,0,1,1},
        {0,1,1,1,0,1,0},
        {1,1,0,1,0,1,1},
        {1,1,0,1,1,1,1},
        {1,0,1,0,0,1,0},
        {1,1,1,1,1,1,1},
        {1,1,1,1,0,1,1}};

        readonly Point[][] pt = new Point[7][];


        public SegmentDisply(Graphics graf)
        {
            this.graf = graf;
            pt[0] = new Point[] { new Point(3, 2), new Point(39, 2), new Point(31, 10), new Point(11, 10) };
            pt[1] = new Point[] { new Point(3, 2), new Point(10, 11), new Point(10, 31), new Point(2, 35) };
            pt[2] = new Point[] { new Point(40, 3), new Point(40,35), new Point(32, 31), new Point(32, 11) };
            pt[3] = new Point[] { new Point(3, 36), new Point(11, 32), new Point(31, 32), new Point(39, 36),
                new Point (31,40),new Point (11,40) };
            pt[4] = new Point[] { new Point(2, 37), new Point(10, 41), new Point(10, 61), new Point(2, 69) };
            pt[5] = new Point[] { new Point(40, 37), new Point(40,69), new Point(32, 61), new Point(32, 41) };
            pt[6] = new Point[] { new Point(11, 62), new Point(31, 62), new Point(39, 70), new Point(3, 70) };

        }

        public SizeF MesureString(string str, Font font)
        {
            SizeF sizef = new SizeF(0, graf.DpiX * font.SizeInPoints / 72);

            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsDigit(str[i]))
                    sizef.Width += 42 * graf.DpiX * font.SizeInPoints / 72 / 72;
                else if (str[i] == ':')
                    sizef.Width  += 12 * graf.DpiX * font.SizeInPoints / 72 / 72;

            }
            return sizef;
        }

        public void DrawString(string str, Font font, Brush brush, float x, float y)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsDigit(str[i]))
                    x = Number(str[i] - '0', font, brush, x, y);
                else if (str[i] == ':')
                    x = Clone(font, brush, x, y);
            }
        }

        float Number(int num, Font font, Brush brush, float x, float y)
        {
            for (int i = 0; i < pt.Length; i++)   
                if (bySegment[num, i] == 1)
                    Fill(pt[i], font, brush, x, y);


            return x + 42 * graf.DpiX * font.SizeInPoints / 72 / 72;          
        }

        float Clone(Font font, Brush brush, float x, float y)
        {
            Point[][] pt = new Point[2][];
            pt[0] = new Point[] { new Point(2, 21), new Point(6, 17), new Point(10, 21), new Point(6, 25) };
            pt[1] = new Point[] { new Point(2, 51), new Point(6, 47), new Point(10, 51), new Point(6, 55) };

           for (int i = 0; i < pt.Length; i++)
                  Fill(pt[i], font, brush, x, y);


            return x + 12 * graf.DpiX * font.SizeInPoints / 72 / 72;          
        }
          
       void Fill(Point[] pt, Font font,Brush brush, float x, float y)
        {
            PointF[] ptf = new PointF[pt.Length];

            for (int i = 0; i < pt.Length; i++)
            {
                ptf[i].X = x + pt[i].X * graf.DpiX * font.SizeInPoints / 72 / 72;
                ptf[i].Y = y + pt[i].Y * graf.DpiY * font.SizeInPoints / 72 / 72;

            }
            graf.FillPolygon(brush, ptf);
        }
    }
}
