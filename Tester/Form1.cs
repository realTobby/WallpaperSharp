using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tester
{
    public partial class Form1 : Form
    {
        private Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WallpaperSharp.API api = new WallpaperSharp.API();

            var t = api.SearchTag(textBox1.Text);
            var image = t.Data[rnd.Next(0, t.Data.Count() - 1)];
            pictureBox1.Load(image.Path.OriginalString);

            string originalFileName = image.ToHashCode().ToString();
            string extension = "";

            switch(image.FileType)
            {
                case WallpaperSharp.FileType.ImageJpeg:
                    extension = "jpg";
                    break;
                case WallpaperSharp.FileType.ImagePng:
                    extension = "png";
                    break;
            }


            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(image.Path.OriginalString), originalFileName + extension);
                SetWallpaper("latestImage." + originalFileName + extension, Style.Centered);
            }
            



        }

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public enum Style : int
        {
            Tiled,
            Centered,
            Stretched
        }

        public static void SetWallpaper(string imgPath, Style style)
        {
            var img = Image.FromFile(Path.GetFullPath(imgPath));
            string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
            img.Save(tempPath, ImageFormat.Bmp);

            var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);

            switch (style)
            {
                case Style.Tiled:
                    key.SetValue(@"WallpaperStyle", 1.ToString());
                    key.SetValue(@"TileWallpaper", 1.ToString());
                    break;
                case Style.Centered:
                    key.SetValue(@"WallpaperStyle", 1.ToString());
                    key.SetValue(@"TileWallpaper", 0.ToString());
                    break;
                case Style.Stretched:
                    key.SetValue(@"WallpaperStyle", 2.ToString());
                    key.SetValue(@"TileWallpaper", 0.ToString());
                    break;
                default:
                    break;
            }

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, tempPath, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
    }
}
