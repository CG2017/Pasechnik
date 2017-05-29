/*
 * ColorReplacer dimababin
*/

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using ColorReplacer.Properties;


namespace ColorReplacer
{
    public partial class MainForm : Form
    {
        private readonly ColorSubstitutionFilter _filterData = new ColorSubstitutionFilter();
        private Color _colAfter;
        private Color _colBefore;
        private Lab _labAfter;
        private Lab _labBefore;
        private Lab _labPixel;
        private Lab _labRange = new Lab();
        private Color _pixel;

        private Rgb _rgbPixel;

        private int _treshold;

        public MainForm()
        {
            InitializeComponent();

        }

        private void ApplyFilter()
        {
            if (picSource.Image != null)
            {
                if (inLabcb.Checked)
                {
                
                   
                    picResult.Image = ApplyFilterLab(picSource.Image);
                }
                else
                {
                    _filterData.SourceColor = pnlSourceColor.BackColor;
                    _filterData.ThresholdValue = (byte)(255.0f / 100.0f * trcThreshHold.Value);
                    _filterData.NewColor = pnlResultColor.BackColor;
                    picResult.Image = ((Bitmap)picSource.Image).ColorSubstitution(_filterData);
                }
                pnlFilter.Enabled = true;
                btnSave.Enabled = true;
                btnResultAsSource.Enabled = true;
            }
        }

      
        private Image ApplyFilterLab(Image image)
        {
            
                _colBefore = pnlSourceColor.BackColor;
                _colAfter = pnlResultColor.BackColor;
                _treshold = trcThreshHold.Value;
                _labBefore = new Rgb { R = _colBefore.R, G = _colBefore.G, B = _colBefore.B }.To<Lab>();
                _labAfter = new Rgb { R = _colAfter.R, G = _colAfter.G, B = _colAfter.B }.To<Lab>();


                var img = new Bitmap(image);
                for (var i = 0; i < img.Width; i++)
                {
                    for (var j = 0; j < img.Height; j++)
                    {
                        _pixel = img.GetPixel(i, j);
                        _labPixel = (_rgbPixel = new Rgb { R = _pixel.R, G = _pixel.G, B = _pixel.B }).To<Lab>();

                        if (Max_range(_labPixel, _labBefore, _treshold, ref _labRange))
                        {
                            _labPixel.L = _labAfter.L + _labRange.L;
                            _labPixel.A = _labAfter.A + _labRange.A;
                            _labPixel.B = _labAfter.B + _labRange.B;
                            _rgbPixel = _labPixel.To<Rgb>();

                            if (_rgbPixel.R < 0)
                            {
                                _rgbPixel.R = 0;
                            }
                            if (_rgbPixel.R > 255)
                            {
                                _rgbPixel.R = 255;
                            }
                            if (_rgbPixel.G < 0)
                            {
                                _rgbPixel.G = 0;
                            }
                            if (_rgbPixel.G > 255)
                                _rgbPixel.G = 255;
                            if (_rgbPixel.B < 0)
                            {
                                _rgbPixel.B = 0;
                            }
                            if (_rgbPixel.B > 255)
                                _rgbPixel.B = 255;

                            img.SetPixel(i, j, Color.FromArgb((int)_rgbPixel.R, (int)_rgbPixel.G, (int)_rgbPixel.B));
                        }
                    }
                }
                return img;
            

        }


        private bool Max_range(Lab labPixel, Lab labBefore, int treshold, ref Lab labRange)
        {
            if (Math.Abs(labPixel.L - labBefore.L) < treshold)
            {
                labRange.L = labPixel.L - labBefore.L;
            }

            if (Math.Abs(labPixel.A - labBefore.A) < treshold)
            {
                labRange.A = labPixel.A - labBefore.A;
            }
            if (Math.Abs(labPixel.B - labBefore.B) < treshold)
            {
                labRange.B = labPixel.B - labBefore.B;
            }

            var evklidRange = Math.Sqrt((labPixel.L - labBefore.L) * (labPixel.L - labBefore.L) +
                                        (labPixel.A - labBefore.A) * (labPixel.A - labBefore.A) +
                                        (labPixel.B - labBefore.B) * (labPixel.B - labBefore.B));
            return evklidRange < treshold;
        }

        private void btnResultAsSource_Click(object sender, EventArgs e)
        {
            picSource.Image = ((Bitmap)picResult.Image).Format32BppArgbCopy();
        }

