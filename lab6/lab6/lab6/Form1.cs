using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using AForge;
using AForge.Imaging.Filters;
using ImageMagick;
using lab6.ConvertToGrayscale;
using lab6.Properties;
using AdaptiveSmoothing = lab6.ImageProcessing.AdaptiveSmoothing;
using BayerDithering = lab6.Binarization.BayerDithering;
using BradleyLocalThresholding = lab6.Binarization.BradleyLocalThresholding;
using ContrastCorrection = lab6.ImageProcessing.ContrastCorrection;
using GammaCorrection = lab6.ImageProcessing.GammaCorrection;
using ImageConverter = lab6.ConvertToGrayscale.ImageConverter;
using Invert = lab6.ImageProcessing.Invert;
using IterativeThreshold = lab6.Binarization.IterativeThreshold;
using JarvisJudiceNinkeDithering = lab6.Binarization.JarvisJudiceNinkeDithering;
using LevelsLinear = lab6.ImageProcessing.LevelsLinear;
using OtsuThreshold = lab6.Binarization.OtsuThreshold;
using SierraDithering = lab6.Binarization.SierraDithering;
using Threshold = lab6.Binarization.Threshold;

namespace lab6
{
    public partial class Lab6 : Form
    {
        private Image _image;

        public Lab6()
        {
            InitializeComponent();
        }


        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            LoadImage();
        }

        private void LoadImage()
        {
            var file = new OpenFileDialog();
            file.Filter = Resources.Filter_Formats;
            file.FilterIndex = 6;
            if (file.ShowDialog() == DialogResult.OK)
            {
                var path = Path.GetExtension(file.FileName);
                if (path != null)
                {
                    var extension = path.ToUpper();

                    if (extension == ".PCX")
                    {
                        using (var magickImage = new MagickImage(file.FileName))
                        {
                            magickImage.Format = MagickFormat.Gray;
                            _image = magickImage.ToBitmap();
                            var gray = GrayBmpFile.CreateGrayBitmapArray(_image);
                            _image = ImageConverter.ByteArrayToImage(gray);
                        }
                    }
                    else
                    {
                        _image = Image.FromFile(file.FileName);
                        var gray = GrayBmpFile.CreateGrayBitmapArray(_image);
                        _image = ImageConverter.ByteArrayToImage(gray);
                    }

                    pictureBox1.Image = _image;
                }
            }
        }

        private void invertCB_CheckedChanged(object sender, EventArgs e)
        {
            InvertImage();
        }

        private void InvertImage()
        {
            if (invertCB.Checked)
                if (_image != null)
                {
                    var fImage = (Bitmap)pictureBox1.Image;
                    var filter = new Invert();
                    var result = filter.Apply(fImage);
                    pictureBox2.Image = new Bitmap(result);
                }
        }
       

        private void contrastCorrectionCB_CheckedChanged(object sender, EventArgs e)
        {
            ContrastCorrection();
        }

        private void ContrastCorrection()
        {
            if (contrastCorrectionCB.Checked)
                if (_image != null)
                {
                    var fImage = (Bitmap) pictureBox1.Image;
                    var filter = new ContrastCorrection(contrastTBar.Value);
                    var result = filter.Apply(fImage);
                    pictureBox2.Image = result;
                }
        }

        private void adaptiveSmoothingCB_CheckedChanged(object sender, EventArgs e)
        {
            Smoothing();
        }

        private void Smoothing()
        {
            if (adaptiveSmoothingCB.Checked)
                if (_image != null)
                {
                    var fImage = (Bitmap) pictureBox1.Image;
                    var filter = new AdaptiveSmoothing(smoothTBar.Value);
                    var result = filter.Apply(fImage);
                    pictureBox2.Image = new Bitmap(result);
                }
        }

        private void gammaCorrectionCB_CheckedChanged(object sender, EventArgs e)
        {
            GammaCorrection();
        }

        private void GammaCorrection()
        {
            if (gammaCorrectionCB.Checked)
                if (_image != null)
                {
                    var fImage = (Bitmap) pictureBox1.Image;
                    var filter = new GammaCorrection(gammaTBar.Value*0.01);

                    var resilt = filter.Apply(fImage);
                    pictureBox2.Image = new Bitmap(resilt);
                }
        }


        private void binarizationTresholdCB_CheckedChanged(object sender, EventArgs e)
        {
            BinarizationThreshold();
        }

