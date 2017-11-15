using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace kript
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static Size imSize9 = new Size(8, 8);
        private static byte[] rgbValues;
        private static int p = 17;
        private static int q = 59;
        private static long d = 927;
        private static long n = p * q;
        private static int numBytes;
        private static int CountText;
        static string name1 = @"C:\Users\User\Documents\Visual Studio 2013\Projects\kript\image2.png";
        string name2 = @"C:\Users\User\Documents\Visual Studio 2013\Projects\kript\out.txt";
        string name3 = @"C:\Users\User\Documents\Visual Studio 2013\Projects\kript\out2.txt";
        string name4 = @"C:\Users\User\Documents\Visual Studio 2013\Projects\kript\image3.png";

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Multiselect = true;
                Bitmap b;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    b = new Bitmap(openFileDialog1.FileName);
                    if (b.PixelFormat == System.Drawing.Imaging.PixelFormat.Format1bppIndexed ||
                        b.PixelFormat == System.Drawing.Imaging.PixelFormat.Format4bppIndexed ||
                        b.PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                        b = new Bitmap(b);
                    pictureBox1.Image = b;
           
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //выделение области
            Bitmap png1 = new Bitmap(pictureBox1.Image);
            Resampling TrimBitmap = new Resampling();
            Resampling.TrimBitmap(ref png1, name1);
            pictureBox2.Image = png1;
            Bitmap bmpSave = (Bitmap)pictureBox2.Image;
            bmpSave.Save(name4, ImageFormat.Png);

            //RSA
            Bitmap png2 = new Bitmap(name1);
            RSA RGBLength = new RSA();
            RSA.RGBLength(png2, ref rgbValues, ref numBytes);

            //Кодироваине 
            RSA Encrypt = new RSA();
            Encrypt.Encrypt(rgbValues, numBytes, p, q, name2);

            //LSB 
            //Шифрованеие
            Bitmap png3 = new Bitmap(name4);
            LSB PNGWrite = new LSB();
            LSB.PNGWrite(ref png3, numBytes, name2, ref CountText);
            pictureBox2.Image = png3;

            //Изъятие заначений младших битов у пикселей  
            Bitmap png4 = new Bitmap(png3);
            LSB PNGRead = new LSB();
            LSB.PNGRead(ref png3, numBytes, name2, CountText);
            pictureBox2.Image = png4;

            //Декодирование
            RSA Decipher = new RSA();
            Decipher.Decipher(d, n, name2, name3);

            //Конец
            Bitmap png5 = new Bitmap(png4); // png4 должно быть
            Resampling TrimBitmap2 = new Resampling();
            Resampling.TrimBitmap2(ref png5, name3, numBytes);
            pictureBox3.Image = png5;
        } 
    }
}
