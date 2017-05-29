using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace lab6.ImageProcessing
{
    /// <summary>
    ///     Invert image.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The filter inverts colored and grayscale images.
    ///     </para>
    ///     <para>
    ///         The filter accepts 8, 16 bpp grayscale and 24, 48 bpp color images for processing.
    ///     </para>
    ///     <para>
    ///         Sample usage:
    ///     </para>
    ///     <code>
    /// // create filter
    ///              Invert filter = new Invert( );
    ///              // apply the filter
    ///              filter.ApplyInPlace( image );
    /// 
    /// </code>
    ///     <para>
    ///         <b>Initial image:</b>
    ///     </para>
    ///     <img src="img/imaging/sample1.jpg" width="480" height="361" />
    ///     <para>
    ///         <b>Result image:</b>
    ///     </para>
    ///     <img src="img/imaging/invert.jpg" width="480" height="361" />
    /// </remarks>
    public sealed class Invert : BaseInPlacePartialFilter
    {
        private readonly Dictionary<PixelFormat, PixelFormat> _formatTranslations =
            new Dictionary<PixelFormat, PixelFormat>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:lab6.ImageProcessing.Invert" /> class.
        /// </summary>
        public Invert()
        {
            _formatTranslations[PixelFormat.Format8bppIndexed] = PixelFormat.Format8bppIndexed;
            _formatTranslations[PixelFormat.Format24bppRgb] = PixelFormat.Format24bppRgb;
            _formatTranslations[PixelFormat.Format16bppGrayScale] = PixelFormat.Format16bppGrayScale;
            _formatTranslations[PixelFormat.Format48bppRgb] = PixelFormat.Format48bppRgb;
        }

        /// <summary>
        ///     Format translations dictionary.
        /// </summary>
        public override Dictionary<PixelFormat, PixelFormat> FormatTranslations
        {
            get { return _formatTranslations; }
        }

        /// <summary>
        ///     Process the filter on the specified image.
        /// </summary>
        /// <param name="image">Source image data.</param>
        /// <param name="rect">Image rectangle for processing by the filter.</param>
        protected override unsafe void ProcessFilter(UnmanagedImage image, Rectangle rect)
        {
            var num1 = image.PixelFormat == PixelFormat.Format8bppIndexed ||
                       image.PixelFormat == PixelFormat.Format16bppGrayScale
                ? 1
                : 3;
            var top = rect.Top;
            var num2 = top + rect.Height;
            var num3 = rect.Left*num1;
            var num4 = num3 + rect.Width*num1;
            var numPtr1 = (byte*) image.ImageData.ToPointer();
            if (image.PixelFormat == PixelFormat.Format8bppIndexed || image.PixelFormat == PixelFormat.Format24bppRgb)
            {
                var num5 = image.Stride - (num4 - num3);
                var numPtr2 = numPtr1 + (top*image.Stride + rect.Left*num1);
                for (var index = top; index < num2; ++index)
                {
                    var num6 = num3;
                    while (num6 < num4)
                    {
                        *numPtr2 = (byte) (byte.MaxValue - (uint) *numPtr2);
                        ++num6;
                        ++numPtr2;
                    }
                    numPtr2 += num5;
                }
            }
            else
            {
                var stride = image.Stride;
                var numPtr2 = numPtr1 + (top*image.Stride + rect.Left*num1*2);
                for (var index = top; index < num2; ++index)
                {
                    var numPtr3 = (ushort*) numPtr2;
                    var num5 = num3;
                    while (num5 < num4)
                    {
                        *numPtr3 = (ushort) (ushort.MaxValue - (uint) *numPtr3);
                        ++num5;
                        ++numPtr3;
                    }
                    numPtr2 += stride;
                }
            }
        }
    }
}