using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            pictureBox1.Load(t.Data[rnd.Next(0, t.Data.Count()-1)].Path.OriginalString);
        }
    }
}
