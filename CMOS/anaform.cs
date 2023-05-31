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
using System.Data.OleDb;
using System.Drawing.Printing;


namespace CMOS
{
    public partial class anaform : Form
    {
        public anaform()
        {
            InitializeComponent();
        }
        int demo = 1;
        string secilen;
        new int Move;
        int Mouse_X;
        int Mouse_Y;
        string tl = " TL";
        int toplamsayfa = 1;
        int sayfano = 1;
        SqlConnection baglanma = new SqlConnection("Data Source=.;Initial Catalog=CMOS;Integrated Security=True");    
        public void goster()
        {
         // baglanma girisi.
            baglanma.Open();
            DataTable tbl = new DataTable();
            SqlDataAdapter adaptor = new SqlDataAdapter("select sayı,BilgisayarAdı,musterino,musteriAdisoyAdi,giristarihi,cıkıcaktarih,Hata,durum,ücret from pcadı  ", baglanma);
            adaptor.Fill(tbl);
            baglanma.Close();
            dataGridView1.DataSource = tbl;
        }
        private void anaform_Load(object sender, EventArgs e)
        {
            if (demo==0) {
                button1.Enabled = false;
            }
            goster();
            dataGridView1.Columns[0].HeaderText = "Müşteri Sayısı";

            dataGridView1.Columns[1].HeaderText = "Bilgisayar Markası";

            dataGridView1.Columns[2].HeaderText = "Müşterinin Cep Telefonu";

            dataGridView1.Columns[3].HeaderText = "Müşterinin Adı Soyadı";

            dataGridView1.Columns[4].HeaderText = "İlk Kayıt Tarihi";

            dataGridView1.Columns[5].HeaderText = "Tahmini Onarılacağı Tarih";

            dataGridView1.Columns[6].HeaderText = "Hata Sebebi";

            dataGridView1.Columns[7].HeaderText = "Durum";

            dataGridView1.Columns[8].HeaderText = "Ücreti";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (textBox1.TextLength<1 || textBox2.TextLength < 1 || textBox3.TextLength < 1 || textBox4.TextLength < 1 || textBox9.TextLength < 1)
                {
                    MessageBox.Show("Boş yer bırakmayınız LÜTFEN."); return;
                }
                string zaman = DateTime.Now.ToShortDateString();
                string drm = "Bakımda";
                baglanma.Open();
                SqlCommand komut = new SqlCommand("insert into pcadı (BilgisayarAdı,musterino,musteriAdisoyAdi,cıkıcaktarih,Hata,giristarihi,durum,ücret) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','"+dateTimePicker1.Value.ToShortDateString() +"','" + textBox4.Text + "','"+zaman+"','"+drm+"','"+textBox9.Text+tl+"')", baglanma);               
                komut.ExecuteNonQuery();                                                      
                baglanma.Close();
                goster();
                textBox1.ResetText();
                textBox2.ResetText();
                textBox3.ResetText();
                textBox4.ResetText();
                textBox9.ResetText();
                dateTimePicker1.Value = DateTime.Now;
            }
            catch { MessageBox.Show("Bir hata oluştu lütfen yazdıgınız kelimelere dikkat ediniz eğer yine olmaz ise DESTEK ekibine başvurunuz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MüsterininGoreceigi gecis = new MüsterininGoreceigi();
            gecis.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBox7.TextLength<1)||(textBox8.TextLength<1))
                {
                    MessageBox.Show("Lütfen kullanıcı hakkındaki bilgileri doldurunuz.","BoşYerVar",MessageBoxButtons.OK,MessageBoxIcon.Warning); return;
                }
                if (textBox10.TextLength<1||textBox6.TextLength<1)
                    {
                        MessageBox.Show("Güncellenecek değeri girmediniz.","Değer girmediniz!",MessageBoxButtons.OK,MessageBoxIcon.Error); return;
                    }
                if (baglanma.State == ConnectionState.Closed)
                    baglanma.Open();
                SqlCommand komut = new SqlCommand("select * from pcadı where BilgisayarAdı ='" + textBox7.Text + "' and musteriAdisoyAdi='" + textBox8.Text + "'", baglanma);
                SqlDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    if ((textBox10.TextLength >0 )&& (textBox6.TextLength>0))
                    {
                        oku.Close();
                        SqlCommand komut3 = new SqlCommand("update pcadı set durum = '" + textBox6.Text + "' , ücret ='"+textBox10.Text+tl+"' where BilgisayarAdı ='" + textBox7.Text + "' and musteriAdisoyAdi='" + textBox8.Text + "' ", baglanma);
                        komut3.ExecuteNonQuery();
                        baglanma.Close();
                        oku.Close();
                        goster();
                        MessageBox.Show("Başarıyla  Güncellendi");
                    }
                    if ((textBox10.TextLength>0 )&&( textBox6.TextLength<1))
                    {
                        oku.Close();
                        SqlCommand komut3 = new SqlCommand("update pcadı set  ücret ='" + textBox10.Text +tl+ "' where BilgisayarAdı ='" + textBox7.Text + "' and musteriAdisoyAdi='" + textBox8.Text + "' ", baglanma);
                        komut3.ExecuteNonQuery();
                        baglanma.Close(); oku.Close();
                        goster();
                        MessageBox.Show("Başarıyla  Güncellendi");
                    }
                    if ((textBox10.TextLength < 1) && (textBox6.TextLength > 0))
                    {
                        oku.Close();
                    SqlCommand komut2 = new SqlCommand("update pcadı set durum = '" + textBox6.Text + "' where BilgisayarAdı ='" + textBox7.Text + "' and musteriAdisoyAdi='" + textBox8.Text + "' ", baglanma);                  
                    komut2.ExecuteNonQuery();
                baglanma.Close(); oku.Close();
                        goster();             
                    MessageBox.Show("Başarıyla  Güncellendi");
                    }
                    

                }
                else
                {
                                   
                    MessageBox.Show("Güncellemek istediğiniz bilgi kayıtta yoktur.", "HATA: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   
                    oku.Close();
                    baglanma.Close();
                     
                   
                }
            }
           catch (Exception b) { MessageBox.Show("Veritabanından kaynaklanma bir sorun oluştu,Kapatıp tekrar acınız.Düzelmez ise Destek ekibine başvurunuz.", "HATA : " + b.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //silme
            try
            {
                DialogResult mesaj = new DialogResult();
                mesaj = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (mesaj == DialogResult.Yes)
                {
                    baglanma.Open();
                    SqlCommand komut = new SqlCommand("delete from pcadı where sayı = (" + secilen + ")", baglanma);
                    komut.ExecuteNonQuery();
                    baglanma.Close();
                    goster();
                }
            }
            catch { MessageBox.Show("Teker teker seçin veya veri tabanından hata oluşmuş olabilir DESTEK ekibine başvurunuz Lütfen."); return; }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {                
        }

        private void dataGridView1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            secilen = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult mesaj = new DialogResult();
            mesaj = MessageBox.Show("Çıkmak istediğinize eminmisiniz ?", "Oturumu kapat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (mesaj==DialogResult.Yes)
            {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Close();
            }
            else
            {
                                   
            }
            
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
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

        private void button6_Click(object sender, EventArgs e)
        {
            Yazdırma yzdr = new Yazdırma();
            yzdr.ShowDialog();
           
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
            baglanma.Open();
            SqlCommand komut = new SqlCommand("select * from  pcadı where sayı = (" + secilen + ")", baglanma);
            komut.ExecuteNonQuery();
            baglanma.Close();
            Pen p = new Pen(Color.Pink);

            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            gr.DrawRectangle(p, yazdirma_alani);

            gr.DrawString(komut + " : " + sayfano, f, Brushes.Black, (yazdirma_alani.X + 10), (yazdirma_alani.Y + 20));

            sayfano++;

            if (sayfano < toplamsayfa)

                e.HasMorePages = true;

            else

                e.HasMorePages = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
            string marka = dataGridView1.Rows[seçilialan].Cells[1].Value.ToString();
            string adsosyad = dataGridView1.Rows[seçilialan].Cells[3].Value.ToString();

            textBox7.Text = marka;
            textBox8.Text = adsosyad;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.ToUpper();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = textBox3.Text.ToUpper();
            textBox3.SelectionStart = textBox3.Text.Length;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e) {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) {

        }
    }
}
