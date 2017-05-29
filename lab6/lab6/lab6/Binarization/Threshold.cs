using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace lab6.Binarization
{
    /// <summary>
    ///     Threshold binarization.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The filter does image binarization using specified threshold value. All pixels
    ///         with intensities equal or higher than threshold value are converted to white pixels. All other
    ///         pixels with intensities below threshold value are converted to black pixels.
    ///     </para>
    ///     <para>
    ///         The filter accepts 8 and 16 bpp grayscale images for processing.
    ///     </para>
    ///     <para>
    ///         <note>
    ///             Since the filter can be applied as to 8 bpp and to 16 bpp images,
    ///             the <see cref="P:lab6.Binarization.Threshold.ThresholdValue" /> value should be set appropriately to the pixel format.
    ///             In the case of 8 bpp images the threshold value is in the [0, 255] range, but in the case
    ///             of 16 bpp images the threshold value is in the [0, 65535] range.
    ///         </note>
    ///     </para>
    ///     <para>
    ///         Sample usage:
    ///     </para>
    ///     <code>
    /// // create filter
    ///             Threshold filter = new Threshold( 100 );
    ///             // apply the filter
    ///             filter.ApplyInPlace( image );
    /// 
    /// </code>
    ///     <para>
    ///         <b>Initial image:</b>
    ///     </para>
    ///     <img src="img/imaging/grayscale.jpg" width="480" height="361" />
    ///     <para>
    ///         <b>Result image:</b>
    ///     </para>
    ///     <img src="img/imaging/threshold.jpg" width="480" height="361" />
    /// </remarks>
    public class Threshold : BaseInPlacePartialFilter
    {
        private readonly Dictionary<PixelFormat, PixelFormat> _formatTranslations =
            new Dictionary<PixelFormat, PixelFormat>();

        /// <summary>
        ///     Threshold value.
        /// </summary>
        private int _threshold = 128;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:lab6.Binarization.Threshold" /> class.
        /// </summary>
        public Threshold()
        {
            _formatTranslations[PixelFormat.Format8bppIndexed] = PixelFormat.Format8bppIndexed;
            _formatTranslations[PixelFormat.Format16bppGrayScale] = PixelFormat.Format16bppGrayScale;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:lab6.Binarization.Threshold" /> class.
        /// </summary>
        /// <param name="threshold">Threshold value.</param>
        public Threshold(int threshold)
            : this()
        {
            _threshold = threshold;
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
        ///     Default value is set to <b>128</b>.
        /// </remarks>
        public int ThresholdValue
        {
            get { return _threshold; }
            set { _threshold = value; }
        }

        /// <summary>
        ///     Process the filter on the specified image.
        /// </summary>
        /// <param name="image">Source image data.</param>
        /// <param name="rect">Image rectangle for processing by the filter.</param>
        protected override unsafe void ProcessFilter(UnmanagedImage image, Rectangle rect)
        {
            var left = rect.Left;
            var top = rect.Top;
            var num1 = left + rect.Width;
            var num2 = top + rect.Height;
            if (image.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                var num3 = image.Stride - rect.Width;
                var numPtr = (byte*) ((IntPtr) image.ImageData.ToPointer() + (top*image.Stride + left));
                for (var index = top; index < num2; ++index)
                {
                    var num4 = left;
                    while (num4 < num1)
                    {
                        *numPtr = (int) *numPtr >= _threshold ? byte.MaxValue : (byte) 0;
                        ++num4;
                        ++numPtr;
                    }
                    numPtr += num3;
                }
            }
            else
            {
                var numPtr1 = (byte*) ((IntPtr) image.ImageData.ToPointer() + left*2);
                var stride = image.Stride;
                for (var index = top; index < num2; ++index)
                {
                    var numPtr2 = (ushort*) (numPtr1 + stride*index);
                    var num3 = left;
                    while (num3 < num1)
                    {
                        *numPtr2 = (int) *numPtr2 >= _threshold ? ushort.MaxValue : (ushort) 0;
                        ++num3;
                        ++numPtr2;
                    }
                }
            }
        }
    }
}