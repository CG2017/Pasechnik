/*

*/

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ColorReplacer
{
    public static class ExtBitmap
    {
        public static Bitmap ColorSubstitution(this Bitmap sourceBitmap, ColorSubstitutionFilter filterData)
        {
            var resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height, PixelFormat.Format32bppArgb);

            var sourceData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var resultData = resultBitmap.LockBits(new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            var resultBuffer = new byte[resultData.Stride*resultData.Height];
            Marshal.Copy(sourceData.Scan0, resultBuffer, 0, resultBuffer.Length);

            sourceBitmap.UnlockBits(sourceData);

            byte sourceRed, sourceGreen, sourceBlue, sourceAlpha;
            int resultRed, resultGreen, resultBlue;

            var newRedValue = filterData.NewColor.R;
            var newGreenValue = filterData.NewColor.G;
            var newBlueValue = filterData.NewColor.B;

            var redFilter = filterData.SourceColor.R;
            var greenFilter = filterData.SourceColor.G;
            var blueFilter = filterData.SourceColor.B;

            byte minValue = 0;
            byte maxValue = 255;

            for (var k = 0; k < resultBuffer.Length; k += 4)
            {
                sourceAlpha = resultBuffer[k + 3];

                if (sourceAlpha != 0)
                {
                    sourceBlue = resultBuffer[k];
                    sourceGreen = resultBuffer[k + 1];
                    sourceRed = resultBuffer[k + 2];

                    if (sourceBlue < blueFilter + filterData.ThresholdValue &&
                        sourceBlue > blueFilter - filterData.ThresholdValue &&
                        sourceGreen < greenFilter + filterData.ThresholdValue &&
                        sourceGreen > greenFilter - filterData.ThresholdValue &&
                        sourceRed < redFilter + filterData.ThresholdValue &&
                        sourceRed > redFilter - filterData.ThresholdValue)
                    {
                        resultBlue = blueFilter - sourceBlue + newBlueValue;

                        if (resultBlue > maxValue)
                        {
                            resultBlue = maxValue;
                        }
                        else if (resultBlue < minValue)
                        {
                            resultBlue = minValue;
                        }

                        resultGreen = greenFilter - sourceGreen + newGreenValue;

                        if (resultGreen > maxValue)
                        {
                            resultGreen = maxValue;
                        }
                        else if (resultGreen < minValue)
                        {
                            resultGreen = minValue;
                        }

                        resultRed = redFilter - sourceRed + newRedValue;

                        if (resultRed > maxValue)
                        {
                            resultRed = maxValue;
                        }
                        else if (resultRed < minValue)
                        {
                            resultRed = minValue;
                        }

                        resultBuffer[k] = (byte) resultBlue;
                        resultBuffer[k + 1] = (byte) resultGreen;
                        resultBuffer[k + 2] = (byte) resultRed;
                        resultBuffer[k + 3] = sourceAlpha;
                    }
                }
            }

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        public static Bitmap Format32BppArgbCopy(this Bitmap sourceBitmap)
        {
            var copyBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height, PixelFormat.Format32bppArgb);

            using (var graphicsObject = Graphics.FromImage(copyBitmap))
            {
                graphicsObject.CompositingQuality = CompositingQuality.HighQuality;
                graphicsObject.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsObject.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphicsObject.SmoothingMode = SmoothingMode.HighQuality;

                graphicsObject.DrawImage(sourceBitmap, new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height),
                    new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), GraphicsUnit.Pixel);
            }

            return copyBitmap;
        }
    }

    public class ColorSubstitutionFilter
    {
        public int ThresholdValue { get; set; } = 10;

        public Color SourceColor { get; set; } = Color.White;

        public Color NewColor { get; set; } = Color.White;
    }
}