namespace lab6.Binarization
{
    /// <summary>
    ///     Dithering using Sierra error diffusion.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The filter represents binarization filter, which is based on
    ///         error diffusion dithering with Sierra coefficients. Error is diffused
    ///         on 10 neighbor pixels with next coefficients:
    ///     </para>
    ///     <code lang="none">
    /// | * | 5 | 3 |
    ///             | 2 | 4 | 5 | 4 | 2 |
    ///                 | 2 | 3 | 2 |
    /// 
    ///             / 32
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
    ///             SierraDithering filter = new SierraDithering( );
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
    ///     <img src="img/imaging/sierra.jpg" width="480" height="361" />
    /// </remarks>
    /// <seealso cref="T:AForge.Imaging.Filters.BurkesDithering" />
    /// <seealso cref="T:AForge.Imaging.Filters.FloydSteinbergDithering" />
    /// <seealso cref="T:AForge.Imaging.Filters.JarvisJudiceNinkeDithering" />
    /// <seealso cref="T:AForge.Imaging.Filters.StuckiDithering" />
    public sealed class SierraDithering : ErrorDiffusionToAdjacentNeighbors
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:lab6.Binarization.SierraDithering" /> class.
        /// </summary>
        public SierraDithering()
            : base(new[]
            {
                new[]
                {
                    5,
                    3
                },
                new[]
                {
                    2,
                    4,
                    5,
                    4,
                    2
                },
                new[]
                {
                    2,
                    3,
                    2
                }
            })
        {
        }
    }
}