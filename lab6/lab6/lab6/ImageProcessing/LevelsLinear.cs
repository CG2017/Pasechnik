using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using Image = System.Drawing.Image;

namespace lab6.ImageProcessing
{
    /// <summary>
    ///     Linear correction of RGB channels.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The filter performs linear correction of RGB channels by mapping specified
    ///         channels' input ranges to output ranges. It is similar to the
    ///         <see cref="T:AForge.Imaging.Filters.ColorRemapping" />, but the remapping is linear.
    ///     </para>
    ///     <para>
    ///         The filter accepts 8 bpp grayscale and 24/32 bpp color images for processing.
    ///     </para>
    ///     <para>
    ///         Sample usage:
    ///     </para>
    ///     <code>
    /// // create filter
    ///             LevelsLinear filter = new LevelsLinear( );
    ///             // set ranges
    ///             filter.InRed   = new IntRange( 30, 230 );
    ///             filter.InGreen = new IntRange( 50, 240 );
    ///             filter.InBlue  = new IntRange( 10, 210 );
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
    ///     <img src="img/imaging/levels_linear.jpg" width="480" height="361" />
    /// </remarks>
    /// <seealso cref="T:AForge.Imaging.Filters.HSLLinear" />
    /// <seealso cref="T:AForge.Imaging.Filters.YCbCrLinear" />
    public class LevelsLinear : BaseInPlacePartialFilter
    {
        private readonly Dictionary<PixelFormat, PixelFormat> _formatTranslations =
            new Dictionary<PixelFormat, PixelFormat>();

        private readonly byte[] _mapBlue = new byte[256];
        private readonly byte[] _mapGreen = new byte[256];
        private readonly byte[] _mapRed = new byte[256];

        private IntRange _inBlue = new IntRange(0, byte.MaxValue);
        private IntRange _inGreen = new IntRange(0, byte.MaxValue);
        private IntRange _inRed = new IntRange(0, byte.MaxValue);
        private IntRange _outBlue = new IntRange(0, byte.MaxValue);
        private IntRange _outGreen = new IntRange(0, byte.MaxValue);
        private IntRange _outRed = new IntRange(0, byte.MaxValue);

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:lab6.ImageProcessing.LevelsLinear" /> class.
        /// </summary>
        public LevelsLinear()
        {
            CalculateMap(_inRed, _outRed, _mapRed);
            CalculateMap(_inGreen, _outGreen, _mapGreen);
            CalculateMap(_inBlue, _outBlue, _mapBlue);
            _formatTranslations[PixelFormat.Format8bppIndexed] = PixelFormat.Format8bppIndexed;
            _formatTranslations[PixelFormat.Format24bppRgb] = PixelFormat.Format24bppRgb;
            _formatTranslations[PixelFormat.Format32bppRgb] = PixelFormat.Format32bppRgb;
            _formatTranslations[PixelFormat.Format32bppArgb] = PixelFormat.Format32bppArgb;
        }

        /// <summary>
        ///     Format translations dictionary.
        /// </summary>
        public override Dictionary<PixelFormat, PixelFormat> FormatTranslations
        {
            get { return _formatTranslations; }
        }

        /// <summary>
        ///     Red component's input range.
        /// </summary>
        public IntRange InRed
        {
            get { return _inRed; }
            set
            {
                _inRed = value;
                CalculateMap(_inRed, _outRed, _mapRed);
            }
        }

        /// <summary>
        ///     Green component's input range.
        /// </summary>
        public IntRange InGreen
        {
            get { return _inGreen; }
            set
            {
                _inGreen = value;
                CalculateMap(_inGreen, _outGreen, _mapGreen);
            }
        }

        /// <summary>
        ///     Blue component's input range.
        /// </summary>
        public IntRange InBlue
        {
            get { return _inBlue; }
            set
            {
                _inBlue = value;
                CalculateMap(_inBlue, _outBlue, _mapBlue);
            }
        }

        /// <summary>
        ///     Gray component's input range.
        /// </summary>
        public IntRange InGray
        {
            get { return _inGreen; }
            set
            {
                _inGreen = value;
                CalculateMap(_inGreen, _outGreen, _mapGreen);
            }
        }

