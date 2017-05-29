using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging;
using AForge.Imaging.Filters;
using Image = System.Drawing.Image;

namespace lab6.ImageProcessing
{
    /// <summary>
    ///     Adaptive Smoothing - noise removal with edges preserving.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The filter is aimed to perform image smoothing, but keeping sharp edges.
    ///         This makes it applicable to additive noise removal and smoothing objects' interiors, but
    ///         not applicable for spikes (salt and pepper noise) removal.
    ///     </para>
    ///     <para>
    ///         The next calculations are done for each pixel:
    ///         <list type="bullet">
    ///             <item>
    ///                 weights are calculate for 9 pixels - pixel itself and 8 neighbors:
    ///                 <code lang="none">
    /// w(x, y) = exp( -1 * (Gx^2 + Gy^2) / (2 * factor^2) )
    ///             Gx(x, y) = (I(x + 1, y) - I(x - 1, y)) / 2
    ///             Gy(x, y) = (I(x, y + 1) - I(x, y - 1)) / 2
    /// 
    /// </code>
    ///                 ,
    ///                 where <see cref="P:lab6.ImageProcessing.AdaptiveSmoothing.Factor">factor</see> is a configurable value determining
    ///                 smoothing's quality.
    ///             </item>
    ///             <item>
    ///                 sum of 9 weights is calclated (weightTotal);
    ///             </item>
    ///             <item>
    ///                 sum of 9 weighted pixel values is calculatd (total);
    ///             </item>
    ///             <item>
    ///                 destination pixel is calculated as <b>total / weightTotal</b>.
    ///             </item>
    ///         </list>
    ///     </para>
    ///     <para>
    ///         Description of the filter was found in
    ///         <b>
    ///             "An Edge Detection Technique Using
    ///             the Facet Model and Parameterized Relaxation Labeling" by Ioannis Matalas, Student Member,
    ///             IEEE, Ralph Benjamin, and Richard Kitney
    ///         </b>
    ///         .
    ///     </para>
    ///     <para>
    ///         The filter accepts 8 bpp grayscale images and 24 bpp
    ///         color images for processing.
    ///     </para>
    ///     <para>
    ///         Sample usage:
    ///     </para>
    ///     <code>
    /// // create filter
    ///             AdaptiveSmoothing filter = new AdaptiveSmoothing( );
    ///             // apply the filter
    ///             filter.ApplyInPlace( image );
    /// 
    /// </code>
    ///     <para>
    ///         <b>Initial image:</b>
    ///     </para>
    ///     <img src="img/imaging/sample13.png" width="480" height="361" />
    ///     <para>
    ///         <b>Result image:</b>
    ///     </para>
    ///     <img src="img/imaging/adaptive_smooth.png" width="480" height="361" />
    /// </remarks>
    public class AdaptiveSmoothing : BaseUsingCopyPartialFilter
    {
        private readonly Dictionary<PixelFormat, PixelFormat> _formatTranslations =
            new Dictionary<PixelFormat, PixelFormat>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:lab6.ImageProcessing.AdaptiveSmoothing" /> class.
        /// </summary>
        public AdaptiveSmoothing()
        {
            _formatTranslations[PixelFormat.Format8bppIndexed] = PixelFormat.Format8bppIndexed;
            _formatTranslations[PixelFormat.Format24bppRgb] = PixelFormat.Format24bppRgb;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:lab6.ImageProcessing.AdaptiveSmoothing" /> class.
        /// </summary>
        /// <param name="factor">Factor value.</param>
        public AdaptiveSmoothing(double factor)
            : this()
        {
            Factor = factor;
        }

        /// <summary>
        ///     Format translations dictionary.
        /// </summary>
        public override Dictionary<PixelFormat, PixelFormat> FormatTranslations
        {
            get { return _formatTranslations; }
        }

        /// <summary>
        ///     Factor value.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Factor determining smoothing quality (see <see cref="T:lab6.ImageProcessing.AdaptiveSmoothing" />
        ///         documentation).
        ///     </para>
        ///     <para>
        ///         Default value is set to <b>3</b>.
        ///     </para>
        /// </remarks>
        public double Factor { get; set; } = 3.0;

        /// <summary>
        ///     Process the filter on the specified image.
        /// </summary>
        /// <param name="source">Source image data.</param>
        /// <param name="destination">Destination image data.</param>
        /// <param name="rect">Image rectangle for processing by the filter.</param>
        protected override unsafe void ProcessFilter(UnmanagedImage source, UnmanagedImage destination, Rectangle rect)
        {
            var index1 = Image.GetPixelFormatSize(source.PixelFormat)/8;
            var index2 = index1*2;
            var left = rect.Left;
            var top = rect.Top;
            var num1 = left + rect.Width;
            var num2 = top + rect.Height;
            var num3 = left + 2;
            var num4 = top + 2;
            var num5 = num1 - 2;
            var num6 = num2 - 2;
            var stride1 = source.Stride;
            var stride2 = destination.Stride;
            var num7 = stride1 - rect.Width*index1;
            var num8 = stride2 - rect.Width*index1;
            var num9 = -8.0*Factor*Factor;
            var numPtr1 = (byte*) ((IntPtr) source.ImageData.ToPointer() + stride1*2);
            var numPtr2 = (byte*) ((IntPtr) destination.ImageData.ToPointer() + stride2*2);
            var numPtr3 = numPtr1 + (top*stride1 + left*index1);
            var numPtr4 = numPtr2 + (top*stride2 + left*index1);
            for (var index3 = num4; index3 < num6; ++index3)
            {
                var numPtr5 = numPtr3 + index2;
                var numPtr6 = numPtr4 + index2;
                for (var index4 = num3; index4 < num5; ++index4)
                {
                    var num10 = 0;
                    while (num10 < index1)
                    {
                        var num11 = 0.0;
                        var num12 = 0.0;
                        double num13 = numPtr5[-stride1] - numPtr5[-index2 - stride1];
                        double num14 = numPtr5[-index1] - numPtr5[-index1 - 2*stride1];
                        var num15 = Math.Exp((num13*num13 + num14*num14)/num9);
                        var num16 = num12 + num15*numPtr5[-index1 - stride1];
                        var num17 = num11 + num15;
                        double num18 = numPtr5[index1 - stride1] - numPtr5[-index1 - stride1];
                        double num19 = *numPtr5 - numPtr5[-2*stride1];
                        var num20 = Math.Exp((num18*num18 + num19*num19)/num9);
                        var num21 = num16 + num20*numPtr5[-stride1];
                        var num22 = num17 + num20;
                        double num23 = numPtr5[index2 - stride1] - numPtr5[-stride1];
                        double num24 = numPtr5[index1] - numPtr5[index1 - 2*stride1];
                        var num25 = Math.Exp((num23*num23 + num24*num24)/num9);
                        var num26 = num21 + num25*numPtr5[index1 - stride1];
                        var num27 = num22 + num25;
                        double num28 = *numPtr5 - numPtr5[-index2];
                        double num29 = numPtr5[-index1 + stride1] - numPtr5[-index1 - stride1];
                        var num30 = Math.Exp((num28*num28 + num29*num29)/num9);
                        var num31 = num26 + num30*numPtr5[-index1];
                        var num32 = num27 + num30;
                        double num33 = numPtr5[index1] - numPtr5[-index1];
                        double num34 = numPtr5[stride1] - numPtr5[-stride1];
                        var num35 = Math.Exp((num33*num33 + num34*num34)/num9);
                        var num36 = num31 + num35**numPtr5;
                        var num37 = num32 + num35;
                        double num38 = numPtr5[index2] - *numPtr5;
                        double num39 = numPtr5[index1 + stride1] - numPtr5[index1 - stride1];
                        var num40 = Math.Exp((num38*num38 + num39*num39)/num9);
                        var num41 = num36 + num40*numPtr5[index1];
                        var num42 = num37 + num40;
                        double num43 = numPtr5[stride1] - numPtr5[-index2 + stride1];
                        double num44 = numPtr5[-index1 + 2*stride1] - numPtr5[-index1];
                        var num45 = Math.Exp((num43*num43 + num44*num44)/num9);
                        var num46 = num41 + num45*numPtr5[-index1 + stride1];
                        var num47 = num42 + num45;
                        double num48 = numPtr5[index1 + stride1] - numPtr5[-index1 + stride1];
                        double num49 = numPtr5[2*stride1] - *numPtr5;
                        var num50 = Math.Exp((num48*num48 + num49*num49)/num9);
                        var num51 = num46 + num50*numPtr5[stride1];
                        var num52 = num47 + num50;
                        double num53 = numPtr5[index2 + stride1] - numPtr5[stride1];
                        double num54 = numPtr5[index1 + 2*stride1] - numPtr5[index1];
                        var num55 = Math.Exp((num53*num53 + num54*num54)/num9);
                        var num56 = num51 + num55*numPtr5[index1 + stride1];
                        var num57 = num52 + num55;
                        *numPtr6 = num57 == 0.0 ? *numPtr5 : (byte) Math.Min(num56/num57, byte.MaxValue);
                        ++num10;
                        ++numPtr5;
                        ++numPtr6;
                    }
                }
                numPtr3 = numPtr5 + (num7 + index2);
                numPtr4 = numPtr6 + (num8 + index2);
            }
        }
    }
}