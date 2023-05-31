using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CMOS
{
    public partial class KayıtOlmaYeri : Form
    {
        public KayıtOlmaYeri()
        {
            InitializeComponent();
        }
        new int Move;
        int Mouse_X;
        int Mouse_Y;
        SqlConnection sqlgırıs = new SqlConnection("Data Source=.;Initial Catalog=CMOS;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            //Kayıt olma YERİ
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("Lütfen BOŞ yer bırakmayınız...", "UYARI!!");
                return;
            }
            if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("Lütfen BOŞ yer bırakmayınız...", "UYARI!!");
                return;
            }
            if (textBox3.Text.Length == 0)
            {
                MessageBox.Show("Lütfen BOŞ yer bırakmayınız...", "UYARI!!");
                return;
            }
            if (textBox2.Text != textBox3.Text)
            {
                errorProvider1.SetError(textBox3, "Şifreler aynı değil! Lütfen düzeltiniz.");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }
            sqlgırıs.Open();
            SqlCommand komut = new SqlCommand("insert into gırıs values ('" + textBox1.Text + "','" + textBox2.Text + "')", sqlgırıs);
            komut.ExecuteNonQuery();
            sqlgırıs.Close();
            Form1 frm1 = new Form1();
            frm1.Visible = true;
            this.Close();
        }

        private void KayıtOlmaYeri_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 20;
            textBox2.MaxLength = 20;
            textBox3.MaxLength = 20;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            Close();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }
    }
}
