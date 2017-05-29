

using System;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging;

namespace lab6.Binarization
{
    /// <summary>
    /// Iterative threshold search and binarization.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// <para>
    /// The algorithm works in the following way:
    /// 
    /// <list type="bullet">
    /// 
    /// <item>
    /// select any start threshold;
    /// </item>
    /// 
    /// <item>
    /// compute average value of Background (µB) and Object (µO) values:
    ///             1) all pixels with a value that is below threshold, belong to the Background values;
    ///             2) all pixels greater or equal threshold, belong to the Object values.
    /// 
    /// </item>
    /// 
    /// <item>
    /// calculate new thresghold: (µB + µO) / 2;
    /// </item>
    /// 
    /// <item>
    /// if |oldThreshold - newThreshold| is less than a given manimum allowed error, then stop iteration process
    ///             and create the binary image with the new threshold.
    /// </item>
    /// 
    /// </list>
    /// 
    /// </para>
    /// 
    /// <para>
    /// For additional information see <b>Digital Image Processing, Gonzalez/Woods. Ch.10 page:599</b>.
    /// </para>
    /// 
    /// <para>
    /// The filter accepts 8 and 16 bpp grayscale images for processing.
    /// </para>
    /// 
    /// <para>
    /// <note>Since the filter can be applied as to 8 bpp and to 16 bpp images,
    ///             the initial value of <see cref="P:AForge.Imaging.Filters.Threshold.ThresholdValue"/> property should be set appropriately to the
    ///             pixel format. In the case of 8 bpp images the threshold value is in the [0, 255] range, but
    ///             in the case of 16 bpp images the threshold value is in the [0, 65535] range.</note>
    /// </para>
    /// 
    /// <para>
    /// Sample usage:
    /// </para>
    /// 
    /// <code>
    /// // create filter
    ///             IterativeThreshold filter = new IterativeThreshold( 2, 128 );
    ///             // apply the filter
    ///             Bitmap newImage = filter.Apply( image );
    /// 
    /// </code>
    /// 
    /// <para>
    /// <b>Initial image:</b>
    /// </para>
    /// <img src="img/imaging/sample11.png" width="256" height="256"/>
    /// <para>
    /// <b>Result image (calculated threshold is 102):</b>
    /// </para>
    /// <img src="img/imaging/iterative_threshold.png" width="256" height="256"/>
    /// </remarks>
    /// <seealso cref="T:AForge.Imaging.Filters.OtsuThreshold"/><seealso cref="T:AForge.Imaging.Filters.SISThreshold"/>
    public class IterativeThreshold : AForge.Imaging.Filters.Threshold
    {
        private int _minError;

