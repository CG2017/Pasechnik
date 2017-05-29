using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace lab6.ImageProcessing
{
    /// <summary>
    ///     Contrast adjusting in RGB color space.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The filter operates in <b>RGB</b> color space and adjusts
    ///         pixels' contrast value by increasing RGB values of bright pixel and decreasing
    ///         RGB values of dark pixels (or vise versa if contrast needs to be decreased).
    ///         The filter is based on <see cref="T:AForge.Imaging.Filters.LevelsLinear" />
    ///         filter and simply sets all input ranges to (<see cref="P:lab6.ImageProcessing.ContrastCorrection.Factor" />, 255-
    ///         <see cref="P:lab6.ImageProcessing.ContrastCorrection.Factor" />) and
    ///         all output range to (0, 255) in the case if the factor value is positive.
    ///         If the factor value is negative, then all input ranges are set to
    ///         (0, 255 ) and all output ranges are set to
    ///         (-<see cref="P:lab6.ImageProcessing.ContrastCorrection.Factor" />, 255_<see cref="P:lab6.ImageProcessing.ContrastCorrection.Factor" />).
    ///     </para>
    ///     <para>
    ///         See <see cref="T:AForge.Imaging.Filters.LevelsLinear" /> documentation forr more information about the base
    ///         filter.
    ///     </para>
    ///     <para>
    ///         The filter accepts 8 bpp grayscale and 24/32 bpp color images for processing.
    ///     </para>
    ///     <para>
    ///         Sample usage:
    ///     </para>
    ///     <code>
    /// // create filter
    ///              ContrastCorrection filter = new ContrastCorrection( 15 );
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
    ///     <img src="img/imaging/contrast_correction.jpg" width="480" height="361" />
    /// </remarks>
    /// <seealso cref="T:AForge.Imaging.Filters.LevelsLinear" />
    public class ContrastCorrection : BaseInPlacePartialFilter
    {
        private readonly LevelsLinear _baseFilter = new LevelsLinear();
        private int _factor;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:lab6.ImageProcessing.ContrastCorrection" /> class.
        /// </summary>
        public ContrastCorrection()
        {
            Factor = 10;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:lab6.ImageProcessing.ContrastCorrection" /> class.
        /// </summary>
        /// <param name="factor">Contrast <see cref="P:lab6.ImageProcessing.ContrastCorrection.Factor">adjusting factor</see>.</param>
        public ContrastCorrection(int factor)
        {
            Factor = factor;
        }

        /// <summary>
        ///     Contrast adjusting factor, [-127, 127].
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Factor which is used to adjust contrast. Factor values greater than
        ///         0 increase contrast making light areas lighter and dark areas darker. Factor values
        ///         less than 0 decrease contrast - decreasing variety of contrast.
        ///     </para>
        ///     <para>
        ///         Default value is set to <b>10</b>.
        ///     </para>
        /// </remarks>
        public int Factor
        {
            get { return _factor; }
            set
            {
                _factor = Math.Max(-127, Math.Min(sbyte.MaxValue, value));
                if (_factor > 0)
                {
                    _baseFilter.InRed =
                        _baseFilter.InGreen =
                            _baseFilter.InBlue = _baseFilter.InGray = new IntRange(_factor, byte.MaxValue - _factor);
                    _baseFilter.OutRed =
                        _baseFilter.OutGreen = _baseFilter.OutBlue = _baseFilter.OutGray = new IntRange(0, byte.MaxValue);
                }
                else
                {
                    _baseFilter.OutRed =
                        _baseFilter.OutGreen =
                            _baseFilter.OutBlue = _baseFilter.OutGray = new IntRange(-_factor, byte.MaxValue + _factor);
                    _baseFilter.InRed =
                        _baseFilter.InGreen = _baseFilter.InBlue = _baseFilter.InGray = new IntRange(0, byte.MaxValue);
                }
            }
        }

        /// <summary>
        ///     Format translations dictionary.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         See <see cref="P:AForge.Imaging.Filters.IFilterInformation.FormatTranslations" />
        ///         documentation for additional information.
        ///     </para>
        /// </remarks>
        public override Dictionary<PixelFormat, PixelFormat> FormatTranslations
        {
            get { return _baseFilter.FormatTranslations; }
        }

        /// <summary>
        ///     Process the filter on the specified image.
        /// </summary>
        /// <param name="image">Source image data.</param>
        /// <param name="rect">Image rectangle for processing by the filter.</param>
        protected override void ProcessFilter(UnmanagedImage image, Rectangle rect)
        {
            _baseFilter.ApplyInPlace(image, rect);
        }
    }
}