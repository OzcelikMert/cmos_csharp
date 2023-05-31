using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace CMOS
{
    public partial class Yazdırma : Form
    {
        public Yazdırma()
        {
            InitializeComponent();
        }
        int toplamsayfa = 2;

        int sayfano = 1;
        private void Yazdırma_Load(object sender, EventArgs e)
        {

        }

        private void printPreviewControl1_Click(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //yazi fontumuzu ayarliyoruz

            Graphics gr = e.Graphics;

            //yazi tipi Trebuchet MS, boyutu 30 ve bold karakterlerle yazilicak

            Font f = new Font("Trebuchet MS", 30, FontStyle.Bold);

            //yazdirma alanimizin ozellıklerını belirliyoruz

            Rectangle yazdirma_alani = new Rectangle();

            yazdirma_alani.X = this.printDocument1.DefaultPageSettings.Margins.Left;

            yazdirma_alani.Y = this.printDocument1.DefaultPageSettings.Margins.Top;

            yazdirma_alani.Width = this.printDocument1.DefaultPageSettings.PaperSize.Width - this.printDocument1.DefaultPageSettings.Margins.Left - this.printDocument1.DefaultPageSettings.Margins.Right;

            yazdirma_alani.Height = this.printDocument1.DefaultPageSettings.PaperSize.Height - this.printDocument1.DefaultPageSettings.Margins.Top - this.printDocument1.DefaultPageSettings.Margins.Bottom;
            if (this.printDocument1.DefaultPageSettings.Landscape)

            {

                int tempwidth = yazdirma_alani.Width;

                yazdirma_alani.Width = yazdirma_alani.Height;

                yazdirma_alani.Height = tempwidth;

            }
            //yazdirma alaninin kenarındaki cizgilerin ozelliklerini ayarliyoruz

            Pen p = new Pen(Color.Pink);

            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            gr.DrawRectangle(p, yazdirma_alani);

            gr.DrawString(this.textBox1.Text + " : " + sayfano, f, Brushes.Black, (yazdirma_alani.X + 10), (yazdirma_alani.Y + 20));

            sayfano++;

            if (sayfano < toplamsayfa)

                e.HasMorePages = true;

            else

                e.HasMorePages = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.pageSetupDialog1.ShowDialog();

            PageSettings ayarlar = this.pageSetupDialog1.PageSettings;

            this.printDocument1.DefaultPageSettings = ayarlar;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.printPreviewControl1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.printDialog1.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