        private void BinarizationThreshold()
        {
            if (binarizationDownTresholdCB.Checked || binarizationUpThresholdCB.Checked)
            {
                var image = pictureBox1.Image;
                var filter = new Threshold((binarizationUpThresholdCB.Checked)?(thresholdTB.Maximum-thresholdTB.Value):thresholdTB.Value);
                // apply the filter
                var result = filter.Apply((Bitmap) image);
                    pictureBox2.Image = result;
            }
        }

        private void binarizationJarvisCB_CheckedChanged(object sender, EventArgs e)
        {
            BinarizationJarvis();
        }

        private void BinarizationJarvis()
        {
            if (binarizationJarvisCB.Checked)
            {
                var image = pictureBox1.Image;
                var filter = new JarvisJudiceNinkeDithering();
                var result = filter.Apply((Bitmap) image);
                pictureBox2.Image = result;
            }
        }

        private void LinearCorrection()
        {
            if (linearCB.Checked)
            {
                var image = pictureBox1.Image;
                var filter = new LevelsLinear();
                // set ranges

                filter.InGray = new IntRange(linearMinTBar.Value, linearMaxTBar.Value);
                // apply the filter
                var result = filter.Apply((Bitmap) image);
                pictureBox2.Image = result;
            }
        }
        private void IterativeBinarization()
        {
            if (iterativeCB.Checked)
            {
                var image = pictureBox1.Image;
                // create filter
                var filter = new IterativeThreshold(linearMinTBar.Value, linearMaxTBar.Value);

                // apply the filter
                var result = filter.Apply((Bitmap)image);
                pictureBox2.Image = result;
            }
        }
        private
            void thresholdTB_Scroll(object sender, EventArgs e)
        {
            thresholdTextBox.Text = thresholdTB.Value.ToString();
            if (binarizationDownTresholdCB.Checked || binarizationUpThresholdCB.Checked)
                BinarizationThreshold();
        }

        private void binarizationBayerCB_CheckedChanged(object sender, EventArgs e)
        {
            if (binarizationBayerCB.Checked)
            {
                var image = pictureBox1.Image;
                var filter = new BayerDithering();

                var result = filter.Apply((Bitmap) image);
                pictureBox2.Image = result;
            }
        }

        private void binarizationSierraCB_CheckedChanged(object sender, EventArgs e)
        {
            if (binarizationSierraCB.Checked)
            {
                var image = pictureBox1.Image;
                var filter = new SierraDithering();

                var result = filter.Apply((Bitmap) image);
                pictureBox2.Image = result;
            }
        }

        private void otsuCB_CheckedChanged(object sender, EventArgs e)
        {
            if (otsuCB.Checked)
            {
                var image = pictureBox1.Image;
                var filter = new OtsuThreshold();

                var result = filter.Apply((Bitmap) image);
                pictureBox2.Image = result;
            }
        }

        private void contrastTBar_Scroll(object sender, EventArgs e)
        {
            contrastTBox.Text = contrastTBar.Value.ToString();
            ContrastCorrection();
        }

        private void smoothTBar_Scroll(object sender, EventArgs e)
        {
            smoothTBox.Text = smoothTBar.Value.ToString();
            Smoothing();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            gammaTBox.Text = (gammaTBar.Value*0.01).ToString(CultureInfo.CurrentCulture);
            GammaCorrection();
        }

        private void linearMinTBar_Scroll(object sender, EventArgs e)
        {
            linearMinTBox.Text = linearMinTBar.Value.ToString();
            linearMaxTBox.Text = linearMaxTBar.Value.ToString();
            LinearCorrection();
            IterativeBinarization();
        }




        private void linearMaxTBar_Scroll(object sender, EventArgs e)
        {
            linearMinTBox.Text = linearMinTBar.Value.ToString();
            linearMaxTBox.Text = linearMaxTBar.Value.ToString();
            LinearCorrection();
            IterativeBinarization();
        }

        private void binarizationDownCB_CheckedChanged(object sender, EventArgs e)
        {
            BinarizationThreshold();
        }

        private void iterativeCB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void bradlyCB_CheckedChanged(object sender, EventArgs e)
        {
            BradlyBinarization();
        }

        private void BradlyBinarization()
        {
            if (bradlyCB.Checked)
            {
                var image = pictureBox1.Image;
                var filter = new BradleyLocalThresholding();

                var result = filter.Apply((Bitmap)image);
                pictureBox2.Image = result;
            }
        }
    }
}