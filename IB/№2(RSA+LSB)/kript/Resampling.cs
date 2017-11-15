using System.Drawing;
using System;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace kript
{
    class Resampling
    {
        public static void TrimBitmap(ref Bitmap input, string name1)
        {
            Bitmap png = new Bitmap(input.Width, input.Height);
            Bitmap png1 = new Bitmap(50, 50);


            for (int i = 0; i <= 49; i++)
            {
                for (int j = 0; j <= 49; j++)
                {
                    png1.SetPixel(i, j, input.GetPixel(i + 150, j + 150));
                }
            }

            png1.Save(name1);

            for (int i = 0; i < input.Width; i++)
            {
                for (int j = 0; j < input.Height; j++)
                {
                    if (i > 149 && j > 149 && j < 200 && i < 200)
                    {
                        png.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }
                    else
                    {
                        png.SetPixel(i, j, input.GetPixel(i, j));
                    }
                }
            }

            input = png;
        }

        public static void TrimBitmap2(ref Bitmap input, string name3, int numBytes)
        {
            Bitmap png = new Bitmap(input.Width, input.Height);
            int size = 0;

            List<byte> rgbValues = new List<byte>();

            StreamReader sr = new StreamReader(name3);

            while (!sr.EndOfStream)
            {
                rgbValues.Add(byte.Parse(sr.ReadLine()));
            }

            sr.Close();
            for (int i = 0; i < input.Width; i++)
            {
                for (int j = 0; j < input.Height; j++) 
                {
                    if (i > 149 && j > 149 && j < 200 && i < 200)
                    {
                        png.SetPixel(j, i, Color.FromArgb(rgbValues[size], rgbValues[size + (numBytes / 3)], rgbValues[size + (numBytes - (numBytes / 3))]));
                            size++;
                    }
                    else
                    {
                        png.SetPixel(i, j, input.GetPixel(i, j));
                    }
                }
            }

            input = png;
        }
    }
}
