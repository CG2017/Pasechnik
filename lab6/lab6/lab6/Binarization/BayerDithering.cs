using AForge.Imaging.Filters;

namespace lab6.Binarization
{
    /// <summary>
    ///     Ordered dithering using Bayer matrix.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The filter represents <see cref="T:AForge.Imaging.Filters.OrderedDithering" /> filter initialized
    ///         with the next threshold matrix:
    ///     </para>
    ///     <code>
    /// byte[,] matrix = new byte[4, 4]
    ///             {
    ///             	{   0, 192,  48, 240 },
    ///             	{ 128,  64, 176, 112 },
    ///             	{  32, 224,  16, 208 },
    ///             	{ 160,  96, 144,  80 }
    ///             };
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
    ///             BayerDithering filter = new BayerDithering( );
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
    ///     <img src="img/imaging/bayer_dithering.jpg" width="480" height="361" />
    /// </remarks>
    public sealed class BayerDithering : OrderedDithering
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:lab6.Binarization.BayerDithering" /> class.
        /// </summary>
        public BayerDithering()
            : base(new[,]
            {
                {
                    (byte) 0,
                    (byte) 192,
                    (byte) 48,
                    (byte) 240
                },
                {
                    (byte) 128,
                    (byte) 64,
                    (byte) 176,
                    (byte) 112
                },
                {
                    (byte) 32,
                    (byte) 224,
                    (byte) 16,
                    (byte) 208
                },
                {
                    (byte) 160,
                    (byte) 96,
                    (byte) 144,
                    (byte) 80
                }
            })
        {
        }
    }
}