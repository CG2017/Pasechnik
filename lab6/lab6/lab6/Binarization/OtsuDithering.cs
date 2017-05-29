// Decompiled with JetBrains decompiler
// Type: AForge.Imaging.Filters.OtsuThreshold
// Assembly: AForge.Imaging, Version=2.2.5.0, Culture=neutral, PublicKeyToken=ba8ddea9676ca48b
// MVID: 37B60E10-52A5-43BE-987A-B8D4999A69A7
// Assembly location: D:\Dropbox\source\3k\6s\kg\lab6\lab6\packages\AForge.Imaging.2.2.5\lib\AForge.Imaging.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace lab6.Binarization
{
    /// <summary>
    ///     Otsu thresholding.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The class implements Otsu thresholding, which is described in
    ///         <b>
    ///             N. Otsu, "A threshold selection method from gray-level histograms", IEEE Trans. Systems,
    ///             Man and Cybernetics 9(1), pp. 62–66, 1979.
    ///         </b>
    ///     </para>
    ///     <para>
    ///         This implementation instead of minimizing the weighted within-class variance
    ///         does maximization of between-class variance, what gives the same result. The approach is
    ///         described in <a href="http://sampl.ece.ohio-state.edu/EE863/2004/ECE863-G-segclust2.ppt">this presentation</a>.
    ///     </para>
    ///     <para>
    ///         The filter accepts 8 bpp grayscale images for processing.
    ///     </para>
    ///     <para>
    ///         Sample usage:
    ///     </para>
    ///     <code>
    /// // create filter
    ///             OtsuThreshold filter = new OtsuThreshold( );
    ///             // apply the filter
    ///             filter.ApplyInPlace( image );
    ///             // check threshold value
    ///             byte t = filter.ThresholdValue;
    ///             // ...
    /// 
    /// </code>
    ///     <para>
    ///         <b>Initial image:</b>
    ///     </para>
    ///     <img src="img/imaging/sample11.png" width="256" height="256" />
    ///     <para>
    ///         <b>Result image (calculated threshold is 97):</b>
    ///     </para>
    ///     <img src="img/imaging/otsu_threshold.png" width="256" height="256" />
    /// </remarks>
    /// <seealso cref="T:AForge.Imaging.Filters.IterativeThreshold" />
    /// <seealso cref="T:AForge.Imaging.Filters.SISThreshold" />
    public class OtsuThreshold : BaseInPlacePartialFilter
    {
        private readonly Dictionary<PixelFormat, PixelFormat> _formatTranslations =
            new Dictionary<PixelFormat, PixelFormat>();

        private readonly AForge.Imaging.Filters.Threshold _thresholdFilter = new AForge.Imaging.Filters.Threshold();

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:lab6.Binarization.OtsuThreshold" /> class.
        /// </summary>
        public OtsuThreshold()
        {
            _formatTranslations[PixelFormat.Format8bppIndexed] = PixelFormat.Format8bppIndexed;
        }

        /// <summary>
        ///     Format translations dictionary.
        /// </summary>
        public override Dictionary<PixelFormat, PixelFormat> FormatTranslations
        {
            get { return _formatTranslations; }
        }

        /// <summary>
        ///     Threshold value.
        /// </summary>
        /// <remarks>
        ///     The property is read only and represents the value, which
        ///     was automaticaly calculated using Otsu algorithm.
        /// </remarks>
        public int ThresholdValue
        {
            get { return _thresholdFilter.ThresholdValue; }
        }

        /// <summary>
        ///     Calculate binarization threshold for the given image.
        /// </summary>
        /// <param name="image">Image to calculate binarization threshold for.</param>
        /// <param name="rect">Rectangle to calculate binarization threshold for.</param>
        /// <returns>
        ///     Returns binarization threshold.
        /// </returns>
        /// <remarks>
        ///     <para>
        ///         The method is used to calculate binarization threshold only. The threshold
        ///         later may be applied to the image using <see cref="T:AForge.Imaging.Filters.Threshold" /> image processing
        ///         filter.
        ///     </para>
        /// </remarks>
        /// <exception cref="T:AForge.Imaging.UnsupportedImageFormatException">
        ///     Source pixel format is not supported by the routine. It should be
        ///     8 bpp grayscale (indexed) image.
        /// </exception>
        public int CalculateThreshold(Bitmap image, Rectangle rect)
        {
            var bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly,
                image.PixelFormat);
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
        ///     Calculate binarization threshold for the given image.
        /// </summary>
        /// <param name="image">Image to calculate binarization threshold for.</param>
        /// <param name="rect">Rectangle to calculate binarization threshold for.</param>
        /// <returns>
        ///     Returns binarization threshold.
        /// </returns>
        /// <remarks>
        ///     <para>
        ///         The method is used to calculate binarization threshold only. The threshold
        ///         later may be applied to the image using <see cref="T:AForge.Imaging.Filters.Threshold" /> image processing
        ///         filter.
        ///     </para>
        /// </remarks>
        /// <exception cref="T:AForge.Imaging.UnsupportedImageFormatException">
        ///     Source pixel format is not supported by the routine. It should be
        ///     8 bpp grayscale (indexed) image.
        /// </exception>
        public int CalculateThreshold(BitmapData image, Rectangle rect)
        {
            return CalculateThreshold(new UnmanagedImage(image), rect);
        }

        /// <summary>
        ///     Calculate binarization threshold for the given image.
        /// </summary>
        /// <param name="image">Image to calculate binarization threshold for.</param>
        /// <param name="rect">Rectangle to calculate binarization threshold for.</param>
        /// <returns>
        ///     Returns binarization threshold.
        /// </returns>
        /// <remarks>
        ///     <para>
        ///         The method is used to calculate binarization threshold only. The threshold
        ///         later may be applied to the image using <see cref="T:AForge.Imaging.Filters.Threshold" /> image processing
        ///         filter.
        ///     </para>
        /// </remarks>
        /// <exception cref="T:AForge.Imaging.UnsupportedImageFormatException">
        ///     Source pixel format is not supported by the routine. It should be
        ///     8 bpp grayscale (indexed) image.
        /// </exception>
        public unsafe int CalculateThreshold(UnmanagedImage image, Rectangle rect)
        {
            if (image.PixelFormat != PixelFormat.Format8bppIndexed)
                throw new UnsupportedImageFormatException("Source pixel format is not supported by the routine.");
            var num1 = 0;
            var left = rect.Left;
            var top = rect.Top;
            var num2 = left + rect.Width;
            var num3 = top + rect.Height;
            var num4 = image.Stride - rect.Width;
            var numArray1 = new int[256];
            var numArray2 = new double[256];
            var numPtr = (byte*) ((IntPtr) image.ImageData.ToPointer() + (top*image.Stride + left));
            for (var index = top; index < num3; ++index)
            {
                var num5 = left;
                while (num5 < num2)
                {
                    ++numArray1[*numPtr];
                    ++num5;
                    ++numPtr;
                }
                numPtr += num4;
            }
            var num6 = (num2 - left)*(num3 - top);
            var num7 = 0.0;
            for (var index = 0; index < 256; ++index)
            {
                numArray2[index] = numArray1[index]/(double) num6;
                num7 += numArray2[index]*index;
            }
            var num8 = double.MinValue;
            var num9 = 0.0;
            var num10 = 1.0;
            var num11 = 0.0;
            for (var index = 0; index < 256 && num10 > 0.0; ++index)
            {
                var num5 = num11;
                var num12 = (num7 - num5*num9)/num10;
                var num13 = num9*(1.0 - num9)*Math.Pow(num5 - num12, 2.0);
                if (num13 > num8)
                {
                    num8 = num13;
                    num1 = index;
                }
                var num14 = num11*num9;
                num9 += numArray2[index];
                num10 -= numArray2[index];
                num11 = num14 + index*numArray2[index];
                if (Math.Abs(num9) > Double.Epsilon)
                    num11 /= num9;
            }
            return num1;
        }

        /// <summary>
        ///     Process the filter on the specified image.
        /// </summary>
        /// <param name="image">Source image data.</param>
        /// <param name="rect">Image rectangle for processing by the filter.</param>
        protected override void ProcessFilter(UnmanagedImage image, Rectangle rect)
        {
            _thresholdFilter.ThresholdValue = CalculateThreshold(image, rect);
            _thresholdFilter.ApplyInPlace(image, rect);
        }
    }
}