/*
 * Created by Rajan Tawate.
 * User: Owner
 * Date: 9/3/2006
 * Time: 8:00 PM
 * 

 */

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace lab6.ConvertToGrayscale
{
    /// <summary>
    ///     Description of ImageConverter.
    /// </summary>
    public static class ImageConverter
    {
        public static byte[] ImageToByteArray(Image imageIn)
        {
            var ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Bmp);
            return ms.ToArray();
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
            var returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}