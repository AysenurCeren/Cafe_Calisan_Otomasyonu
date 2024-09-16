using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeOtomasyon
{
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Fatura", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void Temizle()
        {
            Txtid.Text = string.Empty;
            Txtodemeturu.Text = string.Empty;
            Txttarih.Text= string.Empty;
            Txttutar.Text= string.Empty;
            Txtsiparis.Text = string.Empty;
        }
       
        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            Listele();
            Temizle();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime islemTarihi = DateTime.ParseExact(Txttarih.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                using (SqlCommand komut = new SqlCommand("INSERT INTO Fatura (islem_tarihi, odenecek_tutar, odeme_turu, siparis_id) VALUES (@p1, @p2, @p3, @p4)", bgl.baglanti()))
                {
                    komut.Parameters.AddWithValue("@p1", islemTarihi);
                    komut.Parameters.AddWithValue("@p2", Txttutar.Text);
                    komut.Parameters.AddWithValue("@p3", Txtodemeturu.Text);
                    komut.Parameters.AddWithValue("@p4", Txtsiparis.Text);

                    komut.ExecuteNonQuery();
                }

                bgl.baglanti().Close();
                Listele();
                Temizle();
                MessageBox.Show("Fatura Başarıyla Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Fatura where fatura_id=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            Listele();
            Temizle();
            MessageBox.Show("Fatura Başarıyla Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime islemTarihi = DateTime.ParseExact(Txttarih.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                using (SqlCommand komut = new SqlCommand("Update Fatura set islem_tarihi=@p1, odenecek_tutar=@p2, odeme_turu=@p3, siparis_id=@p4 where fatura_id=@p5", bgl.baglanti()))
                {
                    komut.Parameters.AddWithValue("@p1", islemTarihi);
                    komut.Parameters.AddWithValue("@p2", Txttutar.Text);
                    komut.Parameters.AddWithValue("@p3", Txtodemeturu.Text);
                    komut.Parameters.AddWithValue("@p4", Txtsiparis.Text);
                    komut.Parameters.AddWithValue("@p4", Txtid.Text);

                    komut.ExecuteNonQuery();
                }

                bgl.baglanti().Close();
                Listele();
                Temizle();
                MessageBox.Show("Fatura Başarıyla Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Temizle();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                DateTime siparisTarihi = Convert.ToDateTime(dr["islem_tarihi"]);
                Txttarih.Text = siparisTarihi.ToString("dd.MM.yyyy");

                Txtid.Text = dr["fatura_id"].ToString();
                Txtodemeturu.Text = dr["odeme_turu"].ToString();
                Txttutar.Text = dr["odenecek_tutar"].ToString();
                Txtsiparis.Text = dr["siparis_id"].ToString();
            }
        }
    }
}
