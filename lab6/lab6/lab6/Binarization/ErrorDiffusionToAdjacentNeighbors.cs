using AForge.Imaging.Filters;

namespace lab6.Binarization
{
    /// <summary>
    ///     Base class for error diffusion dithering, where error is diffused to
    ///     adjacent neighbor pixels.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The class does error diffusion to adjacent neighbor pixels
    ///         using specified set of coefficients. These coefficients are represented by
    ///         2 dimensional jugged array, where first array of coefficients is for
    ///         right-standing pixels, but the rest of arrays are for bottom-standing pixels.
    ///         All arrays except the first one should have odd number of coefficients.
    ///     </para>
    ///     <para>
    ///         Suppose that error diffusion coefficients are represented by the next
    ///         jugged array:
    ///     </para>
    ///     <code>
    /// int[][] coefficients = new int[2][] {
    ///                 new int[1] { 7 },
    ///                 new int[3] { 3, 5, 1 }
    ///             };
    /// 
    /// </code>
    ///     <para>
    ///         The above coefficients are used to diffuse error over the next neighbor
    ///         pixels (<b>*</b> marks current pixel, coefficients are placed to corresponding
    ///         neighbor pixels):
    ///     </para>
    ///     <code lang="none">
    /// | * | 7 |
    ///             | 3 | 5 | 1 |
    /// 
    ///             / 16
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
    ///             ErrorDiffusionToAdjacentNeighbors filter = new ErrorDiffusionToAdjacentNeighbors(
    ///                 new int[3][] {
    ///                     new int[2] { 5, 3 },
    ///                     new int[5] { 2, 4, 5, 4, 2 },
    ///                     new int[3] { 2, 3, 2 }
    ///                 } );
    ///             // apply the filter
    ///             filter.ApplyInPlace( image );
    /// 
    /// </code>
    /// </remarks>
    public class ErrorDiffusionToAdjacentNeighbors : ErrorDiffusionDithering
    {
        private int[][] _coefficients;
        private int _coefficientsSum;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:lab6.Binarization.ErrorDiffusionToAdjacentNeighbors" /> class.
        /// </summary>
        /// <param name="coefficients">Diffusion coefficients.</param>
        public ErrorDiffusionToAdjacentNeighbors(int[][] coefficients)
        {
            _coefficients = coefficients;
            CalculateCoefficientsSum();
        }

        /// <summary>
        ///     Diffusion coefficients.
        /// </summary>
        /// <remarks>
        ///     Set of coefficients, which are used for error diffusion to
        ///     pixel's neighbors.
        /// </remarks>
        public int[][] Coefficients
        {
            get { return _coefficients; }
            set
            {
                _coefficients = value;
                CalculateCoefficientsSum();
            }
        }

        /// <summary>
        ///     Do error diffusion.
        /// </summary>
        /// <param name="error">Current error value.</param>
        /// <param name="ptr">Pointer to current processing pixel.</param>
        /// <remarks>
        ///     All parameters of the image and current processing pixel's coordinates
        ///     are initialized by base class.
        /// </remarks>
        protected override unsafe void Diffuse(int error, byte* ptr)
        {
            var numArray1 = _coefficients[0];
            var index1 = 1;
            var index2 = 0;
            for (var length = numArray1.Length; index2 < length && x + index1 < stopX; ++index2)
            {
                var num1 = ptr[index1] + error*numArray1[index2]/_coefficientsSum;
                var num2 = num1 < 0 ? 0 : (num1 > (int) byte.MaxValue ? byte.MaxValue : num1);
                ptr[index1] = (byte) num2;
                ++index1;
            }
            var index3 = 1;
            for (var length1 = _coefficients.Length; index3 < length1 && y + index3 < stopY; ++index3)
            {
                ptr += stride;
                var numArray2 = _coefficients[index3];
                var index4 = 0;
                var length2 = numArray2.Length;
                for (var index5 = -(length2 >> 1); index4 < length2 && x + index5 < stopX; ++index4)
                {
                    if (x + index5 >= startX)
                    {
                        var num1 = ptr[index5] + error*numArray2[index4]/_coefficientsSum;
                        var num2 = num1 < 0 ? 0 : (num1 > (int) byte.MaxValue ? byte.MaxValue : num1);
                        ptr[index5] = (byte) num2;
                    }
                    ++index5;
                }
            }
        }

        private void CalculateCoefficientsSum()
        {
            _coefficientsSum = 0;
            var index1 = 0;
            for (var length1 = _coefficients.Length; index1 < length1; ++index1)
            {
                var numArray = _coefficients[index1];
                var index2 = 0;
                for (var length2 = numArray.Length; index2 < length2; ++index2)
                    _coefficientsSum += numArray[index2];
            }
        }
    }
}