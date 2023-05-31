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
using System.Diagnostics;

namespace CMOS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        new int Move;
        int Mouse_X;
        int Mouse_Y;
        public string eposta;
        public string sifre;
        SqlConnection sqlgırıs = new SqlConnection("Data Source=.;Initial Catalog=CMOS;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        { // giriş kısımı
            try
            {
              Properties.Settings.Default["Kullanıcıadı"] = textBox1.Text;
              Properties.Settings.Default.Save();
                eposta = textBox1.Text;
                sifre = textBox2.Text;
                if (sqlgırıs.State == ConnectionState.Closed)
                    sqlgırıs.Open();
                SqlCommand komut = new SqlCommand("select * from gırıs where KullanıcıAdi ='" + eposta + "' and sifre='" + sifre + "'", sqlgırıs);
                SqlDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    oku.Close();
                    sqlgırıs.Close();
                    anaform anafrm = new anaform();
                    anafrm.Show();
                    this.Visible = false;
                }
                else
                {
                    MessageBox.Show("Eposta veya şifre yanlış","Yanlış Giriş",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    oku.Close();
                    sqlgırıs.Close();
                    textBox2.ResetText();
                    return;                
                }
            }
            catch { MessageBox.Show("Bir hata meydana geldi. Tekrar deneyiniz düzelmez ise DESTEK ekibine başvurunuz.", "Yanlış Giriş", MessageBoxButtons.OK, MessageBoxIcon.Error);  textBox2.ResetText(); return; }
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            KayıtOlmaYeri kayıtol = new KayıtOlmaYeri();
            kayıtol.Show();
            this.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            textBox1.Text = Properties.Settings.Default["Kullanıcıadı"].ToString();
            checkBox1.Checked = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult mesaj = new DialogResult();
            mesaj = MessageBox.Show("Çıkmak istediğinize eminmisiniz ?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (mesaj == DialogResult.Yes)
            {
              Application.Exit();
            }
            else
            {

            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar ='*';
            }
            else
            {
                textBox2.PasswordChar= '\0';
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ProcessStartInfo adminlink2 = new ProcessStartInfo("http://amcaogullari.com/");
            Process.Start(adminlink2);
        }
    }
}