        /// <summary>
        /// Minimum error, value when iterative threshold search is stopped.
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Default value is set to <b>0</b>.
        /// </remarks>
        public int MinimumError
        {
            get
            {
                return _minError;
            }
            set
            {
                _minError = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:lab6.Binarization.IterativeThreshold"/> class.
        /// 
        /// </summary>
        public IterativeThreshold()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:lab6.Binarization.IterativeThreshold"/> class.
        /// 
        /// </summary>
        /// <param name="minError">Minimum allowed error, that ends the iteration process.</param>
        public IterativeThreshold(int minError)
        {
            _minError = minError;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:lab6.Binarization.IterativeThreshold"/> class.
        /// 
        /// </summary>
        /// <param name="minError">Minimum allowed error, that ends the iteration process.</param><param name="threshold">Initial threshold value.</param>
        public IterativeThreshold(int minError, int threshold)
        {
            _minError = minError;
            this.threshold = threshold;
        }

        /// <summary>
        /// Calculate binarization threshold for the given image.
        /// 
        /// </summary>
        /// <param name="image">Image to calculate binarization threshold for.</param><param name="rect">Rectangle to calculate binarization threshold for.</param>
        /// <returns>
        /// Returns binarization threshold.
        /// </returns>
        /// 
        /// <remarks>
        /// 
        /// <para>
        /// The method is used to calculate binarization threshold only. The threshold
        ///             later may be applied to the image using <see cref="T:AForge.Imaging.Filters.Threshold"/> image processing filter.
        /// </para>
        /// 
        /// </remarks>
        /// <exception cref="T:AForge.Imaging.UnsupportedImageFormatException">Source pixel format is not supported by the routine. It should
        ///             8 bpp grayscale (indexed) or 16 bpp grayscale image.</exception>
        public int CalculateThreshold(Bitmap image, Rectangle rect)
        {
            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);
            try
            {
                return CalculateThreshold(bitmapData, rect);
            }
            finally
            {
                image.UnlockBits(bitmapData);
            }
        }

        /// <summary>
        /// Calculate binarization threshold for the given image.
        /// 
        /// </summary>
        /// <param name="image">Image to calculate binarization threshold for.</param><param name="rect">Rectangle to calculate binarization threshold for.</param>
        /// <returns>
        /// Returns binarization threshold.
        /// </returns>
        /// 
        /// <remarks>
        /// 
        /// <para>
        /// The method is used to calculate binarization threshold only. The threshold
        ///             later may be applied to the image using <see cref="T:AForge.Imaging.Filters.Threshold"/> image processing filter.
        /// </para>
        /// 
        /// </remarks>
        /// <exception cref="T:AForge.Imaging.UnsupportedImageFormatException">Source pixel format is not supported by the routine. It should
        ///             8 bpp grayscale (indexed) or 16 bpp grayscale image.</exception>
        public int CalculateThreshold(BitmapData image, Rectangle rect)
        {
            return CalculateThreshold(new UnmanagedImage(image), rect);
        }

        /// <summary>
        /// Calculate binarization threshold for the given image.
        /// 
        /// </summary>
        /// <param name="image">Image to calculate binarization threshold for.</param><param name="rect">Rectangle to calculate binarization threshold for.</param>
        /// <returns>
        /// Returns binarization threshold.
        /// </returns>
        /// 
        /// <remarks>
        /// 
        /// <para>
        /// The method is used to calculate binarization threshold only. The threshold
        ///             later may be applied to the image using <see cref="T:AForge.Imaging.Filters.Threshold"/> image processing filter.
        /// </para>
        /// 
        /// </remarks>
        /// <exception cref="T:AForge.Imaging.UnsupportedImageFormatException">Source pixel format is not supported by the routine. It should
        ///             8 bpp grayscale (indexed) or 16 bpp grayscale image.</exception>
        public unsafe int CalculateThreshold(UnmanagedImage image, Rectangle rect)
        {
            if (image.PixelFormat != PixelFormat.Format8bppIndexed && image.PixelFormat != PixelFormat.Format16bppGrayScale)
                throw new UnsupportedImageFormatException("Source pixel format is not supported by the routine.");
            int num1 = threshold;
            int left = rect.Left;
            int top = rect.Top;
            int num2 = left + rect.Width;
            int num3 = top + rect.Height;
            int[] numArray;
            int num4;
            if (image.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                numArray = new int[256];
                num4 = 256;
                byte* numPtr1 = (byte*)image.ImageData.ToPointer();
                int num5 = image.Stride - rect.Width;
                byte* numPtr2 = numPtr1 + (top * image.Stride + left);
                for (int index = top; index < num3; ++index)
                {
                    int num6 = left;
                    while (num6 < num2)
                    {
                        ++numArray[*numPtr2];
                        ++num6;
                        ++numPtr2;
                    }
                    numPtr2 += num5;
                }
            }
            else
            {
                numArray = new int[65536];
                num4 = 65536;
                byte* numPtr1 = (byte*)((IntPtr)image.ImageData.ToPointer() + left * 2);
                int stride = image.Stride;
                for (int index = top; index < num3; ++index)
                {
                    ushort* numPtr2 = (ushort*)(numPtr1 + (index * stride));
                    int num5 = left;
                    while (num5 < num2)
                    {
                        ++numArray[*numPtr2];
                        ++num5;
                        ++numPtr2;
                    }
                }
            }
            int num7;
            do
            {
                num7 = num1;
                double num5 = 0.0;
                int num6 = 0;
                double num8 = 0.0;
                int num9 = 0;
                for (int index = 0; index < num1; ++index)
                {
                    num8 += index * (double)numArray[index];
                    num9 += numArray[index];
                }
                for (int index = num1; index < num4; ++index)
                {
                    num5 += index * (double)numArray[index];
                    num6 += numArray[index];
                }
                double num10 = num8 / num9;
                double num11 = num5 / num6;
                num1 = num9 != 0 ? (num6 != 0 ? (int)((num10 + num11) / 2.0) : (int)num10) : (int)num11;
            }
            while (Math.Abs(num7 - num1) > _minError);
            return num1;
        }

        /// <summary>
        /// Process the filter on the specified image.
        /// 
        /// </summary>
        /// <param name="image">Source image data.</param><param name="rect">Image rectangle for processing by the filter.</param>
        protected override void ProcessFilter(UnmanagedImage image, Rectangle rect)
        {
            threshold = CalculateThreshold(image, rect);
            base.ProcessFilter(image, rect);
        }
    }
}
