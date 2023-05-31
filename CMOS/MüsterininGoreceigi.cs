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
    public partial class MüsterininGoreceigi : Form
    {
        public MüsterininGoreceigi()
        {
            InitializeComponent();
        }
        SqlConnection sqlgırıs = new SqlConnection("Data Source=.;Initial Catalog=CMOS;Integrated Security=True");
        public void goster()
        {
            /*Gosterme yeri Datagriedwiev*/
            sqlgırıs.Open();
            DataTable tbl = new DataTable();
            SqlDataAdapter adaptor = new SqlDataAdapter("select BilgisayarAdı,musteriAdisoyAdi,giristarihi,cıkıcaktarih,durum,ücret from pcadı", sqlgırıs);
            adaptor.Fill(tbl);
            sqlgırıs.Close();
            dataGridView1.DataSource = tbl;
        }
        private void MüsterininGoreceigi_Load(object sender, EventArgs e)
        {
            goster();
            dataGridView1.Columns[0].HeaderText = "Bilgisayar Adı";

            dataGridView1.Columns[1].HeaderText = "AD Soyad";

            dataGridView1.Columns[2].HeaderText = "Başlangıç Tarihi";

            dataGridView1.Columns[3].HeaderText = "Çıkış Tarihi";

            dataGridView1.Columns[4].HeaderText = "Durumu";

            dataGridView1.Columns[5].HeaderText = "Fiyatı";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength<1||textBox2.TextLength<1)
            {
                MessageBox.Show("Lütfen BOŞ YER bırakmayınız");
            }
            sqlgırıs.Open();
            
            SqlCommand komut = new SqlCommand("select * from pcadı where musteriAdisoyAdi like '" + textBox1.Text + "' and BilgisayarAdı like '"+textBox2.Text+"'", sqlgırıs);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            sqlgırıs.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {         
            this.Close();
        }
    }
}
