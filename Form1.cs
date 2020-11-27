using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalogSaat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //global değişkenlerimiz.
        Timer t = new Timer();
        int Genislik = 300, Yukseklik = 300, Snİbre = 140, Dkİbre = 110, Saatİbre = 80;
        int OrtaX, OrtaY;
        Bitmap bmp;
        Graphics g;

        private void timer1_Tick(object sender, EventArgs e)
        {
            g = Graphics.FromImage(bmp);
            int ss = DateTime.Now.Second;
            int mm = DateTime.Now.Minute;
            int hh = DateTime.Now.Hour;

            int[] ibrekoordinat = new int[2]; //indis 0 için x ekseni, indis 1 için y ekseni.
            g.Clear(Color.White);

            g.DrawEllipse(new Pen(Color.Black, 1F), 0, 0, Yukseklik, Genislik);
            g.DrawString("12", new Font("Arial", 12), Brushes.Black, new PointF(140, 2)); //Saat 12 için.

            g.DrawEllipse(new Pen(Color.Black, 1F), 0, 0, Yukseklik, Genislik);
            g.DrawString("3", new Font("Arial", 12), Brushes.Black, new PointF(286, 140)); //Saat 3 için.

            g.DrawEllipse(new Pen(Color.Black, 1F), 0, 0, Yukseklik, Genislik);
            g.DrawString("6", new Font("Arial", 12), Brushes.Black, new PointF(142, 282)); //Saat 6 için.

            g.DrawEllipse(new Pen(Color.Black, 1F), 0, 0, Yukseklik, Genislik);
            g.DrawString("9", new Font("Arial", 12), Brushes.Black, new PointF(0, 140)); //Saat 9 için.

          

            //saniye için çizdirme.
            ibrekoordinat = mscoord(ss, Snİbre);
            g.DrawLine(new Pen(Color.Red, 1F), new Point(OrtaX, OrtaY), new Point(ibrekoordinat[0], ibrekoordinat[1]));

            //dakika için yelkovan çizdirme.
            ibrekoordinat = mscoord(mm, Dkİbre);
            g.DrawLine(new Pen(Color.Green, 2F), new Point(OrtaX, OrtaY), new Point(ibrekoordinat[0], ibrekoordinat[1]));

            //saat için akrep çizdirme. Saat 12de bir başa döndüğü için modu aldık.
            ibrekoordinat = hrcoord(hh%12,mm, Saatİbre);
            g.DrawLine(new Pen(Color.Red, 3F), new Point(OrtaX, OrtaY), new Point(ibrekoordinat[0], ibrekoordinat[1]));

            pictureBox1.Image = bmp;
            this.Text = "Saat:" + hh + ":" + mm + ":" + ss;
            g.Dispose();
        }

        private int[] mscoord(int sndeger, int saataci) ///dakika ve saniye için.
        {
            int [] coord = new int [2];
            sndeger *= 6;

            if(sndeger>=0 &&  sndeger<=180)
            {
                coord[0] = OrtaX + (int)(saataci * Math.Sin(Math.PI * sndeger / 180));
                coord[1] = OrtaY - (int)(saataci * Math.Cos(Math.PI * sndeger / 180));
            }

            else
            {
                coord[0] = OrtaX - (int)(saataci * -Math.Sin(Math.PI * sndeger / 180));
                coord[1] = OrtaY - (int)(saataci * Math.Cos(Math.PI * sndeger / 180));
            }
            return coord;

        }

        int[] coord = new int[2];

        private int[] hrcoord(int saatdeger,int dkdeger,int saataci) //saat için.
        {
            int val=(int)((saatdeger*30)+dkdeger*0.5);

            if(val>=0 && val<=180)
            {
                coord[0] = OrtaX + (int)(saataci * Math.Sin(Math.PI * val / 180));
                coord[1] = OrtaY - (int)(saataci * Math.Cos(Math.PI * val / 180));
            }

            else
            {
                coord[0] = OrtaX + (int)(saataci * -Math.Sin(Math.PI * val / 180));
                coord[1] = OrtaY - (int)(saataci * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }
       

        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(Genislik + 1, Yukseklik + 1);
            OrtaX = Genislik / 2;
            OrtaY = Yukseklik / 2;
            this.BackColor = Color.White;
            t.Interval = 1000;
            t.Tick += new EventHandler(this.timer1_Tick);
            t.Start();
        }
    }
}