        /// <summary>
        ///     Input range for RGB components.
        /// </summary>
        /// <remarks>
        ///     The property allows to set red, green and blue input ranges to the same value.
        /// </remarks>
        public IntRange Input
        {
            set
            {
                _inRed = _inGreen = _inBlue = value;
                CalculateMap(_inRed, _outRed, _mapRed);
                CalculateMap(_inGreen, _outGreen, _mapGreen);
                CalculateMap(_inBlue, _outBlue, _mapBlue);
            }
        }

        /// <summary>
        ///     Red component's output range.
        /// </summary>
        public IntRange OutRed
        {
            get { return _outRed; }
            set
            {
                _outRed = value;
                CalculateMap(_inRed, _outRed, _mapRed);
            }
        }

        /// <summary>
        ///     Green component's output range.
        /// </summary>
        public IntRange OutGreen
        {
            get { return _outGreen; }
            set
            {
                _outGreen = value;
                CalculateMap(_inGreen, _outGreen, _mapGreen);
            }
        }

        /// <summary>
        ///     Blue component's output range.
        /// </summary>
        public IntRange OutBlue
        {
            get { return _outBlue; }
            set
            {
                _outBlue = value;
                CalculateMap(_inBlue, _outBlue, _mapBlue);
            }
        }

        /// <summary>
        ///     Gray component's output range.
        /// </summary>
        public IntRange OutGray
        {
            get { return _outGreen; }
            set
            {
                _outGreen = value;
                CalculateMap(_inGreen, _outGreen, _mapGreen);
            }
        }

        /// <summary>
        ///     Output range for RGB components.
        /// </summary>
        /// <remarks>
        ///     The property allows to set red, green and blue output ranges to the same value.
        /// </remarks>
        public IntRange Output
        {
            set
            {
                _outRed = _outGreen = _outBlue = value;
                CalculateMap(_inRed, _outRed, _mapRed);
                CalculateMap(_inGreen, _outGreen, _mapGreen);
                CalculateMap(_inBlue, _outBlue, _mapBlue);
            }
        }

        /// <summary>
        ///     Process the filter on the specified image.
        /// </summary>
        /// <param name="image">Source image data.</param>
        /// <param name="rect">Image rectangle for processing by the filter.</param>
        protected override unsafe void ProcessFilter(UnmanagedImage image, Rectangle rect)
        {
            var num1 = Image.GetPixelFormatSize(image.PixelFormat)/8;
            var left = rect.Left;
            var top = rect.Top;
            var num2 = left + rect.Width;
            var num3 = top + rect.Height;
            var num4 = image.Stride - rect.Width*num1;
            var numPtr = (byte*) ((IntPtr) image.ImageData.ToPointer() + (top*image.Stride + left*num1));
            if (image.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                for (var index = top; index < num3; ++index)
                {
                    var num5 = left;
                    while (num5 < num2)
                    {
                        *numPtr = _mapGreen[*numPtr];
                        ++num5;
                        ++numPtr;
                    }
                    numPtr += num4;
                }
            }
            else
            {
                for (var index = top; index < num3; ++index)
                {
                    var num5 = left;
                    while (num5 < num2)
                    {
                        numPtr[2] = _mapRed[numPtr[2]];
                        numPtr[1] = _mapGreen[numPtr[1]];
                        *numPtr = _mapBlue[*numPtr];
                        ++num5;
                        numPtr += num1;
                    }
                    numPtr += num4;
                }
            }
        }

        /// <summary>
        ///     Calculate conversion map.
        /// </summary>
        /// <param name="inRange">Input range.</param>
        /// <param name="outRange">Output range.</param>
        /// <param name="map">Conversion map.</param>
        private void CalculateMap(IntRange inRange, IntRange outRange, byte[] map)
        {
            var num1 = 0.0;
            var num2 = 0.0;
            if (inRange.Max != inRange.Min)
            {
                num1 = (outRange.Max - outRange.Min)/(double) (inRange.Max - inRange.Min);
                num2 = outRange.Min - num1*inRange.Min;
            }
            for (var index = 0; index < 256; ++index)
            {
                var num3 = (byte) index;
                var num4 = (int) num3 < inRange.Max
                    ? ((int) num3 > inRange.Min ? (byte) (num1*num3 + num2) : (byte) outRange.Min)
                    : (byte) outRange.Max;
                map[index] = num4;
            }
        }
    }
}