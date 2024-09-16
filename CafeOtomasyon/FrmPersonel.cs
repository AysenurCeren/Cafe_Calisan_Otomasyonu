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
    public partial class FrmPersonel : Form
    {
        public FrmPersonel()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Personel", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            Listele();
            Temizle();
        }
        public void Temizle()
        {
            Txtid.Text = string.Empty;
            Txtad.Text = string.Empty;
            Txtsoyad.Text = string.Empty;
            Txttarih.Text = string.Empty;
            Txttc.Text = string.Empty;
            Txttelefon.Text = string.Empty;
            Txtmaas.Text = string.Empty;
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime iseBaslamaTarihi = DateTime.ParseExact(Txttarih.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                using (SqlCommand komut = new SqlCommand("INSERT INTO Personel (ad, soyad, tc_no, telefon_no, e_mail, maas, ise_baslama_tarihi, adres) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8)", bgl.baglanti()))
                {
                    komut.Parameters.AddWithValue("@p1", Txtad.Text);
                    komut.Parameters.AddWithValue("@p2", Txtsoyad.Text);
                    komut.Parameters.AddWithValue("@p3", Txttc.Text);
                    komut.Parameters.AddWithValue("@p4", Txttelefon.Text);
                    komut.Parameters.AddWithValue("@p5", Txtemail.Text);
                    komut.Parameters.AddWithValue("@p6", Txtmaas.Text);
                    komut.Parameters.AddWithValue("@p7", iseBaslamaTarihi);
                    komut.Parameters.AddWithValue("@p8", Rchadres.Text);

                    komut.ExecuteNonQuery();
                }

                bgl.baglanti().Close();
                MessageBox.Show("Personel Başarıyla Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Personel where personel_id=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Başarıyla Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Listele();
            Temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime iseBaslamaTarihi = DateTime.ParseExact(Txttarih.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                using (SqlCommand komut = new SqlCommand("UPDATE Personel SET ad=@p1, soyad=@p2, tc_no=@p3, telefon_no=@p4, e_mail=@p5, maas=@p6, ise_baslama_tarihi=@p7, adres=@p8 WHERE personel_id=@p9", bgl.baglanti()))
                {
                    komut.Parameters.AddWithValue("@p1", Txtad.Text);
                    komut.Parameters.AddWithValue("@p2", Txtsoyad.Text);
                    komut.Parameters.AddWithValue("@p3", Txttc.Text);
                    komut.Parameters.AddWithValue("@p4", Txttelefon.Text);
                    komut.Parameters.AddWithValue("@p5", Txtemail.Text);
                    komut.Parameters.AddWithValue("@p6", Txtmaas.Text);
                    komut.Parameters.AddWithValue("@p7", iseBaslamaTarihi);
                    komut.Parameters.AddWithValue("@p8", Rchadres.Text);
                    komut.Parameters.AddWithValue("@p9", Txtid.Text);

                    komut.ExecuteNonQuery();
                }

                bgl.baglanti().Close();
                MessageBox.Show("Personel Başarıyla Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
                Temizle();
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
                DateTime siparisTarihi = Convert.ToDateTime(dr["ise_baslama_tarihi"]);
                Txttarih.Text = siparisTarihi.ToString("dd.MM.yyyy");

                Txtid.Text = dr["personel_id"].ToString();
                Txtad.Text = dr["ad"].ToString();
                Txtsoyad.Text = dr["soyad"].ToString();
                Txttc.Text = dr["tc_no"].ToString();
                Txttelefon.Text = dr["telefon_no"].ToString();
                Txtemail.Text = dr["e_mail"].ToString();
                Txtmaas.Text = dr["maas"].ToString();
                Rchadres.Text = dr["adres"].ToString();
            }
        }
    }
}
