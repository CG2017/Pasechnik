using System;

namespace ColorReplacer
{
    /// <summary>
    ///     Implements the DE2000 method of delta-e: http://en.wikipedia.org/wiki/Color_difference#CIEDE2000
    ///     Correct implementation provided courtesy of Jonathan Hofinger, jaytar42
    /// </summary>
    public class CieDe2000Comparison : IColorSpaceComparison
    {
        /// <summary>
        ///     Calculates the DE2000 delta-e value: http://en.wikipedia.org/wiki/Color_difference#CIEDE2000
        ///     Correct implementation provided courtesy of Jonathan Hofinger, jaytar42
        /// </summary>
        public double Compare(IColorSpace c1, IColorSpace c2)
        {
            //Set weighting factors to 1
            var kL = 1.0d;
            var kC = 1.0d;
            var kH = 1.0d;


            //Change Color Space to L*a*b:
            var lab1 = c1.To<Lab>();
            var lab2 = c2.To<Lab>();

            //Calculate Cprime1, Cprime2, Cabbar
            var cStar1Ab = Math.Sqrt(lab1.A*lab1.A + lab1.B*lab1.B);
            var cStar2Ab = Math.Sqrt(lab2.A*lab2.A + lab2.B*lab2.B);
            var cStarAverageAb = (cStar1Ab + cStar2Ab)/2;

            var cStarAverageAbPot7 = cStarAverageAb*cStarAverageAb*cStarAverageAb;
            cStarAverageAbPot7 *= cStarAverageAbPot7*cStarAverageAb;

            var g = 0.5d*(1 - Math.Sqrt(cStarAverageAbPot7/(cStarAverageAbPot7 + 6103515625))); //25^7
            var a1Prime = (1 + g)*lab1.A;
            var a2Prime = (1 + g)*lab2.A;

            var cPrime1 = Math.Sqrt(a1Prime*a1Prime + lab1.B*lab1.B);
            var cPrime2 = Math.Sqrt(a2Prime*a2Prime + lab2.B*lab2.B);
            //Angles in Degree.
            var hPrime1 = (Math.Atan2(lab1.B, a1Prime)*180d/Math.PI + 360)%360d;
            var hPrime2 = (Math.Atan2(lab2.B, a2Prime)*180d/Math.PI + 360)%360d;

            var deltaLPrime = lab2.L - lab1.L;
            var deltaCPrime = cPrime2 - cPrime1;

            var hBar = Math.Abs(hPrime1 - hPrime2);
            double deltaHPrime;
            if (cPrime1*cPrime2 == 0) deltaHPrime = 0;
            else
            {
                if (hBar <= 180d)
                {
                    deltaHPrime = hPrime2 - hPrime1;
                }
                else if (hBar > 180d && hPrime2 <= hPrime1)
                {
                    deltaHPrime = hPrime2 - hPrime1 + 360.0;
                }
                else
                {
                    deltaHPrime = hPrime2 - hPrime1 - 360.0;
                }
            }
            var delta_H_prime = 2*Math.Sqrt(cPrime1*cPrime2)*Math.Sin(deltaHPrime*Math.PI/360d);

            // Calculate CIEDE2000
            var lPrimeAverage = (lab1.L + lab2.L)/2d;
            var cPrimeAverage = (cPrime1 + cPrime2)/2d;

            //Calculate h_prime_average

            double hPrimeAverage;
            if (cPrime1*cPrime2 == 0) hPrimeAverage = 0;
            else
            {
                if (hBar <= 180d)
                {
                    hPrimeAverage = (hPrime1 + hPrime2)/2;
                }
                else if (hBar > 180d && hPrime1 + hPrime2 < 360d)
                {
                    hPrimeAverage = (hPrime1 + hPrime2 + 360d)/2;
                }
                else
                {
                    hPrimeAverage = (hPrime1 + hPrime2 - 360d)/2;
                }
            }
            var lPrimeAverageMinus50Square = lPrimeAverage - 50;
            lPrimeAverageMinus50Square *= lPrimeAverageMinus50Square;

            var sL = 1 + .015d*lPrimeAverageMinus50Square/Math.Sqrt(20 + lPrimeAverageMinus50Square);
            var sC = 1 + .045d*cPrimeAverage;
            var T = 1
                    - .17*Math.Cos(DegToRad(hPrimeAverage - 30))
                    + .24*Math.Cos(DegToRad(hPrimeAverage*2))
                    + .32*Math.Cos(DegToRad(hPrimeAverage*3 + 6))
                    - .2*Math.Cos(DegToRad(hPrimeAverage*4 - 63));
            var sH = 1 + .015*T*cPrimeAverage;
            var hPrimeAverageMinus275Div25Square = (hPrimeAverage - 275)/25;
            hPrimeAverageMinus275Div25Square *= hPrimeAverageMinus275Div25Square;
            var deltaTheta = 30*Math.Exp(-hPrimeAverageMinus275Div25Square);

            var cPrimeAveragePot7 = cPrimeAverage*cPrimeAverage*cPrimeAverage;
            cPrimeAveragePot7 *= cPrimeAveragePot7*cPrimeAverage;
            var rC = 2*Math.Sqrt(cPrimeAveragePot7/(cPrimeAveragePot7 + 6103515625));

            var rT = -Math.Sin(DegToRad(2*deltaTheta))*rC;

            var deltaLPrimeDivKLSL = deltaLPrime/(sL*kL);
            var deltaCPrimeDivKCSC = deltaCPrime/(sC*kC);
            var deltaHPrimeDivKHSH = delta_H_prime/(sH*kH);

            var ciede2000 = Math.Sqrt(
                deltaLPrimeDivKLSL*deltaLPrimeDivKLSL
                + deltaCPrimeDivKCSC*deltaCPrimeDivKCSC
                + deltaHPrimeDivKHSH*deltaHPrimeDivKHSH
                + rT*deltaCPrimeDivKCSC*deltaHPrimeDivKHSH
                );

            return ciede2000;
        }

        private double DegToRad(double degrees)
        {
            return degrees*Math.PI/180;
        }
    }
}