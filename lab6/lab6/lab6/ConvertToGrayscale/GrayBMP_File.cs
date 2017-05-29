/* Create by: Muhammad Chishty Asheque
 * Date: Friday, April 02, 2010
 * Contact: twinkle_rip@hotmail.com 
 */

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace lab6.ConvertToGrayscale
{
    internal static class GrayBmpFile
    {
        private static readonly byte[] BmpFileHeader = new byte[14];
        private static readonly byte[] DibHeader = new byte[40];
        private static byte[] _colorPalette = new byte[1024]; //a palette containing 256 colors
        private static byte[] _bitmapData;

        //creates byte array of 256 color grayscale palette
        private static byte[] create_palette()
        {
            var colorPalette = new byte[1024];
            for (var i = 0; i < 256; i++)
            {
                colorPalette[i*4 + 0] = (byte) i; //bule
                colorPalette[i*4 + 1] = (byte) i; //green
                colorPalette[i*4 + 2] = (byte) i; //red
                colorPalette[i*4 + 3] = 0; //padding
            }
            return colorPalette;
        }

        //create different part of a bitmap file
        private static void create_parts(Image img)
        {
            //Create Bitmap Data
            _bitmapData = ConvertToGrayscale(img);
            //Create Bitmap File Header (populate BMP_File_Header array)
            Copy_to_Index(BmpFileHeader, new[] {(byte) 'B', (byte) 'M'}, 0); //magic number
            Copy_to_Index(BmpFileHeader, BitConverter.GetBytes(BmpFileHeader.Length
                                                               + DibHeader.Length + _colorPalette.Length +
                                                               _bitmapData.Length), 2); //file size
            Copy_to_Index(BmpFileHeader, new[] {(byte) 'M', (byte) 'C', (byte) 'A', (byte) 'T'}, 6);
            //reserved for application generating the bitmap file (not imprtant)
            Copy_to_Index(BmpFileHeader, BitConverter.GetBytes(BmpFileHeader.Length
                                                               + DibHeader.Length + _colorPalette.Length), 10);
            //bitmap raw data offset
            //Create DIB Header (populate DIB_header array)
            Copy_to_Index(DibHeader, BitConverter.GetBytes(DibHeader.Length), 0); //DIB header length
            Copy_to_Index(DibHeader, BitConverter.GetBytes(((Bitmap) img).Width), 4); //image width
            Copy_to_Index(DibHeader, BitConverter.GetBytes(((Bitmap) img).Height), 8); //image height
            Copy_to_Index(DibHeader, new[] {(byte) 1, (byte) 0}, 12); //color planes. N.B. Must be set to 1
            Copy_to_Index(DibHeader, new[] {(byte) 8, (byte) 0}, 14); //bits per pixel
            Copy_to_Index(DibHeader, BitConverter.GetBytes(0), 16); //compression method N.B. BI_RGB = 0
            Copy_to_Index(DibHeader, BitConverter.GetBytes(_bitmapData.Length), 20); //lenght of raw bitmap data
            Copy_to_Index(DibHeader, BitConverter.GetBytes(1000), 24); //horizontal reselution N.B. not important
            Copy_to_Index(DibHeader, BitConverter.GetBytes(1000), 28); //vertical reselution N.B. not important
            Copy_to_Index(DibHeader, BitConverter.GetBytes(256), 32); //number of colors in the palette
            Copy_to_Index(DibHeader, BitConverter.GetBytes(0), 36);
            //number of important colors used N.B. 0 = all colors are imprtant
            //Create Color palett
            _colorPalette = create_palette();
        }

        //convert the color pixels of source image into a grayscale bitmap (raw data)
        private static byte[] ConvertToGrayscale(Image source)
        {
            var sourceImage = (Bitmap) source;
            var padding = sourceImage.Width%4 != 0 ? 4 - sourceImage.Width%4 : 0; //determine padding needed for bitmap file
            var bytes = new byte[sourceImage.Width*sourceImage.Height + padding*sourceImage.Height];
            //create array to contain bitmap data with paddin
            for (var y = 0; y < sourceImage.Height; y++)
            {
                for (var x = 0; x < sourceImage.Width; x++)
                {
                    var c = sourceImage.GetPixel(x, y);
                    var g = Convert.ToInt32(0.3*c.R + 0.59*c.G + 0.11*c.B); //grayscale shade corresponding to rgb
                    bytes[(sourceImage.Height - 1 - y)*sourceImage.Width + (sourceImage.Height - 1 - y)*padding + x] = (byte) g;
                }
                //add the padding
                for (var i = 0; i < padding; i++)
                {
                    bytes[(sourceImage.Height - y)*sourceImage.Width + (sourceImage.Height - 1 - y)*padding + i] = 0;
                }
            }
            return bytes;
        }

        //creates a grayscale bitmap file of Image specified by Path
        public static bool CreateGrayBitmapFile(Image image, string path)
        {
            try
            {
                create_parts(image);
                //Write to file
                FileStream oFileStream;
                oFileStream = new FileStream(path, FileMode.OpenOrCreate);
                oFileStream.Write(BmpFileHeader, 0, BmpFileHeader.Length);
                oFileStream.Write(DibHeader, 0, DibHeader.Length);
                oFileStream.Write(_colorPalette, 0, _colorPalette.Length);
                oFileStream.Write(_bitmapData, 0, _bitmapData.Length);
                oFileStream.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //returns a byte array of a grey scale bitmap image
        public static byte[] CreateGrayBitmapArray(Image image)
        {
            try
            {
                create_parts(image);
                //Create the array
                var bitmapArray = new byte[BmpFileHeader.Length + DibHeader.Length
                                           + _colorPalette.Length + _bitmapData.Length];
                Copy_to_Index(bitmapArray, BmpFileHeader, 0);
                Copy_to_Index(bitmapArray, DibHeader, BmpFileHeader.Length);
                Copy_to_Index(bitmapArray, _colorPalette, BmpFileHeader.Length + DibHeader.Length);
                Copy_to_Index(bitmapArray, _bitmapData, BmpFileHeader.Length + DibHeader.Length + _colorPalette.Length);

                return bitmapArray;
            }
            catch
            {
                return new byte[1]; //return a null single byte array if fails
            }
        }

        //adds dtata of source array to Destinition array at the Index
        private static void Copy_to_Index(byte[] destination, byte[] source, int index)
        {
            try
            {
                for (var i = 0; i < source.Length; i++)
                {
                    destination[i + index] = source[i];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}