using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace lab6.ImageProcessing
{
    /// <summary>
    ///     Gamma correction filter.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The filter performs <a href="http://en.wikipedia.org/wiki/Gamma_correction">gamma correction</a>
    ///         of specified image in RGB color space. Each pixels' value is converted using the V<sub>out</sub>=V<sub>in</sub>
    ///         <sup>g</sup>
    ///         equation, where <b>g</b> is <see cref="P:lab6.ImageProcessing.GammaCorrection.Gamma">gamma value</see>.
    ///     </para>
    ///     <para>
    ///         The filter accepts 8 bpp grayscale and 24 bpp color images for processing.
    ///     </para>
    ///     <para>
    ///         Sample usage:
    ///     </para>
    ///     <code>
    /// // create filter
    ///             GammaCorrection filter = new GammaCorrection( 0.5 );
    ///             // apply the filter
    ///             filter.ApplyInPlace( image );
    /// 
    /// </code>
    ///     <para>
    ///         <b>Initial image:</b>
    ///     </para>
    ///     <img src="img/imaging/sample1.jpg" width="480" height="361" />
    ///     <para>
    ///         <b>Result image:</b>
    ///     </para>
    ///     <img src="img/imaging/gamma.jpg" width="480" height="361" />
    /// </remarks>
    public class GammaCorrection : BaseInPlacePartialFilter
    {
        private readonly Dictionary<PixelFormat, PixelFormat> _formatTranslations =
            new Dictionary<PixelFormat, PixelFormat>();

        private readonly byte[] _table = new byte[256];

        private double _gamma;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:lab6.ImageProcessing.GammaCorrection" /> class.
        /// </summary>
        public GammaCorrection()
            : this(2.2)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:lab6.ImageProcessing.GammaCorrection" /> class.
        /// </summary>
        /// <param name="gamma">Gamma value.</param>
        public GammaCorrection(double gamma)
        {
            Gamma = gamma;
            _formatTranslations[PixelFormat.Format8bppIndexed] = PixelFormat.Format8bppIndexed;
            _formatTranslations[PixelFormat.Format24bppRgb] = PixelFormat.Format24bppRgb;
        }

        /// <summary>
        ///     Format translations dictionary.
        /// </summary>
        public override Dictionary<PixelFormat, PixelFormat> FormatTranslations
        {
            get { return _formatTranslations; }
        }

        /// <summary>
        ///     Gamma value, [0.1, 5.0].
        /// </summary>
        /// <remarks>
        ///     Default value is set to <b>2.2</b>.
        /// </remarks>
        public double Gamma
        {
            get { return _gamma; }
            set
            {
                _gamma = Math.Max(0.1, Math.Min(5.0, value));
                var y = 1.0/_gamma;
                for (var index = 0; index < 256; ++index)
                    _table[index] =
                        (byte)
                            Math.Min(byte.MaxValue,
                                (int) (Math.Pow(index/(double) byte.MaxValue, y)*byte.MaxValue + 0.5));
            }
        }

        /// <summary>
        ///     Process the filter on the specified image.
        /// </summary>
        /// <param name="image">Source image data.</param>
        /// <param name="rect">Image rectangle for processing by the filter.</param>
        protected override unsafe void ProcessFilter(UnmanagedImage image, Rectangle rect)
        {
            var num1 = image.PixelFormat == PixelFormat.Format8bppIndexed ? 1 : 3;
            var num2 = rect.Left*num1;
            var top = rect.Top;
            var num3 = num2 + rect.Width*num1;
            var num4 = top + rect.Height;
            var num5 = image.Stride - rect.Width*num1;
            var numPtr = (byte*) ((IntPtr) image.ImageData.ToPointer() + (top*image.Stride + num2));
            for (var index = top; index < num4; ++index)
            {
                var num6 = num2;
                while (num6 < num3)
                {
                    *numPtr = _table[*numPtr];
                    ++num6;
                    ++numPtr;
                }
                numPtr += num5;
            }
        }
    }
}