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
    public partial class Güncelle : Form
    {
        public Güncelle()
        {
            InitializeComponent();
        }
        SqlConnection sqlgırıs = new SqlConnection("Data Source=.;Initial Catalog=CMOS;Integrated Security=True");
        public void goster()
        {
            /*Gosterme yeri Datagriedwiev*/
            sqlgırıs.Open();
            DataTable tbl = new DataTable();
            SqlDataAdapter adaptor = new SqlDataAdapter("select * from pcadı", sqlgırıs);
            adaptor.Fill(tbl);
            sqlgırıs.Close();
            dataGridView1.DataSource = tbl;
        }
        private void Güncelle_Load(object sender, EventArgs e)
        {
            goster();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.TextLength < 1)
                {
                    MessageBox.Show("Bilgisayar adı ve müşteri adı girmek zorunludur."); return;
                }
                if (textBox3.TextLength < 1)
                {
                    MessageBox.Show("Bilgisayar adı ve müşteri adı girmek zorunludur."); return;
                }
                sqlgırıs.Open();
                SqlCommand komut = new SqlCommand("update pcadı set soyad = '" + textBox1.Text + "' where BilgisayarAdı ='" + textBox1.Text + "' & musteriAdisoyAdi='" + textBox3.Text + "' ", sqlgırıs);
                komut.ExecuteNonQuery();
                sqlgırıs.Close();
                MessageBox.Show("Başarıyla  Güncellendi");
            }
            catch(Exception a) { MessageBox.Show("Veritabanında sorun çıkmış olabilir tekrar deneyiniz olmaz ise DESTEK ekibine bildiriniz.","HATA : "+a.ToString(),MessageBoxButtons.OK,MessageBoxIcon.Error); }   
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
            string pcadi = dataGridView1.Rows[seçilialan].Cells[0].Value.ToString();
            string musterino = dataGridView1.Rows[seçilialan].Cells[1].Value.ToString();
            string msteriadisoyadi = dataGridView1.Rows[seçilialan].Cells[2].Value.ToString();
     
            string cıkıstarhi = dataGridView1.Rows[seçilialan].Cells[4].Value.ToString();
            string hata = dataGridView1.Rows[seçilialan].Cells[5].Value.ToString();
            string durumu = dataGridView1.Rows[seçilialan].Cells[6].Value.ToString();

            textBox1.Text = pcadi;
            textBox2.Text = musterino;
            textBox3.Text = msteriadisoyadi;
            textBox4.Text = hata;
            textBox5.Text = cıkıstarhi;         
            textBox6.Text = durumu;
        }
    }
}
