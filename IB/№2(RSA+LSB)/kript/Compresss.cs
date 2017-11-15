using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;

namespace kript
{
    class Compresss
    {
        static public void VaryQualityLevel()
        {
            // Get a bitmap.
            Bitmap bmp1 = new Bitmap(@"C:\Users\User\Documents\Visual Studio 2013\Projects\kript\image.png");
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Png);

            System.Drawing.Imaging.Encoder myEncoder =
                System.Drawing.Imaging.Encoder.Quality;

            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save(@"C:\Users\User\Documents\Visual Studio 2013\Projects\kript\image21111.png", jpgEncoder, myEncoderParameters);


        }

           static public ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

    }
}
