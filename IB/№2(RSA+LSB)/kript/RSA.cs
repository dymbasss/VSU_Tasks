using System.Drawing;
using System;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Numerics;
using System.IO;

namespace kript
{
    class RSA
    {
        public void characters2(int[] characters)
        {
            for (int i = 0; i < 255; i++)
            {
                characters[i] = Convert.ToByte(i);
            }
        }

        public static void RGBLength(Bitmap result1, ref byte[] rgbValues, ref int numBytes)
        {

            numBytes = result1.Width * result1.Height * 3;
            rgbValues = new byte[numBytes];

            Color color = new Color();
            int size = 0;

            if (rgbValues.Length == result1.Width * result1.Height * 3)
            {
                for (int j = 0; j < result1.Height; j++)
                {
                    for (int i = 0; i < result1.Width ; i++)
                    {
                        color = result1.GetPixel(i, j);
                        rgbValues[size] += color.R;                      
                        rgbValues[size + (numBytes / 3)] += color.G;
                        rgbValues[size + (numBytes - (numBytes / 3))] += color.B;
                        size++;
                    }
                }
            }
        }

        public void Encrypt(byte[] rgbValues, int numBytes, long p, long q, string name2) // Зашифровать 
        {

                if (IsTheNumberSimple(p) && IsTheNumberSimple(q))
                {
                    byte[] RGB = new byte[numBytes];
                    for (int i = 0; i < rgbValues.Length; i++)
                    {
                        RGB[i] = rgbValues[i];
                    }

                    long n = p * q;
                    long m = (p - 1) * (q - 1);
                    long d = Calculate_d(m);
                    long e_ = Calculate_e(d, m);

                    List<string> result = RSA_Endoce(RGB, e_, n);

                    StreamWriter sw = new StreamWriter(name2);
                    foreach (string item in result)
                        sw.WriteLine(item);  //WriteLine - если нужно в столбец
                    sw.Close();

                }
        }

        public void Decipher(long d, long n, string name2, string name3) // Расшифровать
        {
            if ((d > 0) && (n > 0))
            {

                List<string> input = new List<string>();

                StreamReader sr = new StreamReader(name2);

                while (!sr.EndOfStream)
                {
                    input.Add(sr.ReadLine());
                }

                sr.Close();

                List<string> result = RSA_Dedoce(input, d, n);

                StreamWriter sw = new StreamWriter(name3);
                foreach (string item in result)
                    sw.WriteLine(item);
                sw.Close();
            }
        }

        private List<string> RSA_Endoce(byte[] RGB, long e, long n)
        {
            List<string> result = new List<string>();
            BigInteger bi;
            int[] characters = new int[255];
            characters2(characters);

            for (int i = 0; i < RGB.Length; i++)
            {

                int index = Array.IndexOf(characters, RGB[i]);
                bi = new BigInteger(index);
                bi = BigInteger.Pow(bi, (int)e);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                result.Add(bi.ToString());
            }

            return result;
        }

        private List<string> RSA_Dedoce(List<string> input, long d, long n)
        {
            List<string> result = new List<string>();
            BigInteger bi;
            int[] characters = new int[255];
            characters2(characters);

                foreach (string item in input)
                {
                    bi = new BigInteger(Convert.ToDouble(item));
                    bi = BigInteger.Pow(bi, (int)d);

                    BigInteger n_ = new BigInteger((int)n);

                    bi = bi % n_;

                    int index = Convert.ToInt32(bi.ToString());

                    result.Add(characters[index].ToString());
                }

            return result;
        }

        private bool IsTheNumberSimple(long n)
        {
            if (n < 2)
                return false;

            if (n == 2)
                return true;

            for (long i = 2; i < n; i++)
                if (n % i == 0)
                    return false;

            return true;
        }

        private long Calculate_d(long m)
        {
            long d = m - 1;

            for (long i = 2; i <= m; i++)
                if ((m % i == 0) && (d % i == 0)) //если имеют общие делители
                {
                    d--;
                    i = 1;
                }

            return d;
        }

        private long Calculate_e(long d, long m)
        {
            long e = 10;

            while (true)
            {
                if ((e * d) % m == 1)
                    break;
                else
                    e++;
            }

            return e;
        }

    }
}
