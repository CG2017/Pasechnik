using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ImageMagick;
using MetadataExtractor;
using Directory = MetadataExtractor.Directory;

namespace lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string _path;
        
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Load();
            List<string> images = GetImagesPath(_path);
            if (images.Count > 0)
            {
                foreach (var image in images)
                {
                    dataGridView1.Rows.Add(image);
                }
            }
        }
        private new void Load()
        {
            var fsd = new FolderSelectDialog();
            fsd.Title = "What to select";
            fsd.InitialDirectory = @"c:\";
            if (fsd.ShowDialog(IntPtr.Zero))
            {
                //Console.WriteLine(fsd.FileName);
                if (dataGridView1.DataSource != null)
                {
                    dataGridView1.DataSource = null;
                }
                else
                {
                    dataGridView1.Rows.Clear();
                }
                _path = fsd.FileName;
                textBox1.Text = _path;
            }
        }
        public List<String> GetImagesPath(String folderName)
        {

            DirectoryInfo Folder;
            FileInfo[] Images;

            Folder = new DirectoryInfo(folderName);
            Images = Folder.GetFiles();
            List<String> imagesList = new List<String>();

            for (int i = 0; i < Images.Length; i++)
            {
                if(Images[i].Extension.Contains("jp")| Images[i].Extension.Contains("tif")| Images[i].Extension.Contains("png")| Images[i].Extension.Contains("pcx")| Images[i].Extension.Contains("bmp"))
                imagesList.Add(String.Format(@"{0}/{1}", folderName, Images[i].Name));               
            }

            return imagesList;
        }

        private Image LoadImage(string file)
        {

            var localExtension = Path.GetExtension(file);
            if (localExtension != null)
            {
                var extension = localExtension.ToUpper();

                if (extension == ".PCX")
                {
                    using (MagickImage magickImage = new MagickImage(file))
                    {
                        magickImage.Format = MagickFormat.Jpg;
                        return magickImage.ToBitmap();
                    }
                }
                return System.Drawing.Image.FromFile(file);
            }
            return null;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                string filePath = dataGridView1.SelectedCells[0].Value.ToString();
                if (dataGridView2.DataSource != null)
                {
                    dataGridView2.DataSource = null;
                }
                else
                {
                    dataGridView2.Rows.Clear();
                }
                IEnumerable<Directory> directories = ImageMetadataReader.ReadMetadata(filePath);
                foreach (var folder in directories)
                    foreach (var desc in folder.Tags)
                    {
                        dataGridView2.Rows.Add(folder.Name, desc.Name, desc.Description);
                    }
               pictureBox1.Image = LoadImage(filePath);
            }
            

        }
       
    }
}
