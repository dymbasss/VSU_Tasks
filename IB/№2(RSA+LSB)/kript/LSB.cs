using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace kript
{
    class LSB
    {

        static public void PNGWrite(ref Bitmap input, int numBytes, string name2, ref int CountText)
        {
            Bitmap png = new Bitmap(input.Height, input.Width);

            FileStream sr = new FileStream(name2, FileMode.Open);
            BinaryReader bText = new BinaryReader(sr, Encoding.ASCII);

            List<byte> bList = new List<byte>();
            while (bText.PeekChar() != -1)
            { //считали весь текстовый файл для шифрования в лист байт
                bList.Add(bText.ReadByte());
            }

            CountText = bList.Count; // в CountText - количество в байтах текста, который нужно закодировать
            bText.Close();
            sr.Close();

            //проверяем, поместиться ли исходный текст в картинке
            if (CountText > ((png.Width * png.Height)) - numBytes)
            {
                MessageBox.Show("Выбранная картинка мала для размещения выбранного текста", "Информация", MessageBoxButtons.OK);
                return;
            }

            int index = 0;
            for (int i = 0; i < input.Width; i++)
            {
                for (int j = 0; j < input.Height; j++)
                {
                    Color pixelColor = input.GetPixel(i, j);

                    if (index == bList.Count)
                    {
                        png.SetPixel(i, j, input.GetPixel(i, j));
                    }
                    else
                    {
                        if (i > 149 && j > 149 && j < 200 && i < 200)
                        {
                            i++;
                            j++;
                        }
                        else
                        {
                            BitArray colorArray = ByteToBit(pixelColor.R);
                            BitArray messageArray = ByteToBit(bList[index]);
                            colorArray[0] = messageArray[0]; //меняем
                            colorArray[1] = messageArray[1]; // в нашем цвете биты
                            byte newR = BitToByte(colorArray);

                            colorArray = ByteToBit(pixelColor.G);
                            colorArray[0] = messageArray[2];
                            colorArray[1] = messageArray[3];
                            colorArray[2] = messageArray[4];
                            byte newG = BitToByte(colorArray);

                            colorArray = ByteToBit(pixelColor.B);
                            colorArray[0] = messageArray[5];
                            colorArray[1] = messageArray[6];
                            colorArray[2] = messageArray[7];
                            byte newB = BitToByte(colorArray);

                            Color newColor = Color.FromArgb(newR, newG, newB);
                            png.SetPixel(i, j, newColor);
                            index++;
                        }
                    }
                }
            }

            input = png;
        }

        static public void PNGRead(ref Bitmap input, int numBytes, string name2, int CountText)
        {
            Bitmap png = new Bitmap(input.Height, input.Width);
            int countSymbol = CountText; //считали количество зашифрованных символов
            byte[] message = new byte[countSymbol];
            int index = 0;
            for (int i = 0; i < input.Width; i++)
            {
                for (int j = 0; j < input.Height; j++)
                {
                    Color pixelColor = input.GetPixel(i, j);
                    if (index == message.Length)
                    {
                        png.SetPixel(i, j, input.GetPixel(i, j));
                    }
                    else
                    {
                        if (i > 149 && j > 149 && j < 200 && i < 200)
                        {
                            i++;
                            j++;
                        }
                        else
                        {
                            BitArray colorArray = ByteToBit(pixelColor.R);
                            BitArray messageArray = ByteToBit(pixelColor.R);
                            messageArray[0] = colorArray[0];
                            messageArray[1] = colorArray[1];
                            byte newR = BitToByte(colorArray);

                            colorArray = ByteToBit(pixelColor.G);
                            messageArray[2] = colorArray[0];
                            messageArray[3] = colorArray[1];
                            messageArray[4] = colorArray[2];
                            byte newG = BitToByte(colorArray);
                            colorArray = ByteToBit(pixelColor.B);
                            messageArray[5] = colorArray[0];
                            messageArray[6] = colorArray[1];
                            messageArray[7] = colorArray[2];
                            message[index] = BitToByte(messageArray);
                            byte newB = BitToByte(colorArray);

                            Color newColor = Color.FromArgb(newR, newG, newB);
                            png.SetPixel(i, j, newColor);
                            index++;
                        }
                    }
                }
            }
            input = png;
            StreamWriter sw = new StreamWriter(name2);
            string strMessage = Encoding.GetEncoding(1251).GetString(message);
            foreach (char item in strMessage)
                sw.Write(item);
            sw.Close();

        }

        private static BitArray ByteToBit(byte src)
        {
            BitArray bitArray = new BitArray(8);
            bool st = false;
            for (int i = 0; i < 8; i++)
            {
                if ((src >> i & 1) == 1)
                {
                    st = true;
                }
                else st = false;
                bitArray[i] = st;
            }
            return bitArray;
        }

        private static byte BitToByte(BitArray scr)
        {
            byte num = 0;
            for (int i = 0; i < scr.Count; i++)
                if (scr[i] == true)
                    num += (byte)Math.Pow(2, i);
            return num;
        }

    }
}
