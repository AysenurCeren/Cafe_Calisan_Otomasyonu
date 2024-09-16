using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeOtomasyon
{
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Musteri", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        public void Temizle()
        {
            Txtid.Text = string.Empty;
            Txtad.Text = string.Empty;
            Txtemail.Text = string.Empty;
            Txtsoyad.Text = string.Empty;
            Txttelefon.Text = string.Empty;
            Rchadres.Text = string.Empty;
        }
        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            Listele();
            Temizle();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Musteri (ad, soyad, telefon, email, adres) values (@p1, @p2, @p3, @p4, @p5)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Txtad.Text);
            komut.Parameters.AddWithValue("@p2",Txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3",Txttelefon.Text);
            komut.Parameters.AddWithValue("@p4",Txtemail.Text);
            komut.Parameters.AddWithValue("@p5",Rchadres.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Başarıyla Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Musteri where musteri_id=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Başarıyla Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Listele();
            Temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Musteri set ad=@p1, soyad=@p2, telefon=@p3, email=@p4, adres=@p5 where musteri_id=@p6", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Txtad.Text);
            komut.Parameters.AddWithValue("@p2", Txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", Txttelefon.Text);
            komut.Parameters.AddWithValue("@p4", Txtemail.Text);
            komut.Parameters.AddWithValue("@p5", Rchadres.Text);
            komut.Parameters.AddWithValue("@p6", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Başarıyla Guncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Temizle();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                Txtid.Text = dr["musteri_id"].ToString();
                Txtad.Text = dr["ad"].ToString();
                Txtsoyad.Text = dr["soyad"].ToString();
                Txttelefon.Text = dr["telefon"].ToString();
                Txtemail.Text = dr["email"].ToString();
                Rchadres.Text = dr["adres"].ToString();
            }
        }
    }
}
