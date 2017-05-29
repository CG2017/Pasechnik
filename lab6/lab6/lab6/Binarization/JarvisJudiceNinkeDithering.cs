namespace lab6.Binarization
{
    /// <summary>
    ///     Dithering using Jarvis, Judice and Ninke error diffusion.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The filter represents binarization filter, which is based on
    ///         error diffusion dithering with Jarvis-Judice-Ninke coefficients. Error is diffused
    ///         on 12 neighbor pixels with next coefficients:
    ///     </para>
    ///     <code lang="none">
    /// | * | 7 | 5 |
    ///              | 3 | 5 | 7 | 5 | 3 |
    ///              | 1 | 3 | 5 | 3 | 1 |
    /// 
    ///              / 48
    /// 
    /// </code>
    ///     <para>
    ///         The filter accepts 8 bpp grayscale images for processing.
    ///     </para>
    ///     <para>
    ///         Sample usage:
    ///     </para>
    ///     <code>
    /// // create filter
    ///              JarvisJudiceNinkeDithering filter = new JarvisJudiceNinkeDithering( );
    ///              // apply the filter
    ///              filter.ApplyInPlace( image );
    /// 
    /// </code>
    ///     <para>
    ///         <b>Initial image:</b>
    ///     </para>
    ///     <img src="img/imaging/grayscale.jpg" width="480" height="361" />
    ///     <para>
    ///         <b>Result image:</b>
    ///     </para>
    ///     <img src="img/imaging/jarvis_judice_ninke.jpg" width="480" height="361" />
    /// </remarks>
    /// <seealso cref="T:AForge.Imaging.Filters.BurkesDithering" />
    /// <seealso cref="T:AForge.Imaging.Filters.FloydSteinbergDithering" />
    /// <seealso cref="T:lab6.Binarization.SierraDithering" />
    /// <seealso cref="T:AForge.Imaging.Filters.StuckiDithering" />
    public sealed class JarvisJudiceNinkeDithering : ErrorDiffusionToAdjacentNeighbors
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:lab6.Binarization.JarvisJudiceNinkeDithering" /> class.
        /// </summary>
        public JarvisJudiceNinkeDithering()
            : base(new[]
            {
                new[]
                {
                    7,
                    5
                },
                new[]
                {
                    3,
                    5,
                    7,
                    5,
                    3
                },
                new[]
                {
                    1,
                    3,
                    5,
                    3,
                    1
                }
            })
        {
        }
    }
}