        private void ShowColorDialogButtonClickEventHandler(object sender, EventArgs e)
        {
            using (var colorDlg = new ColorDialog())
            {
                colorDlg.AllowFullOpen = true;
                colorDlg.AnyColor = true;
                colorDlg.FullOpen = true;

                if (sender == btnSelectColorToReplace)
                {
                    colorDlg.Color = pnlSourceColor.BackColor;
                }
                else if (sender == btnSelectReplacementColor)
                {
                    colorDlg.Color = pnlResultColor.BackColor;
                }

                if (colorDlg.ShowDialog() == DialogResult.OK)
                {
                    if (sender == btnSelectColorToReplace)
                    {
                        pnlSourceColor.BackColor = colorDlg.Color;
                    }
                    else if (sender == btnSelectReplacementColor)
                    {
                        pnlResultColor.BackColor = colorDlg.Color;
                    }
                    double hue;
                    double saturation;
                    double value;
                    HsvCM.ColorToHsv(pnlResultColor.BackColor, out hue, out saturation, out value);
                    hueTB.Value = (int)hue;

                    ApplyFilter();
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = Resources.MainForm_btnLoad_Click_Specify_a_Source_file_name_and_file_path;
            ofd.Filter = Resources.MainForm_btnLoad_Click_Jpeg_Images_All_files;
            var maximal = 500;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var streamReader = new StreamReader(ofd.FileName);
                var sourceBitmap = new Bitmap(streamReader.BaseStream);
                streamReader.Close();

                picSource.Image = sourceBitmap.Format32BppArgbCopy();
                if (picSource.Image.Width > maximal || picSource.Image.Height > maximal)
                {
                    var mul = (double)picSource.Image.Width / picSource.Image.Height;

                    if (mul.CompareTo(1.0) >= 0)
                    {
                        picSource.Image = ResizeImage(picSource.Image,
                            new Size(maximal, (int)(maximal / mul)));
                    }
                    else
                    {
                        picSource.Image = ResizeImage(picSource.Image,
                            new Size((int)(maximal * mul), maximal));
                    }
                }
                ApplyFilter();
            }
        }

        public static Image ResizeImage(Image imgToResize, Size size)
        {
            return new Bitmap(imgToResize, size);
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (picResult.Image != null)
            {
                var sfd = new SaveFileDialog();
                sfd.Title = Resources.MainForm_btnSave_Click_Specify_a_file_name_and_file_path;
                sfd.Filter =
                    Resources
                        .MainForm_btnSave_Click_Png_Images___png____png_Jpeg_Images___jpg____jpg_Bitmap_Images___bmp____bmp;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    var extension = Path.GetExtension(sfd.FileName);
                    if (extension != null)
                    {
                        var fileExtension = extension.ToUpper();
                        var imgFormat = ImageFormat.Png;

                        if (fileExtension == "BMP")
                        {
                            imgFormat = ImageFormat.Bmp;
                        }
                        else if (fileExtension == "JPG")
                        {
                            imgFormat = ImageFormat.Jpeg;
                        }


                        var streamWriter = new StreamWriter(sfd.FileName, false);
                        picResult.Image.Save(streamWriter.BaseStream, imgFormat);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }
            }
        }

        private void trcThreshHold_Scroll(object sender, EventArgs e)
        {
            lblThreshold.Text = Resources.MainForm_trcThreshHold_Scroll_ + trcThreshHold.Value +
                                Resources.MainForm_trcThreshHold_Scroll__percent;
            ApplyFilter();
        }

        private void hueTB_Scroll(object sender, EventArgs e)
        {
            var original = pnlResultColor.BackColor;

            double hue;
            double saturation;
            double value;
            HsvCM.ColorToHsv(original, out hue, out saturation, out value);

            var copy = HsvCM.ColorFromHsv(hueTB.Value, saturation, value);

            pnlResultColor.BackColor = copy;

            ApplyFilter();
        }

        private void picResult_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is PictureBox)
            {
                var eventSource = (PictureBox)sender;
                using (var bmpSource = new Bitmap(eventSource.Width, eventSource.Height))
                {
                    picResult.DrawToBitmap(bmpSource, new Rectangle(0, 0, eventSource.Width, eventSource.Height));

                    var color = bmpSource.GetPixel(e.X, e.Y);
                    picker2.BackColor = color;
                    picker.Text = string.Format("R:{0:D3} G:{1:D3} B:{2:D3}", color.R, color.G, color.B);
                }
            }
        }


        private void PictureBox1MouseUpEventHandler(object sender, MouseEventArgs e)
        {
            if (sender is PictureBox)
            {
                var eventSource = (PictureBox)sender;
                using (var bmpSource = new Bitmap(eventSource.Width, eventSource.Height))
                {
                    picSource.DrawToBitmap(bmpSource, new Rectangle(0, 0, eventSource.Width, eventSource.Height));
                    pnlSourceColor.BackColor = bmpSource.GetPixel(e.X, e.Y);
                }

                ApplyFilter();
            }
        }
    }
}