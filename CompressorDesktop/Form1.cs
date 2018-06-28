using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace CompressorDesktop
{
	public partial class Form1 : Form
	{
		private string _fileName;

		public Form1()
		{
			InitializeComponent();
		}

		public Image Image => Image.FromFile(_fileName);

		private void button1_Click(object sender, EventArgs e)
		{
			var sizeW = Image.Width;
			var percent = 1.0m;

			if (sizeW > 4000)
				percent = 0.20m;
			else if (sizeW > 3000)
				percent = 0.30m;
			else if (sizeW > 2000)
				percent = 0.40m;
			else if (sizeW > 1000)
				percent = 0.80m;

			Comprimir(_fileName, percent);
		}
		private void button2_Click(object sender, EventArgs e)
		{
			var dialog = new OpenFileDialog();

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				_fileName = dialog.FileName;

				pictureBox1.Image = Image;

				label3.Text = $"{Image.Width}px";
				label4.Text = $"{Image.Height}px";
				label6.Text = $"{new FileInfo(_fileName).Length / 1024} KB";

				button1.Enabled = true;
			}
		}

		private void Comprimir(string fileName, decimal percentage)
		{
			var info = new FileInfo(fileName);
			using (var image = Image.FromFile(fileName))
			using (var resizedImage = ImageHelper.ResizeImage(image, percentage))
			{
				var newFile = $"{info.DirectoryName}\\{info.Name.Substring(0, info.Name.LastIndexOf(info.Extension))}_{resizedImage.Width}{resizedImage.Height}{info.Extension}";

				resizedImage.Save(newFile, ImageFormat.Jpeg);

				_fileName = newFile;
				label3.Text = $"{resizedImage.Width}px";
				label4.Text = $"{resizedImage.Height}px";
				label6.Text = $"{new FileInfo(newFile).Length / 1024} KB";
			}
		}
	}
}
