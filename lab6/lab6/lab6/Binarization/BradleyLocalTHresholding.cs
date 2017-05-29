

using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace lab6.Binarization
{
    /// <summary>
    /// Adaptive thresholding using the internal image.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// <para>
    /// The image processing routine implements local thresholding technique described
    ///             by Derek Bradley and Gerhard Roth in the "Adaptive Thresholding Using the Integral Image" paper.
    /// 
    /// </para>
    /// 
    /// <para>
    /// The brief idea of the algorithm is that every image's pixel is set to black if its brightness
    ///             is <i>t</i> percent lower (see <see cref="P:lab6.Binarization.BradleyLocalThresholding.PixelBrightnessDifferenceLimit"/>) than the average brightness
    ///             of surrounding pixels in the window of the specified size (see <see cref="P:lab6.Binarization.BradleyLocalThresholding.WindowSize"/>), othwerwise it is set
    ///             to white.
    /// </para>
    /// 
    /// <para>
    /// Sample usage:
    /// </para>
    /// 
    /// <code>
    /// // create the filter
    ///             BradleyLocalThresholding filter = new BradleyLocalThresholding( );
    ///             // apply the filter
    ///             filter.ApplyInPlace( image );
    /// 
    /// </code>
    /// 
    /// <para>
    /// <b>Initial image:</b>
    /// </para>
    /// <img src="img/imaging/sample20.png" width="320" height="240"/>
    /// <para>
    /// <b>Result image:</b>
    /// </para>
    /// <img src="img/imaging/bradley_local_thresholding.png" width="320" height="240"/>
    /// </remarks>
    public class BradleyLocalThresholding : BaseInPlaceFilter
    {
        private Dictionary<PixelFormat, PixelFormat> _formatTranslations = new Dictionary<PixelFormat, PixelFormat>();
        private int _windowSize = 41;
        private float _pixelBrightnessDifferenceLimit = 0.15f;

        /// <summary>
        /// Window size to calculate average value of pixels for.
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// 
        /// <para>
        /// The property specifies window size around processing pixel, which determines number of
        ///             neighbor pixels to use for calculating their average brightness.
        /// </para>
        /// 
        /// <para>
        /// Default value is set to <b>41</b>.
        /// </para>
        /// 
        /// <para>
        /// <note>The value should be odd.</note>
        /// </para>
        /// 
        /// </remarks>
        public int WindowSize
        {
            get
            {
                return _windowSize;
            }
            set
            {
                _windowSize = Math.Max(3, value | 1);
            }
        }

        /// <summary>
        /// Brightness difference limit between processing pixel and average value across neighbors.
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// 
        /// <para>
        /// The property specifies what is the allowed difference percent between processing pixel
        ///              and average brightness of neighbor pixels in order to be set white. If the value of the
        ///              current pixel is <i>t</i> percent (this property value) lower than the average then it is set
        ///              to black, otherwise it is set to white.
        /// </para>
        /// 
        /// <para>
        /// Default value is set to <b>0.15</b>.
        /// </para>
        /// 
        /// </remarks>
        public float PixelBrightnessDifferenceLimit
        {
            get
            {
                return _pixelBrightnessDifferenceLimit;
            }
            set
            {
                _pixelBrightnessDifferenceLimit = Math.Max(0.0f, Math.Min(1f, value));
            }
        }

        /// <summary>
        /// Format translations dictionary.
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// 
        /// <para>
        /// See <see cref="P:AForge.Imaging.Filters.IFilterInformation.FormatTranslations"/> for more information.
        /// </para>
        /// 
        /// </remarks>
        public override Dictionary<PixelFormat, PixelFormat> FormatTranslations
        {
            get
            {
                return _formatTranslations;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:lab6.Binarization.BradleyLocalThresholding"/> class.
        /// 
        /// </summary>
        public BradleyLocalThresholding()
        {
            _formatTranslations[PixelFormat.Format8bppIndexed] = PixelFormat.Format8bppIndexed;
        }

        /// <summary>
        /// Process the filter on the specified image.
        /// 
        /// </summary>
        /// <param name="image">Source image data.</param>
        protected override unsafe void ProcessFilter(UnmanagedImage image)
        {
            IntegralImage integralImage = IntegralImage.FromBitmap(image);
            int width = image.Width;
            int height = image.Height;
            int num1 = width - 1;
            int num2 = height - 1;
            int num3 = image.Stride - width;
            int num4 = _windowSize / 2;
            float num5 = 1f - _pixelBrightnessDifferenceLimit;
            byte* numPtr = (byte*)image.ImageData.ToPointer();
            for (int index = 0; index < height; ++index)
            {
                int y1 = index - num4;
                int y2 = index + num4;
                if (y1 < 0)
                    y1 = 0;
                if (y2 > num2)
                    y2 = num2;
                int num6 = 0;
                while (num6 < width)
                {
                    int x1 = num6 - num4;
                    int x2 = num6 + num4;
                    if (x1 < 0)
                        x1 = 0;
                    if (x2 > num1)
                        x2 = num1;
                    *numPtr = (int)*numPtr < (int)((double)integralImage.GetRectangleMeanUnsafe(x1, y1, x2, y2) * (double)num5) ? (byte)0 : byte.MaxValue;
                    ++num6;
                    ++numPtr;
                }
                numPtr += num3;
            }
        }
    }
}
